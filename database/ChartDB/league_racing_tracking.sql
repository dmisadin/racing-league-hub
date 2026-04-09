CREATE SCHEMA IF NOT EXISTS "public";

CREATE TABLE "public"."verdict" (
    "id" bigint GENERATED ALWAYS AS IDENTITY,
    "incident_id" bigint NOT NULL,
    "summary" varchar(256) NOT NULL,
    "explanation" bigint NOT NULL,
    "created_at" timestamptz NOT NULL,
    "steward_penalty_type" smallint NOT NULL,
    -- 3 race bans, 5 seconds, 2 penalty points
    "penalty_amount" smallint,
    PRIMARY KEY ("id")
);
COMMENT ON COLUMN "public"."verdict"."penalty_amount" IS '3 race bans, 5 seconds, 2 penalty points';
-- Indexes
CREATE INDEX "verdict_incident_id_idx" ON "public"."verdict" ("incident_id");

CREATE TABLE "public"."grand_prix_result" (
    "id" bigint GENERATED ALWAYS AS IDENTITY,
    "grand_prix_driver_id" bigint NOT NULL,
    "session_type" smallint NOT NULL,
    "position" bigint NOT NULL,
    "grid_position" bigint,
    "result_status" smallint NOT NULL,
    "race_time_in_ms" bigint,
    "time_penalty" smallint NOT NULL,
    "steward_time_penalty" bigint,
    "laps_completed" smallint NOT NULL,
    -- NULL if session is not giving out points
    "points_gained" smallint,
    "used_tyres" varchar(8),
    "fastest_lap_in_ms" bigint,
    "best_time_tyre" varchar(2),
    PRIMARY KEY ("id")
);
COMMENT ON COLUMN "public"."grand_prix_result"."points_gained" IS 'NULL if session is not giving out points';
-- Indexes
CREATE INDEX "grand_prix_result_grand_prix_driver_id_idx" ON "public"."grand_prix_result" ("grand_prix_driver_id");

CREATE TABLE "public"."league_user" (
    "id" bigint GENERATED ALWAYS AS IDENTITY,
    "league_id" bigint NOT NULL,
    "user_id" bigint NOT NULL,
    "is_owner" boolean NOT NULL,
    "is_admin" boolean NOT NULL,
    "is_editor" boolean NOT NULL,
    "is_steward" boolean NOT NULL,
    PRIMARY KEY ("id")
);
-- Indexes
CREATE UNIQUE INDEX "league_user_league_id_user_id_key" ON "public"."league_user" ("league_id", "user_id");

CREATE TABLE "public"."driver" (
    "id" bigint GENERATED ALWAYS AS IDENTITY,
    "nickname" varchar(64) NOT NULL,
    "first_name" varchar(64),
    "last_name" varchar(64),
    "country" varchar(2),
    "slug" varchar(64) NOT NULL,
    PRIMARY KEY ("id")
);
-- Indexes
CREATE UNIQUE INDEX "driver_nickname_key" ON "public"."driver" ("nickname");
CREATE UNIQUE INDEX "driver_slug_key" ON "public"."driver" ("slug");

CREATE TABLE "public"."season_points" (
    "id" bigint GENERATED ALWAYS AS IDENTITY,
    "season_id" bigint NOT NULL,
    "session_type" smallint NOT NULL,
    "position" smallint NOT NULL,
    "points" smallint NOT NULL,
    PRIMARY KEY ("id")
);
-- Indexes
CREATE INDEX "season_points_season_id_idx" ON "public"."season_points" ("season_id");

CREATE TABLE "public"."track" (
    "id" bigint GENERATED ALWAYS AS IDENTITY,
    "name" varchar(128) NOT NULL,
    "country" varchar(2) NOT NULL,
    "city" varchar(64) NOT NULL,
    "short_name" varchar(64),
    PRIMARY KEY ("id")
);

CREATE TABLE "public"."game_team" (
    "id" bigint GENERATED ALWAYS AS IDENTITY,
    "game" smallint NOT NULL,
    "team_id" bigint NOT NULL,
    "name" varchar(128) NOT NULL,
    "short_name" varchar(64) NOT NULL,
    "abbreviation" varchar(3) NOT NULL,
    "color" varchar(32) NOT NULL,
    "logo_resource_id" bigint,
    -- ID reference to data that game sends through telemetry
    "telemetry_id" smallint NOT NULL,
    PRIMARY KEY ("id")
);
COMMENT ON COLUMN "public"."game_team"."telemetry_id" IS 'ID reference to data that game sends through telemetry';
-- Indexes
CREATE UNIQUE INDEX "game_team_game_team_id_key" ON "public"."game_team" ("game", "team_id");

