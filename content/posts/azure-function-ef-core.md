---
date: 2017-11-02T11:14:48-04:00
tags: ["Azure", "Azure Functions", "Entity Framework Core"]
title: "Using Entity Framework Core with Azure Functions "
---

## Dot Net Standard Support in Azure Functions!

<br />

I have talked about Azure Functions before, but up until this point, I felt like it was limited to cases where you needed to interact with a database. One prime example being listening on an Ftp folder and tracking the files in a database. You would do this in the past don't get me wrong, but I wanted to use the newest stuff with the newest stuff, and using an older mechanism to interact with a database with something as cool as Azure Functions bummed me out. Things have changed! Now Azure Functions have support for Dot Net Standard 2.0, which opens up a ton of opportunities, one of which being able to leverage Entity Framework Core to interact with data.

<br />

## What about DI?

<br />

DI is awesome, we all know that, but the path isn't obviously clear on how to inject dependencies into Azure Functions, something that is a good idea when using Entity Framework. In traditional examples, we could register our DbContext in the Configure Services method Startup.cs like so

<br />

```csharp
      services.AddDbContext<BlogContext>(options =>
               options.UseSqlServer(Configuration.GetConnectionString("BlogDatabase")));
```

<br />

But there is no Startup.cs with Azure Functions, in fact, there is no Main entry pont that is exposed at all, the main reason why serverless is so great, taking out the pomp and circumstance if you have a chunk of code you just want to run. Since there is no place to register our services, we need to create one. This blog post basically walks your through the entire process, in fact, I copy pasted exactly for my POC and it worked perfectly. I am not going to re-hash here, but basically you add a binding rule for your Azure Function and leverage the DI Extension namespace to register your ScopeService (in this case DBContext). The actual code to register is the exact same, just inside your custom registration service. There are some gotchas that Boris talks about, so if you are curious read that blog, it is quality content.

<br />

To get the actual DI experience, all you have to do is [Inject] your DBContext into your function

<br />

```csharp
 [FunctionName("InsertDbRecord")]
        public static async Task<HttpResponseMessage> Run([HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = null)]HttpRequestMessage req, TraceWriter log, [Inject]TestContext _context)
```

<br />

## Is that really it?

<br />

Yep that is it, you now have a context that you can use the way you typically would, for instance if you want to insert records

<br />

```csharp
var title = Guid.NewGuid();
            _context.Posts.Add(new PostEntity {
                Title = title.ToString()
            });
            _context.SaveChanges();
```

<br />

In this case, I have a Posts table with 2 fields, an Id and a Title. What the function will do is very HttpRequest to function Endpoint (using HttpTrigger) it will insert a record into this table with a Guid for the title.

<br />

## Next steps?

<br />

Next steps are obviously doing something more meaningful and scalable, such as having a Class Library that contains a service that interacts with your Context and you DI that into your function instead. Now that you have Dot Net Standard support, you can really extend your functions to be "Console Apps in Cloud" (don't call them that, they aren't that, I regret saying that).