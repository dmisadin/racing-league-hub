-- MIGRONDI:NAME=1789586889811_add-divisions-within-league.sql
-- MIGRONDI:TIMESTAMP=1789586889811
-- ---------- MIGRONDI:UP ----------
-- Add your SQL migration code below. You can delete this line but do not delete the comments above.

CREATE TABLE division (
    id          int         GENERATED ALWAYS AS IDENTITY PRIMARY KEY,
    league_id   int         NOT NULL,
    "name"      varchar(64) NOT NULL,
    slug        varchar(32) NOT NULL,
    tier        smallint    NOT NULL,

    CONSTRAINT division_league_id_slug_key
        UNIQUE (league_id, slug),

    CONSTRAINT division_league_id
        FOREIGN KEY(league_id) REFERENCES public.league(id)
);

CREATE TABLE public.season_division (
    id          int GENERATED ALWAYS AS IDENTITY PRIMARY KEY,
    season_id   int NOT NULL,
    division_id int NOT NULL,

    UNIQUE (season_id, division_id),
    UNIQUE (id, season_id),

    FOREIGN KEY (season_id)
        REFERENCES public.season (id),

    FOREIGN KEY (division_id)
        REFERENCES public.division (id)
);

ALTER TABLE public.grand_prix
    ADD season_division_id int NULL;

ALTER TABLE public.grand_prix
    ADD CONSTRAINT grand_prix_season_division_id_fkey
        FOREIGN KEY (season_division_id)
        REFERENCES public.season_division (id);

-- ---------- MIGRONDI:DOWN ----------
-- Add your SQL rollback code below. You can delete this line but do not delete the comment above.


