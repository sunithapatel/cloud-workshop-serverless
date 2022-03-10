#!/bin/bash

# Set variables for the names of the Azure resources
resourceGroupName='cloud-workshop-resources'
resourceGroupLocation='eastus'
cosmosDbAccountName='cosmos-db-cloud-workshop'
cosmosDbDatabaseName='CloudWorkshop'
cosmosDbContainerName='Resources'
staticWebAppName='static-web-app-cloud-workshop'

# Create a resource group
az group create \
    --name $resourceGroupName \
    --location $resourceGroupLocation

# Apply CosmosDb ARM template
az deployment group create \
  --name DeployCosmosDbTemplate \
  --resource-group $resourceGroupName \
  --template-file cosmos-db-arm.json \
  --parameters accountName=$cosmosDbAccountName \
               databaseName=$cosmosDbDatabaseName \
               containerName=$cosmosDbContainerName \
  --verbose

# Get CosmosDb connection info for API configuration settings
cosmosDbAccount=$(az cosmosdb show --resource-group ${resourceGroupName} --name ${cosmosDbAccountName} --query documentEndpoint -o tsv)
cosmosDbKey=$(az cosmosdb keys list --resource-group ${resourceGroupName} --name ${cosmosDbAccountName} --query primaryMasterKey -o tsv)

# Apply static web app ARM template
az deployment group create \
  --name DeployStaticWebAppTemplate \
  --resource-group $resourceGroupName \
  --template-file static-web-app-arm.json \
  --parameters \
    name=$staticWebAppName \
    location=eastus2 \
    appSettings="{
        \"CosmosDbAccount\":\"${cosmosDbAccount}\",
        \"CosmosDbKey\":\"${cosmosDbKey}\",
        \"CosmosDbDatabaseName\":\"${cosmosDbDatabaseName}\",
        \"CosmosDbContainerName\":\"${cosmosDbContainerName}\"
    }" \
  --verbose


