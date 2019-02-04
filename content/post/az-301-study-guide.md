---

tags: ["Azure", "Certifications"]
title: "AZ-301 Microsoft Azure Architect Design"
hide: true
---

## Intro

<br />

<strong>This is one guide in a series of guides that can act as study material for Microsoft's new Azure oriented certification exams.</strong>

* [AZ-203 Exam Guide](/post/az-203-study-guide)
* [AZ-300 Exam Guide](/post/az-300-study-guide)

<br />

The AZ-301 exam, which is titled "Microsoft Azure Architect Design" and has a target audience of Azure Solution Architects. This exam was in beta for some time and was recently released proper in January 2019. Architects used to the Microsoft certification world will see this exam as a replacement for 70-535, which is the older iteration of Azure technology geared for architects. Passing this exam is one requirement to earn the "Microsoft Certified Azure Solutions Architect Expert" certification, the other being AZ-300. Going forward, most Microsoft certifications are moving to a [job-role based](https://buildazure.com/2018/09/24/introducing-role-based-microsoft-azure-certification-shakeup/) (great take by Chris Pietschmann at Build Azure) approach, which in my opinion is a good move, as it allows folks to focus on passing exams that contain content that will directly be used on the job. For more information about the exam, take a look at the [Official Exam Page](https://www.microsoft.com/learning/exam-AZ-301.aspx)

<br />
## Skills Measured


<br />
Here’s a high level list of the skills and objectives measured on the AZ-301 Microsoft Azure Architect Design certification exam. The percentages next to each objective area represents the number of questions in that objective area on the exam. I have included links to docs, code samples, and other resources that I have found helpful as I studied. Hope you enjoy!

<br />

**Determine Workload Requirements (10-15%)**

<br />

*   Gather Information and Requirements
    - Identify compliance requirements, identity and access management infrastructure, and service-oriented architectures (e.g., integration patterns, service design, service discoverability)
    - Identify accessibility (e.g. Web Content Accessibility Guidelines), availability (e.g. Service Level Agreement), capacity planning and scalability, deploy-ability (e.g., repositories, failback, slot-based deployment), configurability, governance, maintainability (e.g. logging, debugging, troubleshooting, recovery, training), security (e.g. authentication, authorization, attacks), and sizing (e.g. support costs, optimization) requirements
    - Recommend changes during project execution (ongoing)
    - Evaluate products and services to align with solution
    - Create testing scenarios
*   Optimize Consumption Strategy
    -  Optimize app service, compute, identity, network, and storage costs
*   Design an Auditing and Monitoring Strategy
    - Define logical groupings (tags) for resources to be monitored
    - Determine levels and storage locations for logs
    - Plan for integration with monitoring tools
    - Recommend appropriate monitoring tool(s) for a solution
    - Specify mechanism for event routing and escalation
    - Design auditing for compliance requirements
    - Design auditing policies and traceability requirements

<br />

**Design for Identity and Security (20-25%)**

<br />

*   Design Identity Management
    - Choose an identity management approach
    - Design an identity delegation strategy, identity repository (including directory, application, systems, etc.)
    - Design self-service identity management and user and persona provisioning
    - Define personas and roles
    - Recommend appropriate access control strategy (e.g., attribute-based, discretionary access, history-based, identity-based, mandatory, organization-based, role-based, rule-based, responsibility-based)
*   Design Authentication
    - Choose an authentication approach
    - Design a single-sign on approach
    - Design for IPSec, logon, multi-factor, network access, and remote authentication
*   Design Authorization
    - Choose an authorization approach
    - Define access permissions and privileges
    - Design secure delegated access (e.g., oAuth, OpenID, etc.)
    - Recommend when and how to use API Keys.
*   Design for Risk Prevention for Identity
    - Design a risk assessment strategy (e.g., access reviews, RBAC policies, physical access)
    - Evaluate agreements involving services or products from vendors and contractors
    - Update solution design to address and mitigate changes to existing security policies, standards, guidelines and procedures
*   Design a Monitoring Strategy for Identity and Security
    - Design for alert notifications
    - Design an alert and metrics strategy
    - Recommend authentication monitors

<br />

**Design a Data Platform Solution (15-20%)**

<br />

*   Design a Data Management Strategy
    - Choose between managed and unmanaged data store
    - Choose between relational and non-relational databases
    - Design data auditing and caching strategie
    - Identify data attributes (e.g., relevancy, structure, frequency, size, durability, etc.)
    - Recommend Database Transaction Unit (DTU) sizing
    - Design a data retention policy
    - Design for data availability, consistency, and durability
    - Design a data warehouse strategy
*   Design a Data Protection Strategy
    - Recommend geographic data storage
    - Design an encryption strategy for data at rest, for data in transmission, and for data in use
    - Design a scalability strategy for data
    - Design secure access to data
    - Design a data loss prevention (DLP) policy
*   Design and Document Data Flows
    - Identify data flow requirements
    - Create a data flow diagram
    - Design a data flow to meet business requirements
    - Design a data import and export strategy
*   Design a Monitoring Strategy for the Data Platform
    - Design for alert notifications
    - Design an alert and metrics strategy

<br />

**Design a Business Continuity Strategy (15-20%)**

<br />

*   Design a Site Recovery Strategy
    - Design a recovery solution
    - Design a site recovery replication policy
    - Design for site recovery capacity and for storage replication
    - Design site failover and failback (planned/unplanned)
    - Design the site recovery network
    - Recommend recovery objectives (e.g., Azure, on-prem, hybrid, Recovery Time Objective (RTO), Recovery Level Objective (RLO), Recovery Point Objective (RPO))
    - Identify resources that require site recovery
    - Identify supported and unsupported workloads
    - Recommend a geographical distribution strategy
*   Design for High Availability
    - Design for application redundancy, autoscaling, data center and fault domain redundancy, and network redundancy
    - Identify resources that require high availability
    - Identify storage types for high availability
*   Design a disaster recovery strategy for individual workloads
    - Design failover/failback scenario(s)
    - Document recovery requirements
    - Identify resources that require backup
    - Recommend a geographic availability strategy
*   Design a Data Archiving Strategy
    - Recommend storage types and methodology for data archiving
    - Identify requirements for data archiving and business compliance requirements for data archiving
    - Identify SLA(s) for data archiving

<br />

**Design for Deployment, Migration, and Integration (10-15%)**

<br />

*   Design Deployments
    - Design a compute, container, data platform, messaging solution, storage, and web app and service deployment strategy
*   Design Migrations
    - Recommend a migration strategy
    - Design data import/export strategies during migration
    - Determine the appropriate application migration, data transfer, and network connectivity method
    - Determine migration scope, including redundant, related, trivial, and outdated data
    - Determine application and data compatibility
*   Design an API Integration Strategy
    - Design an API gateway strategy
    - Determine policies for internal and external consumption of APIs
    - Recommend a hosting structure for API management

<br />

**Design an Infrastructure Strategy (15-20%)**

<br />

*   Design a Storage Strategy
    - Design a storage provisioning strategy
    - Design storage access strategy
    - Identify storage requirements
    - Recommend a storage solution and storage management tools
*   Design a Compute Strategy
    - Design compute provisioning and secure compute strategies
    - Determine appropriate compute technologies (e.g., virtual machines, functions, service fabric, container instances, etc.)
    - Design an Azure HPC environment
    - Identify compute requirements
    - Recommend management tools for compute
*   Design a Networking Strategy
    - Design network provisioning and network security strategies
    - Determine appropriate network connectivity technologies
    - Identify networking requirements
    - Recommend network management tools
*   Design a Monitoring Strategy for Infrastructure
    - Design for alert notifications
    - Design an alert and metrics strategy