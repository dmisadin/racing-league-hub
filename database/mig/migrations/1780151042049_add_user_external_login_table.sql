-- MIGRONDI:NAME=1780151042049_add_user_external_login_table.sql
-- MIGRONDI:TIMESTAMP=1780151042049
-- ---------- MIGRONDI:UP ----------
-- Add your SQL migration code below. You can delete this line but do not delete the comments above.
CREATE TABLE user_external_login (
    id bigint GENERATED ALWAYS AS IDENTITY PRIMARY KEY,
    user_id bigint NOT NULL REFERENCES "user"(id) ON DELETE CASCADE,
    provider text NOT NULL,
    provider_user_id text NOT NULL,
    email text NULL,
    display_name text NULL,
    picture_url text NULL,
    created_at timestamptz NOT NULL DEFAULT now(),

    CONSTRAINT ux_user_external_login_provider_user
        UNIQUE (provider, provider_user_id)
);

CREATE INDEX user_external_logins_user_id_idx
ON user_external_login(user_id);

CREATE INDEX user_external_logins_email_idx
ON user_external_login(email);

-- ---------- MIGRONDI:DOWN ----------
-- Add your SQL rollback code below. You can delete this line but do not delete the comment above.