CREATE TABLE "public"."incident_driver" (
    "incident_id" bigint NOT NULL,
    "driver_id" bigint NOT NULL,
    PRIMARY KEY ("incident_id", "driver_id")
);

CREATE TABLE "public"."incident" (
    "id" bigint GENERATED ALWAYS AS IDENTITY,
    -- submitted by
    "user_id" bigint NOT NULL,
    "grand_prix_id" bigint NOT NULL,
    "session_type" smallint NOT NULL,
    "title" varchar(128) NOT NULL,
    "description" text NOT NULL,
    "evidence" text NOT NULL,
    "created_at" timestamptz NOT NULL,
    PRIMARY KEY ("id")
);
COMMENT ON COLUMN "public"."incident"."user_id" IS 'submitted by';
-- Indexes
CREATE INDEX "incident_grand_prix_id_idx" ON "public"."incident" ("grand_prix_id");
CREATE INDEX "incident_user_id_idx" ON "public"."incident" ("user_id");

CREATE TABLE "public"."grand_prix" (
    "id" bigint GENERATED ALWAYS AS IDENTITY,
    "track_layout_id" bigint NOT NULL,
    "season_id" bigint NOT NULL,
    "name" varchar(100) NOT NULL,
    "starting_at" timestamptz NOT NULL,
    "vod_url" text,
    "slug" varchar(64) NOT NULL,
    PRIMARY KEY ("id")
);
-- Indexes
CREATE INDEX "grand_prix_season_id_idx" ON "public"."grand_prix" ("season_id");
CREATE INDEX "grand_prix_track_layout_id_idx" ON "public"."grand_prix" ("track_layout_id");
CREATE UNIQUE INDEX "grand_prix_season_id_slug_key" ON "public"."grand_prix" ("season_id", "slug");

CREATE TABLE "public"."user" (
    "id" bigint GENERATED ALWAYS AS IDENTITY,
    "username" varchar(32) NOT NULL,
    "email" varchar(64) NOT NULL,
    "password" text NOT NULL,
    "is_admin" boolean NOT NULL,
    "driver_id" bigint,
    PRIMARY KEY ("id")
);
-- Indexes
CREATE INDEX "user_driver_id_idx" ON "public"."user" ("driver_id");

CREATE TABLE "public"."track_layout_game" (
    "track_layout_id" bigint NOT NULL,
    "game" smallint NOT NULL,
    PRIMARY KEY ("track_layout_id", "game")
);

CREATE TABLE "public"."team" (
    "id" bigint GENERATED ALWAYS AS IDENTITY,
    "name" varchar(100) NOT NULL,
    "color" varchar(16),
    PRIMARY KEY ("id")
);

CREATE TABLE "public"."season" (
    "id" bigint GENERATED ALWAYS AS IDENTITY,
    "league_id" bigint NOT NULL,
    "name" varchar(100) NOT NULL,
    "platform" smallint NOT NULL,
    "game" smallint NOT NULL,
    "lap_percentage_required" smallint NOT NULL DEFAULT 90,
    "slug" varchar(64) NOT NULL,
    "logo_resource_id" bigint,
    PRIMARY KEY ("id")
);
-- Indexes
CREATE INDEX "grand_prix_league_id_idx" ON "public"."season" ("league_id");
CREATE UNIQUE INDEX "season_league_id_slug_key" ON "public"."season" ("league_id", "slug");

CREATE TABLE "public"."league" (
    "id" bigint GENERATED ALWAYS AS IDENTITY,
    "region" smallint NOT NULL,
    "name" varchar(500) NOT NULL,
    "abbreviation" varchar(5) NOT NULL,
    "description" text,
    -- IANA https://www.iana.org/time-zones
    "timezone" text NOT NULL,
    "slug" varchar(64) NOT NULL,
    "logo_resource_id" bigint,
    PRIMARY KEY ("id")
);
COMMENT ON COLUMN "public"."league"."timezone" IS 'IANA https://www.iana.org/time-zones';
-- Indexes
CREATE UNIQUE INDEX "league_slug_key" ON "public"."league" ("slug");

CREATE TABLE "public"."grand_prix_driver" (
    "id" bigint GENERATED ALWAYS AS IDENTITY,
    "grand_prix_id" bigint NOT NULL,
    "driver_id" bigint NOT NULL,
    "team_id" bigint NOT NULL,
    "is_reserve" boolean NOT NULL,
    PRIMARY KEY ("id")
);
-- Indexes
CREATE INDEX "grand_prix_driver_driver_id_idx" ON "public"."grand_prix_driver" ("driver_id");
CREATE INDEX "grand_prix_driver_grand_prix_id_idx" ON "public"."grand_prix_driver" ("grand_prix_id");
CREATE INDEX "grand_prix_driver_team_id_idx" ON "public"."grand_prix_driver" ("team_id");

