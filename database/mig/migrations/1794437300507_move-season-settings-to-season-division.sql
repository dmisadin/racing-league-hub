-- MIGRONDI:NAME=1794437300507_move-season-settings-to-season-division.sql
-- MIGRONDI:TIMESTAMP=1794437300507
-- ---------- MIGRONDI:UP ----------

-- Points, assists, lobby settings and driver entries move from season to season_division:
--   season_points         -> season_division_points
--   season_assists        -> season_division_assists
--   season_lobby_settings -> season_division_lobby_settings
--   season_driver         -> season_division_driver
-- Requires 1794437299109_add-default-division-per-season (every season has a season_division)
-- and 1794437299807_move-grand-prix-to-season-division (grand_prix.season_division_id is set).

-- Renames a table together with its identity sequence and the constraints named after it
-- (primary key, foreign keys and, on PostgreSQL 18+, NOT NULL constraints).
CREATE FUNCTION pg_temp.rename_table(old_name text, new_name text) RETURNS void
LANGUAGE plpgsql AS $$
DECLARE
    seq text;
    c record;
BEGIN
    EXECUTE format('ALTER TABLE public.%I RENAME TO %I', old_name, new_name);

    seq := pg_get_serial_sequence(format('public.%I', new_name), 'id');
    IF seq IS NOT NULL THEN
        EXECUTE format('ALTER SEQUENCE %s RENAME TO %I', seq, new_name || '_id_seq');
    END IF;

    FOR c IN
        SELECT conname
        FROM pg_constraint
        WHERE conrelid = format('public.%I', new_name)::regclass
          AND starts_with(conname, old_name || '_')
    LOOP
        EXECUTE format('ALTER TABLE public.%I RENAME CONSTRAINT %I TO %I',
            new_name,
            c.conname,
            left(new_name || substr(c.conname, length(old_name) + 1), 63));
    END LOOP;
END $$;

-- Each season's top-tier division (lowest tier, then oldest season_division).
CREATE TEMP VIEW top_season_division AS
SELECT DISTINCT ON (sd.season_id)
    sd.season_id,
    sd.id AS season_division_id
FROM public.season_division sd
JOIN public.division d ON d.id = sd.division_id
ORDER BY sd.season_id, d.tier, sd.id;

-- -------------------------------------------------------------------------------------
-- Points: every division of the season starts with the season's points system
-- -------------------------------------------------------------------------------------

ALTER TABLE public.season_points
    ADD COLUMN season_division_id int NULL;

UPDATE public.season_points t
SET season_division_id = top.season_division_id
FROM top_season_division top
WHERE top.season_id = t.season_id;

INSERT INTO public.season_points (season_id, season_division_id, session_type, "position", points)
SELECT t.season_id, sd.id, t.session_type, t."position", t.points
FROM public.season_points t
JOIN public.season_division sd
    ON sd.season_id = t.season_id
   AND sd.id <> t.season_division_id;

-- Dropping season_id also drops its foreign key and index.
ALTER TABLE public.season_points
    ALTER COLUMN season_division_id SET NOT NULL,
    DROP COLUMN season_id;

SELECT pg_temp.rename_table('season_points', 'season_division_points');

ALTER TABLE public.season_division_points
    ADD CONSTRAINT season_division_points_season_division_id_fkey
        FOREIGN KEY (season_division_id) REFERENCES public.season_division (id);

CREATE INDEX season_division_points_season_division_id_idx
    ON public.season_division_points (season_division_id);

-- -------------------------------------------------------------------------------------
-- Assists: every division of the season starts with the season's assists
-- -------------------------------------------------------------------------------------

ALTER TABLE public.season_assists
    ADD COLUMN season_division_id int NULL;

UPDATE public.season_assists t
SET season_division_id = top.season_division_id
FROM top_season_division top
WHERE top.season_id = t.season_id;

INSERT INTO public.season_assists (season_id, season_division_id, racing_line, gearbox, traction_control, abs)
SELECT t.season_id, sd.id, t.racing_line, t.gearbox, t.traction_control, t.abs
FROM public.season_assists t
JOIN public.season_division sd
    ON sd.season_id = t.season_id
   AND sd.id <> t.season_division_id;

ALTER TABLE public.season_assists
    ALTER COLUMN season_division_id SET NOT NULL,
    DROP COLUMN season_id;

SELECT pg_temp.rename_table('season_assists', 'season_division_assists');

ALTER TABLE public.season_division_assists
    ADD CONSTRAINT season_division_assists_season_division_id_fkey
        FOREIGN KEY (season_division_id) REFERENCES public.season_division (id);

CREATE INDEX season_division_assists_season_division_id_idx
    ON public.season_division_assists (season_division_id);

-- -------------------------------------------------------------------------------------
-- Lobby settings: every division of the season starts with the season's lobby settings
-- -------------------------------------------------------------------------------------

ALTER TABLE public.season_lobby_settings
    ADD COLUMN season_division_id int NULL;

