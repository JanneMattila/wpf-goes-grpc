# WPF goes gRPC

Example WPF application which calls backend gRPC server.

## Build Status

[![Build Status](https://jannemattila.visualstudio.com/jannemattila/_apis/build/status/JanneMattila.QuizMaker?branchName=master)](https://jannemattila.visualstudio.com/jannemattila/_build/latest?definitionId=44&branchName=master)

[![License: MIT](https://img.shields.io/badge/License-MIT-yellow.svg)](https://opensource.org/licenses/MIT)

## Instructions

### Important notice about certificates

In order for Kestrel to work correctly with gRPC and HTTP/2 you need
to have TLS certificate set. **For demo purposes** this example has `grpcbackend.pfx` 
certificate bundled directly into the project. 

### How to create image locally

```batch
# Build container image
cd src/Backend
docker build . -t grpcbackend:latest

# Run container using command
docker run -p "20001:443" grpcbackend:latest
``` 

### How to test locally

Run WPF application named `Frontend` from the solution.
Update backend service address in the top most textbox and
invoke any of the gRPC commands.

### How to deploy to Azure Container Instances (ACI)

Deploy published image to [Azure Container Instances (ACI)](https://docs.microsoft.com/en-us/azure/container-instances/) the Azure CLI way:

```batch
# Variables
aciName="grpc"
resourceGroup="grpc-dev-rg"
location="westeurope"
image="jannemattila/grpcbackend:latest"

# Login to Azure
az login

# *Explicitly* select your working context
az account set --subscription <YourSubscriptionName>

# Create new resource group
az group create --name $resourceGroup --location $location

# Create ACI
az container create --name $aciName --image $image --resource-group $resourceGroup --ip-address public --ports 443

# Show the properties
az container show --name $aciName --resource-group $resourceGroup

# Show the logs
az container logs --name $aciName --resource-group $resourceGroup

# Wipe out the resources
az group delete --name $resourceGroup -y
``` 

Deploy published image to [Azure Container Instances (ACI)](https://docs.microsoft.com/en-us/azure/container-instances/) the Azure PowerShell way:

```powershell
# Variables
$aciName="grpc"
$resourceGroup="grpc-dev-rg"
$location="westeurope"
$image="jannemattila/grpcbackend:latest"

# Login to Azure
Login-AzAccount

# *Explicitly* select your working context
Select-AzSubscription -SubscriptionName <YourSubscriptionName>

# Create new resource group
New-AzResourceGroup -Name $resourceGroup -Location $location

# Create ACI
New-AzContainerGroup -Name $aciName -Image $image -ResourceGroupName $resourceGroup -IpAddressType Public -Port 443

# Show the properties
Get-AzContainerGroup -Name $aciName -ResourceGroupName $resourceGroup

# Show the logs
Get-AzContainerInstanceLog -ContainerGroupName $aciName -ResourceGroupName $resourceGroup

# Wipe out the resources
Remove-AzResourceGroup -Name $resourceGroup -Force
```
