---

tags: ["Azure", "Certifications"]
title: "AZ-300 Azure Solutions Architect Expert"
hide: true
---

## Intro

<br />

<strong>This is one guide in a series of guides that can act as study material for Microsoft's new Azure oriented certification exams.</strong>

* [AZ-203 Exam Guide](/post/az-203-study-guide)
* [AZ-301 Exam Guide](/post/az-301-study-guide)

<br />

The AZ-300 exam, which is titled "Azure Solutions Architect Expert" and has a target audience of Azure Solution Architects. This exam was in beta for some time and was recently released proper in January 2019. Architects used to the Microsoft certification world will see this exam as a replacement for 70-535, which is the older iteration of Azure technology geared for architects. Passing this exam is one requirement to earn the "Microsoft Certified Azure Solutions Architect Expert" certification, the other being AZ-301. Going forward, most Microsoft certifications are moving to a [job-role based](https://buildazure.com/2018/09/24/introducing-role-based-microsoft-azure-certification-shakeup/) (great take by Chris Pietschmann at Build Azure) approach, which in my opinion is a good move, as it allows folks to focus on passing exams that contain content that will directly be used on the job. For more information about the exam, take a look at the [Official Exam Page](https://www.microsoft.com/learning/exam-AZ-300.aspx)

<br />
## Skills Measured


<br />
Here’s a high level list of the skills and objectives measured on the AZ-300 Azure Solutions Architect Expert certification exam. The percentages next to each objective area represents the number of questions in that objective area on the exam. I have included links to docs, code samples, and other resources that I have found helpful as I studied. Hope you enjoy!

<br />

**Deploy and Configure Infrastructure (25-30%)**

<br />

*   Analyze resource utilization and consumption
    - Configure diagnostic settings on resources
    - Create baseline for resources
    - Create and rest alerts
    - Analyze alerts across subscription
    - Analyze metrics across subscription
    - Create action groups
    - Monitor for unused resources
    - Monitor spend
    - Report on spend
    - Utilize Log Search query functions
    - View alerts in Log Analytics
*   Create and configure storage accounts
    - Configure network access to the storage account
    - Create and configure storage account
    - Generate shared access signature
    - Install and use Azure Storage Explorer
    - Manage access keys
    - Monitor activity log by using Log Analytics
    - Implement Azure storage replication
*   Create and configure a Virtual Machine (VM) for Windows and Linux
    - Configure high availability
    - Configure monitoring, networking, storage, and virtual machine size
    - Deploy and configure scale sets
*   Automate deployment of Virtual Machines (VMs)
    - Modify Azure Resource Manager (ARM) template
    - Configure location of new VMs
    - Configure VHD template
    - Deploy from template
    - Save a deployment as an ARM template
    - Deploy Windows and Linux VMs
*   Create connectivity between virtual networks
    - Create and configure VNET peering
    - Create and configure VNET to VNET
    - Verify virtual network connectivity
    - Create virtual network gateway
*   Implement and manage virtual networking
    - Configure private and public IP addresses, network routes, network interface, subnets, and virtual network
*   Manage Azure Active Directory (AD)
    - Add custom domains
    - Configure Azure AD Identity Protection, Azure AD Join, and Enterprise State Roaming
    - Configure self-service password reset
    - Implement conditional access policies
    - Manage multiple directories
    - Perform an access review
*   Implement and manage hybrid identities
    - Install and configure Azure AD Connect
    - Configure federation and single sign-on
    - Manage Azure AD Connect
    - Manage password sync and writeback

<br />

**Implement Workloads and Security (20-25%)**

<br />

*   Migrate servers to Azure
    - Migrate by using Azure Site Recovery (ASR)
    - Migrate using P2V
    - Configure storage
    - Create a backup vault
    - Prepare source and target environments
    - Backup and restore data
    - Deploy Azure Site Recovery (ASR) agent
    - Prepare virtual network
*   Configure serverless computing
    - Create and manage objects
    - Manage a Logic App resource
    - Manage Azure Function app settings
    - Manage Event Grid
    - Manage Service Bus
*   Implement application load balancing
    - Configure application gateway and load balancing rules
    - Implement front end IP configurations
    - Manage application load balancing
*   Integrate on premises network with Azure virtual network
    - Create and configure Azure VPN Gateway
    - Create and configure site to site VPN
    - Configure Express Route
    - Verify on premises connectivity
    - Manage on-premise connectivity with Azure
*   Manage role-based access control (RBAC)
    - Create a custom role
    - Configure access to Azure resources by assigning roles
    - Configure management access to Azure
    - Troubleshoot RBAC
    - Implement RBAC policies
    - Assign RBAC roles
*   Implement Multi-Factor Authentication (MFA)
    - Enable MFA for an Azure tenant
    - Configure user accounts for MFA
    - Configure fraud alerts
    - Configure bypass options
    - Configure trusted IPs
    - Configure verification methods
    - Manage role-based access control (RBAC)
    - Implement RBAC policies
    - Assign RBAC Roles
    - Create a custom role
    - Configure access to Azure resources by assigning roles
    - Configure management access to Azure

<br />

**Architect Cloud Technology Solutions (5-10%)**

<br />

*   Select an appropriate compute solution
    - Leverage appropriate design patterns
    - Select appropriate network connectivity options
    - Design for hybrid topologies
*   Select an appropriate integration solution
    - Address computational bottlenecks, state management, and OS requirements
    - Provide for web hosting if applicable
    - Evaluate minimum number of nodes
*   Select an appropriate storage solution
    - Validate data storage technology capacity limitations
    - Address durability of data
    - Provide for appropriate throughput of data access
    - Evaluate structure of data storage
    - Provide for data archiving, retention, and compliance

<br />

**Create and Deploy Apps (5-10%)**

<br />

*   Create web applications by using PaaS
    - Create an Azure app service web app by using Azure CLI, PowerShell, and other tools
    - Create documentation for the API by using open source and other tools
    - Create an App Service Web App for containers
    - Create an App Service background task by using WebJobs
*   Create app or service that runs on Service Fabric
    - Develop a stateful Reliable Service and a stateless Reliable Service
    - Develop an actor-based Reliable Service
    - Write code to consume Reliable Collections in your service
*   Design and develop applications that run in containers
    - Configure diagnostic settings on resources
    - Create a container image by using a Docker file
    - Create an Azure Container Service (ACS/AKS) cluster by using the Azure CLI and Azure Portal
    - Publish an image to the Azure Container Registry
    - Implement an application that runs on an Azure Container Instance
    - Implement container instances by using Azure Container Service (ACS/AKS), Azure Service Fabric, and other tools
    - Manage container settings by using code

<br />

**Implement Authentication and Secure Data (5-10%)**

<br />

*   Implement authentication
    - Implement authentication by using certificates, forms-based authentication, tokens, Windows-integrated authentication
    - Implement multi-factor authentication by using Azure AD options
*   Implement secure data solutions
    - Encrypt and decrypt data at rest
    - Encrypt data with Always Encrypted
    - Implement Azure Confidential Compute and SSL/TLS communications
    - Manage cryptographic keys in the Azure Key Vault

<br />

**Develop for the Cloud (20-25%)**

<br />

*   Develop long-running tasks
    - Implement large-scale, parallel, and high-performance apps by using batches
    - Implement resilient apps by using queues
    - Implement code to address application events by using web hooks
    - Address continuous processing tasks by using web jobs
*   Configure a message-based integration architecture
    - Configure an app or service to send emails, Event Grid, and the Azure Relay Service
    - Create and configure a Notification Hub, an Event Hub, and a Service Bus
    - Configure queries across multiple products
    - Configure an app or service with Microsoft Graph
*   Develop for asynchronous processing
    - Implement parallelism, multithreading, processing, durable functions, Azure logic apps, interfaces with storage, interfaces to data access, and appropriate asynchronous compute models
*   Develop for autoscaling
    - Implement autoscaling rules and patterns (schedule, operational/system metrics, code that addresses singleton application instances, and code that addresses transient state
*   Implement distributed transactions
    - Identify tools to implement distributed transactions (e.g., ADO.NET, elastic transactions, multi-database transactions)
    - Manage transaction scope
    - Manage transactions across multiple databases and servers
*   Develop advanced cloud workloads
    - Develop solutions by using intelligent algorithms that identify items from images and videos
    - Develop solutions by using intelligent algorithms related to speech, natural language processing, Bing Search, and recommendations and decision making
    - Create and integrate bots
    - Integrate machine learning solutions in an app
    - Create and implement IoT solutions