UPDATE public.season_lobby_settings t
SET season_division_id = top.season_division_id
FROM top_season_division top
WHERE top.season_id = t.season_id;

INSERT INTO public.season_lobby_settings (
    season_id, season_division_id, qualifying_type, race_distance_percentage, formation_lap, weather,
    corner_cutting, car_damage, car_damage_rate, parc_ferme, equal_car_performance, safety_car,
    collisions, ghosting, race_start)
SELECT
    t.season_id, sd.id, t.qualifying_type, t.race_distance_percentage, t.formation_lap, t.weather,
    t.corner_cutting, t.car_damage, t.car_damage_rate, t.parc_ferme, t.equal_car_performance, t.safety_car,
    t.collisions, t.ghosting, t.race_start
FROM public.season_lobby_settings t
JOIN public.season_division sd
    ON sd.season_id = t.season_id
   AND sd.id <> t.season_division_id;

ALTER TABLE public.season_lobby_settings
    ALTER COLUMN season_division_id SET NOT NULL,
    DROP COLUMN season_id;

SELECT pg_temp.rename_table('season_lobby_settings', 'season_division_lobby_settings');

ALTER TABLE public.season_division_lobby_settings
    ADD CONSTRAINT season_division_lobby_settings_season_division_id_fkey
        FOREIGN KEY (season_division_id) REFERENCES public.season_division (id);

CREATE INDEX season_division_lobby_settings_season_division_id_idx
    ON public.season_division_lobby_settings (season_division_id);

-- -------------------------------------------------------------------------------------
-- Drivers: a season entry belongs to the division the driver raced in. Picked from the
-- season's Grands Prix: most races for the entry's team, then most races overall, then
-- the higher tier. Drivers without races go to the season's top-tier division.
-- (season_id, driver_id, team_id) was unique and every entry maps to one division of its
-- own season, so (season_division_id, driver_id, team_id) is unique as well.
-- -------------------------------------------------------------------------------------

ALTER TABLE public.season_driver
    ADD COLUMN season_division_id int NULL;

UPDATE public.season_driver t
SET season_division_id = COALESCE(
    (
        SELECT gp.season_division_id
        FROM public.grand_prix_driver gpd
        JOIN public.grand_prix gp ON gp.id = gpd.grand_prix_id
        JOIN public.season_division sd ON sd.id = gp.season_division_id
        JOIN public.division d ON d.id = sd.division_id
        WHERE sd.season_id = t.season_id
          AND gpd.driver_id = t.driver_id
        GROUP BY gp.season_division_id, d.tier
        ORDER BY
            count(*) FILTER (WHERE gpd.team_id = t.team_id) DESC,
            count(*) DESC,
            d.tier,
            gp.season_division_id
        LIMIT 1
    ),
    (
        SELECT top.season_division_id
        FROM top_season_division top
        WHERE top.season_id = t.season_id
    ));

-- Dropping season_id also drops season_driver_season_id_driver_id_team_id_key and the foreign key.
ALTER TABLE public.season_driver
    ALTER COLUMN season_division_id SET NOT NULL,
    DROP COLUMN season_id;

SELECT pg_temp.rename_table('season_driver', 'season_division_driver');

ALTER TABLE public.season_division_driver
    ADD CONSTRAINT season_division_driver_season_division_id_fkey
        FOREIGN KEY (season_division_id) REFERENCES public.season_division (id);

-- Also serves the season_division_id foreign key lookups.
CREATE UNIQUE INDEX season_division_driver_season_division_id_driver_id_team_id_key
    ON public.season_division_driver (season_division_id, driver_id, team_id);

CREATE INDEX season_division_driver_driver_id_idx
    ON public.season_division_driver (driver_id);

DROP VIEW top_season_division;
DROP FUNCTION pg_temp.rename_table(text, text);

-- ---------- MIGRONDI:DOWN ----------

-- Lossy: a season keeps the settings of one division only (the highest-tier division that has rows),
-- and a driver entered with the same team in several divisions of a season keeps one entry.

CREATE FUNCTION pg_temp.rename_table(old_name text, new_name text) RETURNS void
LANGUAGE plpgsql AS $$
DECLARE
    seq text;
    c record;
BEGIN
    EXECUTE format('ALTER TABLE public.%I RENAME TO %I', old_name, new_name);

    seq := pg_get_serial_sequence(format('public.%I', new_name), 'id');
    IF seq IS NOT NULL THEN
        EXECUTE format('ALTER SEQUENCE %s RENAME TO %I', seq, new_name || '_id_seq');
    END IF;

    FOR c IN
        SELECT conname
        FROM pg_constraint
        WHERE conrelid = format('public.%I', new_name)::regclass
          AND starts_with(conname, old_name || '_')
    LOOP
        EXECUTE format('ALTER TABLE public.%I RENAME CONSTRAINT %I TO %I',
            new_name,
            c.conname,
            left(new_name || substr(c.conname, length(old_name) + 1), 63));
    END LOOP;
END $$;

