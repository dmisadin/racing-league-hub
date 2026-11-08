-- =====================================================================================
-- Development seed: teams, tracks, drivers, divisions, seasons, season drivers,
-- season points and grands prix for leagues 1, 3 and 4.
--
-- Prerequisites:
--   * all migrations applied
--   * dc_country populated (database/dc_data/dc_country.sql)
--   * leagues with ids 1, 3 and 4 exist
--
-- Re-runnable: rows are only inserted when missing (ON CONFLICT DO NOTHING / NOT EXISTS).
-- Existing tracks are reused when a layout with the same telemetry_id already exists,
-- existing teams are reused when the name matches.
--
-- Calendars are fixed dates chosen around 2026-11-04 so that every season status shows up:
--   completed, ongoing (some rounds raced) and upcoming (incl. one season without calendar).
-- All race times are 19:00 UTC.
-- =====================================================================================

BEGIN;

DO $$
BEGIN
    IF NOT EXISTS (SELECT 1 FROM public.dc_country) THEN
        RAISE EXCEPTION 'dc_country is empty - run database/dc_data/dc_country.sql first';
    END IF;

    IF (SELECT count(*) FROM public.league WHERE id IN (1, 3, 4)) <> 3 THEN
        RAISE EXCEPTION 'Leagues 1, 3 and 4 must exist before seeding';
    END IF;
END $$;

-- -------------------------------------------------------------------------------------
-- Seed definitions (session temp tables, so the script also works with auto-commit on)
-- -------------------------------------------------------------------------------------

DROP TABLE IF EXISTS seed_team, seed_track, seed_driver, seed_division,
    seed_season, seed_season_division, seed_gp;

CREATE TEMP TABLE seed_team (ord int, name text, color text);

INSERT INTO seed_team (ord, name, color) VALUES
    (1,  'Red Bull Racing', '#3671C6'),
    (2,  'Ferrari',         '#E8002D'),
    (3,  'Mercedes',        '#27F4D2'),
    (4,  'McLaren',         '#FF8000'),
    (5,  'Aston Martin',    '#229971'),
    (6,  'Alpine',          '#0093CC'),
    (7,  'Williams',        '#64C4FF'),
    (8,  'Racing Bulls',    '#6692FF'),
    (9,  'Kick Sauber',     '#52E252'),
    (10, 'Haas',            '#B6BABD');

CREATE TEMP TABLE seed_track (
    key             text,
    gp_name         text,
    track_name      text,
    country         char(2),
    city            text,
    short_name      text,
    corners_total   smallint,
    corners_left    smallint,
    laps            smallint,
    length          smallint,
    telemetry_id    smallint
);

