FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 8080

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

# Copy csproj from current folder (no extra subfolder)
COPY ["NetappGpt.csproj", "."]
RUN dotnet restore "NetappGpt.csproj"

# Copy all other files
COPY . .
RUN dotnet build "NetappGpt.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "NetappGpt.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .

# Bind to Docker port
ENV ASPNETCORE_URLS=http://+:8080

ENTRYPOINT ["dotnet", "NetappGpt.dll"]
