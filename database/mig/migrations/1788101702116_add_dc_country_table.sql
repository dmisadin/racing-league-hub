-- MIGRONDI:NAME=1788101702116_add_dc_country_table.sql
-- MIGRONDI:TIMESTAMP=1788101702116
-- ---------- MIGRONDI:UP ----------
-- Add your SQL migration code below. You can delete this line but do not delete the comments above.

CREATE TABLE "public"."dc_country" (
    "id"            integer         PRIMARY KEY,
    "code_alpha_2"  char(2)         NOT NULL UNIQUE,
    "code_alpha_3"  char(3)         NOT NULL UNIQUE,
    "name"          varchar(100)    NOT NULL,
    "is_active"       boolean         NOT NULL
);

-- ---------- MIGRONDI:DOWN ----------
-- Add your SQL rollback code below. You can delete this line but do not delete the comment above.