INSERT INTO seed_track VALUES
    ('melbourne',     'Australian Grand Prix',      'Albert Park Circuit',                 'AU', 'Melbourne',     'Albert Park',   14, 6,  58, 5278, 0),
    ('bahrain',       'Bahrain Grand Prix',         'Bahrain International Circuit',       'BH', 'Sakhir',        'Sakhir',        15, 6,  57, 5412, 3),
    ('barcelona',     'Spanish Grand Prix',         'Circuit de Barcelona-Catalunya',      'ES', 'Montmeló',      'Catalunya',     14, 5,  66, 4657, 4),
    ('monaco',        'Monaco Grand Prix',          'Circuit de Monaco',                   'MC', 'Monte Carlo',   'Monaco',        19, 7,  78, 3337, 5),
    ('montreal',      'Canadian Grand Prix',        'Circuit Gilles Villeneuve',           'CA', 'Montreal',      'Montreal',      14, 6,  70, 4361, 6),
    ('silverstone',   'British Grand Prix',         'Silverstone Circuit',                 'GB', 'Silverstone',   'Silverstone',   18, 8,  52, 5891, 7),
    ('hungaroring',   'Hungarian Grand Prix',       'Hungaroring',                         'HU', 'Mogyoród',      'Hungaroring',   14, 8,  70, 4381, 9),
    ('spa',           'Belgian Grand Prix',         'Circuit de Spa-Francorchamps',        'BE', 'Stavelot',      'Spa',           19, 9,  44, 7004, 10),
    ('monza',         'Italian Grand Prix',         'Autodromo Nazionale Monza',           'IT', 'Monza',         'Monza',         11, 4,  53, 5793, 11),
    ('singapore',     'Singapore Grand Prix',       'Marina Bay Street Circuit',           'SG', 'Singapore',     'Marina Bay',    19, 11, 62, 4940, 12),
    ('suzuka',        'Japanese Grand Prix',        'Suzuka International Racing Course',  'JP', 'Suzuka',        'Suzuka',        18, 8,  53, 5807, 13),
    ('abu-dhabi',     'Abu Dhabi Grand Prix',       'Yas Marina Circuit',                  'AE', 'Abu Dhabi',     'Yas Marina',    16, 7,  58, 5281, 14),
    ('austin',        'United States Grand Prix',   'Circuit of the Americas',             'US', 'Austin',        'COTA',          20, 11, 56, 5513, 15),
    ('interlagos',    'São Paulo Grand Prix',       'Autódromo José Carlos Pace',          'BR', 'São Paulo',     'Interlagos',    15, 10, 71, 4309, 16),
    ('red-bull-ring', 'Austrian Grand Prix',        'Red Bull Ring',                       'AT', 'Spielberg',     'Red Bull Ring', 10, 3,  71, 4318, 17),
    ('mexico',        'Mexico City Grand Prix',     'Autódromo Hermanos Rodríguez',        'MX', 'Mexico City',   'Mexico City',   17, 7,  71, 4304, 19),
    ('baku',          'Azerbaijan Grand Prix',      'Baku City Circuit',                   'AZ', 'Baku',          'Baku',          20, 12, 51, 6003, 20),
    ('zandvoort',     'Dutch Grand Prix',           'Circuit Zandvoort',                   'NL', 'Zandvoort',     'Zandvoort',     14, 4,  72, 4259, 26),
    ('imola',         'Emilia Romagna Grand Prix',  'Autodromo Enzo e Dino Ferrari',       'IT', 'Imola',         'Imola',         19, 9,  63, 4909, 27),
    ('jeddah',        'Saudi Arabian Grand Prix',   'Jeddah Corniche Circuit',             'SA', 'Jeddah',        'Jeddah',        27, 16, 50, 6174, 29),
    ('miami',         'Miami Grand Prix',           'Miami International Autodrome',       'US', 'Miami',         'Miami',         19, 8,  57, 5412, 30),
    ('las-vegas',     'Las Vegas Grand Prix',       'Las Vegas Strip Circuit',             'US', 'Las Vegas',     'Las Vegas',     17, 11, 50, 6201, 31),
    ('lusail',        'Qatar Grand Prix',           'Lusail International Circuit',        'QA', 'Lusail',        'Lusail',        16, 6,  57, 5419, 32);

CREATE TEMP TABLE seed_driver (
    ord             int,
    nickname        text,
    first_name      text,
    last_name       text,
    country         char(2),
    slug            text,
    racing_number   smallint
);

INSERT INTO seed_driver VALUES
    (1,  'Vortex',        'Luka',       'Horvat',      'HR', 'vortex',         7),
    (2,  'ApexHunter',    'Marko',      'Novak',       'SI', 'apex-hunter',    11),
    (3,  'Kerbside',      'Nikola',     'Petrović',    'RS', 'kerbside',       23),
    (4,  'LateBraker',    'Ivan',       'Kovačević',   'HR', 'late-braker',    4),
    (5,  'Slipstream',    'Tomas',      'Berg',        'SE', 'slipstream',     16),
    (6,  'DRSZone',       'Jan',        'de Vries',    'NL', 'drs-zone',       33),
    (7,  'TyreWhisperer', 'Matteo',     'Rossi',       'IT', 'tyre-whisperer', 5),
    (8,  'PitWall',       'Lukas',      'Schneider',   'DE', 'pit-wall',       12),
    (9,  'Undercut',      'Alejandro',  'García',      'ES', 'undercut',       55),
    (10, 'Overcut',       'Hugo',       'Lefèvre',     'FR', 'overcut',        10),
    (11, 'Sector3',       'Jakub',      'Nowak',       'PL', 'sector-3',       21),
    (12, 'PurpleLap',     'Elias',      'Gruber',      'AT', 'purple-lap',     3),
    (13, 'GripLevel',     'Bence',      'Nagy',        'HU', 'grip-level',     9),
    (14, 'BoxBox',        'Ondřej',     'Dvořák',      'CZ', 'boxbox',         14),
    (15, 'BlueFlag',      'Martin',     'Horváth',     'SK', 'blue-flag',      6),
    (16, 'Chicane',       'Arthur',     'Peeters',     'BE', 'chicane',        17),
    (17, 'Flatout',       'Eetu',       'Virtanen',    'FI', 'flatout',        77),
    (18, 'Parabolica',    'Mads',       'Jensen',      'DK', 'parabolica',     27),
    (19, 'EauRouge',      'Tiago',      'Silva',       'PT', 'eau-rouge',      31),
    (20, 'Senninha',      'Rafael',     'Costa',       'BR', 'senninha',       88),
    (21, 'TrackLimits',   'Jake',       'Miller',      'US', 'track-limits',   2),
    (22, 'Marbles',       'Oliver',     'Hughes',      'GB', 'marbles',        22),
    (23, 'Ghosted',       'Amar',       'Hodžić',      'BA', 'ghosted',        44),
    (24, 'Sideways',      'Filip',      'Stojanović',  'RS', 'sideways',       18),
    (25, 'Hotlap',        'Dino',       'Babić',       'HR', 'hotlap',         8),
    (26, 'Stint',         'Jure',       'Kranjc',      'SI', 'stint',          13),
    (27, 'Downforce',     'Aleksandar', 'Trajkovski',  'MK', 'downforce',      63),
    (28, 'Diffuser',      'Petar',      'Vuković',     'ME', 'diffuser',       19),
    (29, 'Telemetry',     'Leon',       'Weber',       'DE', 'telemetry',      81),
    (30, 'SafetyCar',     'Noah',       'Clarke',      'GB', 'safety-car',     99);

