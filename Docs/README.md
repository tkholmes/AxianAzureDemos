## About
This repo is intended to showcase toolchain examples and key moments when working with .NET Core, Azure Service Bus, and Functions on MacOS/Linux.

## Dependencies
We expect the following to be installed to correctly run these examples.  
- [.NET Core 3.1](https://dotnet.microsoft.com/download/dotnet-core/3.1)  
- [Azure CLI](https://docs.microsoft.com/en-us/cli/azure/install-azure-cli?view=azure-cli-latest)
- [Azure Functions Core Tools](https://docs.microsoft.com/en-us/azure/azure-functions/functions-run-local?tabs=macos%2Ccsharp%2Cbash#v2)
- [Visual Studio Code](https://code.visualstudio.com/download)  
  - (Recommended Extension) [Azure Resource Manager (ARM) Tools](https://marketplace.visualstudio.com/items?itemName=msazurermtools.azurerm-vscode-tools)
  - (Recommended Extension) [C# for VS Code](https://marketplace.visualstudio.com/items?itemName=ms-dotnettools.csharp)

## Getting Started
1. Clone this repository to your local workstation
2. Install the dependencies listed above (see Dependencies)
3. Deploy the ARM template (see below)
4. Run tests/explore. Consider activites like:
    - Putting a message in a topic, inspecting it in a subscription using a tool like [Cerulean](https://www.cerebrata.com/products/cerulean/download)
    - Debug/Consume a payload from either a Service Bus or Http Trigger
    - Deploy a function to Azure
    - Execute/see output of functions running in Azure

### Deploying ARM Template
1. Open a terminal and login to Azure CLI, select the correct subscription (if you have multiple).
Example:
```bash
$> az login         // Associate this terminal session w/an Azure credential
$> az account show  // Current selected Azure Subscription
$> az account list  // Lists Azure Subscriptions available to you
$> az account set --subscription "Subscription Name" // Use a specific subscription
```
2. Provision a resource group in Azure. This is the logical container/grouping of resources that is intended to be your personal development environment.
```bash
$> az group create -l westus2 -n ocp-webenv-YOURNAME
```

3. Edit the ARM template parameters file (`parameters.json`) and set the `environment-name` to `YOURNAME` in the command above. Run the command below to provision the ARM template into the resource group we just created.
```bash
$> az group deployment create -g ocp-webenv-YOURNAME --template-file ArmTemplate/environment.json --parameters ArmTemplate/parameters.json
```



