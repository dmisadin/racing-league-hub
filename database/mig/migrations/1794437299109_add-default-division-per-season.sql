-- MIGRONDI:NAME=1794437299109_add-default-division-per-season.sql
-- MIGRONDI:TIMESTAMP=1794437299109
-- ---------- MIGRONDI:UP ----------

-- Every league gets at least one division and every season at least one season_division,
-- so season-scoped data can reference season_division instead of season (next migrations).

-- Leagues without divisions get a default one.
INSERT INTO public.division (league_id, "name", slug, tier)
SELECT l.id, 'Main', 'main', 1
FROM public.league l
WHERE NOT EXISTS (
    SELECT 1 FROM public.division d WHERE d.league_id = l.id
);

-- Seasons without divisions join their league's top-tier division.
INSERT INTO public.season_division (season_id, division_id)
SELECT s.id, top_division.id
FROM public.season s
JOIN LATERAL (
    SELECT d.id
    FROM public.division d
    WHERE d.league_id = s.league_id
    ORDER BY d.tier, d.id
    LIMIT 1
) top_division ON true
WHERE NOT EXISTS (
    SELECT 1 FROM public.season_division sd WHERE sd.season_id = s.id
);

-- A season_division is part of its season: deleting a season removes its (empty) season divisions.
-- Season divisions that still have Grands Prix, drivers or settings keep blocking the delete.
ALTER TABLE public.season_division
    DROP CONSTRAINT season_division_season_id_fkey;

ALTER TABLE public.season_division
    ADD CONSTRAINT season_division_season_id_fkey
        FOREIGN KEY (season_id) REFERENCES public.season (id) ON DELETE CASCADE;

-- Division lookups from season_division (FK side).
CREATE INDEX season_division_division_id_idx ON public.season_division (division_id);

-- ---------- MIGRONDI:DOWN ----------

ALTER TABLE public.season_division
    DROP CONSTRAINT season_division_season_id_fkey;

ALTER TABLE public.season_division
    ADD CONSTRAINT season_division_season_id_fkey
        FOREIGN KEY (season_id) REFERENCES public.season (id);

DROP INDEX IF EXISTS public.season_division_division_id_idx;

-- The default divisions and season divisions are kept: once data references them they cannot be told apart.
