#!/bin/bash

resourceGroupName='cloud-workshop-resources'
staticWebAppName='static-web-app-cloud-workshop'

# Set deployment token as secret in current GitHub repo to be used by GitHub action for code deployment
staticWebAppApiKey=$(az staticwebapp secrets list --resource-group ${resourceGroupName} --name ${staticWebAppName} --query properties.apiKey -o tsv)

gh secret set AZURE_STATIC_WEB_APPS_API_TOKEN --body $staticWebAppApiKey
