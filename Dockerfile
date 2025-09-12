# Use the official .NET runtime as base image
FROM mcr.microsoft.com/dotnet/runtime:8.0 AS base
WORKDIR /app

# Use the SDK image to build the application
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["src/WP1.App/WP1.App.csproj", "src/WP1.App/"]
RUN dotnet restore "src/WP1.App/WP1.App.csproj"
COPY . .
WORKDIR "/src/src/WP1.App"
RUN dotnet build "WP1.App.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "WP1.App.csproj" -c Release -o /app/publish /p:UseAppHost=false

# Final stage/image
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "WP1.App.dll"]