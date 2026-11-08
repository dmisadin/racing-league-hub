# RacingLeagueHub --- Claude Code Instructions

## 1. Project overview

RacingLeagueHub is an F1 esports league-racing platform --- conceptually
similar to HLTV.org, but for F1 esports.

The application is intended to support: - Public historical racing data,
results, and statistics. - Custom leagues and league
membership/permissions. - Seasons, divisions, Grand Prix events, tracks,
and track layouts. - Teams, game teams, drivers, and player/team
relationships. - Event schedules, race results, and upcoming events. -
User authentication and league administration.

The main repository is named `racing-league-hub`.

**Important:** Treat the existing source code and database schema as the
source of truth. This document records architectural preferences and
decisions, but it may not describe every implementation detail or the
latest state of every feature. Inspect relevant code before making
changes.

## 2. Technology stack

-   Backend: .NET 10, ASP.NET Core Web API, C#.
-   Frontend: Angular 21, standalone components, signals.
-   Database: PostgreSQL.
-   ORM/database access: Entity Framework Core is used in Infrastructure
    for persistence.
-   Database naming: snake_case, configured with
    `UseSnakeCaseNamingConvention()`.
-   Database migrations are done with **migrondi** project from GitHub
-   Object storage: AWS S3 through an application storage abstraction.
-   Frontend styling: Tailwind CSS and DaisyUI.
-   Frontend component approach: Angular standalone APIs; do not
    introduce Angular Material.

The solution has these main backend projects:

-   `RacingLeagueHub.Api`
-   `RacingLeagueHub.Application`
-   `RacingLeagueHub.Domain`
-   `RacingLeagueHub.Infrastructure`

The Angular application is in `RacingLeagueHub.Web`.

## 3. Architecture and dependency boundaries

Follow the existing Clean Architecture structure.

General responsibilities:

### Domain

-   Contains domain entities and domain-level rules.
-   Must not depend on Infrastructure, ASP.NET Core, or persistence
    implementation details.
-   Avoid putting API DTOs or database-specific mapping logic here.

### Application

-   Contains application services, use cases, application-facing
    interfaces, and DTOs.
-   Owns service contracts and DTO definitions for its features.
-   Coordinates application operations through abstractions.
-   Must not directly access EF Core, `DbContext`, `IQueryable`, or
    Infrastructure implementations.

**Critical preference:** Do not put `DbContext`, EF Core queries,
`IQueryable`, or calls such as `Query()` in Application services.
Application services should not know how database queries are
implemented.

### Infrastructure

-   Implements persistence and other external integrations.
-   Owns EF Core `DbContext` usage, database queries, and
    persistence-specific implementations.
-   Implements interfaces declared by Application.
-   Uses the existing mapping and persistence conventions (Query/Commands).

### API

-   Contains controllers, HTTP concerns, authentication/authorization
    integration, and API configuration.
-   Controllers should remain thin and delegate application work to
    services.
-   Follow existing controller base classes and response conventions.

### Dependency direction

Keep dependencies pointing inward through abstractions. Do not introduce
dependencies from Domain or Application toward Infrastructure.

## 4. Application services, queries, and DTO mapping

Follow these conventions unless the existing feature being changed
clearly establishes a newer pattern.

-   Feature services belong in Application.
-   DTOs and service interfaces should be owned by the relevant
    Application feature.
-   Database query implementations belong in Infrastructure.
-   Prefer explicit query interfaces, such as `ITeamQueries`,
    implemented in Infrastructure (for example,
    `Infrastructure.Persistence.Teams.TeamQueries`).
-   Infrastructure query implementations may use `RacingContext` and EF
    Core.
-   Use `IDtoMapper<TEntity, TDto>` implementations for entity-to-DTO
    mapping.
-   Do not add static `ToDto()` methods to entities or DTOs.
-   Do not move EF Core queries into application services merely to
    reduce the number of classes.
-   Avoid reviving the old repository pattern where a focused query
    abstraction is more appropriate.

A typical flow is:

1.  API controller calls an Application service.
2.  Application service coordinates the use case.
3.  Application service calls an Application-owned query abstraction.
4.  Infrastructure implements that abstraction and performs the database
    query.