CREATE TEMP TABLE seed_division (league_id int, name text, slug text, tier smallint);

INSERT INTO seed_division VALUES
    (1, 'Division 1', 'div-1', 1),
    (1, 'Division 2', 'div-2', 2);

-- platform: 1 Steam, 2 PlayStation, 3 Xbox, 4 Origin, 5 Crossplay
-- game:     24 F1 24, 25 F1 25
-- driver_from / driver_to: range of seed_driver.ord registered for the season
CREATE TEMP TABLE seed_season (
    league_id       int,
    slug            text,
    name            text,
    platform        smallint,
    game            smallint,
    lap_pct         smallint,
    driver_from     int,
    driver_to       int
);

INSERT INTO seed_season VALUES
    -- League 1: two completed, one ongoing (both divisions), one upcoming
    (1, 'season-1',    'Season 1',       1, 24, 90, 1,  20),
    (1, 'season-2',    'Season 2',       1, 24, 90, 1,  20),
    (1, 'season-3',    'Season 3',       1, 25, 90, 1,  30),
    (1, 'season-4',    'Season 4',       1, 25, 90, 1,  24),
    -- League 3: completed, ongoing, upcoming without calendar
    (3, 'season-1',    'Season 1',       2, 24, 85, 11, 30),
    (3, 'season-2',    'Season 2',       2, 25, 85, 11, 30),
    (3, 'season-3',    'Season 3',       2, 25, 85, 11, 22),
    -- League 4: two completed, one upcoming
    (4, 'spring-2026', 'Spring 2026',    5, 25, 90, 6,  25),
    (4, 'summer-2026', 'Summer 2026',    5, 25, 90, 6,  25),
    (4, 'winter-2026', 'Winter 2026/27', 5, 25, 90, 6,  15);

CREATE TEMP TABLE seed_season_division (league_id int, season_slug text, division_slug text);

INSERT INTO seed_season_division VALUES
    (1, 'season-1', 'div-1'),
    (1, 'season-2', 'div-1'),
    (1, 'season-3', 'div-1'),
    (1, 'season-3', 'div-2'),
    (1, 'season-4', 'div-1');

-- division_slug NULL = league without divisions
CREATE TEMP TABLE seed_gp (
    league_id       int,
    season_slug     text,
    division_slug   text,
    track_key       text,
    starting_at     timestamptz
);

