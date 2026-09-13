-- MIGRONDI:NAME=1788102190217_add_dc_country_relations.sql
-- MIGRONDI:TIMESTAMP=1788102190217
-- ---------- MIGRONDI:UP ----------
-- Add your SQL migration code below. You can delete this line but do not delete the comments above.

ALTER TABLE public.driver
    DROP COLUMN country;

ALTER TABLE public.driver
    ADD COLUMN country_id integer;

ALTER TABLE public.driver
    ADD CONSTRAINT driver_country_id_fkey
    FOREIGN KEY (country_id)
    REFERENCES public.dc_country (id);


ALTER TABLE public.track
    DROP COLUMN country;

ALTER TABLE public.track
    ADD COLUMN country_id integer;

ALTER TABLE public.track
    ADD CONSTRAINT track_country_id_fkey
    FOREIGN KEY (country_id)
    REFERENCES public.dc_country (id);

-- ---------- MIGRONDI:DOWN ----------
-- Add your SQL rollback code below. You can delete this line but do not delete the comment above.


