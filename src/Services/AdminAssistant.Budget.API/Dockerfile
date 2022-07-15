#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.
#
# Docker Reference Docs
# https://docs.docker.com/engine/reference/builder/
#
FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Services/AdminAssistant.Budget.API/AdminAssistant.Budget.API.csproj", "Services/AdminAssistant.Budget.API/"]
RUN dotnet restore "Services/AdminAssistant.Budget.API/AdminAssistant.Budget.API.csproj"
COPY . .
WORKDIR "/src/Services/AdminAssistant.Budget.API"
RUN dotnet build "AdminAssistant.Budget.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "AdminAssistant.Budget.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "AdminAssistant.Budget.API.dll"]
