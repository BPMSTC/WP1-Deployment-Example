# WP1-Deployment-Example

Repository for deployment of an app in WP1 with initial test setup.

## Project Structure

- `src/WP1.App/` - Main console application
- `tests/WP1.App.Tests/` - Unit tests for the application
- `Dockerfile` - Container configuration for deployment

## Running the Application

```bash
# Build and run locally
dotnet run --project src/WP1.App

# Run tests
dotnet test

# Build with Docker
docker build -t wp1-app .
docker run wp1-app
```

## Features

- ✅ Basic .NET 8 console application
- ✅ Initial unit tests with xUnit
- ✅ Docker deployment configuration
- ✅ Clean project structure for WP1 deployment example
