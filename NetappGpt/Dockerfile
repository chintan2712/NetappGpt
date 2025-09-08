# =====================
# Build Stage
# =====================
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copy only csproj first to leverage Docker cache for restore
COPY NetappGpt/NetappGpt.csproj ./NetappGpt/
WORKDIR /src/NetappGpt

# Restore NuGet packages
RUN dotnet restore

# Copy all project files
COPY NetappGpt/. ./

# Publish the project to /app
RUN dotnet publish -c Release -o /app /p:GenerateAssemblyInfo=false

# =====================
# Runtime Stage
# =====================
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app

# Copy published app from build stage
COPY --from=build /app ./

# Expose ports
EXPOSE 8080 8081

# Set environment variables for Kestrel
ENV ASPNETCORE_URLS=http://+:8080;https://+:8081 \
    DOTNET_RUNNING_IN_CONTAINER=true \
    DOTNET_USE_POLLING_FILE_WATCHER=true

# Run the application
ENTRYPOINT ["dotnet", "NetappGpt.dll"]
