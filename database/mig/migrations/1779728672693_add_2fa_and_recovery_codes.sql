-- MIGRONDI:NAME=1779728672693_add_2fa_and_recovery_codes.sql
-- MIGRONDI:TIMESTAMP=1779728672693
-- ---------- MIGRONDI:UP ----------
-- Add your SQL migration code below. You can delete this line but do not delete the comments above.

ALTER TABLE "public"."user" 
ADD COLUMN two_factor_enabled   boolean NOT NULL DEFAULT false,
ADD COLUMN two_factor_secret    text NULL,
ADD COLUMN two_factor_enabled_at    timestamptz NULL,
ADD COLUMN last_totp_time_step_used bigint NULL;

CREATE TABLE user_recovery_code (
    id bigint GENERATED ALWAYS AS IDENTITY PRIMARY KEY,
    user_id bigint NOT NULL REFERENCES "public"."user"("id") ON DELETE CASCADE,
    code_hash text NOT NULL,
    created_at timestamptz NOT NULL DEFAULT now(),
    used_at timestamptz NULL
);

CREATE INDEX user_recovery_code_user_id_idx
ON user_recovery_code(user_id);

CREATE UNIQUE INDEX user_recovery_code_user_id_code_hash_key
ON user_recovery_code(user_id, code_hash);

-- ---------- MIGRONDI:DOWN ----------
-- Add your SQL rollback code below. You can delete this line but do not delete the comment above.


