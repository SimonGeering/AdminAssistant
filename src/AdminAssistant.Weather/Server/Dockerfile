#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["AdminAssistant.Weather/Server/AdminAssistant.Weather.Server.csproj", "AdminAssistant.Weather/Server/"]
COPY ["AdminAssistant.Weather/Client/AdminAssistant.Weather.Client.csproj", "AdminAssistant.Weather/Client/"]
COPY ["AdminAssistant/AdminAssistant.csproj", "AdminAssistant/"]
RUN dotnet restore "AdminAssistant.Weather/Server/AdminAssistant.Weather.Server.csproj"
COPY . .
WORKDIR "/src/AdminAssistant.Weather/Server"
RUN dotnet build "AdminAssistant.Weather.Server.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "AdminAssistant.Weather.Server.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "AdminAssistant.Weather.Server.dll"]
