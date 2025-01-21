# Imaginea de bază pentru rulare
FROM mcr.microsoft.com/dotnet/aspnet:8.0-jammy AS base
WORKDIR /app
EXPOSE 8080

# Imaginea SDK pentru construirea aplicației
FROM mcr.microsoft.com/dotnet/sdk:8.0-jammy AS build
ARG BUILD_CONFIGURATION=Release

WORKDIR /app
COPY . .  

RUN dotnet restore --source "https://api.nuget.org/v3/index.json" "CMVMD.sln"
RUN dotnet build "Server/CMVMD.Server.csproj" --configuration $BUILD_CONFIGURATION --no-restore
RUN dotnet publish "Server/CMVMD.Server.csproj" --configuration $BUILD_CONFIGURATION --no-build --output /publish

# Faza finală - construirea imaginii pentru rulare
FROM base AS final
WORKDIR /app
COPY --from=build /publish .

# Script pentru migrații


# Configurarea entrypoint-ului
ENTRYPOINT ["dotnet", "CMVMD.Server.dll"]