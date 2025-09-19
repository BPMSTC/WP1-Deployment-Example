# WP1-Deployment-Example

Repository for deployment of a .NET application in WP1 to Azure App Service.

## Application Structure

- `WP1DeploymentApp/` - ASP.NET Core web application
- `.github/workflows/azure-deploy.yml` - GitHub Actions workflow for Azure deployment

## Azure Deployment Setup

This repository includes a GitHub Actions workflow that automatically builds and deploys the .NET application to Azure App Service.

### Required Secrets

To enable Azure deployment, configure the following secrets in your GitHub repository:

- `AZURE_CLIENT_ID` - Azure service principal client ID
- `AZURE_TENANT_ID` - Azure tenant ID  
- `AZURE_SUBSCRIPTION_ID` - Azure subscription ID
- `AZURE_CLIENT_SECRET` - Azure service principal client secret

### Workflow Configuration

The workflow is configured to:
1. Build the .NET application on push to `main` branch
2. Publish the application artifacts
3. Deploy to Azure App Service using `azure/login` and `azure/webapps-deploy` actions

Update the `AZURE_WEBAPP_NAME` environment variable in the workflow file to match your Azure App Service name.