CREATE TABLE "public"."season_driver" (
    "id" bigint GENERATED ALWAYS AS IDENTITY,
    "season_id" bigint NOT NULL,
    "team_id" bigint NOT NULL,
    "driver_id" bigint NOT NULL,
    "racing_number" smallint,
    "penalty_points" smallint NOT NULL,
    PRIMARY KEY ("id")
);
-- Indexes
CREATE UNIQUE INDEX "season_driver_season_id_driver_id_team_id_key" ON "public"."season_driver" ("season_id", "driver_id", "team_id");

CREATE TABLE "public"."resource" (
    "id" bigint GENERATED ALWAYS AS IDENTITY,
    "storage_id" uuid NOT NULL DEFAULT gen_random_uuid(),
    "url" text NOT NULL,
    "file_name" text NOT NULL,
    "extension" varchar(10) NOT NULL,
    "mime_type" varchar(100) NOT NULL,
    "size_in_bytes" bigint NOT NULL,
    "created_at" timestamptz NOT NULL,
    "is_thumbnail" boolean,
    "status" smallint NOT NULL,
    PRIMARY KEY ("id")
);

CREATE TABLE "public"."track_layout" (
    "id" bigint GENERATED ALWAYS AS IDENTITY,
    "track_id" bigint NOT NULL,
    "name" varchar(128) NOT NULL,
    "pit_stop_duration" smallint,
    "corners_total" smallint NOT NULL,
    "corners_left" smallint NOT NULL,
    "laps_grand_prix" smallint NOT NULL,
    "length" smallint NOT NULL,
    "elevation_change" numeric(5, 1),
    "telemetry_id" smallint NOT NULL,
    "map_image_resource_id" bigint,
    "cover_image_resource_id" bigint,
    PRIMARY KEY ("id")
);
-- Indexes
CREATE INDEX "track_layout_track_id_idx" ON "public"."track_layout" ("track_id");

CREATE TABLE "public"."season_assists" (
    "id" bigint GENERATED ALWAYS AS IDENTITY,
    "season_id" bigint NOT NULL,
    "racing_line" smallint NOT NULL,
    "gearbox" smallint NOT NULL,
    "traction_control" smallint NOT NULL,
    "abs" boolean NOT NULL,
    PRIMARY KEY ("id")
);
-- Indexes
CREATE INDEX "season_assists_season_id_idx" ON "public"."season_assists" ("season_id");

CREATE TABLE "public"."season_lobby_settings" (
    "id" bigint GENERATED ALWAYS AS IDENTITY,
    "season_id" bigint NOT NULL,
    "qualifying_type" smallint NOT NULL,
    "race_distance_percentage" smallint NOT NULL,
    "formation_lap" boolean NOT NULL,
    "weather" smallint NOT NULL,
    "corner_cutting" smallint NOT NULL,
    "car_damage" smallint NOT NULL,
    "car_damage_rate" smallint NOT NULL,
    "parc_ferme" boolean NOT NULL,
    "equal_car_performance" boolean NOT NULL,
    "safety_car" smallint NOT NULL,
    "collisions" boolean NOT NULL,
    "ghosting" boolean NOT NULL,
    "race_start" smallint NOT NULL,
    PRIMARY KEY ("id")
);
-- Indexes
CREATE INDEX "season_lobby_settings_season_id_idx" ON "public"."season_lobby_settings" ("season_id");

