# WP1-Deployment-Example
A comprehensive example demonstrating ASP.NET Core deployment with GitHub Actions, Azure App Service, and automated testing.

## 🎯 Project Overview
This repository serves as a learning resource for students in Web Programming 1, demonstrating:
- **ASP.NET Core 8.0** web application development
- **GitHub Actions** CI/CD pipeline setup
- **Azure App Service** deployment (both code and container-based)
- **Unit Testing** with xUnit and automated test execution
- **Integration Testing** with in-memory test server
- **Docker** containerization best practices

## 📋 Learning Objectives
By the end of this tutorial, students will understand:
1. How to structure a .NET solution with separate test projects
2. How to write meaningful unit tests for ASP.NET Core controllers
3. How to configure GitHub Actions for automated testing and deployment
4. How to implement proper CI/CD practices with test gates
5. How to deploy applications to Azure App Service using multiple methods
6. How to gather and interpret automated test coverage metrics
7. How to differentiate between unit and integration tests and run them separately

## 🚀 Current Status

### ✅ Completed Steps
- [x] **Project Analysis**
- [x] **Deployment Setup** (base workflows)
- [x] **Containerization** (multi-stage Dockerfile)
- [x] **Step 1: Test Project Structure**
- [x] **Step 2: Sample Unit Tests**
- [x] **Step 3 (Phase 1)**: CI + gated deploy workflow
- [x] **Step 4: Coverage Reporting**
- [x] **Step 5 (Initial Integration Tests)**: Added `HomeControllerIntegrationTests`

### 🔄 In Progress
- [ ] **Refinements**: Badges, coverage thresholds, environment protection docs, integration coverage strategy

### 📝 Planned / Future Enhancements
- [ ] Add additional integration scenarios (form posts, error handling)
- [ ] Introduce performance or load testing examples (optional extension)

---

## 📚 Step-by-Step Implementation Guide

### Unit vs Integration Tests
| Aspect | Unit Test | Integration Test |
|--------|-----------|------------------|
| Scope | Single method/class | End-to-end request pipeline |
| Startup | Manual controller instantiation | Full `Program` boot via `WebApplicationFactory<Program>` |
| Speed | Very fast | Slower (host + middleware) |
| Fail Cause | Logic regression | Middleware/routing/config issues |
| Trait Used | (none / default) | `Category=Integration` |

### Filtering Tests
Run only unit tests:
```
dotnet test --filter "Category!=Integration"
```
Run only integration tests:
```
dotnet test --filter "Category=Integration"
```

### Step 5: Integration Tests (Implemented - Initial)
**Added File:** `deploymentfall25.Tests/Integration/HomeControllerIntegrationTests.cs`

**What It Does:**
- Spins up in-memory server using `WebApplicationFactory<Program>`
- Sends real HTTP GET requests to `/` and `/Home/Privacy`
- Asserts 200 OK + `text/html` content-type

**Why This Matters:**
- Validates routing, middleware pipeline, MVC view rendering chain
- Catches misconfigurations not visible to pure unit tests

**CI Workflow Adaptation:**
- Two test invocations:
  1. Unit-only + coverage (`--filter Category!=Integration`)
  2. Integration-only (no coverage aggregation by default)
- Coverage report built from unit tests to keep metrics stable & fast

**Potential Next Steps:**
- Add integration coverage (merge reports) once volume justifies
- Add trait filters for categories like `Database`, `Slow`, etc.

### Coverage Notes
Current coverage excludes integration tests for determinism. To include them later, run both with coverage and merge Cobertura XMLs using ReportGenerator `-reports:` pattern with multiple paths.

---

## 🔧 CI/CD Workflows Overview
| Workflow | File | Purpose | Notes |
|----------|------|---------|-------|
| CI (Build & Test) | `.github/workflows/ci.yml` | Build + unit coverage + integration tests | Separate filtered runs |
| Deploy (Code) | `.github/workflows/azure-deploy.yml` | Build, test, coverage, publish & deploy | Main branch only |
| Deploy Container Slot | `.github/workflows/deploy-container-slot.yml` | Container image build + slot update | Manual trigger |

---

## ▶️ Quick Start (Local)
```bash
dotnet restore
dotnet test -c Release --filter "Category!=Integration" --collect "XPlat Code Coverage"
dotnet test -c Release --filter "Category=Integration"
dotnet run --project deploymentfall25/deploymentfall25/deploymentfall25.csproj
```

## 📦 Local Coverage Reproduction (Unit Only)
```bash
dotnet tool install --global dotnet-reportgenerator-globaltool || dotnet tool update --global dotnet-reportgenerator-globaltool
rm -rf TestResults CoverageReport || true
dotnet test -c Release --filter "Category!=Integration" --collect "XPlat Code Coverage" --results-directory ./TestResults/Unit
reportgenerator -reports:TestResults/Unit/**/coverage.cobertura.xml -targetdir:CoverageReport -reporttypes:Html;TextSummary
```

## 🧪 Adding More Integration Tests
Example pattern:
```csharp
[Fact]
[Trait("Category","Integration")]
public async Task Post_Form_SubmitsSuccessfully()
{
    var client = _factory.CreateClient();
    var response = await client.PostAsync("/Items/Create", new FormUrlEncodedContent(...));
    response.EnsureSuccessStatusCode();
}
```

## 📖 Additional Resources
- ASP.NET Core Testing: https://learn.microsoft.com/aspnet/core/test/
- xUnit: https://xunit.net/
- Coverlet: https://github.com/coverlet-coverage/coverlet
- ReportGenerator: https://github.com/danielpalme/ReportGenerator
- WebApplicationFactory: https://learn.microsoft.com/aspnet/core/test/integration-tests
- GitHub Actions for .NET: https://docs.github.com/actions/automating-builds-and-tests/building-and-testing-net