INSERT INTO seed_gp VALUES
    -- League 1 / Season 1 (completed, Wednesdays)
    (1, 'season-1', 'div-1', 'bahrain',       '2025-02-05 19:00+00'),
    (1, 'season-1', 'div-1', 'jeddah',        '2025-02-19 19:00+00'),
    (1, 'season-1', 'div-1', 'melbourne',     '2025-03-05 19:00+00'),
    (1, 'season-1', 'div-1', 'suzuka',        '2025-03-19 19:00+00'),
    (1, 'season-1', 'div-1', 'imola',         '2025-04-02 19:00+00'),
    (1, 'season-1', 'div-1', 'monaco',        '2025-04-16 19:00+00'),
    -- League 1 / Season 2 (completed)
    (1, 'season-2', 'div-1', 'zandvoort',     '2025-09-03 19:00+00'),
    (1, 'season-2', 'div-1', 'monza',         '2025-09-17 19:00+00'),
    (1, 'season-2', 'div-1', 'baku',          '2025-10-01 19:00+00'),
    (1, 'season-2', 'div-1', 'singapore',     '2025-10-15 19:00+00'),
    (1, 'season-2', 'div-1', 'austin',        '2025-10-29 19:00+00'),
    (1, 'season-2', 'div-1', 'mexico',        '2025-11-12 19:00+00'),
    (1, 'season-2', 'div-1', 'interlagos',    '2025-11-26 19:00+00'),
    (1, 'season-2', 'div-1', 'abu-dhabi',     '2025-12-10 19:00+00'),
    -- League 1 / Season 3 (ongoing, 4 of 8 raced) - Division 1; Division 2 is copied below
    (1, 'season-3', 'div-1', 'bahrain',       '2026-09-16 19:00+00'),
    (1, 'season-3', 'div-1', 'jeddah',        '2026-09-30 19:00+00'),
    (1, 'season-3', 'div-1', 'miami',         '2026-10-14 19:00+00'),
    (1, 'season-3', 'div-1', 'imola',         '2026-10-28 19:00+00'),
    (1, 'season-3', 'div-1', 'barcelona',     '2026-11-11 19:00+00'),
    (1, 'season-3', 'div-1', 'red-bull-ring', '2026-11-25 19:00+00'),
    (1, 'season-3', 'div-1', 'silverstone',   '2026-12-09 19:00+00'),
    (1, 'season-3', 'div-1', 'spa',           '2026-12-16 19:00+00'),
    -- League 1 / Season 4 (upcoming)
    (1, 'season-4', 'div-1', 'melbourne',     '2027-02-10 19:00+00'),
    (1, 'season-4', 'div-1', 'suzuka',        '2027-02-24 19:00+00'),
    (1, 'season-4', 'div-1', 'monaco',        '2027-03-10 19:00+00'),
    (1, 'season-4', 'div-1', 'montreal',      '2027-03-24 19:00+00'),
    (1, 'season-4', 'div-1', 'hungaroring',   '2027-04-07 19:00+00'),
    (1, 'season-4', 'div-1', 'abu-dhabi',     '2027-04-21 19:00+00'),
    -- League 3 / Season 1 (completed, Thursdays)
    (3, 'season-1', NULL,    'bahrain',       '2026-03-05 19:00+00'),
    (3, 'season-1', NULL,    'jeddah',        '2026-03-19 19:00+00'),
    (3, 'season-1', NULL,    'suzuka',        '2026-04-02 19:00+00'),
    (3, 'season-1', NULL,    'miami',         '2026-04-16 19:00+00'),
    (3, 'season-1', NULL,    'monaco',        '2026-04-30 19:00+00'),
    (3, 'season-1', NULL,    'barcelona',     '2026-05-14 19:00+00'),
    -- League 3 / Season 2 (ongoing, 3 of 7 raced)
    (3, 'season-2', NULL,    'silverstone',   '2026-10-15 19:00+00'),
    (3, 'season-2', NULL,    'hungaroring',   '2026-10-22 19:00+00'),
    (3, 'season-2', NULL,    'spa',           '2026-10-29 19:00+00'),
    (3, 'season-2', NULL,    'zandvoort',     '2026-11-05 19:00+00'),
    (3, 'season-2', NULL,    'monza',         '2026-11-12 19:00+00'),
    (3, 'season-2', NULL,    'singapore',     '2026-11-19 19:00+00'),
    (3, 'season-2', NULL,    'las-vegas',     '2026-11-26 19:00+00'),
    -- League 3 / Season 3: no calendar yet
    -- League 4 / Spring 2026 (completed, Sundays)
    (4, 'spring-2026', NULL, 'melbourne',     '2026-03-08 19:00+00'),
    (4, 'spring-2026', NULL, 'bahrain',       '2026-03-22 19:00+00'),
    (4, 'spring-2026', NULL, 'jeddah',        '2026-04-05 19:00+00'),
    (4, 'spring-2026', NULL, 'suzuka',        '2026-04-19 19:00+00'),
    (4, 'spring-2026', NULL, 'miami',         '2026-05-03 19:00+00'),
    (4, 'spring-2026', NULL, 'imola',         '2026-05-17 19:00+00'),
    (4, 'spring-2026', NULL, 'monaco',        '2026-05-31 19:00+00'),
    (4, 'spring-2026', NULL, 'montreal',      '2026-06-14 19:00+00'),
    -- League 4 / Summer 2026 (completed)
    (4, 'summer-2026', NULL, 'red-bull-ring', '2026-07-05 19:00+00'),
    (4, 'summer-2026', NULL, 'silverstone',   '2026-07-19 19:00+00'),
    (4, 'summer-2026', NULL, 'hungaroring',   '2026-08-02 19:00+00'),
    (4, 'summer-2026', NULL, 'spa',           '2026-08-16 19:00+00'),
    (4, 'summer-2026', NULL, 'zandvoort',     '2026-08-30 19:00+00'),
    -- League 4 / Winter 2026/27 (upcoming)
    (4, 'winter-2026', NULL, 'lusail',        '2027-01-10 19:00+00'),
    (4, 'winter-2026', NULL, 'abu-dhabi',     '2027-01-24 19:00+00'),
    (4, 'winter-2026', NULL, 'interlagos',    '2027-02-07 19:00+00'),
    (4, 'winter-2026', NULL, 'mexico',        '2027-02-21 19:00+00'),
    (4, 'winter-2026', NULL, 'austin',        '2027-03-07 19:00+00'),
    (4, 'winter-2026', NULL, 'las-vegas',     '2027-03-21 19:00+00');