5.  Results are mapped using the appropriate DTO mapper.
6.  Application service returns DTOs/results to the controller.

Do not force this exact flow onto every operation if an established
feature uses a different appropriate abstraction. Preserve consistency
with neighboring code.

## 5. Pagination

The preferred paged result shape is:

`PagedResult<T>(Items, Page, PageSize, TotalCount)`

-   Default page size: 10.
-   The result exposes `HasMore`.
-   Preserve the existing paging conventions when adding endpoints.
-   Avoid returning unbounded collections for endpoints intended to
    serve lists.

Inspect the existing implementation before changing pagination
contracts.

## 6. Domain model and naming context

The domain includes concepts such as:

-   League
-   Season
-   Division
-   GrandPrix
-   Track
-   TrackLayout
-   GameTeam
-   Team
-   Driver
-   LeagueUser
-   User
-   RefreshToken
-   UserRecoveryCode
-   Resource

Important relationships and design intentions:

-   A League has Seasons.
-   A Division belongs to a League and is intended to persist across
    that League's seasons.
-   Divisions represent competitive tiers for drivers/results within a
    league.
-   A Grand Prix belongs to a Season.
-   Tracks can have multiple TrackLayouts.
-   Teams and GameTeams are distinct concepts; do not conflate them.
-   League membership and permissions are represented by `LeagueUser`.
-   Global user administration is distinct from league-level
    permissions.

### League permissions

`LeagueUser` contains league-level permission flags such as: -
`IsOwner` - `IsAdmin` - `IsEditor` - `IsSteward`

`User.IsAdmin` is a separate global administrator concept. Do not treat
it as equivalent to `LeagueUser.IsAdmin`.

When creating a league, the creator should become its owner and receive
the appropriate initial league permissions, following the existing
creation flow.

## 7. IDs, slugs, and API routes

-   The database ID convention is `int` rather than `long` for the
    application's main entities. The database was being migrated from
    `bigint`/`long` to `int`; inspect the current schema and code before
    introducing or changing ID types.
-   Be careful with foreign-key type compatibility.
-   Do not reintroduce `long` IDs without a concrete reason and
    consistency with the referenced entity.

Slugs are used for public-facing resource URLs. Current intended
uniqueness scopes include:

-   League: `slug`
-   Season: `(league_id, slug)`
-   Grand Prix: `(season_id, slug)`
-   Division: `(league_id, slug)`

The API is moving toward slug-based nested routes, for example:

`/api/leagues/{leagueSlug}/...`

Prefer the existing route conventions in the feature being modified. Do
not casually replace slug-based routes with numeric-ID routes.

## 8. Database conventions

-   PostgreSQL is the database.
-   Table and column names use snake_case.
-   EF Core naming convention is configured with
    `UseSnakeCaseNamingConvention()`.
-   Database changes are managed primarily through manual SQL migration
    scripts under `database/mig`.
-   Do not assume EF Core migrations are the project's primary migration
    workflow.
-   Inspect existing SQL migration scripts before writing a new one.
-   Keep foreign-key types aligned with the referenced primary-key
    types.
-   Respect existing unique constraints and indexes.
-   Reference data tables use the `dc_` prefix where established.
-   Reference data is generally maintained through upserts; do not
    introduce destructive delete/recreate workflows without a clear
    requirement.
-   Preserve existing `is_active` conventions for reference data where
    applicable.
-   Country identifiers/codes and related foreign keys must follow the
    current schema; inspect the actual types before editing.

When changing the schema: 1. Inspect the current entity configuration
and SQL schema. 2. Check all dependent foreign keys and queries. 3. Add
a migration script consistent with the repository's existing format. 4.
Consider existing data and migration order. 5. Update relevant
application and API code.

Do not assume the database is empty.

## 9. Entity mapping and persistence configuration

-   Existing entity mapping classes use the `DbMapBase<T>` convention.
-   Follow the established mapping configuration style.
-   Keep database-specific configuration in Infrastructure.
-   Avoid duplicating configuration already provided by shared base
    mappings.
