-- MIGRONDI:NAME=1794437299807_move-grand-prix-to-season-division.sql
-- MIGRONDI:TIMESTAMP=1794437299807
-- ---------- MIGRONDI:UP ----------

-- A Grand Prix belongs to a season division only; its season is reached through season_division.
-- Requires 1794437299109_add-default-division-per-season (every season has a season_division).

-- grand_prix.season_id is the source of truth for the season. A Grand Prix that has no division, or whose
-- season_division belongs to another season (nothing enforced that before), is assigned to:
--   1. the same division within its own season, when that season has it, otherwise
--   2. its season's top-tier division (lowest tier, then oldest season_division).
UPDATE public.grand_prix gp
SET season_division_id = COALESCE(
    (
        SELECT own.id
        FROM public.season_division current_sd
        JOIN public.season_division own
            ON own.division_id = current_sd.division_id
           AND own.season_id = gp.season_id
        WHERE current_sd.id = gp.season_division_id
    ),
    (
        SELECT sd.id
        FROM public.season_division sd
        JOIN public.division d ON d.id = sd.division_id
        WHERE sd.season_id = gp.season_id
        ORDER BY d.tier, sd.id
        LIMIT 1
    ))
WHERE gp.season_division_id IS NULL
   OR NOT EXISTS (
        SELECT 1
        FROM public.season_division sd
        WHERE sd.id = gp.season_division_id
          AND sd.season_id = gp.season_id
   );

ALTER TABLE public.grand_prix
    ALTER COLUMN season_division_id SET NOT NULL;

-- Slugs are unique within a division's calendar, so divisions can race the same track under the same slug.
DROP INDEX public.grand_prix_season_id_slug_key;
DROP INDEX public.grand_prix_season_id_idx;

ALTER TABLE public.grand_prix
    DROP CONSTRAINT grand_prix_season_id_fkey,
    DROP COLUMN season_id;

-- Also serves the season_division_id foreign key lookups.
CREATE UNIQUE INDEX grand_prix_season_division_id_slug_key
    ON public.grand_prix (season_division_id, slug);

-- Was only there for a composite (season_division_id, season_id) foreign key, which is no longer needed.
ALTER TABLE public.season_division
    DROP CONSTRAINT season_division_id_season_id_key;

-- ---------- MIGRONDI:DOWN ----------

ALTER TABLE public.season_division
    ADD CONSTRAINT season_division_id_season_id_key UNIQUE (id, season_id);

ALTER TABLE public.grand_prix
    ADD COLUMN season_id int NULL;

UPDATE public.grand_prix gp
SET season_id = sd.season_id
FROM public.season_division sd
WHERE sd.id = gp.season_division_id;

ALTER TABLE public.grand_prix
    ALTER COLUMN season_id SET NOT NULL,
    ALTER COLUMN season_division_id DROP NOT NULL,
    ADD CONSTRAINT grand_prix_season_id_fkey FOREIGN KEY (season_id) REFERENCES public.season (id);

DROP INDEX public.grand_prix_season_division_id_slug_key;

CREATE INDEX grand_prix_season_id_idx ON public.grand_prix (season_id);

-- Fails if two divisions of a season share a slug; rename those Grands Prix first.
CREATE UNIQUE INDEX grand_prix_season_id_slug_key ON public.grand_prix (season_id, slug);
