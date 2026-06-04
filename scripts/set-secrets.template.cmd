@echo off

REM Copy this file to set-secrets.local.cmd and replace the values.
REM Do NOT commit set-secrets.local.cmd.

setx Jwt__Secret "REPLACE_WITH_DEV_JWT_SECRET_AT_LEAST_32_CHARS"

setx Authentication__Google__ClientId "REPLACE_WITH_GOOGLE_CLIENT_ID"
setx Authentication__Google__ClientSecret "REPLACE_WITH_GOOGLE_CLIENT_SECRET"

echo Secrets saved to Windows user environment variables.
echo Restart Visual Studio / VS Code / terminal before running the app.

pause