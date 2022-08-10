# Th# This Dockerfile contains Build and Release steps:
# 1. Build image(https://hub.docker.com/_/microsoft-dotnet-core-sdk/)
FROM mcr.microsoft.com/dotnet/sdk:6.0.400-bullseye-slim-amd64 AS build
WORKDIR /source

# Cache nuget restore
COPY "Backend.csproj" .
RUN dotnet restore "Backend.csproj"

# Copy sources and compile
COPY . .
RUN dotnet publish "Backend.csproj" --output /app --configuration Release

# 2. Release image
FROM mcr.microsoft.com/dotnet/aspnet:6.0.8-bullseye-slim-amd64
WORKDIR /app
EXPOSE 80
EXPOSE 443
ENV ASPNETCORE_URLS=http://+:80;https://+:443

# Copy content from Build image
COPY --from=build /app .
ENTRYPOINT ["dotnet", "Backend.dll"]