-- Division 2 of League 1 / Season 3 races the same calendar one day later
INSERT INTO seed_gp (league_id, season_slug, division_slug, track_key, starting_at)
SELECT league_id, season_slug, 'div-2', track_key, starting_at + interval '1 day'
FROM seed_gp
WHERE league_id = 1 AND season_slug = 'season-3' AND division_slug = 'div-1';

-- -------------------------------------------------------------------------------------
-- Reference data: teams, tracks, track layouts
-- -------------------------------------------------------------------------------------

INSERT INTO public.team (name, color)
SELECT st.name, st.color
FROM seed_team st
WHERE NOT EXISTS (SELECT 1 FROM public.team t WHERE t.name = st.name);

-- Skip tracks whose layout (by telemetry id) already exists under a different track name
INSERT INTO public.track (name, country_id, city, short_name)
SELECT st.track_name, c.id, st.city, st.short_name
FROM seed_track st
JOIN public.dc_country c ON c.code_alpha2 = st.country
WHERE NOT EXISTS (SELECT 1 FROM public.track t WHERE t.name = st.track_name)
  AND NOT EXISTS (SELECT 1 FROM public.track_layout tl WHERE tl.telemetry_id = st.telemetry_id);

INSERT INTO public.track_layout (track_id, name, corners_total, corners_left, laps_grand_prix, length, telemetry_id)
SELECT t.id, 'Grand Prix', st.corners_total, st.corners_left, st.laps, st.length, st.telemetry_id
FROM seed_track st
JOIN public.track t ON t.name = st.track_name
WHERE NOT EXISTS (SELECT 1 FROM public.track_layout tl WHERE tl.telemetry_id = st.telemetry_id);

INSERT INTO public.track_layout_game (track_layout_id, game)
SELECT tl.id, g.game
FROM seed_track st
JOIN public.track_layout tl ON tl.telemetry_id = st.telemetry_id
CROSS JOIN (VALUES (24::smallint), (25::smallint)) AS g(game)
ON CONFLICT DO NOTHING;

-- -------------------------------------------------------------------------------------
-- Drivers
-- -------------------------------------------------------------------------------------

INSERT INTO public.driver (nickname, first_name, last_name, country_id, slug)
SELECT sd.nickname, sd.first_name, sd.last_name, c.id, sd.slug
FROM seed_driver sd
LEFT JOIN public.dc_country c ON c.code_alpha2 = sd.country
ON CONFLICT DO NOTHING;

-- -------------------------------------------------------------------------------------
-- Divisions, seasons, season divisions
-- -------------------------------------------------------------------------------------

INSERT INTO public.division (league_id, name, slug, tier)
SELECT league_id, name, slug, tier
FROM seed_division
ON CONFLICT (league_id, slug) DO NOTHING;

INSERT INTO public.season (league_id, name, platform, game, lap_percentage_required, slug)
SELECT league_id, name, platform, game, lap_pct, slug
FROM seed_season
ON CONFLICT (league_id, slug) DO NOTHING;

