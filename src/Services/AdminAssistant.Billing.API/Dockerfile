#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.
#
# Docker Reference Docs
# https://docs.docker.com/engine/reference/builder/
#
FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Services/AdminAssistant.Billing.API/AdminAssistant.Billing.API.csproj", "Services/AdminAssistant.Billing.API/"]
RUN dotnet restore "Services/AdminAssistant.Billing.API/AdminAssistant.Billing.API.csproj"
COPY . .
WORKDIR "/src/Services/AdminAssistant.Billing.API"
RUN dotnet build "AdminAssistant.Billing.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "AdminAssistant.Billing.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "AdminAssistant.Billing.API.dll"]
