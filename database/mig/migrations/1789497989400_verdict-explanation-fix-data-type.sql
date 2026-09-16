-- MIGRONDI:NAME=1789497989400_verdict-explanation-fix-data-type.sql
-- MIGRONDI:TIMESTAMP=1789497989400
-- ---------- MIGRONDI:UP ----------
-- Add your SQL migration code below. You can delete this line but do not delete the comments above.

ALTER TABLE public.verdict
    ALTER COLUMN explanation TYPE TEXT;

-- ---------- MIGRONDI:DOWN ----------
-- Add your SQL rollback code below. You can delete this line but do not delete the comment above.


