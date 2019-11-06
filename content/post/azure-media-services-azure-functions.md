---
date: 2018-06-12T11:25:05-04:00
tags: ["Azure", "C#", "Azure Functions"]
title: "Azure Media Services with Azure Functions"
repo: "https://github.com/isaacrlevin/AzureMediaServicesDemo"

---

## New Version of Azure Media Services

<br />

I have been a fan of Azure Media Services for a long time. I worked with a customer roll it our to their organization a few years back and immediately saw the opportunity to enable a company with the ability to wholly own their video content and host/play the media inside their organization. Previously, the SDK that interfaced with Azure Media Services, consisted of 2 External Packages

<br />

* windowsazure.mediaservices
* windowsazure.mediaservices.extensions

<br />

Pulling those down and you could be off and running working against Azure Media Services. There are some great tutorials on getting started using the older version of the SDK. Here are some

<br />

[Getting Started with Azure Media Services](https://docs.microsoft.com/azure/media-services/previous/media-services-dotnet-get-started)

[Building a YouTube like Media Portal using ASP.NET MVC and Azure Media Services](http://www.dotnetcurry.com/windows-azure/924/azure-media-services-youtube-media-portal-aspnet-mvc)

<br />

One thing to mention is that the above referenced SDKs have not been updated in 7 months, and I would assume any no n-break fix type of updates to not happen in this SDK. The team has moved on to v3 of the SDK, which functions using Azure Resource Manager, the new template-driven process to interact with Azure Resources. Using ARM allows us to build a configurable environment with a json schemed file that will when deployed to Azure, builds our environment for us, eliminating the headache of doing it by hand. Be sure to check out the docs on Azure Media Services v3

<br />

[Azure Media Services v3 Overview](https://docs.microsoft.com/azure/media-services/latest/media-services-overview)

<br />

## Working against Azure Media Services in a New Way

<br />

In that example, I used an Azure App Service Web Job that polled an Azure Storage Queue for messages and took those messages and got the video to process from Blob Storage, move them to an Azure Media Service Asset and encode the video. This worked very well, but I was responsible for wiring up the plumbing for the polling process, which even though fairly straightforward, is a pain that I shouldn't need to write. That is where Azure Functions is so great, as it has Triggers that allow developers to subscribe to events and all the plumbing is done for you. In my case, I want to "watch" a Azure Storage Blob Container for new videos to be uploaded to, and process the video that gets uploaded. This is simply done by setting the trigger for your Azure Function to Blob. The function that gets created will look like so.

<br />

```csharp
public static class ProcessVideo
{
    [FunctionName("ProcessVideo")]
    public async static Task Run([BlobTrigger("input/{name}", Connection = "AzureWebJobsStorage")]Stream myBlob, string name, ILogger log)
    {
        log.LogInformation($"C# Blob trigger function Processing blob\n Name:{name} \n Size: {myBlob.Length} Bytes");
        log.LogInformation($"C# Blob trigger function Processed");
    }
```

<br />

So the above function will listen on the "input" container of the AzureWebJobsStorage Blob Storage account, and log the size of the blob. That is it, all the pomp and circumstance of listening on something by hand is done for us. And the best part, since Azure Functions run on consumption time, we only pay for the time the function runs, not 24/7. So Azure Functions is a superb solution for this task.

<br />

## Plugging in Azure Media Services Interactions

<br />

As I mentioned, the v3 version of Azure Media Services uses a different approach to interacting with Azure Media Services. The new process uses Azure Active Directory to authenticate requests to Azure Media Services with a Service Principal. A service principal basically is a mechanism allows the handshake with your application and Azure AD. You can read more on that here.

<br />
[https://docs.microsoft.com/en-us/azure/active-directory/develop/active-directory-application-objects](https://docs.microsoft.com/en-us/azure/active-directory/develop/active-directory-application-objects)

<br />

The process to do this is pretty straightforward, and I am not going to list them all here, so I will point you at the doc that shows how to do that.

<br />
[https://docs.microsoft.com/en-us/azure/media-services/latest/stream-files-dotnet-quickstart](https://docs.microsoft.com/en-us/azure/media-services/latest/stream-files-dotnet-quickstart
)

<br />

This doc is actually more than just the auth part, this is all the code to get a Console Application working against Azure Media Services v3. I took this code base, and repurposed it as an Azure Function, YAAAAY for "borrowing" code from others. Take a look at the doc to get more detail about the process to interact with Azure Media Services, but in summary:

<br />

1. Supply vide file to process
2. Create AzureMediaServicesClient with Service Principal credentials
3. Get or Create Azure Media Services Transform, which is used to defined AMS workflows of tasks
4. Create an InputAsset for Azure Media Services to process (more on that below)
5. Create a job based on the transform and input specified, to new OutputAsset
6. Run and monitor Job
7. Once finished, get output asset and publish it to stream

<br />

That is the straightforward workflow to working with videos against Azure Media Services. In the above sample. The media file that is encoded is an already online file, and static. For our process, we want to take the file that is uploaded to Blob Storage and create an input asset from it. Here is a snippet to do that, but in just:

<br />

1. Get reference to exisiting file in Blob Storage
2. Create reference to new file in input asset location
3. Copy the file from one area to another

<br />

```csharp
private async Task<Asset> CreateInputAssetAsync(
    IAzureMediaServicesClient client,
    string resourceGroupName,
    string accountName,
    string assetName,
    string fileToUpload)
{
    Asset asset = await client.Assets.CreateOrUpdateAsync(resourceGroupName, accountName, assetName, new Asset());

    var response = await client.Assets.ListContainerSasAsync(
        resourceGroupName,
        accountName,
        assetName,
        permissions: AssetContainerPermission.ReadWrite,
        expiryTime: DateTime.UtcNow.AddHours(4).ToUniversalTime());

    var sasUri = new Uri(response.AssetContainerSasUrls.First());

    _log.LogInformation($"Uploading {fileToUpload}");

    _storageHelpers.CopyBlobAsync(sasUri, fileToUpload).Wait();
    _log.LogInformation("Upload Complete");

    asset = client.Assets.GetAsync(resourceGroupName, accountName, assetName, new System.Threading.CancellationToken()).Result;
    return asset;
}
```

<br />

CopyBlobAsync() does what you would expect it to do, see the finished repo to see what it actually does. The rest of the code is basically left as is. Since I want to view these videos in some way, I created a sample Web App to view the videos using the streaming endpoint URLs. I am storing the video name and the URL in Azure Table Storage as an easy way to handle this. The Web App is in the final repo to view as well. A major difference here is that I refactored the code into services, which allows me to use Dependency Injection to have a more robust process for handling these dependencies. This leads me to the most challenging part of this exercise.

<br />

## Dependency Injection of Services is not out of the box supported in Azure Functions!

<br />

Yea you read the above right, there isn't a story currently to have full-fledged Dependency Injection experience (like the one in ASP.NET Core). This is currently being looked into by the Azure Functions team, but currently in preview v2 of Functions, you have to do it yourself.

<br />

So I did a lot of research into this and found an excellent repo on wiring this up, which saved me a ton of time. What you need to do get DI with Azure Functions is Configure the ExtensionProvider (just how it is done with ASP.NET Core). Once the provider is Initialized, we have to configure the ServiceProvider. This is where we can extend the injected services to include many things (I have done EntityFrameworkCore in the past) such as ILogger<T> (not supported by default with Functions) and any services we want to leverage, which I did here. The way I did it allowed me to create a Startup class similar to how ASP.NET Core handles it. My Startup looks like this

<br />

```csharp
public class Startup
{
    private readonly ILogger<Startup> _logger;

    public Startup(ILogger<Startup> logger)
    {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }
    public void ConfigureServices(IServiceCollection services)
    {
        services.AddSingleton(typeof(ConfigWrapper));
        services.AddTransient<IStorageHelper, StorageHelper>();
        services.AddTransient<IAzureMediaServicesHelper, AzureMediaServicesHelper>();
        services.AddTransient<IVideoService, VideoService>();
    }

    public void Configure(IConfigurationBuilder app)
    {
        var executingAssembly = new FileInfo(Assembly.GetExecutingAssembly().Location);
        _logger.LogInformation($"Using \"{executingAssembly.Directory.FullName}\" as base path to load configuration files.");
        app
            .SetBasePath(executingAssembly.Directory.FullName)
            .AddJsonFile("local.settings.json", optional: true, reloadOnChange: true)
            .AddEnvironmentVariables();
    }
}
```

<br />

As you can see, I have many services registered, as well as ILogger. Take a look at the final repo on how to wire up dependencies in your services. For wiring up our function with ILogger<T> and one of our services, we modify our run method to look like so.

<br />

```csharp
[FunctionName("ProcessVideo")]
public async static Task Run([BlobTrigger("input/{name}", Connection = "AzureWebJobsStorage")]Stream myBlob, string name, [Inject(typeof(ILoggerFactory))]ILoggerFactory loggerFactory, [Inject(typeof(IVideoService))]IVideoService videoService)
{
    var log = loggerFactory.CreateLogger(typeof(ProcessVideo).FullName);
    log.LogInformation($"C# Blob trigger function Processing blob\n Name:{name} \n Size: {myBlob.Length} Bytes");
    await videoService.AddVideo(name);
    log.LogInformation($"C# Blob trigger function Processed");
}
```

<br />

It doesn't look that pretty, but we have to use Reflection to get the loaded type at runtime for the resolution to work appropriately. AddVideo() just calls into the above defined workflow.

<br />

## AND DONE!!!

<br />

When deployed, my function will listen on the specified container, encode any videos placed in the container, and store metadata used to view the video at a later time. The final repo has steps needed to deploy this to your own Azure as well.

<br />

## Gotchas with Azure Functions at this Point

<br />

I mentioned the Dependency Injection limitation with Azure Functions, but there are 2 more to be aware of when working with Azure Media Services v3.

<br />

AMS v3 uses Azure Active Directory to authenticate requests. This is done using the Microsoft.IdentityModel.Clients.ActiveDirectory nuget package. The issue is that there is a Azure Functions using newer versions of the package, issue below

<br />

[https://github.com/Azure/azure-functions-host/issues/2373](https://github.com/Azure/azure-functions-host/issues/2373)

<br />

So in order to get our function to authenticate against Azure AD, we have to provide a hard reference to 3.14.0. Adding that package resolves that issue.

<br />

Azure Media Services also utilizes the Azure Storage SDK under the covers to store the input and output assets. There is a known issue with a version mismatch of WindowsAzure.Storage.

<br />

[https://github.com/Azure/Azure-Functions/issues/821](https://github.com/Azure/Azure-Functions/issues/821)

<br />

In order to resolve this, we need a hard reference to 8.6.0 of that package, and then we are working!

<br />

## Thanks for Reading!

<br />

Thank you so much for reading, I hope you take a look at the repo and see how cool Azure Media Services v3 is and how you can use Azure Functions to leverage it.