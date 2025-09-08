# ---------- Stage 1: Build ----------
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copy only the project file for dependency caching
COPY NetappGpt.csproj ./

# Restore dependencies
RUN dotnet restore

# Copy all source files
COPY . ./

# Build and publish the app to /app folder
RUN dotnet publish -c Release -o /app --no-restore /p:GenerateAssemblyInfo=false

# ---------- Stage 2: Runtime ----------
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app

# Copy published files from build stage
COPY --from=build /app ./

# Expose ports (adjust if needed)
EXPOSE 8080
EXPOSE 8081

# Set the entrypoint
ENTRYPOINT ["dotnet", "NetappGpt.dll"]