INSERT INTO public.season_division (season_id, division_id)
SELECT s.id, d.id
FROM seed_season_division ssd
JOIN public.season s ON s.league_id = ssd.league_id AND s.slug = ssd.season_slug
JOIN public.division d ON d.league_id = ssd.league_id AND d.slug = ssd.division_slug
ON CONFLICT (season_id, division_id) DO NOTHING;

-- Standard race points (session_type 15 = Race), positions 1-10
INSERT INTO public.season_points (season_id, session_type, position, points)
SELECT s.id, 15, p.position, p.points
FROM seed_season ss
JOIN public.season s ON s.league_id = ss.league_id AND s.slug = ss.slug
CROSS JOIN (VALUES
    (1, 25), (2, 18), (3, 15), (4, 12), (5, 10),
    (6, 8),  (7, 6),  (8, 4),  (9, 2),  (10, 1)
) AS p(position, points)
WHERE NOT EXISTS (SELECT 1 FROM public.season_points sp WHERE sp.season_id = s.id);

-- -------------------------------------------------------------------------------------
-- Season drivers: drivers of the season's range are spread over the 10 teams in order
-- -------------------------------------------------------------------------------------

INSERT INTO public.season_driver (season_id, team_id, driver_id, racing_number, penalty_points)
SELECT s.id, t.id, d.id, sd.racing_number, 0
FROM seed_season ss
JOIN public.season s ON s.league_id = ss.league_id AND s.slug = ss.slug
JOIN seed_driver sd ON sd.ord BETWEEN ss.driver_from AND ss.driver_to
JOIN public.driver d ON d.slug = sd.slug
JOIN seed_team st ON st.ord = ((sd.ord - ss.driver_from) % 10) + 1
JOIN LATERAL (
    SELECT id FROM public.team WHERE name = st.name ORDER BY id LIMIT 1
) t ON true
ON CONFLICT (season_id, driver_id, team_id) DO NOTHING;

-- -------------------------------------------------------------------------------------
-- Grands prix
-- -------------------------------------------------------------------------------------

INSERT INTO public.grand_prix (track_layout_id, season_id, season_division_id, name, starting_at, slug)
SELECT
    tl.id,
    s.id,
    sdiv.id,
    st.gp_name,
    g.starting_at,
    g.track_key || CASE WHEN g.division_slug IS NOT NULL AND g.division_slug <> 'div-1'
                        THEN '-' || g.division_slug
                        ELSE '' END
FROM seed_gp g
JOIN public.season s ON s.league_id = g.league_id AND s.slug = g.season_slug
JOIN seed_track st ON st.key = g.track_key
JOIN LATERAL (
    SELECT id FROM public.track_layout WHERE telemetry_id = st.telemetry_id ORDER BY id LIMIT 1
) tl ON true
LEFT JOIN public.division d ON d.league_id = g.league_id AND d.slug = g.division_slug
LEFT JOIN public.season_division sdiv ON sdiv.season_id = s.id AND sdiv.division_id = d.id
ON CONFLICT (season_id, slug) DO NOTHING;

-- -------------------------------------------------------------------------------------
-- Verification: fail (and roll back) if any planned grand prix is missing
-- -------------------------------------------------------------------------------------

DO $$
DECLARE
    missing int;
BEGIN
    SELECT count(*) INTO missing
    FROM seed_gp g
    JOIN public.season s ON s.league_id = g.league_id AND s.slug = g.season_slug
    WHERE NOT EXISTS (
        SELECT 1 FROM public.grand_prix gp
        WHERE gp.season_id = s.id
          AND gp.slug = g.track_key || CASE WHEN g.division_slug IS NOT NULL AND g.division_slug <> 'div-1'
                                            THEN '-' || g.division_slug
                                            ELSE '' END
    );

    IF missing > 0 THEN
        RAISE EXCEPTION '% seeded grands prix are missing - check track/country data', missing;
    END IF;

    RAISE NOTICE 'Seed OK: % seasons, % grands prix, % season drivers in leagues 1, 3, 4',
        (SELECT count(*) FROM public.season WHERE league_id IN (1, 3, 4)),
        (SELECT count(*) FROM public.grand_prix gp JOIN public.season s ON s.id = gp.season_id WHERE s.league_id IN (1, 3, 4)),
        (SELECT count(*) FROM public.season_driver sd JOIN public.season s ON s.id = sd.season_id WHERE s.league_id IN (1, 3, 4));
END $$;

COMMIT;

DROP TABLE IF EXISTS seed_team, seed_track, seed_driver, seed_division,
    seed_season, seed_season_division, seed_gp;
