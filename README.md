# RacingLeagueHub

RacingLeagueHub is a platform for F1 esports league racing, in the spirit of HLTV.org but for sim racing leagues. It hosts public historical data (results, standings, statistics) and the tools leagues need to run themselves:

- **Leagues** with membership and per-league permissions (owner, admin, editor, steward)
- **Seasons** and **divisions** (competitive tiers that persist across a league's seasons)
- **Grand Prix** events, schedules and results
- **Tracks** and **track layouts**
- **Teams**, **game teams** and **drivers**
- **Accounts**: JWT authentication with refresh tokens, TOTP two-factor auth with recovery codes, and Google sign-in

## Tech stack

| Layer | Technology |
| --- | --- |
| Backend | .NET 10, ASP.NET Core Web API, C# |
| Persistence | Entity Framework Core 10 + Npgsql, `UseSnakeCaseNamingConvention()` |
| Database | PostgreSQL |
| Migrations | [Migrondi](https://github.com/AngelMunoz/Migrondi) (plain SQL migration scripts) |
| Object storage | AWS S3, behind an application storage abstraction |
| Auth | JWT (HS256) access tokens + HttpOnly refresh-token cookie, TOTP (Otp.NET), Google OIDC |
| Frontend | Angular 21 (standalone components, signals, zoneless-friendly) |
| Styling | Tailwind CSS 4 + DaisyUI 5, `ng-select` for select controls |

## Repository layout

```
racing-league-hub/
├── RacingLeagueHub.Api/             # Controllers, auth setup, HTTP concerns
├── RacingLeagueHub.Application/     # Use-case services, DTOs, query interfaces
├── RacingLeagueHub.Domain/          # Entities and domain rules
├── RacingLeagueHub.Infrastructure/  # EF Core (RacingContext), queries, S3 storage, auth providers
├── RacingLeagueHub.Web/             # Angular frontend
├── database/
│   ├── mig/                         # Migrondi config + SQL migrations
│   ├── dc_data/                     # Reference data (dc_* tables), e.g. countries
│   ├── seed/                        # Development seed data
├── scripts/                         # Templates for loading local secrets
└── RacingLeagueHub.sln / .slnx
```

### Backend architecture

The backend follows Clean Architecture, with dependencies pointing inward:

```
Api ──► Application ──► Domain
 │           ▲
 └──► Infrastructure
```

- **Domain** has no dependency on ASP.NET Core, EF Core or Infrastructure.
- **Application** owns services, DTOs and query interfaces (e.g. `ITeamQueries`). It never touches `DbContext`, `IQueryable` or EF Core directly.
- **Infrastructure** implements those interfaces with EF Core (e.g. `Infrastructure.Persistence.Teams.TeamQueries`). Entity mappings use `DbMapBase<T>`, and entity-to-DTO mapping uses `IDtoMapper<TEntity, TDto>`.
- **Api** controllers are thin and delegate to Application services.

A typical request: controller → Application service → query interface → Infrastructure query → DTO mapper → controller.

List endpoints return `PagedResult<T>(Items, Page, PageSize, TotalCount)` (default page size 10, exposes `HasMore`). Public routes are slug-based and nested, e.g. `/api/leagues/{leagueSlug}/...`.

## Getting started

### Prerequisites

- [.NET 10 SDK](https://dotnet.microsoft.com/download)
- [Node.js](https://nodejs.org/) with npm 10
- PostgreSQL
- [Migrondi](https://github.com/AngelMunoz/Migrondi) (see [Database migrations](#database-migrations))
- Optional: an AWS profile with access to the dev S3 bucket (needed for logo and resource uploads)

### 1. Create the database

```sql
CREATE DATABASE racing_league_hub;
```

### 2. Apply migrations and load data

```bash
cd database/mig
migrondi up
```

Then load the reference data and, if you want sample content, the dev seed:

```bash
psql -d racing_league_hub -f database/dc_data/dc_country.sql
psql -d racing_league_hub -f database/seed/dev_seed_leagues.sql
```

The seed script expects leagues with IDs 1, 3 and 4 to exist already (create them through the app first). It can be re-run safely.

### 3. Configure secrets

Secrets are never committed. Copy the template for your shell, fill in the values, and load it into the shell you run the API from:

```bash
# bash / zsh
cp scripts/set-secrets.template.sh scripts/set-secrets.local.sh
source ./scripts/set-secrets.local.sh
```

```bat
:: Windows cmd
copy scripts\set-secrets.template.cmd scripts\set-secrets.local.cmd
scripts\set-secrets.local.cmd
```

| Variable | Purpose |
| --- | --- |
| `Jwt__Secret` | JWT signing key (at least 32 characters) |
| `Authentication__Google__ClientId` | Google OIDC client ID |
| `Authentication__Google__ClientSecret` | Google OIDC client secret |

`*.local.*` files are gitignored. .NET user secrets are also enabled on the API project if you prefer them.

Other development settings (connection string, S3 bucket, CORS, JWT issuer/audience) are in [RacingLeagueHub.Api/appsettings.Development.json](RacingLeagueHub.Api/appsettings.Development.json). S3 uses the AWS profile named there (`racing-league-hub-dev-s3`).

### 4. Run the API

```bash
dotnet run --project RacingLeagueHub.Api
```

The API listens on `https://localhost:7000` (and `http://localhost:5035`). Swagger UI is at `https://localhost:7000/swagger`.

### 5. Run the frontend

```bash
cd RacingLeagueHub.Web
npm install
npm start
```

The app runs on `http://localhost:4200` and calls the API at `https://localhost:7000/api` (see `src/environments/environment.ts`).

Other scripts: `npm run build`, `npm run watch`, `npm test` (Vitest).

## Database migrations

Schema changes are written as plain SQL and applied with [Migrondi](https://github.com/AngelMunoz/Migrondi). EF Core migrations are **not** used. EF Core maps onto the schema these scripts create.

### Installing Migrondi

The Migrondi binaries in `database/mig` are gitignored, so install it yourself:

```bash
# as a .NET tool
dotnet tool install --global Migrondi
```

```powershell
# or the standalone binary on Windows
iwr https://raw.githubusercontent.com/AngelMunoz/Migrondi/vnext/migrondi_install.ps1 -UseBasicParsing | iex
```

```bash
# or the standalone binary on Linux/macOS
curl -fsSL https://raw.githubusercontent.com/AngelMunoz/Migrondi/vnext/migrondi_install.sh | bash
```

### Configuration

[database/mig/migrondi.json](database/mig/migrondi.json):

```json
{
  "connection": "Host=localhost;Port=5432;Database=racing_league_hub;UserId=postgres;Password=postgres;Include Error Detail=true;",
  "migrations": "./migrations",
  "tableName": "__migrondi_migrations",
  "driver": "postgres"
}
```

Migrondi records applied migrations in the `__migrondi_migrations` table. Change the connection string locally if your database differs. Don't commit credentials for shared environments.

### Commands

Run these from `database/mig`:

| Command | What it does |
| --- | --- |
| `migrondi list` | Show which migrations are applied and which are pending |
| `migrondi up` | Apply all pending migrations |
| `migrondi up <n>` | Apply the next `n` pending migrations |
| `migrondi down <n>` | Roll back the last `n` applied migrations |
| `migrondi new <name>` | Create a new timestamped migration file in `migrations/` |
| `migrondi status <file>` | Check whether one migration file has been applied |

### Writing a migration

`migrondi new add-something` creates `migrations/<timestamp>_add-something.sql`:

```sql
-- MIGRONDI:NAME=1789586889811_add-divisions-within-league.sql
-- MIGRONDI:TIMESTAMP=1789586889811
-- ---------- MIGRONDI:UP ----------
CREATE TABLE division (
    id        int         GENERATED ALWAYS AS IDENTITY PRIMARY KEY,
    league_id int         NOT NULL REFERENCES public.league(id),
    "name"    varchar(64) NOT NULL,
    slug      varchar(32) NOT NULL,
    tier      smallint    NOT NULL,
    CONSTRAINT division_league_id_slug_key UNIQUE (league_id, slug)
);

-- ---------- MIGRONDI:DOWN ----------
DROP TABLE division;
```

Keep the `MIGRONDI:` header comments as they are. Migrondi uses them to identify the file and split the UP and DOWN sections.

Conventions:

- Use **snake_case** for tables and columns.
- Primary keys are `int GENERATED ALWAYS AS IDENTITY`. Keep foreign-key types matching the referenced key (the schema was migrated from `bigint` to `int`).
- Add unique constraints for slug scopes: league `slug`, season `(league_id, slug)`, grand prix `(season_id, slug)`, division `(league_id, slug)`.
- Reference tables use the `dc_` prefix and have an `is_active` flag. Maintain them with upserts, not delete-and-recreate.
- Assume the database already holds data. Write migrations that keep existing rows intact (backfill before adding `NOT NULL` columns, and so on).
- Fill in the DOWN section where you can. Some existing migrations leave it empty, so `migrondi down` can't undo every migration.
- After a schema change, update the matching EF Core mapping (`DbMapBase<T>`) and entity in Infrastructure/Domain.