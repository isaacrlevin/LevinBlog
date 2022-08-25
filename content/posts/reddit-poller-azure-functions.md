---
date: 2017-08-03T11:15:58-04:00
tags: ["C#", "Azure Functions", "Azure"]
title: "Polling for Data in Reddit with Azure Functions"
---

## Being Repitive is not fun

<br />

Ever since I can remember I have loved to automate things. Whether it was taking toys and mounting them on remote control car parts or convincing my brother that the show I wanted to watch at a particular time was far better than anything he wanted to watch. I guess those things aren't really automation rather more like a combination of laziness and and ability to make a little effort so something wasn't as contrived the next time... WAIT, ISN'T THAT AUTOMATION?!?!?! Anyway, I have always enjoyed doing things that make my life easier even if I have to do some work to make it happen. One of those things has always been automating things related to the web.

<br />

Like most developers, I have tons of little apps that do various things, whether scheduled jobs to shut off Media servers, or a trigger on some entity to remind you to do something. Combine that with my love of being the first to see something, I got into screen scraping big time. I have wrote little apps, hosted locally and recently in the cloud to parse a site and give me a dump of some data I wanted. The biggest issue I always had was all the setup around getting something to run. First it was spinning up a Cloud Service Worker Role, than it was a Website Web Job, it all seemed so contrived for something as simple as, "Do this thing and give me data". I don't 30 .dlls for that, I don't need to configure IIS or some Windows Service, can't I just run some code somewhere and it works magically?

<br />

## Introducing Azure Functions

<br />

Well yeah, now you can, it's called an Azure Function and it is glorious. There are hundreds of blog posts that go into detail about Azure Functions and all the caveats, for instance

<br />

- [Azure Functions Overview](https://docs.microsoft.com/en-us/azure/azure-functions/functions-overview)
- [Azure Functions Wiki](https://github.com/Azure/Azure-Functions/wiki)
- [Old but good Hanselman Blog on Functions](https://www.hanselman.com/blog/WhatIsServerlessComputingExploringAzureFunctions.aspx)

<br />

## What is something fun I can do with Azure Functions?

<br />

Those are some good resources to find out more about Azure functions, so lets get right into something that you can do fairly easy with Azure Functions. I am on Reddit alot, and if you don't know what Reddit is, I cannot help you since I really don't know what Reddit is either (social network, onlilne newspaper, another place to look at cats) but I do know that there are sections of Reddit called Subreddits where people with a shared interest can converse on that interest. There are subreddits ranging from politics to technology to a place to post photos and have others create epic images of them using photoshop. There are some subs that you can benefit from monitoring the recent pots (free stuff, great blog posts for example). Being able to receive notification when a new posts occurs can gain you an advantage over others, and this is something that automation can help with.

<br />

I know what you are thinking, aren't there things that already do this (IFTTT for instance) but when you use other tools to do things like this, you lose the ability to customize to your heart's content. What if I wanted to be notified when a certain person posted in a certain subreddit and the post contained "strawberry shortcake". WIthout your customizations, you will proabably miss out on that yummy treat, poor you.

<br />

Previously, I had an Azure Web Job running that sent me an email using Google SMTP when new posts happened, and all the previously mentioned things came into play. I want to make that even lighter, so I ported it over to an Azure Function and it was very very easy. So lets try a very simple example of how you can do it as well. I want to be able to run a very simple task that does the following: "Send me an email when a new post appears in /r/Azure". To do this, I have created an Azure Function that runs every minute and uses Send Grid to notify me. Here is the entire Function App

<br />

## All The Code!!!

<br />

```csharp
 public static class RedditPoller
    {
        [FunctionName("TimerTriggerCSharp")]
        public static void Run([TimerTrigger("0 */1 * * * *")]TimerInfo myTimer, TraceWriter log)
        {
            string url = "https://www.reddit.com/r/azure/new/.json";

            List<Child> children = new List<Child>();
            WebClient client = new WebClient();
            string text1 = client.DownloadString(url);
            RedditObject redditObject = JsonConvert.DeserializeObject<RedditObject>(text1);

            foreach (var post in redditObject.Data.Children)
            {
                if (DateTime.UtcNow.AddMinutes(-1) < post.Data.TimeStampDate)
                {
                    children.Add(post);
                    log.Info($"Reddit Post {post.Data.title} found");
                }
            }

            if (children.Count > 0)
            {
                var apiKey = (ConfigurationManager.AppSettings["SendGridApiKey"]);
                var sgClient = new SendGridClient(apiKey);
                var from = new EmailAddress(ConfigurationManager.AppSettings["EmailAddress"]);
                var subject = "New Reddit Post";
                var to = new EmailAddress(ConfigurationManager.AppSettings["EmailAddress"]);
                var plainTextContent = new StringBuilder();
                foreach (var post in children)
                {
                    plainTextContent.AppendLine($"<a href='{post.Data.Url}'>{post.Data.title}</a><br />");
                }
                var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent.ToString(), plainTextContent.ToString());
                sgClient.SendEmailAsync(msg).Wait();
            }
            log.Info($"C# Timer trigger function executed at: {DateTime.Now}");
        }
    }
```

<br />

Ok, a couple of things to mention here. The scheduling for this Azure Function is built off a CRON Syntax so there is a little complexity there, just trust me that 0 */1 * * * * means run every minute. What than happens is I use WebClient to get a json representation of a page that sorts subreddit posts by timestamp. I then serialize that json into an object (that is another file obviously, but it is pretty obvious what it is). I then loop through the "posts" (seen here as members of an object named Children). If the post is newer than a minute, I add it to a list and than build an email message off that list of urls and send it out. WOW that was easy. Let's talk about some gotchas here

<br />

Send Grid needs an Api Key to work, but don't worry you can send up to 25,000 emails a month for free with an Azure Subscription (which is also free). You obviously need an email address to send it to as well, both those things are stored in AppSettings. App Settings when in the cloud are best stored in Azure Portal instead of a file for obvious reasons. That's it, easy right. This is a very simple example but imagine with some extension what you could do here. I will leave that up to you to play around with, in the meantime, feel free to comment on what you like and not comment on what you hated (my writing style for instance). Thanks for reading!