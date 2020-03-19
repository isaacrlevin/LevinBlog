---
date: 2020-03-19T11:15:58-04:00
tags: ["c#", "Azure Functions", "Azure", "GitHub"]
title: "Storing GitHub Traffic with Azure Functions"
repo: "https://github.com/isaacrlevin/GitHubStatTracker"
---

## Frustrated by GitHub Insights

<br />

As an owner of a handful of large public repos for my job, one of the things that I care about is knowing the traffic to the repos to get an understanding of visibility. GitHub has this amazing feature, called [GitHub Insights](https://github.com/features/insights), which allows you to see some of this data, however it is capped at a date range of 14 days in the past. I am not sure on the long-term goals of Insights, but if that cap stays, it will frustrate folks, some of them very well known [folks](https://github.com/isaacs/github/issues/399).

<br />

## Storing Traffic Data Over Time

<br />

I have seen a few ways people are getting around this date cap, written in all sorts of languages on all sorts of platforms. They all had one thing in common, they all use the [GitHub Api Traffic Endpoint](https://developer.github.com/v3/repos/traffic/) to get this data. This Api has all the info I need in it, so thinking like a lazy developer, here are my options.

<br />

 - Go to the Traffic Pages for all my repos twice a month and collect the data (yea.... no)
 - Write some tool that will "poll" the GitHub Api and store the results of the Api in some data store

<br />

## Azure Functions Saving Me Again

<br />

It seems like whenever I blog, the topic is usually some silly task that I have that I automate with Azure Functions, and this is a silly task, time to automate! The first things that I needed to do was spin up Visual Studio (VS Code works has great tooling for Functions as well) and create a new Azure Functions Project. After that, time to plan out what I needed to do, which I concluded was

<br />

- Hit the GitHub Api to get the Traffic for some repos
- Store that data somewhere I can easily build a Power BI report with

<br />

I wanted to get a working prototype as soon as possible, so I just picked a random repo out of the many I have, wired up the call to get the traffic data, which looked like

<br />

```csharp
var client = new GitHubClient(new ProductHeaderValue("TrackRepos"));
var basicAuth = new Credentials(Environment.GetEnvironmentVariable("GitHubToken"));
client.Credentials = basicAuth;
var data = await client.Repository.Traffic.GetViews("owner", "repo", new RepositoryTrafficRequest(TrafficDayOrWeek.Day));
```

<br />

I found out very quickly that in order to get Traffic data for a repo, I need push access, so I needed a token. After getting a token from GitHub, I was seeing data come in.

<br />

Now that I had some data, I started to think about the quickest way to get that data into storage. I could put it in a SQL table, but that seemed like a ton of overkill for what I was doing(had to create a server as well) and I realized that I needed an Azure Storage account to store the metadata for Azure Functions, which also has a Table feature in that would allow me to store data (Cosmos DB is an option here, but a little overkill as well). So the next thing I needed to do was structure my data and wire up all the bits to Insert and Update Records in there. Good thing I could "borrow" all this code from the [Table Api Docs](https://docs.microsoft.com/azure/cosmos-db/tutorial-develop-table-dotnet) so that was solved pretty quick. At this point, I wire it all up and I have a function that hits the GitHub Api and stores the results in Azure Table Storage.

<br />

### Scaling for More Repos

<br />

Now that I have something working, time to build some code that I can use to collect data for all my repos. I came to an idea that if I had some JSON representation of all my repos, I could easily alter the list and the function would just use it as I go. I knew I had a hierarchy of sorts in my case.

<br />

 - Campaign Name (Collection of Repos)
 - Organization Name (Also known as the "owner")
    * Repository Name (The Repo itself)

<br />

So I created a class structure and JSON schema that represents that.

<br />

```json
[
  {
    "CampaignName": "",
    "OrgName": "",
    "Repos": [ { "RepoName": "" } ]
  },
  {
    "CampaignName": "",
    "OrgName": "",
    "Repos": [
      { "RepoName": "" }
    ]
  }
]
```

<br />

Now with a schema. I will be able to effectively store and group all my traffic data. I decided to store this JSON file that houses my campaigns as a blob in the same Azure Storage account as the data store, reuse is awesome! So the flow my code looks like this

<br />

- Read the JSON campaigns from Blob Storage
- Loop through all the campaigns and repos in campaigns
- Call GitHub Api to get traffic data
- Build POCO that represents a row in Azure Table Storage
- Store said data

<br />

A list is cool, but what about the code?

```csharp
var client = new GitHubClient(new ProductHeaderValue("TrackRepos"));

var basicAuth = new Credentials(Environment.GetEnvironmentVariable("GitHubToken"));
client.Credentials = basicAuth;

string storageConnectionString = Environment.GetEnvironmentVariable("AzureWebJobsStorage");
BlobServiceClient blobServiceClient = new BlobServiceClient(storageConnectionString);
BlobContainerClient containerClient = blobServiceClient.GetBlobContainerClient("repos");
BlobClient blobClient = containerClient.GetBlobClient("Repos.json");
BlobDownloadInfo download = await blobClient.DownloadAsync();

List<Campaign> campaigns = JsonConvert.DeserializeObject<List<Campaign>>(new StreamReader(download.Content).ReadToEnd());

List<RepoStats> views = new List<RepoStats>();

foreach (Campaign campaign in campaigns)
{
    if (!string.IsNullOrEmpty(campaign.CampaignName) && !string.IsNullOrEmpty(campaign.OrgName))
        foreach (Repo repo in campaign.Repos)
        {
            if (!string.IsNullOrEmpty(repo.RepoName))
            {
                var data = await client.Repository.Traffic.GetViews(campaign.OrgName, repo.RepoName, new RepositoryTrafficRequest(TrafficDayOrWeek.Day));
                foreach (var item in data.Views)
                {
                    var stat = new RepoStats($"{campaign.CampaignName}{repo.RepoName}", item.Timestamp.UtcDateTime.ToShortDateString().Replace("/", ""))
                    {
                        OrgName = campaign.OrgName,
                        CampaignName = campaign.CampaignName,
                        RepoName = repo.RepoName,
                        Date = item.Timestamp.UtcDateTime.ToShortDateString(),
                        Views = item.Count,
                        UniqueUsers = item.Uniques
                    };
                    views.Add(stat);
                }
                Thread.Sleep(3000);
            }
        }
}

string tableName = "RepoStats";
CloudTable table = await TableStorageHelper.CreateTableAsync(tableName);

foreach (var view in views)
{
    Console.WriteLine("Insert an Entity.");
    await TableStorageHelper.InsertOrMergeEntityAsync(table, view);
}
```

<br />

I am glossing over a bit here, but most of the code you don't see is boilerplate, and you can look at the repo if you are curious. Now all I needed to do was hook it up to a function that runs on a CRON timer for once a day, and reap the rewards.

<br />
### Hope you enjoy!

<br />
I had fun with this little project, and it solved a need, exactly what I love. From idea to deployed it took a little less than an hours, man Azure Functions are awesome. As always, feel free to let me know your thoughts. TTFN!