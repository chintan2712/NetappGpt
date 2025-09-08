# Stage 1: Base runtime image
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 8080

# Stage 2: Build image
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

# 1️⃣ Copy the .csproj from the subfolder
COPY ["NetappGpt/NetappGpt.csproj", "NetappGpt/"]

# 2️⃣ Restore dependencies
RUN dotnet restore "NetappGpt/NetappGpt.csproj"

# 3️⃣ Copy the rest of the source code
COPY . .

# 4️⃣ Build the project
WORKDIR "/src/NetappGpt"
RUN dotnet build "NetappGpt.csproj" -c $BUILD_CONFIGURATION -o /app/build

# Stage 3: Publish
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "NetappGpt.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# Stage 4: Final runtime image
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .

# 5️⃣ Bind to Railway port
ENV ASPNETCORE_URLS=http://+:8080

# 6️⃣ Start the application
ENTRYPOINT ["dotnet", "NetappGpt.dll"]