-- Foreign key constraints
-- Schema: public
ALTER TABLE "public"."grand_prix_driver" ADD CONSTRAINT "grand_prix_driver_driver_id_fkey" FOREIGN KEY("driver_id") REFERENCES "public"."driver"("id");
ALTER TABLE "public"."user" ADD CONSTRAINT "user_driver_id_fkey" FOREIGN KEY("driver_id") REFERENCES "public"."driver"("id");
ALTER TABLE "public"."season_driver" ADD CONSTRAINT "season_driver_driver_id_fkey" FOREIGN KEY("driver_id") REFERENCES "public"."driver"("id");
ALTER TABLE "public"."game_team" ADD CONSTRAINT "game_team_logo_resource_id_fkey" FOREIGN KEY("logo_resource_id") REFERENCES "public"."resource"("id");
ALTER TABLE "public"."grand_prix_result" ADD CONSTRAINT "grand_prix_result_grand_prix_driver_id_fkey" FOREIGN KEY("grand_prix_driver_id") REFERENCES "public"."grand_prix_driver"("id");
ALTER TABLE "public"."grand_prix_driver" ADD CONSTRAINT "grand_prix_driver_grand_prix_id_fkey" FOREIGN KEY("grand_prix_id") REFERENCES "public"."grand_prix"("id");
ALTER TABLE "public"."incident_driver" ADD CONSTRAINT "incident_driver_incident_id_fkey" FOREIGN KEY("incident_id") REFERENCES "public"."incident"("id");
ALTER TABLE "public"."incident" ADD CONSTRAINT "incident_grand_prix_id_fkey" FOREIGN KEY("grand_prix_id") REFERENCES "public"."grand_prix"("id");
ALTER TABLE "public"."season" ADD CONSTRAINT "season_league_id_fkey" FOREIGN KEY("league_id") REFERENCES "public"."league"("id");
ALTER TABLE "public"."league_user" ADD CONSTRAINT "league_user_league_id_fkey" FOREIGN KEY("league_id") REFERENCES "public"."league"("id");
ALTER TABLE "public"."league" ADD CONSTRAINT "league_logo_resource_id_fkey" FOREIGN KEY("logo_resource_id") REFERENCES "public"."resource"("id");
ALTER TABLE "public"."track_layout" ADD CONSTRAINT "track_layout_map_image_resource_id_fkey" FOREIGN KEY("map_image_resource_id") REFERENCES "public"."resource"("id");
ALTER TABLE "public"."track_layout" ADD CONSTRAINT "track_layout_cover_image_resource_id_fkey" FOREIGN KEY("cover_image_resource_id") REFERENCES "public"."resource"("id");
ALTER TABLE "public"."season_driver" ADD CONSTRAINT "season_driver_season_id_fkey" FOREIGN KEY("season_id") REFERENCES "public"."season"("id");
ALTER TABLE "public"."grand_prix" ADD CONSTRAINT "grand_prix_season_id_fkey" FOREIGN KEY("season_id") REFERENCES "public"."season"("id");
ALTER TABLE "public"."season_points" ADD CONSTRAINT "season_points_season_id_fkey" FOREIGN KEY("season_id") REFERENCES "public"."season"("id");
ALTER TABLE "public"."season" ADD CONSTRAINT "season_logo_resource_id_fkey" FOREIGN KEY("logo_resource_id") REFERENCES "public"."resource"("id");
ALTER TABLE "public"."game_team" ADD CONSTRAINT "game_team_team_id_fkey" FOREIGN KEY("team_id") REFERENCES "public"."team"("id");
ALTER TABLE "public"."grand_prix_driver" ADD CONSTRAINT "grand_prix_driver_team_id_fkey" FOREIGN KEY("team_id") REFERENCES "public"."team"("id");
ALTER TABLE "public"."season_driver" ADD CONSTRAINT "season_driver_team_id_fkey" FOREIGN KEY("team_id") REFERENCES "public"."team"("id");
ALTER TABLE "public"."track_layout" ADD CONSTRAINT "track_layout_track_id_fkey" FOREIGN KEY("track_id") REFERENCES "public"."track"("id");
ALTER TABLE "public"."track_layout_game" ADD CONSTRAINT "track_layout_game_track_layout_id_fkey" FOREIGN KEY("track_layout_id") REFERENCES "public"."track_layout"("id");
ALTER TABLE "public"."grand_prix" ADD CONSTRAINT "grand_prix_track_layout_id_fkey" FOREIGN KEY("track_layout_id") REFERENCES "public"."track_layout"("id");
ALTER TABLE "public"."league_user" ADD CONSTRAINT "league_user_user_id_fkey" FOREIGN KEY("user_id") REFERENCES "public"."user"("id");
ALTER TABLE "public"."verdict" ADD CONSTRAINT "verdict_incident_id_fkey" FOREIGN KEY("incident_id") REFERENCES "public"."incident"("id");
ALTER TABLE "public"."incident_driver" ADD CONSTRAINT "incident_driver_driver_id_fkey" FOREIGN KEY("driver_id") REFERENCES "public"."driver"("id");
ALTER TABLE "public"."incident" ADD CONSTRAINT "incident_user_id_fkey" FOREIGN KEY("user_id") REFERENCES "public"."user"("id");
ALTER TABLE "public"."season_assists" ADD CONSTRAINT "season_assists_season_id_season_id_fkey" FOREIGN KEY("season_id") REFERENCES "public"."season"("id");
ALTER TABLE "public"."season_lobby_settings" ADD CONSTRAINT "season_lobby_settings_season_id_fkey" FOREIGN KEY("season_id") REFERENCES "public"."season"("id");