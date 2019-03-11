---
date: 2019-03-11T11:00:59-04:00
tags: ["Azure", "Certifications"]
title: "AZ-400 Microsoft Azure DevOps Solutions"

---

## Intro

<br />

<strong>This is one guide in a series of guides that can act as study material for Microsoft's new Azure oriented certification exams.</strong>

* [AZ-203 Exam Guide](/post/az-203-study-guide)

<br />

The AZ-400 exam, which is titled "Microsoft Azure DevOps Solutions" and has a target audience of Azure DevOps Engineers. This exam was in beta for some time and was recently released proper in February 2019. Passing this exam is one requirement to earn the "Microsoft Certified: Azure DevOps Engineer" certification, the other being an "Azure Administrator Associate" or "Azure Developer Associate". Going forward, most Microsoft certifications are moving to a [job-role based](https://buildazure.com/2018/09/24/introducing-role-based-microsoft-azure-certification-shakeup/) (great take by Chris Pietschmann at Build Azure) approach, which in my opinion is a good move, as it allows folks to focus on passing exams that contain content that will directly be used on the job. For more information about the exam, take a look at the [Official Exam Page](https://www.microsoft.com/learning/exam-AZ-400.aspx)

<br />
## Skills Measured


<br />
Here’s a high level list of the skills and objectives measured on the AZ-400 Microsoft Azure DevOps Solutions certification exam. The percentages next to each objective area represents the number of questions in that objective area on the exam. I have included links to docs, code samples, and other resources that I have found helpful as I studied. Hope you enjoy!

<br />

### Design a DevOps Strategy (20-25%)

<br />

*   Recommend a migration and consolidation strategy for DevOps tools
    -   Analyze existing artifact (e.g. deployment packages, NuGet) and container repositories ([Azure Artifacts](https://docs.microsoft.com/en-us/azure/devops/pipelines/artifacts/artifacts-overview))
    -   Analyze existing test management tools ([Azure Test Plans](https://azure.microsoft.com/en-us/services/devops/test-plans/))
    -   Analyze existing work management tools ([Azure Boards](https://docs.microsoft.com/en-us/azure/devops/boards/get-started))
    -   Recommend migration and integration strategies for artifact repositories, source control, test management, and work management ([Azure DevOps](https://docs.microsoft.com/en-us/azure/devops/user-guide/what-is-azure-devops-services?view=azure-devops))
*   Design and implement an Agile work management approach
    -   Identify and recommend project metrics, KPIs, and DevOps measurements (e.g. [cycle time, lead time](https://docs.microsoft.com/en-us/azure/devops/report/dashboards/cycle-time-and-lead-time?view=azure-devops), Azure Kubernetes Service, [WIP limit](https://docs.microsoft.com/en-us/azure/devops/boards/boards/wip-limits?view=azure-devops))
    -   Implement tools and processes to support Agile work management ([Agile Culture](https://docs.microsoft.com/en-us/azure/devops/boards/plans/agile-culture?view=azure-devops))
    -   Mentor team members on Agile techniques and practices ([About teams and Agile tools](https://docs.microsoft.com/en-us/azure/devops/organizations/settings/about-teams-and-settings?toc=/azure/devops/boards/plans/toc.json&bc=/azure/devops/boards/plans/breadcrumb/toc.json&view=azure-devops))
    -   Recommend an organization structure that supports scaling Agile practices ([Implement Scaled Agile Framework® to support epics, release trains, and multiple backlogs](https://docs.microsoft.com/en-us/azure/devops/boards/plans/scaled-agile-framework?view=azure-devops))
    -   Recommend in-team and cross-team collaboration mechanisms
*   Design a quality strategy
    -   Analyze existing quality environment ([Analyze test results](https://docs.microsoft.com/en-us/azure/devops/pipelines/test/test-analytics?view=azure-devops))
    -   Identify and recommend quality metrics
    -   Recommend a strategy for feature flag lifecycle ([Explore how to progressively expose your features in production for some or all users](https://docs.microsoft.com/en-us/azure/devops/articles/phase-features-with-feature-flags?view=azure-devops))
    -   Recommend a strategy for measuring and managing technical debt ([Managing Technical Debt with Azure DevOps and SonarCloud](https://www.azuredevopslabs.com/labs/azuredevops/sonarcloud/))
    -   Recommend changes to team structure to optimize quality
    -   Recommend performance testing strategy ([Load test with the Azure portal](https://docs.microsoft.com/en-us/azure/devops/test/load-test/app-service-web-app-performance-test?view=azure-devops))
*   Design a secure development process
    -   Inspect and validate code base for compliance ([Code ownership and software quality](https://docs.microsoft.com/en-us/azure/devops/learn/devops-at-microsoft/code-ownership-software-quality))
    -   Inspect and validate infrastructure for compliance
    -   Recommend a secure development strategy ([Learn how to add continuous security validation to your CI/CD pipeline](https://docs.microsoft.com/en-us/azure/devops/articles/security-validation-cicd-pipeline))
    -   Recommend tools and practices to integrate code security validation (e.g. static code analysis) ([Learn how to add continuous security validation to your CI/CD pipeline](https://docs.microsoft.com/en-us/azure/devops/articles/security-validation-cicd-pipeline))
    -   Recommend tools and practices to integrate infrastructure security validation ([Learn how to add continuous security validation to your CI/CD pipeline](https://docs.microsoft.com/en-us/azure/devops/articles/security-validation-cicd-pipeline))
*   Design a tool integration strategy
    -   Design a license management strategy (e.g. Azure DevOps users, [concurrent pipelines](https://docs.microsoft.com/en-us/azure/devops/pipelines/licensing/concurrent-jobs-vsts), test environments, open source software licensing, DevOps tools and services, package management licensing)
    -   Design a strategy for end-to-end traceability from work items to working software ([Linking, traceability, and managing dependencies](https://docs.microsoft.com/en-us/azure/devops/boards/queries/link-work-items-support-traceability?view=azure-devops&tabs=new-web-form))
    -   Design a strategy for integrating monitoring and feedback to development teams
    -   Design an authentication and access strategy ([Default permissions and access for Azure DevOps](https://docs.microsoft.com/en-us/azure/devops/organizations/security/permissions-access?view=azure-devops))
    -   Design a strategy for integrating on-premises and cloud resources

<br />

### Implement DevOps Development Processes (20-25%)

<br />

*   Design a version control strategy
    -   [Recommend branching models](https://docs.microsoft.com/en-us/azure/devops/repos/git/git-branching-guidance)
    -   [Recommend version control systems](https://docs.microsoft.com/en-us/azure/devops/user-guide/source-control)
    -   Recommend code flow strategy ([Release Flow](https://docs.microsoft.com/en-us/azure/devops/learn/devops-at-microsoft/release-flow))
*   Implement and integrate source control
    -   Integrate external source control ([Build source repositories](https://docs.microsoft.com/en-us/azure/devops/pipelines/repos/?view=azure-devops))
    -   Integrate source control into third-party continuous integration and continuous deployment (CI/CD) systems ([Continuously deploy from a Jenkins build](https://docs.microsoft.com/en-us/azure/devops/pipelines/release/integrate-jenkins-pipelines-cicd?view=azure-devops&tabs=yaml))
*   Implement and manage build infrastructure
    -   Implement private and hosted agents ([Build and release agents](https://docs.microsoft.com/en-us/azure/devops/pipelines/agents/agents?view=azure-devops))
    -   Integrate third party build systems
    -   Recommend strategy for concurrent pipelines ([Parallel Jobs](https://docs.microsoft.com/en-us/azure/devops/pipelines/licensing/concurrent-jobs?view=azure-devops))
    -   Manage VSTS pipeline configuration (e.g. agent queues, [service endpoints](https://docs.microsoft.com/en-us/azure/devops/pipelines/library/service-endpoints), [pools](https://docs.microsoft.com/en-us/azure/devops/pipelines/agents/pools-queues), [webhooks](https://docs.microsoft.com/en-us/azure/devops/service-hooks/services/webhooks))
*   Implement code flow
    -   [Implement pull request strategies](https://docs.microsoft.com/en-us/azure/devops/repos/git/pull-requests?view=vsts&tabs=new-nav)
    -   Implement [branch](https://docs.microsoft.com/en-us/azure/devops/repos/git/git-branching-guidance?view=azure-devops) and [fork](https://docs.microsoft.com/en-us/azure/devops/repos/git/forks) strategies]
    -   [Configure branch policies](https://docs.microsoft.com/en-gb/azure/devops/repos/git/branch-policies)
*   Implement a mobile DevOps strategy
    -   Manage mobile target device sets and distribution groups
    -   [Manage target UI test device sets](https://docs.microsoft.com/en-us/appcenter/test-cloud/vsts-plugin)
    -   Provision tester devices for deployment
    -   [Create public and private distribution groups](https://docs.microsoft.com/en-us/appcenter/distribution/groups)
*   Managing application configuration and secrets
    -   Implement a secure and compliant development process
    -   Implement general (non-secret) configuration data
    -   Manage secrets, tokens, and certificates
    -   Implement applications configurations (e.g. Web App, Azure Kubernetes Service, containers)
    -   Implement secrets management (e.g. Web App, Azure Kubernetes Service, containers, [Azure Key Vault](https://docs.microsoft.com/en-us/azure/devops/pipelines/tasks/deploy/azure-key-vault))
    -   Implement tools for managing [security and compliance in the pipeline](https://docs.microsoft.com/en-us/azure/devops/release-notes/2018/sprint-141-update)

<br />

### Implement Continuous Integration (10-15%)

<br />

*   Manage code quality and security policies
    -   Monitor code quality
    -   Configure build to report on code coverage
    -   Manage automated test quality
    -   Manage test suites and categories
    -   Monitor quality of tests
    -   Integrate security analysis tools (e.g. [SonarQube](https://docs.microsoft.com/en-us/azure/devops/java/sonarqube), [WhiteSource Bolt](https://www.azuredevopslabs.com/labs/vstsextend/WhiteSource/), Open Web Application Security Project)
*   Implement a container build strategy
    -   Create deployable images (e.g. Docker, Azure Container Registry) ([Push an image](https://docs.microsoft.com/en-us/azure/devops/pipelines/languages/docker?view=azure-devops&tabs=yaml#push-an-image))
    -   Analyze and integrate Docker multi-stage builds ([Docker](https://docs.microsoft.com/en-us/azure/devops/pipelines/languages/docker?view=azure-devops&tabs=yaml#push-an-image))
*   Implement a build strategy
    -   Design [build triggers](https://docs.microsoft.com/en-us/azure/devops/pipelines/build/triggers), tools, [integrations](https://azure.microsoft.com/en-us/products/devops-tool-integrations/), and workflow
    -   Implement a hybrid build process
    -   Implement multi-agent builds ([Multiple jobs](https://docs.microsoft.com/en-us/azure/devops/pipelines/process/multiple-phases?view=azure-devops&tabs=yaml))
    -   Recommend build tools and configuration
    -   Set up an automated build workflow ([CI Quickstart](https://docs.microsoft.com/en-us/azure/devops/pipelines/get-started-designer?view=azure-devops-2019&tabs=new-nav))

<br />

### Implement Continuous Delivery (10-15%)

<br />

*   Design a release strategy
    -   Recommend release tools ([Why use Azure Pipelines for releases?](https://docs.microsoft.com/en-us/azure/devops/pipelines/release/what-is-release-management?view=azure-devops))
    -   Identify and recommend release approvals and gates ([Release approvals and gates overview](https://docs.microsoft.com/en-us/azure/devops/pipelines/release/approvals/?view=azure-devops))
    -   Recommend strategy for measuring quality of release and release process ([Communicate package quality with release views](https://docs.microsoft.com/en-us/azure/devops/artifacts/feeds/views?view=azure-devops))
    -   Recommend strategy for release notes and documentation ([Azure DevOps Release Notes Generator](https://azure.microsoft.com/en-us/resources/samples/azure-devops-release-notes/))
    -   Select appropriate deployment pattern
*   Set up a release management workflow
    -   Automate inspection of health signals for release approvals by using release gates ([Use approvals and gates to control your deployment](https://docs.microsoft.com/en-us/azure/devops/pipelines/release/deploy-using-approvals?view=azure-devops))
    -   Configure automated integration and [functional test](https://docs.microsoft.com/en-us/azure/devops/pipelines/tasks/test/run-functional-tests) execution
    -   Create a release pipeline (e.g. [Azure Kubernetes Service](https://docs.microsoft.com/en-us/azure/devops/pipelines/apps/cd/deploy-aks?view=azure-devops), [Service Fabric](https://docs.microsoft.com/en-us/azure/devops-project/azure-devops-project-service-fabric), [WebApp](https://docs.microsoft.com/en-us/azure/devops/pipelines/apps/cd/deploy-webdeploy-webapps?view=azure-devops))
    -   [Create multi-phase release pipelines](https://docs.microsoft.com/en-us/azure/devops/pipelines/release/define-multistage-release-process)
    -   Integrate secrets with release pipeline ([Azure Key Vault task](https://docs.microsoft.com/en-us/azure/devops/pipelines/tasks/deploy/azure-key-vault?view=azure-devops))
    -   Provision and configure environments ([Release stages, queuing policies, and options](https://docs.microsoft.com/en-us/azure/devops/pipelines/release/environments?view=azure-devops))
    -   Manage and modularize tasks and templates (e.g. [task](https://docs.microsoft.com/en-us/azure/devops/pipelines/library/task-groups?view=azure-devops) and [variable groups](https://docs.microsoft.com/en-us/azure/devops/pipelines/library/variable-groups?view=azure-devops&tabs=yaml))
*   Implement an appropriate deployment pattern
    -   [Implement blue-green deployments](https://azure.microsoft.com/en-us/blog/blue-green-deployments-using-azure-traffic-manager/)
    -   [Implement canary deployments](https://docs.microsoft.com/en-us/azure/devops/articles/phase-rollout-with-rings?view=azure-devops)
    -   [Implement progressive exposure deployments](https://blogs.msdn.microsoft.com/devops/2018/05/07/release-gates-enable-progressive-exposure-and-phased-deployments/)
    -   Scale a release pipeline to deploy to multiple endpoints (e.g. [deployment groups](https://docs.microsoft.com/en-us/azure/devops/pipelines/release/deployment-groups), [Azure Kubernetes Service](https://docs.microsoft.com/en-us/azure/devops/pipelines/apps/cd/deploy-aks?view=azure-devops), [Service Fabric](https://docs.microsoft.com/en-us/azure/devops-project/azure-devops-project-service-fabric))

<br />

### Implement Dependency Management (5-10%)

<br />

*   Design a dependency management strategy
    -   Recommend artifact management tools and practices ([Azure Artifacts in Azure DevOps Services and Azure DevOps Server](https://docs.microsoft.com/en-us/azure/devops/artifacts/overview?view=azure-devops))
    -   Abstract common packages to enable sharing and reuse ([Collaborate more and build faster with packages](https://docs.microsoft.com/en-us/azure/devops/artifacts/collaborate-with-packages?view=azure-devops))
    -   Inspect codebase to identify code dependencies that can be converted to packages
    -   Identify and recommend standardized package types and versions across the solution
    -   Refactor existing build pipelines to implement version strategy that publishes packages
*   Manage security and compliance
    -   Inspect open source software packages for security and license compliance to align with corporate standards (e.g. GPLv3)
    -   Configure build pipeline to access package security and license rating (e.g. Black Duck, [White Source](https://www.azuredevopslabs.com/labs/vstsextend/WhiteSource/))
    -   [Configure secure access to package feeds](https://docs.microsoft.com/en-us/azure/devops/artifacts/feeds/feed-permissions)

<br />

### Implement Application Infrastructure (15-20%)

<br />

*   Design an infrastructure and configuration management strategy
    -   Analyze existing and future hosting infrastructure
    -   Analyze existing Infrastructure as Code technologies
    -   [Design a strategy for managing technical debt on templates](https://www.azuredevopslabs.com/labs/azuredevops/sonarcloud/)
    -   [Design a strategy for using transient infrastructure for parts of a delivery lifecycle](https://docs.microsoft.com/en-us/dotnet/standard/modernize-with-azure-and-containers/modernize-existing-apps-to-cloud-optimized/reasons-to-modernize-existing-net-apps-to-cloud-optimized-applications)
    -   [Design a strategy to mitigate infrastructure state drift](https://blogs.msdn.microsoft.com/tomholl/2017/10/16/detecting-drift-between-arm-templates-and-azure-resource-groups/)
*   Implement Infrastructure as Code
    -   [Create nested resource templates](https://samcogan.com/modularisation-and-re-use-with-nested-arm-templates/)
    -   [Manage secrets in resource templates](https://docs.microsoft.com/en-us/azure/azure-resource-manager/resource-manager-keyvault-parameter)
    -   [Provision Azure resources](https://blogs.msdn.microsoft.com/azuredev/2017/02/11/iac-on-azure-an-introduction-of-infrastructure-as-code-iac-with-azure-resource-manager-arm-template/)
    -   Recommend an Infrastructure as Code (IaC) strategy ([What is Infrastructure as Code?](https://docs.microsoft.com/en-us/azure/devops/learn/what-is-infrastructure-as-code))
    -   Recommend appropriate technologies for configuration management
*   Manage Azure Kubernetes Service infrastructure
    -   [Provision Azure Kubernetes Service (e.g. using ARM templates, CLI)](https://www.azuredevopslabs.com/labs/vstsextend/kubernetes/)
    -   Create deployment file for publishing to Azure Kubernetes Service (e.g. kubectl, Helm)
    -   [Develop a scaling plan](https://docs.microsoft.com/en-us/azure/devops/organizations/projects/about-projects)
*   Implement infrastructure compliance and security
    -   Implement compliance and security scanning
    -   [Prevent drift by using configuration management tools](https://docs.microsoft.com/en-us/azure/automation/tutorial-configure-servers-desired-state)
    -   Set up an automated pipeline to inspect security and compliance

<br />

### Implement Continuous Feedback (10-15%)

<br />

*   Recommend and design system feedback mechanisms
    -   Design practices to measure end-user satisfaction (e.g. [Send a Smile](https://blogs.msdn.microsoft.com/devops/2018/10/26/azure-devops-feature-suggestions/), app analytics)
    -   Design processes to capture and analyze user feedback from external sources (e.g. Twitter, Reddit, Help Desk)
    -   Design routing for client application crash report data (e.g. HockeyApp)
    -   [Recommend monitoring tools and technologies](https://azure.microsoft.com/en-us/product-categories/management-tools/)
    -   Recommend system and feature usage tracking tools
*   Implement process for routing system feedback to development teams
    -   Configure crash report integration for client applications
    -   [Develop monitoring and status dashboards](https://docs.microsoft.com/en-us/azure/devops/report/dashboards/dashboards)
    -   Implement routing for client application crash report data (e.g. HockeyApp)
    -   Implement tools to track system usage, feature usage, and flow
    -   Integrate and configure ticketing systems with development team's work management system (e.g. IT Service Management connector, ServiceNow Cloud Management, App Insights work items)
*   Optimize feedback mechanisms
    -   Analyze alerts to establish a baseline
    -   Analyze telemetry to establish a baseline
    -   Perform live site reviews and capture feedback for system outages
    -   Perform ongoing tuning to reduce meaningless or non-actionable alerts

