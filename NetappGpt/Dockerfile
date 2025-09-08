# Build stage
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copy only the .csproj file and restore
COPY NetappGpt/NetappGpt.csproj ./NetappGpt/
WORKDIR /src/NetappGpt
RUN dotnet restore

# Copy the rest of the files
COPY NetappGpt/. ./
RUN dotnet publish -c Release -o /app --no-restore /p:GenerateAssemblyInfo=false

# Runtime stage
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build /app ./

# Expose HTTP port
EXPOSE 8080

# Run the app
ENTRYPOINT ["dotnet", "NetappGpt.dll"]
