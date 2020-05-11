# About
This repo is intended to showcase toolchain examples and key moments when working with .NET Core, Azure Service Bus, and Functions on MacOS/Linux.

# Dependencies
We expect the following to be installed to correctly run these examples.  
- [.NET Core 3.1](https://dotnet.microsoft.com/download/dotnet-core/3.1)  
- [Azure CLI](https://docs.microsoft.com/en-us/cli/azure/install-azure-cli?view=azure-cli-latest)
- [Azure Functions Core Tools](https://docs.microsoft.com/en-us/azure/azure-functions/functions-run-local?tabs=macos%2Ccsharp%2Cbash#v2)
- [Visual Studio Code](https://code.visualstudio.com/download)  
  - (Recommended Extension) [Azure Resource Manager (ARM) Tools](https://marketplace.visualstudio.com/items?itemName=msazurermtools.azurerm-vscode-tools)
  - (Recommended Extension) [C# for VS Code](https://marketplace.visualstudio.com/items?itemName=ms-dotnettools.csharp)
- (Recommended) [Cerulean](https://www.cerebrata.com/products/cerulean/download) - Tool for inspecting Azure Service Bus or equivalent.

# Getting Started
1. Clone this repository to your local workstation
2. Install the dependencies listed above (see Dependencies)
3. Test the above by building the solution (`CMD+Shift+B`, )
3. Deploy the ARM template (see below)
4. Navigate the examples in the section below. Consider activites like:
    - Putting a message in a topic, inspecting it in a subscription using a tool like [Cerulean](https://www.cerebrata.com/products/cerulean/download)
    - Debug/Consume a payload from either a Service Bus or Http Trigger
    - Deploy a function to Azure
    - Execute/see output of functions running in Azure

## Deploying ARM Template
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

## Examples

### Putting a Message on a Topic
This example puts a message on a service bus topic. 

- Open `SendSBMessageExample.cs` in `./Producer/SendSBMessage/SendSBMessageExample.cs` and replace the `ServiceBusConnectionString` with one from your own development environment (see Deploy ARM Template) above. Note that this console app puts a message on the `product-created` topic.
- Run/Debug this by either:
  - **Debug from VS Code**: Set a breakpoint in `SendSBMessageExample.cs`. Open the Run toolbar in VS Code (`CMD+Shift+D`), select `Debug SendSBMessage` and press the play button.
  - **Run from Terminal**: Set the CWD of a terminal session to `./Producer/SendSBMessage`, and run the `dotnet run` command.
- Open up Cerulean and inspect your Service Bus topic `product-created`. Note that the messages we are sending are accruing in the topic subscriptions.
- Get comfortable with activities like:
  - Inspect topic/subscription/message properties (good feedback loop for editing ARM templates)
  - `Modify & Resubmit` messages

### Consuming a Service Bus Message Locally with ServiceBusTriggerExample.cs
This example consumes a message from the `izzy-product-created` subscription. Ensure a message exists in this subscription by running the example above.

- Open `ServicBusTriggerExample.cs` in `./Consumer/ServiceBusTrigger/ServiceBusTriggerExample.cs` and replace the `ConnectionString` and `AzureWebJobsStorage` settings in the related `local.settings.json`.
- Set a breakpoint in `ServicBusTriggerExample.cs`.
- Debug app by opening the Run toolbar in VS Code (`CMD+Shift+D`), select `Debug ServiceBusTrigger` and press the play button.
- After the breakpoint hits, inspect values like `deliveryCount`, `lockToken`, and `message`.
- After the debugging session is complete, note that the message is consumed (no longer in the subscription).

### Debugging HttpTrigger Locally with HttpTriggerExample.cs
This example is activated by a call to a local web server/emulator.

- Open `HttpTriggerExample.cs` in `./Consumer/HttpTrigger/HttpTriggerExample.cs`
- Set a breakpoint in `HttpTriggerExample.cs`.
- Debug app by opening the Run toolbar in VS Code (`CMD+Shift+D`), select `Debug HttpTrigger` and press the play button.
- Activate the function using a Browser, rest client (e.g. Postman), or a CURL:
``` bash
curl http://localhost:7071/api/HttpTriggerExample?name=Name
```
- See the breakpoint hit an inspect the http/function context.

### Debugging TimerTrigger Locally with TimerTriggerExample.cs
This example is activated every 5 seconds by a timer.

- Open `TimerTriggerExample.cs` in `./Consumer/TimerTrigger/TimerTriggerExample.cs` and replace the `AzureWebJobsStorage` setting in the related `local.settings.json`.
- Set a breakpoint in `TimerTriggerExample.cs`.
- Debug app by opening the Run toolbar in VS Code (`CMD+Shift+D`), select `Debug TimerTrigger` and press the play button.
- See the breakpoint hit an inspect the timer/function context.

### Deploying a Function to Azure
This example deploys the HttpFunctionExample to Azure.

- Ensure you've provisionied a personal resource group by deploying the ARM template above.
- Open the VS Code Azure Fuctions Extension (`CMD+Shift+A`).
- Click the `Deploy to Function App` button.
  - Follow the prompts selecting the fuction to deploy and the function app (in Azure) to deploy into.
- Observe logs as the function app is compiled, packaged, and deployed for you.
- Explore controls to `Stop/Start` the function application, source the `Function Url` to activate/run it.
- Explore configuration for the function and how to monitor and manually execute the function.


# Next Steps
These are activites that are being considered to better make this repository useful to OCP staff.

- Add tests with examples for:
  - Dependency injection
  - Tests (XUnit)
- Add CI/CD 
- Containerize this dev environment (see Dependencies section)
