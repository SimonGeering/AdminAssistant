# CLI Tools

Notes on the use of various CLI tools utilised by the project.

Assume starting from src folder.

## Install Global tools

``` dos
dotnet tool list --global
dotnet tool uninstall --global dotnet-ef
dotnet tool install dotnet-ef --global --version 3.1.6 --interactive
dotnet tool update --global dotnet-ef --version 5.0.0-preview.8.20407.4 
```

## Entity framework CLI commands

EF migrations are kept in DAL to allow adding other Apps in future with a common DB.

### Update Database To Latest Migration

``` dos
dotnet ef database update --context applicationdbcontext --project "./AdminAssistant.Infra/AdminAssistant.Infra.csproj" --startup-project "./AdminAssistant.Blazor.Server/AdminAssistant.Blazor.Server.csproj" --no-build
```
### Add 'Initial' Database Migration 

``` dos
dotnet ef migrations add Initial --context applicationdbcontext --project "./AdminAssistant.Infra/AdminAssistant.Infra.csproj" --startup-project "./AdminAssistant.Blazor.Server/AdminAssistant.Blazor.Server.csproj" --output-dir "./DAL/EntityFramework/Migrations" --no-build
```

### List Database Migrations

``` dos
dotnet ef migrations list --context applicationdbcontext --project "./AdminAssistant.Infra/AdminAssistant.Infra.csproj" --startup-project "./AdminAssistant.Blazor.Server/AdminAssistant.Blazor.Server.csproj" --no-build
```

### Remove Latest Migration

``` dos
dotnet ef migrations remove --context applicationdbcontext --project "./AdminAssistant.Infra/AdminAssistant.Infra.csproj" --startup-project "./AdminAssistant.Blazor.Server/AdminAssistant.Blazor.Server.csproj" --no-build
```

## Docx Documentation Site 

Assumes DocFx is available in the 'PATH'

### Build Documentation

Compiles markdown documentation from `../docfx_project` and outputs to `../docs/` as html static site.

``` dos
docfx ../docfx_project/docfx.json --build
```

### Debug

Compiles markdown documentation from `../docfx_project` and hosts on local webpack dev server. 

``` dos
docfx ../docfx_project/docfx.json --serve
```

## Run Tests

### Full Test Suite - 

Standalone

``` dos
dotnet test ./AdminAssistant.Test/AdminAssistant.Test.csproj
```

With Watch

``` dos
dotnet watch test ./AdminAssistant.Test/AdminAssistant.Test.csproj
```

### Unit Tests Only

Standalone

``` dos
dotnet test ./AdminAssistant.Test/AdminAssistant.Test.csproj --filter Category=Unit 
```

With Watch

``` dos
dotnet watch test ./AdminAssistant.Test/AdminAssistant.Test.csproj --filter Category=Unit 
```
