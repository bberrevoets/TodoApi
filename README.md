# TodoApi

A .NET 10 minimal API project implementing a Todo REST API with full OpenTelemetry observability.

**Author:** Bert Berrevoets
**Company:** Berrevoets Systems
**Contact:** bert@berrevoets.net

## Features

- CRUD operations for Todo items via minimal API endpoints
- DTO pattern to prevent leaking internal entity fields
- OpenTelemetry integration for traces, metrics, and logs (OTLP export)
- Dice roll demo endpoint showcasing custom metrics and tracing
- Pre-built Grafana dashboard for observability

## Getting Started

### Prerequisites

- [.NET 10 SDK](https://dotnet.microsoft.com/download)
- (Optional) An OpenTelemetry Collector with Prometheus, Tempo, and Loki backends for full observability

### Build & Run

```bash
# Build
dotnet build TodoApi/TodoApi.csproj

# Run
dotnet run --project TodoApi/TodoApi.csproj

# Run with OTLP export (launch profile)
dotnet run --project TodoApi/TodoApi.csproj --launch-profile https
```

The API will be available at `https://localhost:8080`.

## API Endpoints

| Method | Route                      | Description           |
|--------|----------------------------|-----------------------|
| GET    | `/todoitems/`              | List all todos        |
| GET    | `/todoitems/complete`      | List completed todos  |
| GET    | `/todoitems/{id}`          | Get a single todo     |
| POST   | `/todoitems/`              | Create a todo         |
| PUT    | `/todoitems/{id}`          | Update a todo         |
| DELETE | `/todoitems/{id}`          | Delete a todo         |
| GET    | `/rolldice/{player?}`      | Dice roll demo        |

## Observability

The application exports telemetry data via OTLP to an OpenTelemetry Collector. A pre-built Grafana dashboard is included at `grafana/todo-api-dashboard.json`.

### Dashboard Sections

- **Dice Rolls** — Roll rate by player, total rolls, value distribution
- **HTTP Traffic** — Request rate, p95/p50 duration, status codes, error rate
- **Traces** — Recent distributed traces from Tempo
- **Logs** — Application logs from Loki

### Data Source Label Mapping

| Data Source | Label              | Value      |
|-------------|--------------------|------------|
| Prometheus  | `exported_job`     | `todo-api` |
| Tempo       | `resource.service.name` | `todo-api` |
| Loki        | `service_name`     | `todo-api` |

> **Note:** The OpenTelemetry Collector maps `service.name` to `exported_job` in Prometheus, while Loki and Tempo use their own label conventions.

## Project Structure

```
TodoApi/
  Program.cs          # App entry point, OTel config, API endpoints
  Todo.cs             # Entity model (includes Secret field)
  TodoItemDTO.cs      # Data transfer object (excludes Secret)
  TodoDb.cs           # EF Core DbContext (in-memory)
grafana/
  todo-api-dashboard.json  # Grafana observability dashboard
```

## License

This project is for learning purposes.
