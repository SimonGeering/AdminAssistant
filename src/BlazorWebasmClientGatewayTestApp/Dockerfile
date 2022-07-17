#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["BlazorWebasmClientGatewayTestApp/BlazorWebasmClientGatewayTestApp.csproj", "BlazorWebasmClientGatewayTestApp/"]
RUN dotnet restore "BlazorWebasmClientGatewayTestApp/BlazorWebasmClientGatewayTestApp.csproj"
COPY . .
WORKDIR "/src/BlazorWebasmClientGatewayTestApp"
RUN dotnet build "BlazorWebasmClientGatewayTestApp.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "BlazorWebasmClientGatewayTestApp.csproj" -c Release -o /app/publish

FROM nginx:alpine AS final
WORKDIR /var/www/web
COPY --from=publish /app/publish .
COPY nginx.conf /etc/nginx/nginx.conf
EXPOSE 80
EXPOSE 443
