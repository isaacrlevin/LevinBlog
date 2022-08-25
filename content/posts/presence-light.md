---
date: 2020-05-19T21:00:00-02:00
tags: ["Personal", "PresenceLight", "C#", "Azure"]
title: "Building a PresenceLight"
repo: "https://github.com/isaacrlevin/PresenceLight"

---

## I Did A Thing
<br />
Anyone who knows me knows that I am a fairly large "tinkerer", as in, someone who spends a good amount time trying out new things, but eventually gets bored quickly and moves on. One of the things that I have always been a fan of is productivity enhancements, to coincide with my laziness. Due to this, I have created a fair amount of things, but never truly finished any personal project. This time, I actually finished something (I mean as much as you can actually finish something these days) and to be honest, I am pretty proud of it. I have offically launched [PresenceLight](url to store), a Windows Desktop application written in .NET Core 5 that allows folks to manage smart lights in their home. Right now, it works with [LIFX](https://www.lifx.com/)/[Phillips Hue](https://www2.meethue.com/en-us) lights and allows you to do things like set the color of the lights to your Availability in Microsoft Teams, your Windows 10 theme, or frankly a color you just want the lights to be. You can install PresenceLight from the [Microsoft Store](https://www.microsoft.com/en-us/p/presencelight/9nffkd8gznl7), [Chocolatey](https://chocolatey.org/packages/PresenceLight/), on the [Releases tab of the GitHub repo](https://github.com/isaacrlevin/PresenceLight/releases) and from the new [Windows Package Manager](https://docs.microsoft.com/en-us/windows/package-manager). I think one of the things we struggle with as developers is that we aren't good enough to do certain things, imposter syndrome and all that, but we truly have the ability to build great things. I hope that the fact a lazy developer built something that is actually useful to other people besides me encourages people to try to do the same. Ok, enough of this, on to the tech!

<br />

## Finding Your Motivation

<br />
As someone who has worked from home for a fair amount of time in my career (more than half to be exact), one of things that has always intrigued me is how to let your family know when you are "free" vs "busy" as one of the difficult balancing acts of working from home is being able to focus on work tasks. Since I have 2 small kids, this is even more essential. In the past, I used a program called [Skyue](https://blog.thoughtstuff.co.uk/2016/12/announcing-skyue-a-free-skype-for-business-integration-with-phillips-hue/), a free tool built my Microsoft MVP [Tom Morgan](https://twitter.com/tomorgan) to broadcast your Skype for Business status to a Phillips Hue Light, and it was perfect. I used that tool for a long time, but eventually that came to an end when I started using Microsoft Teams, which eventually replaced Skype for Business. I was bummed as there was nothing comparable to Skyue for Teams, and because of the way that Teams set and retrieved Presence (the new term for status), it seemed like it wasn't possible.

<br />

## Behold the Presence Api

<br />

Around November of 2019, I saw some rumblings in UserVoice of exposing Teams Presence via Microsoft Graph, I was curious. Basically the ask was simple, "Let me get Teams Presence via some Api". I read the thread and saw that someone from Microsoft was commenting, so I did what any self-righteous Microsoftie would do, I bothered him. I found him on Teams and asked him for some more detail, and he was kind enough to tell me that Presence would be available in the beta endpoint of Graph in December, so I waited, and then I saw [this](https://developer.microsoft.com/en-us/graph/blogs/microsoft-graph-presence-apis-are-now-available-in-public-preview/). **Huge shoutout to Vinod Ravichandran for being so nice to me!!**

<br />

## Configuring an Azure Active Directory Application

<br />

Reading through that blog post, and the [Graph Api Docs](https://docs.microsoft.com/en-us/graph/api/presence-get), I started to piece together what I needed to do to get up and running, and I ran into my first hurdle. I had to create an Azure Active Directory Application that had scopes to access the new Presence Api, as well as User, not a big deal right? Well yes, because at the time, Presence required [Admin Consent](https://docs.microsoft.com/en-us/azure/active-directory/manage-apps/grant-admin-consent), which is a death nail for someone who works at a company the size of Microsoft. What Admin Consent basically does is force an AAD Admin to grant your app permission on those scopes, which if needed at Microsoft, requires a very particular process. I was able to get the access that I needed, but it was quite challenging for someone who had never done it before.

<br />

**NOTE: Presence no longer requires Admin Consent, so this is no longer an issue for folks that want to get Presence**

<br />

Once I got my app setup with Admin consent, I had to configure it to work with the WPF app that I was planning on building. My goal was to have a desktop app where I can easily login to Azure AD, and it would poll my Presence data. So the next step was to configure OAuth.... Ugh.... All kidding aside, once I found the right approach (and the right configuration since it was a .NET Core App), I knew that I would need to configure a redirect Uri that pointed to http://localhost. This is based on the [MSAL docs](https://github.com/AzureAD/microsoft-authentication-library-for-dotnet/wiki/System-Browser-on-.Net-Core) which basically states that in order to use the built-in system browser, you need to configure your redirect uri to that. The final step before writing some code, was deciding on what supported account types my app uses, ie Single tenant or Multitenant. At first I was using Single, but after some discussions with some really smart Microsoft folks ([Yina Arenas](https://twitter.com/yina_arenas) and [Jason Johnston](https://twitter.com/JasonJohMSFT)), if I went the route of a Multitenant app, any M365 user could use PresenceLight and they would just have to grant my app access to retrieve their Presence. This was a huge deal for me as it allowed an opportunity to have as many folks take advantage of PresenceLight as possible. With the app configured, I could start building some code.

<br />

**For anyone curious, this is what my AAD App Registration page looks like.**

<br />

[{{< figure src="/images/presence-light/aad-page.png" >}}](/images/presence-light/aad-page.png)


## Wiring Up a Client to Use Graph

<br />

So let's look at some code finally. Wiring up a mechanism to call Graph is pretty simple once you are an expert in OAuth, so not simple at all 😃. In all seriousness, how you call the Graph is very dependent on what kind of app you are writing. If you want to build a Client App (mobile or desktop) you need to decide what OAuth Flow to use. Since the flow we are using doesn't require a secret, we can use a Public Client, which we can configure to use [authorization code flow](https://docs.microsoft.com/en-us/azure/active-directory/develop/v2-oauth2-auth-code-flow). So the application will make a request to AAD, which will have the user login. AAD will than ensure the user has consented for the application to access particular scopes (i.e. Presence, Profile), and if it does, will redirect back to the app's redirect uri with a code. The app than takes that code and uses it to get a token from AAD. After that, the app has an access token and you can use that token to query Graph. Easy right??? To be honest, the [Graph SDK](https://github.com/microsoftgraph/msgraph-sdk-dotnet) handles this for you (**NOTE: Presence is in the beta endpoint, so you will need to use the beta SDK**). The Graph SDK works hand in hand with [MSAL](https://github.com/AzureAD/microsoft-authentication-library-for-dotnet) to facilitate all the OAuth required. To wire up a Public Application, you first need to create an authorization provider that you pass into the constructor of the GraphService exposed via the Graph SDK. To do this, I created a custom WPFAuthorizationProvider that inherits from [IAuthenticationProvider](https://docs.microsoft.com/en-us/dotnet/api/microsoft.graph.iauthenticationprovider?view=graph-core-dotnet).

<br />

```csharp
public class WPFAuthorizationProvider : IAuthenticationProvider
{
    public static IPublicClientApplication Application;
    private readonly List<string> _scopes;

    public WPFAuthorizationProvider(IPublicClientApplication application, List<string> scopes)
    {
        Application = application;
        _scopes = scopes;
    }
}
```

<br />

The important thing to call out in this code is that in order to wire-up this provider, I need to create a PublicClientApplication and provide some scopes. So this is what I am dowing below.

<br />

```csharp
private IAuthenticationProvider CreateAuthorizationProvider()
{
    List<string> scopes = new List<string>
    {
        "https://graph.microsoft.com/.default"
    };

   var msalClient = PublicClientApplicationBuilder.Create(_options.ClientId)
                                            .WithAuthority($"{YOUR INSTANCE URI}common/")
                                            .WithRedirectUri(_options.RedirectUri) // for .NET CORE, this needs to be http://localhost/
                                            .Build();

    // wireup MSAL based Caching of tokens (so you don't need to auth everytime)
    TokenCacheHelper.EnableSerialization(pca.UserTokenCache);
    return new WPFAuthorizationProvider(msalClient, scopes);
}
```

<br />

What I have above is a connection to the AAD app that I built above in the Azure Portal. Once this is done, I have to add an override to the [AuthenticateRequestAsync](https://docs.microsoft.com/en-us/dotnet/api/microsoft.graph.iauthenticationprovider.authenticaterequestasync?view=graph-core-dotnet#Microsoft_Graph_IAuthenticationProvider_AuthenticateRequestAsync_System_Net_Http_HttpRequestMessage_) method on my WPFAuthorizationProvider.

<br />

```csharp
public async Task AuthenticateRequestAsync(HttpRequestMessage request)
{
    AuthenticationResult authResult = null;

    var accounts = await Application.GetAccountsAsync();
    var firstAccount = accounts.FirstOrDefault();

    try
    {
        authResult = await Application.AcquireTokenSilent(_scopes, accounts.FirstOrDefault())
        .ExecuteAsync();
    }
    catch (MsalUiRequiredException)
    {
        try
        {
            await System.Windows.Application.Current.Dispatcher.Invoke<Task>(async () =>
                {
                    authResult = await Application.AcquireTokenInteractive(_scopes)
                    .WithUseEmbeddedWebView(false)
                    .ExecuteAsync();
                });
        }
        catch {}
    }

    if (authResult != null)
    {
        request.Headers.Authorization = new AuthenticationHeaderValue("bearer", authResult.AccessToken);
    }
}
```

<br />

This will fire whenever MSAL makes a request to get a token. It will first look in the cache (Token Silent) or if there is no token, it will get the token interactively, which in .NET Core is popping the default browser which will have a M365 login screen on it.

<br />

When the user successfully logs in, the resulting auth code will be captured and sent back to AAD to get an access token, once we have that token, we stick it into the Header every call we make to Graph. That is it, now we can wire up ANY call to Graph that we are allowed to per our permissions.

<br />

## Obtaining Some Presence

<br />

Hooray, we have a wired up Graph Client, so time now to get some data. For PresenceLight, I want to get the following data from Graph

<br />

- User Name
- Profile photo (if exists)
- Presence

<br />

To be honest, the Graph SDK makes this super easy, for instance the code below will make 3 calls, which you can fix by [batching](https://docs.microsoft.com/en-us/graph/json-batching)

<br />

```csharp
//Image comes through in a byte stream
Stream photo = await _graphServiceClient.Me.Photo.Content.Request().GetAsync();
User profile = await _graphServiceClient.Me.Request().GetAsync();
Presence presence = await _graphServiceClient.Me.Presence.Request().GetAsync();
```

<br />

Seriously, that is it, you have a strongly-typed representation of the data you need. Once you have that, you can add this data to your UI, or push it somewhere, like a smart light for instance. For reference, the PresenceObject represented in JSON looks like this.

<br />

```json
{
    "id": "YOUR USER ID",
    "availability": "Offline",
    "activity": "Offline"
}
```

<br />

## More To Come!

<br />

This isn't the end for PresenceLight, but it does hit a milestone that is important for me. Some of the things top of mind to incorporate into PresenceLight is support for more brands of smart lights, a better UI (this was the first WPF app I ever wrote) and possibly some more type of "statuses" to add. However, there is one big thing I want to incorporate. The top thing stopping from PresenceLight to be used by as many people is possible is that WPF only runs on Windows. There are folks that could use something like PresenceLight but they use MacOS or Linux-based Operating Systems. I have an idea. What if I wrote something in .NET Core that wasn't a client app technically? I have a working prototype of a hybrid app that consists of Blazor and an ASP.NET Core Worker with a shared state service. This allows me to get a web-based UI where I can manage app settings and login to AAD, and offload the polling of Presence data to the worker. Best part is that you don't need the web app open at all. I am still working out the kinks, but this could potentially be a way to get folks on non-Windows machines to PresenceLight.

<br />

All in all, I have learned a ton building PresenceLight, and hope to learn more as I continue to innovate on it. I hope folks install the app, take a look at the GitHub repo, and contribute via opening Issues or Pull Requests. Let me know what you think!