-   Check how related entities and navigation properties are configured
    before adding relationships.

## 10. Resources and file storage

Resources such as league logos are stored through the storage
abstraction backed by AWS S3.

The intended public resource URL pattern is:

`{base}/uploads/{StorageId}.{Extension}`

-   Use the existing storage service abstraction.
-   Do not hardcode S3 URLs or credentials into application code.
-   Keep storage implementation details out of Domain and Application.
-   Inspect existing resource/DTO mapping before changing logo or
    resource handling.
-   Be careful with null storage dependencies in DTO mappers.

## 11. Authentication and authorization

The application uses JWT-based authentication with refresh tokens.

Important established conventions:

-   JWT access tokens are used for API authorization.
-   The JWT subject (`sub`) contains the user's ID as a string; the
    current ID type is intended to be `int`.
-   JWT inbound claim mapping is disabled (`MapInboundClaims = false`).
-   The application uses explicit `sub` and `role` claims.
-   JWT signing uses HS256.
-   Clock skew is configured as zero.
-   Refresh tokens are delivered using an HttpOnly, Secure cookie.
-   The refresh cookie path is scoped to `/api/auth`.
-   Do not store access or refresh tokens in browser `localStorage`.
-   The frontend keeps access-token state in memory and refreshes the
    session through the refresh endpoint.
-   The Angular HTTP interceptor sends credentials and the bearer access
    token as appropriate.
-   Authentication initialization uses Angular's application initializer
    pattern.
-   TOTP and recovery-code support are part of the authentication
    design.
-   Google OIDC is supported/planned through the existing authentication
    integration; inspect the current implementation before extending it.

Authorization policies are used for protected endpoints, for example:

`[Authorize(Policy = AppPolicies.SuperAdmin)]`

When changing authentication: - Preserve the separation between global
roles and league permissions. - Do not expose tokens in API responses
unnecessarily. - Do not weaken cookie security settings for
convenience. - Do not put secrets, signing keys, or provider credentials
in source control. - Check both backend claim handling and frontend
refresh/interceptor behavior.

## 12. Angular frontend conventions

The frontend uses Angular 21.

Preferences:

-   Use standalone components and modern Angular APIs.
-   Prefer signals for local/reactive state where appropriate.
-   Follow the existing zoneless-friendly approach.
-   Use `withComponentInputBinding` where the router setup already
    supports it.
-   Use `[class]` rather than introducing `ngClass` for simple class
    binding.
-   Do not introduce Angular Material.
-   Use Tailwind CSS and DaisyUI consistently with existing components.
-   Reuse existing shared controls and form abstractions.
-   Avoid introducing a new state-management library or abstraction
    without a clear need.
-   Avoid Angular `resource()` for authenticated API requests if it
    bypasses the configured authentication interceptor.
-   Preserve existing API client, interceptor, and authentication
    patterns.

### Forms and controls

A shared `BaseFormControl<T>` abstraction and `ng-select` CVA wrapper
are part of the frontend conventions.

Before creating a new form control: - Check for an existing shared
control. - Follow the project's ControlValueAccessor conventions. -
Preserve validation and disabled-state behavior. - Avoid duplicating
form-control integration logic.

### UI preferences

-   DaisyUI components, using Tailwind 4 but prioritize DaisyUI styling
-   Mobile-first
-   Use only multiples of 2 for Tailwind classes (1, 2, 4, 8, 16, ...)
-   No Angular Material.
-   No beveled/button-emboss styling.
-   Avoid unnecessary rounded badge styling.
-   Prefer clean, functional layouts.
-   Keep desktop/admin workflows usable while considering responsive and
    mobile/PWA behavior.
-   Existing font preferences include Noto Sans, Sora, Inter, and
    JetBrains Mono. Follow the fonts already configured in the
    application rather than adding duplicates.

## 13. API and controller conventions

-   Controllers should be thin.
-   Use the existing API controller base class and shared current-user
    access mechanisms.
-   Inspect the current `ApiController` implementation before changing
    how the current user ID is obtained.
-   Keep HTTP-specific concerns in the API project.
-   Put business/use-case orchestration in Application.
