#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["AdminAssistant.Gateway/AdminAssistant.Gateway.csproj", "AdminAssistant.Gateway/"]
RUN dotnet restore "AdminAssistant.Gateway/AdminAssistant.Gateway.csproj"
COPY . .
WORKDIR "/src/AdminAssistant.Gateway"
RUN dotnet build "AdminAssistant.Gateway.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "AdminAssistant.Gateway.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "AdminAssistant.Gateway.dll"]
