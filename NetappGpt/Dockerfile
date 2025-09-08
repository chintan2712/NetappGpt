FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 8080

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

# Copy only the project file first (for caching)
COPY ["NetappGpt/NetappGpt.csproj", "NetappGpt/"]
RUN dotnet restore "NetappGpt/NetappGpt.csproj"

# Copy all source code
COPY . .

WORKDIR "/src/NetappGpt"
RUN dotnet build "NetappGpt.csproj" -c $BUILD_CONFIGURATION -o /app/build
RUN dotnet publish "NetappGpt.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=build /app/publish .

ENV ASPNETCORE_URLS=http://+:8080
ENTRYPOINT ["dotnet", "NetappGpt.dll"]
