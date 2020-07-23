---
layout: page
title: CLI Tools
permalink: /tools/
---
Assume starting from src folder.

## Entity framework CLI commands

EF migrations are kept in DAL to allow adding other Apps in future with a common DB.

``` dos
dotnet ef database update --context applicationdbcontext --project "./AdminAssistant.DAL/AdminAssistant.DAL.csproj" --startup-project "./AdminAssistant.Blazor/Server/AdminAssistant.Blazor.Server.csproj" --no-build

dotnet ef migrations add Initial --context applicationdbcontext --project "./AdminAssistant.DAL/AdminAssistant.DAL.csproj" --startup-project "./AdminAssistant.Blazor/Server/AdminAssistant.Blazor.Server.csproj" --output-dir "./EntityFramework/Migrations" --no-build

dotnet ef migrations list --context applicationdbcontext --project "./AdminAssistant.DAL/AdminAssistant.DAL.csproj" --startup-project "./AdminAssistant.Blazor/Server/AdminAssistant.Blazor.Server.csproj" --no-build

dotnet ef migrations remove --context applicationdbcontext --project "./AdminAssistant.DAL/AdminAssistant.DAL.csproj" --startup-project "./AdminAssistant.Blazor/Server/AdminAssistant.Blazor.Server.csproj" --no-build
```

## Install Global tools

``` dos
dotnet tool list --global
dotnet tool uninstall --global dotnet-ef
dotnet tool install dotnet-ef --global --version 3.1.6 --interactive
```

## Docs

``` dos
cd ..
cd docs
bundle exec jekyll serve --livereload
```

## NPM
