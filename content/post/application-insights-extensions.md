---
date: 2018-03-01T11:00:59-04:00
tags: ["Azure", "Application Insights", "ASP.NET Core"]
title: "Extensions to Application Insights Telemetry Collection"
repo: "https://github.com/isaacrlevin/ApplicationInsightsTelemetryExtensions"
---

## Extending the Greatness of Application Insights

<br />

I will start off by saying I love Application Insights. I have been using it for a long time, and am delighted at the new roll-out of features for it. I have even been giving a talk on Application Insights and how easy it is to instrument your application, so check that out if you are interested. One thing that is great about Application Insights is how extendable it is. The nature of how the data is structured allows a developer to add custom metadata to the telemetry, as well as add filter out telemetry based on specific criteria. Whenever I spin up a new app, I always notice that I add a handful of extensions to the telemetry collection process and thought it would be helpful to share.

<br />

## Capturing POST/PUT Body on Request

<br />

As mentioned, you are able to add custom metadata to your telemetry collection tracking requests to extend the datapoints that you can query from a reporting standpoint. I write alot of APIs and one thing that is helpful when analyzing request information is the "state" of the request itself, ie parameters passed to the API. One way I do this is by adding a the body of HTTP POST and PUT requests to the telemetry request. You can add this metadata using ITelemetryInitializer by creating a custom Initializer. To add the Initializer, you just create a class that inherits from ITelemetryInitializer

<br />

```csharp
    public class RequestBodyInitializer : ITelemetryInitializer {}
```

<br />

In order to get access to HttpContext, which is needed to get the POST/PUT body, we will need to inject an instance of the IHttpContextAccessor service into our Initializer.

<br />

```csharp
    private IHttpContextAccessor _httpContextAccessor;
    public RequestBodyInitializer(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = _httpContextAccessor ?? throw new ArgumentNullException("httpContextAccessor");
    }
```

<br />

Now that we have access to HttpContext, we can check to see if the Request Method is POST or PUT, and if so, read the request body as a stream and transpose it to a string. The one gotcha here is that HttpContext.Request.Body is a read-once member, which means that one you retrieve it once, it gets disposed, so any additional attempt to retrieve it will throw an ObjectDisposedException. A workaround I have found is to reset the Position on the body to solve this problem. All that is needed to be able to do that is to EnableRewind() on the request. However, based on the nature of the Telemetry request, the Initializer can be called twice on occasion, so to be safe, I wrap the interaction with the request body with a try catch.

<br />

```csharp
    public void Initialize(ITelemetry telemetry)
    {
        if (telemetry != null && telemetry is RequestTelemetry)
        {
            var requestTelemetry = telemetry as RequestTelemetry;
            var httpContext = _httpContextAccessor.HttpContext;
            if ((httpContext.Request.Method == HttpMethods.Post.ToString() || httpContext.Request.Method == HttpMethods.Put.ToString()) && httpContext.Request.Body.CanRead)
            {
                try
                {
                    httpContext.Request.EnableRewind();
                    string bodyContent = new StreamReader(httpContext.Request.Body).ReadToEnd();
                    httpContext.Request.Body.Position = 0;
                    requestTelemetry.Properties.Add("body", bodyContent);
                }
                catch (ObjectDisposedException) { }
           }
       }
    }
```

<br />

Lastly, we will need to wire up our Initializer as well as IHttpContextAccessor in Startup.cs

<br />

```csharp
    public void ConfigureServices(IServiceCollection services)
    {
        services.AddMvc();
        services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

        services.AddSingleton<ITelemetryInitializer, RequestBodyInitializer>();
    }
```

<br />

## Logging Authenticated User on Secure Requests

<br />

One very important metric that Application Insights collects is Session and User information, which can be extended to include the authenticated users identity for secure requests. Is would be very helpful when running reports based on particular user actions on your site. The process is very similar to the approach above to get the request body, since the User's Identity is on the HttpContext. We will again need to wire up the IHttpContextAccessor as before. After that, all that is needed is to extract the Identity if the User is Authenticated and than set the AuthenticatedUserId of the User instance on the telemetry context.

<br />

```csharp
    public class LoggedInUserInitializer : ITelemetryInitializer
    {
        private IHttpContextAccessor _httpContextAccessor;
        public LoggedInUserInitializer(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor ?? throw new ArgumentNullException("httpContextAccessor");
        }
        public void Initialize(ITelemetry telemetry)
        {
            if (telemetry != null && telemetry is RequestTelemetry)
            {
                var httpContext = _httpContextAccessor.HttpContext;
                if (httpContext != null && httpContext.User.Identity.IsAuthenticated == true && httpContext.User.Identity.Name != null)
                {
                    telemetry.Context.User.AuthenticatedUserId = httpContext.User.Identity.Name;
                }
            }
        }
    }
```

<br />

Lastly, like before you will need to register the service in Startup.cs

<br />

```csharp
    services.AddSingleton<ITelemetryInitializer, LoggedInUserInitializer>();
```

<br />

## Filtering Out Requests from Bots

<br />

Application Insights allows us to filter or prevent certain telemetry from being collected based on logic we specify. This technique utilizes ITelemetryProcessor to not log certain items. One example I like to use concerns web crawelers or bots. If you have a public facing website, your site might be crawled by bots often, which can skew your telemetry around page requests, which can be a problem to people in the business. If you have concerns about logging these requests, you can setup a Processor to not log them. The most web crawlers look for a robots.txt file to map your site. Adding simple logic to not log those requests is straightforward.

<br />

```csharp
    /// <summary>
    /// Processor filters out requests for robots.txt, which can skew request numbers if not filtered in reporting
    /// </summary>
    public class BotRequestTracking : ITelemetryProcessor
    {
        public BotRequestTracking(ITelemetryProcessor next)
        {
            Next = next;
        }

        private ITelemetryProcessor Next { get; set; }

        public void Process(ITelemetry item)
        {
            var request = item as RequestTelemetry;
            if (request != null)
            {

                if (request.Name.ToLower().Contains("robots.txt"))
                {
                    return;
                }
            }

            Next.Process(item);
        }
    }
```

<br />

What this Processor is doing is seeing if the request name contains the string "robots.txt" and if it does, do not process the request. Once you have this, all that is needed is for you to register the Processor in the Configure method in Startup.cs

<br />

```csharp
    // snippet from Startup.cs
    var configuration = app.ApplicationServices.GetService<TelemetryConfiguration>();
    configuration.TelemetryProcessorChainBuilder
                            .Use(next => new BotRequestTracking(next))
                            .Build();
```

<br />

What this does is loads up the configuration for Application Insights and appends our new processor to the Application Insights Pipeline. At this point in time, not robots.txt requests will be collected.

<br />

## The Options are Limitless

<br />

I hope I have conveyed the pure potential of Applicaiton Insights and how adding not much code allows developers to enrich the telemetry collection process. The Application Insights Team has added a handful of interesting Initializers on the public repo. I have found that the architecture of the how the telemetry is stored is ideal for extending or filtering based on business needs. If you are using Application Insights (I want to talk to you if you don't) to collect information about your apps, I urge you to look at how you can enhance the experience. Thanks for reading.