# CLAUDE.md

This file provides guidance to Claude Code (claude.ai/code) when working with code in this repository.

## Build & Run Commands

- **Build:** `dotnet build TodoApi/TodoApi.csproj`
- **Run:** `dotnet run --project TodoApi/TodoApi.csproj`
- **Run (with launch profile):** `dotnet run --project TodoApi/TodoApi.csproj --launch-profile https`
- **Solution build:** `dotnet build TodoApi.slnx`

There are no tests in this project currently.

## Architecture

This is a .NET 10 minimal API project — a Todo REST API with OpenTelemetry observability.

### Key files

- **`TodoApi/Program.cs`** — Application entry point. Configures OpenTelemetry (tracing, metrics, logging with console exporter), registers the EF Core in-memory database, and defines all API endpoints using minimal API route groups.
- **`TodoApi/Todo.cs`** — Entity model with a `Secret` field that is intentionally excluded from the DTO.
- **`TodoApi/TodoItemDTO.cs`** — Data transfer object that maps from `Todo`, hiding the `Secret` property.
- **`TodoApi/TodoDb.cs`** — EF Core `DbContext` with a single `Todos` DbSet, using in-memory database provider.

### API Endpoints

All todo endpoints are grouped under `/todoitems`:
- `GET /todoitems/` — List all todos
- `GET /todoitems/complete` — List completed todos
- `GET /todoitems/{id}` — Get single todo
- `POST /todoitems/` — Create todo
- `PUT /todoitems/{id}` — Update todo
- `DELETE /todoitems/{id}` — Delete todo
- `GET /rolldice/{player?}` — Dice roll demo endpoint (OpenTelemetry showcase)

### Design Patterns

- **DTO pattern:** `Todo` entity has a `Secret` field not exposed through the API. All responses use `TodoItemDTO` to prevent data leakage.
- **Minimal API with route groups:** Endpoints are defined as static methods mapped via `MapGroup("/todoitems")`.
- **In-memory database:** Uses EF Core's `InMemoryDatabase` provider — data does not persist across restarts.
- **OpenTelemetry:** Configured for traces, metrics, and logs with console exporters. OTLP endpoint configured in launch profile.

### Observability Stack

The project includes a Grafana dashboard for full observability:

- **`grafana/todo-api-dashboard.json`** — Pre-built Grafana dashboard ("Todo API - Observability") with panels for metrics, traces, and logs.

The dashboard expects three Grafana data sources:
- **Prometheus** — Metrics (label: `exported_job="todo-api"`)
- **Tempo** — Distributed traces (resource attribute: `service.name="todo-api"`)
- **Loki** — Logs (label: `service_name="todo-api"`)

> **Note:** In Prometheus, the OTel Collector maps the OpenTelemetry `service.name` to the `exported_job` label (not `service_name`). Loki and Tempo use `service_name` and `resource.service.name` respectively.

### Runtime Configuration

- Default HTTPS URL: `https://localhost:8080` (configured in `launchSettings.json`)
- OTLP exporter endpoint: `http://192.168.180.3:4318` (via launch profile environment variables)
