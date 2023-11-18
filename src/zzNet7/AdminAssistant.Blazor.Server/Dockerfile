#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.
#
# Docker Reference Docs
# https://docs.docker.com/engine/reference/builder/
#
FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["AdminAssistant.Blazor.Server/AdminAssistant.Blazor.Server.csproj", "AdminAssistant.Blazor.Server/"]
COPY ["AdminAssistant.Blazor.Client/AdminAssistant.Blazor.Client.csproj", "AdminAssistant.Blazor.Client/"]
COPY ["AdminAssistant.DomainModel/AdminAssistant.DomainModel.csproj", "AdminAssistant.DomainModel/"]
COPY ["AdminAssistant/AdminAssistant.csproj", "AdminAssistant/"]
COPY ["AdminAssistant.Infra/AdminAssistant.Infra.csproj", "AdminAssistant.Infra/"]
COPY ["AdminAssistant.UI/AdminAssistant.UI.csproj", "AdminAssistant.UI/"]
RUN dotnet restore "AdminAssistant.Blazor.Server/AdminAssistant.Blazor.Server.csproj"
COPY . .
WORKDIR "/src/AdminAssistant.Blazor.Server"
RUN dotnet build "AdminAssistant.Blazor.Server.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "AdminAssistant.Blazor.Server.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "AdminAssistant.Blazor.Server.dll"]
