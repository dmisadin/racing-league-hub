#!/usr/bin/env bash

# Copy this file to set-secrets.local.sh and replace the values.
# Do NOT commit set-secrets.local.sh.
#
# Run with:
# source ./scripts/set-secrets.local.sh

export Jwt__Secret="REPLACE_WITH_DEV_JWT_SECRET_AT_LEAST_32_CHARS"

export Authentication__Google__ClientId="REPLACE_WITH_GOOGLE_CLIENT_ID"
export Authentication__Google__ClientSecret="REPLACE_WITH_GOOGLE_CLIENT_SECRET"

echo "Secrets loaded into current shell."