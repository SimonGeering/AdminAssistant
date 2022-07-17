#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["AdminAssistant.HealthMonitor/AdminAssistant.HealthMonitor.csproj", "AdminAssistant.HealthMonitor/"]
RUN dotnet restore "AdminAssistant.HealthMonitor/AdminAssistant.HealthMonitor.csproj"
COPY . .
WORKDIR "/src/AdminAssistant.HealthMonitor"
RUN dotnet build "AdminAssistant.HealthMonitor.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "AdminAssistant.HealthMonitor.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "AdminAssistant.HealthMonitor.dll"]