-- Drivers
DROP INDEX public.season_division_driver_season_division_id_driver_id_team_id_key;
DROP INDEX public.season_division_driver_driver_id_idx;
ALTER TABLE public.season_division_driver ADD COLUMN season_id int NULL;

UPDATE public.season_division_driver t
SET season_id = sd.season_id
FROM public.season_division sd
WHERE sd.id = t.season_division_id;

DELETE FROM public.season_division_driver t
USING (
    SELECT
        x.id,
        row_number() OVER (
            PARTITION BY x.season_id, x.driver_id, x.team_id
            ORDER BY d.tier, x.season_division_id, x.id) AS rn
    FROM public.season_division_driver x
    JOIN public.season_division sd ON sd.id = x.season_division_id
    JOIN public.division d ON d.id = sd.division_id
) ranked
WHERE ranked.id = t.id
  AND ranked.rn > 1;

ALTER TABLE public.season_division_driver
    ALTER COLUMN season_id SET NOT NULL,
    DROP COLUMN season_division_id;

SELECT pg_temp.rename_table('season_division_driver', 'season_driver');

ALTER TABLE public.season_driver
    ADD CONSTRAINT season_driver_season_id_fkey FOREIGN KEY (season_id) REFERENCES public.season (id);

CREATE UNIQUE INDEX season_driver_season_id_driver_id_team_id_key
    ON public.season_driver (season_id, driver_id, team_id);

-- Settings: keep the rows of one division per season
DROP INDEX public.season_division_points_season_division_id_idx;
DROP INDEX public.season_division_assists_season_division_id_idx;
DROP INDEX public.season_division_lobby_settings_season_division_id_idx;

ALTER TABLE public.season_division_points ADD COLUMN season_id int NULL;
ALTER TABLE public.season_division_assists ADD COLUMN season_id int NULL;
ALTER TABLE public.season_division_lobby_settings ADD COLUMN season_id int NULL;

UPDATE public.season_division_points t SET season_id = sd.season_id
FROM public.season_division sd WHERE sd.id = t.season_division_id;
UPDATE public.season_division_assists t SET season_id = sd.season_id
FROM public.season_division sd WHERE sd.id = t.season_division_id;
UPDATE public.season_division_lobby_settings t SET season_id = sd.season_id
FROM public.season_division sd WHERE sd.id = t.season_division_id;

DELETE FROM public.season_division_points t
WHERE t.season_division_id <> (
    SELECT x.season_division_id
    FROM public.season_division_points x
    JOIN public.season_division sd ON sd.id = x.season_division_id
    JOIN public.division d ON d.id = sd.division_id
    WHERE x.season_id = t.season_id
    ORDER BY d.tier, sd.id
    LIMIT 1);

DELETE FROM public.season_division_assists t
WHERE t.season_division_id <> (
    SELECT x.season_division_id
    FROM public.season_division_assists x
    JOIN public.season_division sd ON sd.id = x.season_division_id
    JOIN public.division d ON d.id = sd.division_id
    WHERE x.season_id = t.season_id
    ORDER BY d.tier, sd.id
    LIMIT 1);

DELETE FROM public.season_division_lobby_settings t
WHERE t.season_division_id <> (
    SELECT x.season_division_id
    FROM public.season_division_lobby_settings x
    JOIN public.season_division sd ON sd.id = x.season_division_id
    JOIN public.division d ON d.id = sd.division_id
    WHERE x.season_id = t.season_id
    ORDER BY d.tier, sd.id
    LIMIT 1);

ALTER TABLE public.season_division_points
    ALTER COLUMN season_id SET NOT NULL,
    DROP COLUMN season_division_id;
ALTER TABLE public.season_division_assists
    ALTER COLUMN season_id SET NOT NULL,
    DROP COLUMN season_division_id;
ALTER TABLE public.season_division_lobby_settings
    ALTER COLUMN season_id SET NOT NULL,
    DROP COLUMN season_division_id;

SELECT pg_temp.rename_table('season_division_points', 'season_points');
SELECT pg_temp.rename_table('season_division_assists', 'season_assists');
SELECT pg_temp.rename_table('season_division_lobby_settings', 'season_lobby_settings');

ALTER TABLE public.season_points
    ADD CONSTRAINT season_points_season_id_fkey FOREIGN KEY (season_id) REFERENCES public.season (id);
ALTER TABLE public.season_assists
    ADD CONSTRAINT season_assists_season_id_season_id_fkey FOREIGN KEY (season_id) REFERENCES public.season (id);
ALTER TABLE public.season_lobby_settings
    ADD CONSTRAINT season_lobby_settings_season_id_fkey FOREIGN KEY (season_id) REFERENCES public.season (id);

CREATE INDEX season_points_season_id_idx ON public.season_points (season_id);
CREATE INDEX season_assists_season_id_idx ON public.season_assists (season_id);
CREATE INDEX season_lobby_settings_season_id_idx ON public.season_lobby_settings (season_id);

DROP FUNCTION pg_temp.rename_table(text, text);
