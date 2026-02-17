# Changelog

All notable changes to this project will be documented in this file.

The format is based on [Keep a Changelog](https://keepachangelog.com/en/1.1.0/).

## [Unreleased]

### Added
- Grafana observability dashboard (`grafana/todo-api-dashboard.json`) with panels for:
  - Dice roll metrics (rate by player, total rolls, value distribution)
  - HTTP traffic metrics (request rate, p95/p50 duration, status codes, error rate)
  - Distributed traces from Tempo
  - Application logs from Loki
- Dashboard template variables for Prometheus, Tempo, Loki data sources and service selection
- Project documentation: updated README.md, CLAUDE.md, and added CHANGELOG.md

### Fixed
- Grafana dashboard Prometheus queries now use `exported_job` label instead of `service_name` to match OTel Collector label mapping
- Traces panel uses correct `table` type with `traceqlSearch` query for Tempo compatibility
- Logs panel includes required `editorMode` and `queryType` properties for Loki queries

## [0.1.0] - 2026-02-17

### Added
- Initial Todo REST API with .NET 10 minimal API
- CRUD endpoints for Todo items under `/todoitems`
- DTO pattern to hide internal `Secret` field from API responses
- EF Core in-memory database provider
- OpenTelemetry integration with OTLP export (traces, metrics, logs)
- Dice roll demo endpoint (`/rolldice/{player?}`) with custom metrics and tracing
- Solution file (`TodoApi.slnx`) with Documentation folder
