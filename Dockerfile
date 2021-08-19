FROM mcr.microsoft.com/dotnet/aspnet:5.0-buster-slim-arm32v7 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY MQTT_Client/MQTT_Client.csproj ./
RUN dotnet restore
COPY . .
WORKDIR "/src/MQTT_Client"
RUN dotnet build "MQTT_Client.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "MQTT_Client.csproj" -c Release -o /app/publish

LABEL org.opencontainers.image.source="https://github.com/M-Wolfer/MQTT_Client"

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "MQTT_Client.dll"]
