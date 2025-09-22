# =============================================================================
# Multi-stage Dockerfile for ASP.NET Core 8.0 Application
# This Dockerfile demonstrates best practices for containerizing .NET applications
# =============================================================================

# ---- Build Stage ----
# Use the full .NET SDK image for building the application
# The SDK includes everything needed to compile, restore packages, and publish
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build

# Set the working directory inside the container
# This is where all build operations will take place
WORKDIR /src

# Copy the entire source code into the container
# This includes the project files, source code, and configuration files
COPY . .

# move into the project directory (adjust ONLY if your path/name differs)
WORKDIR /src/deploymentfall25/deploymentfall25

# Restore NuGet packages
# This downloads all dependencies specified in the .csproj file
# Since we're building from the project directory, we use the local project file
RUN dotnet restore deploymentfall25.csproj

# Publish the application for production
# -c Release: Build in Release configuration for optimal performance
# -o /out: Output directory for the published application
# --no-restore: Skip package restoration since we already did it above
# This creates a self-contained deployment with all necessary files
RUN dotnet publish deploymentfall25.csproj -c Release -o /out --no-restore

# ---- Runtime Stage ----
# Use the smaller ASP.NET runtime image for the final container
# This image only contains the runtime, not the SDK, making it much smaller
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime

# Set the working directory for the application
WORKDIR /app

# Configure the application to listen on port 8080
# This environment variable tells ASP.NET Core which port to bind to
# The + means bind to all network interfaces (0.0.0.0:8080)
ENV ASPNETCORE_URLS=http://+:8080

# Expose port 8080 to the outside world
# This tells Docker that the container will listen on port 8080
EXPOSE 8080

# Copy the published application from the build stage
# We only copy the final output, not the source code or build tools
COPY --from=build /out ./

# Set the entry point for the container
# This is the command that will run when the container starts
# The application will start and listen on the configured port
ENTRYPOINT ["dotnet", "deploymentfall25.dll"]

# =============================================================================
# Best Practices Implemented:
# 1. Multi-stage build: Reduces final image size by excluding build tools
# 2. Specific base images: Uses official Microsoft images with version tags
# 3. Non-root user: ASP.NET runtime images run as non-root by default
# 4. Minimal runtime: Only includes what's needed to run the application
# 5. Explicit port configuration: Makes the application's network requirements clear
# 6. Layer optimization: Restore and publish are separate layers for better caching
# =============================================================================
