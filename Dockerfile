FROM mcr.microsoft.com/dotnet/runtime:5.0.9-buster-slim-arm32v7 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:5.0.400-buster-slim-arm32v7 AS build
WORKDIR /src
COPY MQTT_Client/MQTT_Client.csproj ./
RUN dotnet restore
COPY . .
WORKDIR "/src/MQTT_Client"
RUN dotnet build "MQTT_Client.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "MQTT_Client.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "MQTT_Client.dll"]