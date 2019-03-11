---
date: 2019-01-14T11:00:59-04:00
tags: ["Azure", "Certifications"]
title: "AZ-203 Developing Solutions for Microsoft Azure Study Guide"

---

<!-- <strong>This is one guide in a series of guides that can act as study material for Microsoft's new Azure oriented certification exams.</strong>

* [AZ-300 Exam Guide](/post/az-300-study-guide)
* [AZ-301 Exam Guide](/post/az-301-study-guide) -->

<br />

<strong>Update here, I passed this exam in January 2019 exclusively using this study guide. Let me know if you have any questions using the contact page.</strong>
<br />
<br />
## Intro

<br />

As a Microsoft employee who works with customers and a cloud enthusiast, I see it essential to be knowledgeable of how the cloud can bring the best value to the developer. Because of this, I am taking the AZ-203 exam, which is titled "Developing Solutions for Microsoft Azure". This exam was in beta for some time and was recently released proper in January 2019. Developers used to the Microsoft certification world will see this exam as a replacement for 70-532, which is the older iteration of Azure technology geared for developers. Passing this exam will reward developers with the "Microsoft Certified Azure Developer Associate" certification. Going forward, most Microsoft certifications are moving to a [job-role based](https://buildazure.com/2018/09/24/introducing-role-based-microsoft-azure-certification-shakeup/) (great take by Chris Pietschmann at Build Azure) approach, which in my opinion is a good move, as it allows folks to focus on passing exams that contain content that will directly be used on the job. For more information about the exam, take a look at the [Official Exam Page](https://www.microsoft.com/learning/exam-AZ-203.aspx)

<br />
## Skills Measured


<br />
Here’s a high level list of the skills and objectives measured on the AZ-203 Developing Solutions for Microsoft Azure certification exam. The percentages next to each objective area represents the number of questions in that objective area on the exam. I have included links to docs, code samples, and other resources that I have found helpful as I studied. Hope you enjoy!

<br />

#### Develop Azure Infrastructure as a Service (IaaS) Compute Solutions (10-15%)</strong>

<br />

* Implement solutions that use virtual machines (VM)
  - Provision VMs ([Windows](https://docs.microsoft.com/azure/virtual-machines/windows/tutorial-manage-vm), [Linux](https://docs.microsoft.com/azure/virtual-machines/linux/tutorial-manage-vm))
  - Create ARM templates ([Portal](https://docs.microsoft.com/en-us/azure/azure-resource-manager/resource-manager-quickstart-create-templates-use-the-portal), [VS Code](https://docs.microsoft.com/en-us/azure/azure-resource-manager/resource-manager-quickstart-create-templates-use-visual-studio-code), [Visual Studio](https://docs.microsoft.com/en-us/azure/azure-resource-manager/vs-azure-tools-resource-groups-deployment-projects-create-deploy))
  - Configure Azure Disk Encryption for VMs ([Windows](https://docs.microsoft.com/azure/virtual-machines/windows/encrypt-disks), [Linux](https://docs.microsoft.com/azure/virtual-machines/linux/encrypt-disks))

* Implement batch jobs by using Azure Batch Services
  - Manage batch jobs by using Batch Service API ([Batch Service API Reference](https://docs.microsoft.com/en-us/rest/api/batchservice/))
  - Run a batch job by using [Azure CLI](https://docs.microsoft.com/azure/batch/quick-create-cli), [Azure Portal](https://docs.microsoft.com/azure/batch/quick-create-portal), and other tools ([.NET](https://docs.microsoft.com/azure/batch/quick-run-dotnet), [Python](https://docs.microsoft.com/en-us/azure/batch/quick-run-python))
  - Write code to run an Azure Batch Services batch job ([Azure Batch Samples Github Repo](https://github.com/Azure/azure-batch-samples))

* Create containerized solutions
  - Create an Azure Managed Kubernetes Service (AKS) cluster ([Azure CLI](https://docs.microsoft.com/en-us/azure/aks/kubernetes-walkthrough), [Azure Portal](https://docs.microsoft.com/en-us/azure/aks/kubernetes-walkthrough-portal))
  - Create container images for solutions ([Part of ACI Tutorial](https://docs.microsoft.com/en-us/azure/container-instances/container-instances-tutorial-prepare-app))
  - Publish an image to the Azure Container Registry ([Part of ACI Tutorial](https://docs.microsoft.com/en-us/azure/container-instances/container-instances-tutorial-prepare-acr))
  - Run containers by using Azure Container Instance or AKS ([ACI](https://docs.microsoft.com/en-us/azure/container-instances/container-instances-tutorial-deploy-app), [AKS](https://docs.microsoft.com/en-us/azure/aks/tutorial-kubernetes-deploy-application))

<br />

#### Develop Azure Platform as a Service (PaaS) Compute Solutions (20-25%)

<br />

* Create Azure App Service Web Apps
  - Create an Azure App Service Web App ([.NET with others in TOC](https://docs.microsoft.com/en-us/azure/app-service/app-service-web-get-started-dotnet))
  - Create an Azure App Service background task by using WebJobs ([Create WebJobs](https://docs.microsoft.com/en-us/azure/app-service/webjobs-create))
  - Enable diagnostics logging ([Azure App Service diagnostics overview](https://docs.microsoft.com/en-us/azure/app-service/overview-diagnostics))

* Create Azure App Service mobile apps
  - Add push notifications for mobile apps ([Android](https://docs.microsoft.com/en-us/azure/app-service-mobile/app-service-mobile-android-get-started-push), [Cordova](https://docs.microsoft.com/en-us/azure/app-service-mobile/app-service-mobile-cordova-get-started-push), [iOS](https://docs.microsoft.com/en-us/azure/app-service-mobile/app-service-mobile-ios-get-started-push), [Windows](https://docs.microsoft.com/en-us/azure/app-service-mobile/app-service-mobile-windows-store-dotnet-get-started-push), [Xamarin.Android](https://docs.microsoft.com/en-us/azure/app-service-mobile/app-service-mobile-xamarin-android-get-started-push), [Xamarin.Forms](https://docs.microsoft.com/en-us/azure/app-service-mobile/app-service-mobile-xamarin-forms-get-started-push), [Xamarin.iOS](https://docs.microsoft.com/en-us/azure/app-service-mobile/app-service-mobile-xamarin-ios-get-started-push))
  - Enable offline sync for mobile app ([Offline Data Sync in Azure Mobile Apps](https://docs.microsoft.com/en-us/azure/app-service-mobile/app-service-mobile-offline-data-sync))
  - Implement a remote instrumentation strategy for mobile devices ([Use TeamViewer to remotely administer Intune devices](https://docs.microsoft.com/en-us/intune/device-profile-android-teamviewer))

* Create Azure App Service API apps
  - Create an Azure App Service API app ([Tutorial: Host a RESTful API with CORS in Azure App Service](https://docs.microsoft.com/en-us/azure/app-service/app-service-web-tutorial-rest-api))
  - Create documentation for the API by using open source and other tools (Above uses [Swagger](https://swagger.io/))

* Implement Azure functions
  - Implement input and output bindings for a function ([Azure Functions triggers and bindings concepts](https://docs.microsoft.com/en-us/azure/azure-functions/functions-triggers-bindings))
  - Implement function triggers by using data operations ([Azure Cosmos DB](https://docs.microsoft.com/en-us/azure/azure-functions/functions-create-cosmos-db-triggered-function), [Blob Storage](https://docs.microsoft.com/en-us/azure/azure-functions/functions-create-storage-blob-triggered-function), [Queue Storage](https://docs.microsoft.com/en-us/azure/azure-functions/functions-create-storage-queue-triggered-function)), [timers](https://docs.microsoft.com/en-us/azure/azure-functions/functions-create-scheduled-function), and [webhooks](https://docs.microsoft.com/en-us/azure/azure-functions/functions-bindings-http-webhook#trigger)
  - Implement Azure Durable Functions ([C#](https://docs.microsoft.com/en-us/azure/azure-functions/durable/durable-functions-create-first-csharp), [Javascript](https://docs.microsoft.com/en-us/azure/azure-functions/durable/quickstart-js-vscode))
  - Create Azure Function apps by using Visual Studio ([Create your first function using Visual Studio](https://docs.microsoft.com/en-us/azure/azure-functions/functions-create-your-first-function-visual-studio))

<br />

### Develop for Azure Storage (15-20%)

<br />

* Develop solutions that use storage tables
  - Design and implement policies for tables ([Azure Storage Security Guide](https://docs.microsoft.com/en-us/azure/storage/common/storage-security-guide)
  - Query table storage by using code ([.NET Sample](https://docs.microsoft.com/en-us/azure/cosmos-db/table-storage-how-to-use-dotnet?toc=%2Fen-us%2Fazure%2Fstorage%2Ftables%2FTOC.json&bc=%2Fen-us%2Fazure%2Fbread%2Ftoc.json))
  - Implement partitioning schemes ([Data partitioning strategies](https://docs.microsoft.com/en-us/azure/architecture/best-practices/data-partitioning-strategies#partitioning-azure-blob-storage) has section on Table Storage)

* Develop solutions that use Cosmos DB storage
  - Create, read, update, and delete data by using appropriate APIs ([Azure Cosmos DB Documentation](https://docs.microsoft.com/en-us/azure/cosmos-db/) has links to all API references)
  - Implement partitioning schemes ([Partioning Cosmos DB](https://docs.microsoft.com/en-us/azure/architecture/best-practices/data-partitioning-strategies#partitioning-cosmos-db))
  - Set the appropriate consistency level for operations ([Choose the right consistency level for your application](https://docs.microsoft.com/en-us/azure/cosmos-db/consistency-levels-choosing))

* Develop solutions that use a relational database
  - Provision and configure relational databases ([Create a single Azure SQL Database](https://docs.microsoft.com/en-us/azure/sql-database/sql-database-get-started-portal))
  - Configure elastic pools for Azure SQL Database ([Create and manage elastic pools in Azure SQL Database](https://docs.microsoft.com/en-us/azure/sql-database/sql-database-elastic-pool-manage))
  - Create, read, update, and delete data tables by using code ([Create a single Azure SQL Database](https://docs.microsoft.com/en-us/azure/sql-database/sql-database-get-started-portal) has query data section)

* Develop solutions that use blob storage
  - Move items in Blob storage between storage accounts or containers ([C# Code](https://github.com/Azure-Samples/storage-blob-dotnet-getting-started/blob/master/BlobStorage/Advanced.cs), [AzCopy](https://docs.microsoft.com/en-us/azure/storage/scripts/storage-common-transfer-between-storage-accounts?toc=%2fpowershell%2fmodule%2ftoc.json))
  - Set and retrieve properties and metadata ([Setting properties and metadata during the import process](https://docs.microsoft.com/en-us/azure/storage/blobs/storage-properties-metadata))
  - Implement blob leasing ([Azure CLI](https://docs.microsoft.com/en-us/cli/azure/storage/blob/lease))
  - Implement data archiving and retention ([Azure Blob Access Tiers for Archiving](https://docs.microsoft.com/en-us/azure/storage/blobs/storage-blob-storage-tiers), [Store business-critical data in Azure Blob storage](https://docs.microsoft.com/en-us/azure/storage/blobs/storage-blob-immutable-storage))

<br />

### Implement Azure Security (10-15%)

<br />

*   Implement authentication
  - Implement authentication by using [certificates](https://docs.microsoft.com/en-us/azure/active-directory/authentication/active-directory-certificate-based-authentication-get-started), [forms-based authentication](https://docs.microsoft.com/en-us/azure/active-directory/authentication/howto-mfaserver-iis), or [tokens](https://docs.microsoft.com/en-us/azure/active-directory/develop/access-tokens)
  - Implement multi-factor or Windows authentication by using Azure AD ([Deploy cloud-based Azure Multi-Factor Authentication](https://docs.microsoft.com/en-us/azure/active-directory/authentication/howto-mfa-getstarted))
  - Implement OAuth2 authentication ([v2.0 Protocols - OAuth 2.0 and OpenID Connect](https://docs.microsoft.com/en-us/azure/active-directory/develop/active-directory-v2-protocols))
  - Implement Managed Service Identity (MSI)/Service Principal authentication ([PowerShell](https://docs.microsoft.com/en-us/azure/active-directory/develop/access-tokens), [Portal](https://docs.microsoft.com/en-us/azure/active-directory/develop/howto-create-service-principal-portal))
*   Implement access control
  -  Implement CBAC (Claims-Based Access Control) authorization ([Blog with GitHub sample](https://azure.microsoft.com/en-us/resources/samples/active-directory-dotnet-webapp-groupclaims/))
  -  Implement RBAC (Role-Based Access Control) authorization ([Portal](https://docs.microsoft.com/en-us/azure/role-based-access-control/quickstart-assign-role-user-portal), [PowerShell](https://docs.microsoft.com/en-us/azure/role-based-access-control/tutorial-role-assignments-user-powershell))
  - Create shared access signatures ([Using shared access signatures (SAS)](https://docs.microsoft.com/en-us/azure/storage/common/storage-dotnet-shared-access-signature-part-1))
*   Implement secure data solutions
  - Encrypt and decrypt data at rest and in transit ([Azure Data Security and Encryption Best Practices](https://docs.microsoft.com/en-us/azure/security/azure-security-data-encryption-best-practices))
  - Create, read, update, and delete keys, secrets, and certificates by using the KeyVault API ([Quickstart: Set and retrieve a secret from Azure Key Vault using Azure CLI with samples in TOC](https://docs.microsoft.com/en-us/azure/key-vault/quick-create-cli))

<br />

#### Monitor, Troubleshoot, and Optimize Azure Solutions (15-20%)

<br />

*   Develop code to support scalability of apps and services
  - Implement autoscaling rules and patterns ([Autoscaling](https://docs.microsoft.com/en-us/azure/architecture/best-practices/auto-scaling))
  - Implement code that handles transient faults ([Transient fault handling](https://docs.microsoft.com/en-us/azure/architecture/best-practices/transient-faults))
* Integrate caching and content delivery within solutions
  - Store and retrieve data in Azure Redis cache ([Quickstart: Use Azure Cache for Redis with a .NET application](https://docs.microsoft.com/en-us/azure/azure-cache-for-redis/cache-dotnet-how-to-use-azure-redis-cache))
  - Develop code to implement CDN’s in solutions ([Getting started on managing CDN in C#](https://azure.microsoft.com/en-us/resources/samples/cdn-dotnet-manage-cdn/
))
  - Invalidate cache content ([CDN](https://docs.microsoft.com/en-us/azure/cdn/cdn-purge-endpoint) or [Redis](https://redis.io/commands/FLUSHALL))
* Instrument solutions to support monitoring and logging
  - Configure instrumentation in an app or service by using Application Insights ([ASP.NET Web Apps](https://docs.microsoft.com/en-us/azure/azure-monitor/app/asp-net))
  - Analyze and troubleshoot solutions by using Azure Monitor ([View or analyze data collected with Log Analytics log search](https://docs.microsoft.com/en-us/azure/azure-monitor/learn/tutorial-viewdata))
  - Implement Application Insights Web Test and Alerts ([Creating an Application Insights Web Test and Alert Programmatically](https://azure.microsoft.com/en-us/blog/creating-a-web-test-alert-programmatically-with-application-insights/))

<br />

#### Connect to and Consume Azure Services and Third-party Services (20-25%)

<br />

*   Develop an App Service Logic App
  - Create a Logic App ([Create a Logic App](https://docs.microsoft.com/en-us/azure/logic-apps/logic-apps-create-logic-apps-from-templates#create-logic-apps-from-templates))
  - Create a custom connector for Logic Apps ([Custom connectors in Logic Apps](https://docs.microsoft.com/en-us/azure/logic-apps/custom-connector-overview))
  - Create a custom template for Logic Apps ([Create Azure Resource Manager templates for deploying logic apps](https://docs.microsoft.com/en-us/azure/logic-apps/logic-apps-create-deploy-template))
* Integrate Azure Search within solutions
  - Create an Azure Search index ([Portal](https://docs.microsoft.com/en-us/azure/search/search-create-index-portal), [.NET](https://docs.microsoft.com/en-us/azure/search/search-create-index-dotnet), [REST](https://docs.microsoft.com/en-us/azure/search/search-create-index-rest-api)
  - Import searchable data ([Portal](https://docs.microsoft.com/en-us/azure/search/search-get-started-portal), [.NET](https://docs.microsoft.com/en-us/azure/search/search-create-index-dotnet), [REST](https://docs.microsoft.com/en-us/azure/search/search-import-data-rest-api))
  - Query the Azure Search index ([Query using Search explorer
](https://docs.microsoft.com/en-us/azure/search/search-get-started-portal#query-index))
* Establish API Gateways
  - Create an APIM instance ([Portal](https://docs.microsoft.com/en-us/azure/api-management/get-started-create-service-instance), [Powershell](https://docs.microsoft.com/en-us/azure/api-management/powershell-create-service-instance))
  - Configure authentication for APIs ([How to secure APIs using client certificate authentication in API Management](https://docs.microsoft.com/en-us/azure/api-management/api-management-howto-mutual-certificates-for-clients))
  - Define policies for APIs ([API Management policies](https://docs.microsoft.com/en-us/azure/api-management/api-management-policies))
* Develop event-based solutions
   - Implement solutions that use Azure Event Grid ([Event Grid Documentation](https://docs.microsoft.com/en-us/azure/event-grid/))
  - Implement solutions that use Azure Notification Hubs ([Azure Notification Hubs Documentation](https://docs.microsoft.com/en-us/azure/notification-hubs/))
  - Implement solutions that use Azure Event Hub ([Azure Event Hubs Documentation](https://docs.microsoft.com/en-us/azure/event-hubs/))
* Develop message-based solutions
  - Implement solutions that use Azure Service Bus ([Azure Service Bus Messaging Documentation](https://docs.microsoft.com/en-us/azure/service-bus-messaging/))
  - Implement solutions that use Azure Queue Storage queues ([.NET Sample](https://docs.microsoft.com/en-us/azure/storage/queues/storage-dotnet-how-to-use-queues))