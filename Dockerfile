FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["MQTT_Client/MQTT_Client.csproj", "MQTT_Client/"]
RUN dotnet restore "MQTT_Client\MQTT_Client.csproj"
COPY . .
WORKDIR "/src/MQTT_Client"
RUN dotnet build "MQTT_Client.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "MQTT_Client.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "MQTT_Client.dll"]