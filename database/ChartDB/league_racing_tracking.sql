CREATE SCHEMA IF NOT EXISTS "public";

CREATE TABLE "public"."verdict" (
    "id" bigint NOT NULL,
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
CREATE INDEX "idx_verdict_incident_id" ON "public"."verdict" ("incident_id");

CREATE TABLE "public"."grand_prix_result" (
    "id" bigserial NOT NULL,
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
CREATE INDEX "idx_grand_prix_result_grand_prix_driver_id" ON "public"."grand_prix_result" ("grand_prix_driver_id");

CREATE TABLE "public"."league_user" (
    "id" bigint NOT NULL,
    "league_id" bigint NOT NULL,
    "user_id" bigint NOT NULL,
    -- owner, editor, steward
    "role" bigint NOT NULL,
    PRIMARY KEY ("id")
);
COMMENT ON COLUMN "public"."league_user"."role" IS 'owner, editor, steward';
-- Indexes
CREATE UNIQUE INDEX "uq_league_user_league_id_user_id" ON "public"."league_user" ("league_id", "user_id");

CREATE TABLE "public"."driver" (
    "id" bigint NOT NULL,
    "nickname" varchar(64) NOT NULL,
    "first_name" varchar(64),
    "last_name" varchar(64),
    "country" varchar(3),
    "slug" varchar(64) NOT NULL,
    PRIMARY KEY ("id")
);
-- Indexes
CREATE UNIQUE INDEX "uq_driver_nickname" ON "public"."driver" ("nickname");
CREATE UNIQUE INDEX "uq_driver_slug" ON "public"."driver" ("slug");

CREATE TABLE "public"."season_points" (
    "id" bigserial NOT NULL,
    "season_id" bigint NOT NULL,
    "session_type" bigint NOT NULL,
    "position" smallint NOT NULL,
    "points" smallint NOT NULL,
    PRIMARY KEY ("id")
);
-- Indexes
CREATE INDEX "idx_season_points_season_id" ON "public"."season_points" ("season_id");

CREATE TABLE "public"."track" (
    "id" bigint NOT NULL,
    "name" varchar(128) NOT NULL,
    "country" bigint NOT NULL,
    "city" varchar(64) NOT NULL,
    "elevation" numeric(5, 1),
    "short_name" varchar(64),
    PRIMARY KEY ("id")
);

CREATE TABLE "public"."game_team" (
    "id" bigint NOT NULL,
    "game" smallint NOT NULL,
    "team_id" bigint NOT NULL,
    "display_name" varchar(128),
    "color" varchar(32),
    "logo_resource_id" bigint,
    -- ID reference to data that game sends through telemetry
    "telemetry_id" smallint NOT NULL,
    PRIMARY KEY ("id")
);
COMMENT ON COLUMN "public"."game_team"."telemetry_id" IS 'ID reference to data that game sends through telemetry';
-- Indexes
CREATE UNIQUE INDEX "uq_game_team_game" ON "public"."game_team" ("game", "team_id");

CREATE TABLE "public"."incident_driver" (
    "id" bigint NOT NULL,
    "incident_id" bigint NOT NULL,
    "driver_id" bigint NOT NULL,
    PRIMARY KEY ("id")
);
-- Indexes
CREATE UNIQUE INDEX "uq_incident_driver_incident_id_driver_id" ON "public"."incident_driver" ("incident_id", "driver_id");

CREATE TABLE "public"."incident" (
    "id" bigint NOT NULL,
    -- submitted by
    "user_id" bigint NOT NULL,
    "grand_prix_id" bigint NOT NULL,
    "session:_type" smallint NOT NULL,
    "title" varchar(128) NOT NULL,
    "description" text NOT NULL,
    "evidence" text NOT NULL,
    "created_at" timestamptz NOT NULL,
    PRIMARY KEY ("id")
);
COMMENT ON COLUMN "public"."incident"."user_id" IS 'submitted by';
-- Indexes
CREATE INDEX "idx_incident_grand_prix_id" ON "public"."incident" ("grand_prix_id");
CREATE INDEX "idx_incident_user_id" ON "public"."incident" ("user_id");

CREATE TABLE "public"."grand_prix" (
    "id" bigserial NOT NULL,
    "track_layout_id" bigint NOT NULL,
    "season_id" bigint NOT NULL,
    "name" varchar(100) NOT NULL,
    "starting_at" timestamptz NOT NULL,
    "vod_url" text,
    "slug" varchar(64) NOT NULL,
    PRIMARY KEY ("id")
);
-- Indexes
CREATE INDEX "idx_grand_prix_season_id" ON "public"."grand_prix" ("season_id");
CREATE INDEX "idx_grand_prix_track_layout_id" ON "public"."grand_prix" ("track_layout_id");
CREATE UNIQUE INDEX "uq_grand_prix_season_id_slug" ON "public"."grand_prix" ("season_id", "slug");

CREATE TABLE "public"."user" (
    "id" bigint NOT NULL,
    "username" varchar(32) NOT NULL,
    "email" varchar(64) NOT NULL,
    "password" text NOT NULL,
    "is_admin" boolean NOT NULL,
    "driver_id" bigint,
    PRIMARY KEY ("id")
);
-- Indexes
CREATE INDEX "idx_user_driver_id" ON "public"."user" ("driver_id");

CREATE TABLE "public"."track_layout_game" (
    "id" bigint NOT NULL,
    "track_layout_id" bigint NOT NULL,
    "game" smallint NOT NULL,
    PRIMARY KEY ("id")
);
-- Indexes
CREATE UNIQUE INDEX "uq_track_layout_game_track_layout_id_game" ON "public"."track_layout_game" ("track_layout_id", "game");

CREATE TABLE "public"."team" (
    "id" bigint NOT NULL,
    "name" varchar(100) NOT NULL,
    "color" varchar(16),
    PRIMARY KEY ("id")
);

CREATE TABLE "public"."season" (
    "id" bigserial NOT NULL,
    "league_id" bigint NOT NULL,
    "name" varchar(100) NOT NULL,
    "platform" smallint NOT NULL,
    "game" smallint NOT NULL,
    "lap_percentage_required" smallint NOT NULL DEFAULT 90,
    "slug" bigint NOT NULL,
    "logo_resource_id" bigint,
    PRIMARY KEY ("id")
);
-- Indexes
CREATE INDEX "idx_grand_prix_league_id" ON "public"."season" ("league_id");
CREATE UNIQUE INDEX "uq_season_league_id_slug" ON "public"."season" ("league_id", "slug");

CREATE TABLE "public"."league" (
    "id" bigserial NOT NULL,
    "region" smallint NOT NULL,
    "name" varchar(500) NOT NULL,
    "description" text,
    -- IANA https://www.iana.org/time-zones
    "timezone" text NOT NULL,
    "slug" varchar(64) NOT NULL,
    "logo_resource_id" bigint,
    PRIMARY KEY ("id")
);
COMMENT ON COLUMN "public"."league"."timezone" IS 'IANA https://www.iana.org/time-zones';
-- Indexes
CREATE UNIQUE INDEX "uq_league_slug" ON "public"."league" ("slug");

CREATE TABLE "public"."grand_prix_driver" (
    "id" bigserial NOT NULL,
    "grand_prix_id" bigint NOT NULL,
    "driver_id" bigint NOT NULL,
    "team_id" bigint NOT NULL,
    "is_reserve" boolean NOT NULL,
    PRIMARY KEY ("id")
);
-- Indexes
CREATE INDEX "idx_grand_prix_driver_driver_id" ON "public"."grand_prix_driver" ("driver_id");
CREATE INDEX "idx_grand_prix_driver_grand_prix_id" ON "public"."grand_prix_driver" ("grand_prix_id");
CREATE INDEX "idx_grand_prix_driver_team_id" ON "public"."grand_prix_driver" ("team_id");

CREATE TABLE "public"."season_driver" (
    "id" bigint NOT NULL,
    "season_id" bigint NOT NULL,
    "team_id" bigint NOT NULL,
    "driver_id" bigint NOT NULL,
    "racing_number" smallint,
    "penalty_points" bigint NOT NULL,
    PRIMARY KEY ("id")
);
-- Indexes
CREATE UNIQUE INDEX "uq_season_driver_season_id_driver_id_team_id" ON "public"."season_driver" ("season_id", "driver_id", "team_id");

CREATE TABLE "public"."resource" (
    "id" bigint NOT NULL,
    "url" text NOT NULL,
    "file_name" text NOT NULL,
    "extension" varchar(10) NOT NULL,
    "mime_type" varchar(100) NOT NULL,
    "size_in_bytes" bigint NOT NULL,
    "created_at" timestamptz NOT NULL,
    "is_thumbnail" boolean,
    PRIMARY KEY ("id")
);

CREATE TABLE "public"."track_layout" (
    "id" bigint NOT NULL,
    "track_id" bigint NOT NULL,
    "name" varchar(128) NOT NULL,
    "pit_stop_duration" smallint,
    "corner_total" smallint NOT NULL,
    "corners_left" smallint NOT NULL,
    "laps_grand_prix" smallint NOT NULL,
    "map_image_resource_id" bigint,
    "cover_image_resource_id" bigint,
    PRIMARY KEY ("id")
);
-- Indexes
CREATE INDEX "idx_track_layout_track_id" ON "public"."track_layout" ("track_id");

-- Foreign key constraints
-- Schema: public
ALTER TABLE "public"."grand_prix_driver" ADD CONSTRAINT "fk_grand_prix_driver_driver_id_driver_id" FOREIGN KEY("driver_id") REFERENCES "public"."driver"("id");
ALTER TABLE "public"."user" ADD CONSTRAINT "fk_user_driver_id_driver_id" FOREIGN KEY("driver_id") REFERENCES "public"."driver"("id");
ALTER TABLE "public"."season_driver" ADD CONSTRAINT "fk_season_driver_driver_id_driver_id" FOREIGN KEY("driver_id") REFERENCES "public"."driver"("id");
ALTER TABLE "public"."resource" ADD CONSTRAINT "fk_resource_id_game_team_logo_resource_id" FOREIGN KEY("id") REFERENCES "public"."game_team"("logo_resource_id");
ALTER TABLE "public"."grand_prix_result" ADD CONSTRAINT "fk_grand_prix_result_grand_prix_driver_id_grand_prix_driver_" FOREIGN KEY("grand_prix_driver_id") REFERENCES "public"."grand_prix_driver"("id");
ALTER TABLE "public"."grand_prix_driver" ADD CONSTRAINT "fk_grand_prix_driver_grand_prix_id_grand_prix_id" FOREIGN KEY("grand_prix_id") REFERENCES "public"."grand_prix"("id");
ALTER TABLE "public"."incident_driver" ADD CONSTRAINT "fk_incident_driver_incident_id_incident_id" FOREIGN KEY("incident_id") REFERENCES "public"."incident"("id");
ALTER TABLE "public"."incident" ADD CONSTRAINT "fk_incident_grand_prix_id_grand_prix_id" FOREIGN KEY("grand_prix_id") REFERENCES "public"."grand_prix"("id");
ALTER TABLE "public"."incident" ADD CONSTRAINT "fk_incident_id_driver_id" FOREIGN KEY("id") REFERENCES "public"."driver"("id");
ALTER TABLE "public"."season" ADD CONSTRAINT "fk_season_league_id_league_id" FOREIGN KEY("league_id") REFERENCES "public"."league"("id");
ALTER TABLE "public"."league_user" ADD CONSTRAINT "fk_league_user_league_id_league_id" FOREIGN KEY("league_id") REFERENCES "public"."league"("id");
ALTER TABLE "public"."league" ADD CONSTRAINT "fk_league_logo_resource_id_resource_id" FOREIGN KEY("logo_resource_id") REFERENCES "public"."resource"("id");
ALTER TABLE "public"."track_layout" ADD CONSTRAINT "fk_track_layout_map_image_resource_id_resource_id" FOREIGN KEY("map_image_resource_id") REFERENCES "public"."resource"("id");
ALTER TABLE "public"."track_layout" ADD CONSTRAINT "fk_track_layout_cover_image_resource_id_resource_id" FOREIGN KEY("cover_image_resource_id") REFERENCES "public"."resource"("id");
ALTER TABLE "public"."season_driver" ADD CONSTRAINT "fk_season_driver_season_id_season_id" FOREIGN KEY("season_id") REFERENCES "public"."season"("id");
ALTER TABLE "public"."grand_prix" ADD CONSTRAINT "fk_grand_prix_season_id_season_id" FOREIGN KEY("season_id") REFERENCES "public"."season"("id");
ALTER TABLE "public"."season_points" ADD CONSTRAINT "fk_season_points_season_id_season_id" FOREIGN KEY("season_id") REFERENCES "public"."season"("id");
ALTER TABLE "public"."resource" ADD CONSTRAINT "fk_resource_id_season_logo_resource_id" FOREIGN KEY("id") REFERENCES "public"."season"("logo_resource_id");
ALTER TABLE "public"."game_team" ADD CONSTRAINT "fk_game_team_team_id_team_id" FOREIGN KEY("team_id") REFERENCES "public"."team"("id");
ALTER TABLE "public"."grand_prix_driver" ADD CONSTRAINT "fk_grand_prix_driver_team_id_team_id" FOREIGN KEY("team_id") REFERENCES "public"."team"("id");
ALTER TABLE "public"."season_driver" ADD CONSTRAINT "fk_season_driver_team_id_team_id" FOREIGN KEY("team_id") REFERENCES "public"."team"("id");
ALTER TABLE "public"."track_layout" ADD CONSTRAINT "fk_track_layout_track_id_track_id" FOREIGN KEY("track_id") REFERENCES "public"."track"("id");
ALTER TABLE "public"."track_layout_game" ADD CONSTRAINT "fk_track_layout_game_track_layout_id_track_layout_id" FOREIGN KEY("track_layout_id") REFERENCES "public"."track_layout"("id");
ALTER TABLE "public"."grand_prix" ADD CONSTRAINT "fk_grand_prix_track_layout_id_track_layout_id" FOREIGN KEY("track_layout_id") REFERENCES "public"."track_layout"("id");
ALTER TABLE "public"."league_user" ADD CONSTRAINT "fk_league_user_user_id_user_id" FOREIGN KEY("user_id") REFERENCES "public"."user"("id");
ALTER TABLE "public"."incident" ADD CONSTRAINT "fk_incident_id_verdict_incident_id" FOREIGN KEY("id") REFERENCES "public"."verdict"("incident_id");