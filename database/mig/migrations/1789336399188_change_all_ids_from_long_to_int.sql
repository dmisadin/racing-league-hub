-- MIGRONDI:NAME=1789336399188_change_all_ids_from_long_to_int.sql
-- MIGRONDI:TIMESTAMP=1789336399188
-- ---------- MIGRONDI:UP ----------
-- Add your SQL migration code below. You can delete this line but do not delete the comments above.

ALTER TABLE public.driver
    ALTER COLUMN id TYPE INT;

ALTER TABLE public.game_team
    ALTER COLUMN id TYPE INT,
    ALTER COLUMN team_id TYPE INT,
    ALTER COLUMN logo_resource_id TYPE INT;

ALTER TABLE public.grand_prix
    ALTER COLUMN id TYPE INT,
    ALTER COLUMN season_id TYPE INT,
    ALTER COLUMN track_layout_id TYPE INT;

ALTER TABLE public.grand_prix_driver
    ALTER COLUMN id TYPE INT,
    ALTER COLUMN grand_prix_id TYPE INT,
    ALTER COLUMN driver_id TYPE INT,
    ALTER COLUMN team_id TYPE INT;

ALTER TABLE public.grand_prix_result
    ALTER COLUMN id TYPE INT,
    ALTER COLUMN grand_prix_driver_id TYPE INT;

ALTER TABLE public.incident
    ALTER COLUMN id TYPE INT,
    ALTER COLUMN user_id TYPE INT,
    ALTER COLUMN grand_prix_id TYPE INT;

ALTER TABLE public.incident_driver
    ALTER COLUMN incident_id TYPE INT,
    ALTER COLUMN driver_id TYPE INT;

ALTER TABLE public.league
    ALTER COLUMN id TYPE INT,
    ALTER COLUMN logo_resource_id TYPE INT;

ALTER TABLE public.league_user
    ALTER COLUMN id TYPE INT,
    ALTER COLUMN league_id TYPE INT,
    ALTER COLUMN user_id TYPE INT;

ALTER TABLE public.password_reset_token
    ALTER COLUMN id TYPE INT,
    ALTER COLUMN user_id TYPE INT;

ALTER TABLE public.refresh_token
    ALTER COLUMN id TYPE INT,
    ALTER COLUMN user_id TYPE INT;

ALTER TABLE public.resource
    ALTER COLUMN id TYPE INT;

ALTER TABLE public.season
    ALTER COLUMN id TYPE INT,
    ALTER COLUMN league_id TYPE INT,
    ALTER COLUMN logo_resource_id TYPE INT;

ALTER TABLE public.season_assists
    ALTER COLUMN id TYPE INT,
    ALTER COLUMN season_id TYPE INT;

ALTER TABLE public.season_driver
    ALTER COLUMN id TYPE INT,
    ALTER COLUMN season_id TYPE INT,
    ALTER COLUMN team_id TYPE INT,
    ALTER COLUMN driver_id TYPE INT;

ALTER TABLE public.season_lobby_settings
    ALTER COLUMN id TYPE INT,
    ALTER COLUMN season_id TYPE INT;

ALTER TABLE public.season_points
    ALTER COLUMN id TYPE INT,
    ALTER COLUMN season_id TYPE INT;

ALTER TABLE public.team
    ALTER COLUMN id TYPE INT;

ALTER TABLE public.track
    ALTER COLUMN id TYPE INT;

ALTER TABLE public.track_layout
    ALTER COLUMN id TYPE INT,
    ALTER COLUMN track_id TYPE INT,
    ALTER COLUMN map_image_resource_id TYPE INT,
    ALTER COLUMN cover_image_resource_id TYPE INT;

ALTER TABLE public.track_layout_game
    ALTER COLUMN track_layout_id TYPE INT;

ALTER TABLE public.user
    ALTER COLUMN id TYPE INT,
    ALTER COLUMN driver_id TYPE INT;

ALTER TABLE public.user_external_login
    ALTER COLUMN id TYPE INT,
    ALTER COLUMN user_id TYPE INT;

ALTER TABLE public.user_recovery_code
    ALTER COLUMN id TYPE INT,
    ALTER COLUMN user_id TYPE INT;

ALTER TABLE public.verdict
    ALTER COLUMN id TYPE INT,
    ALTER COLUMN incident_id TYPE INT;

-- ---------- MIGRONDI:DOWN ----------
-- Add your SQL rollback code below. You can delete this line but do not delete the comment above.


