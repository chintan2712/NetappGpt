# Use the official .NET SDK image
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build

# Set the working directory inside the container
WORKDIR /src

# Copy the .csproj file and restore any dependencies (via dotnet restore)
COPY NetappGpt/NetappGpt.csproj ./NetappGpt/
WORKDIR /src/NetappGpt
RUN dotnet restore

# Copy the rest of the application code
COPY . .

# Publish the application to the /app directory
RUN dotnet publish -c Release -o /app

# Set the base image for the runtime
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base

# Set the working directory inside the container
WORKDIR /app

# Copy the published application from the build stage
COPY --from=build /app .

# Expose the port the app runs on
EXPOSE 80

# Define the entry point for the application
ENTRYPOINT ["dotnet", "NetappGpt.dll"]
