---
date: 2019-08-28T11:00:59-04:00
tags: ["c#", "Azure Functions", "Azure"]
title: "Update My Blog Via Email with Azure Functions"
repo: "https://github.com/isaac2004/UpdateBlogAzureFunction"
---

## Starting with a Manual, Multi-Step Process

<br />
I have [blogged](/post/building-blog) about the changes I made to streamline my site architecture and continue to work on ways to improve it. One thing that I did a month ago was replace the static page that was my [speaking page][/speaking] and make it more dynamic. Before whenever I had a new speaking gig, I would go into my repo and edit the HTML, EVERY... TIME.... I quickly became not a fan of that, so I started to look into options that were less cringey. I discovered in Hugo you can have data-driven pages using a source like json to house content. What I did was build a [shortcode](https://gohugo.io/content-management/shortcodes/) to read data from a json file and output it in a format I wanted, in this case, an HTML table.

<br /><br />

```html
  <table class="tg">
    <tr>
      <th class="tg-0pky">Event</th>
      <th class="tg-0pky">Location</th>
      <th class="tg-0pky">Presentation</th>
      <th class="tg-0pky da">Dates</th>
    </tr>
    {{ $dataJ := getJSON "/data/speaking.json" }}
    {{ $sortOrder := "asc"}}
    {{if (eq ($.Get 0) "true" )}}
    {{ $sortOrder = "desc"}}
    {{end}}
    {{ range sort $dataJ ".startDate" $sortOrder  }}
    {{ if (eq ($.Get 0) .done ) }}
    <tr>
        <td class="tg-0pky"><a href="{{ .url }}" target="_blank">{{ .eventName }}</a></td>
        <td class="tg-0pky">{{ .location }}</td>
        <td class="tg-0pky">{{ replace .talks "|" "<br />" | safeHTML }}</td>
        {{if (eq .startDate .endDate)}}
        <td class="tg-0pky">{{ .startDate | dateFormat "Jan 2, 2006" }}</td>
        {{ else }}
        <td class="tg-0pky">{{ .startDate | dateFormat "Jan 2, 2006" }} - {{ .endDate | dateFormat "Jan 2, 2006" }}</td>
        {{end}}
    </tr>
    {{end}}
    {{ end }}
  </table>
```

<br />

and here is how I declare it

<br />

```html
{{ < speaking "false" > }}
```

<br />

What this code does loops over a json file called `data.json` and builds an HTML table from it. Here is a subset of what that data looks like

```json
[
  {
    "url": "https://www.thatconference.com/",
    "eventName": "That Conference",
    "location": "Wisconsin Dells, WI",
    "talks": "How To Work From Home Without Living At Work",
    "startDate": "2019-08-05",
    "endDate": "2019-08-08",
    "done": "true"
  },
...
]
```

<br />

This data represents all the pertinent information for my speaking engagements, and now all I have to do is update that json file, commit to my repo, and my blog gets updated, pretty cool!
<br /><br />
<strong>NOTE: There are 2 tables because I separate past/present events.</strong>

<br />

## Replacing with Something More Automated

<br />

So I quickly realizes that even though this is better, I still don't like it, because I have to go to a repo and commit still, there has to be a better way!!! I started thinking of an idea.

<br />

- Have some process that triggers an event (email, txt, etc.)
- Said process will have data that represents an event
- Event will take data and commit it to repo using code

<br />
Azure Functions works perfectly for these one-off processes that can be spun up pretty quick, so I moved forward there. I immediately ran into a roadblock as most of the git libraries that exist don't work in Azure Functions, but I knew that I was able to do Git commands in Kudu (the engine behind Azure App Service, and you can run commands via console in it) so I knew that Git is installed in the environment for App Service, now to just find a way to get it all to work. After talking with a few colleagues, I discovered that running `git clone url` would not work, because the path to the git executable is not in the PATH variable, so I had to declare the full path, which is `D:\PROGRA~1\Git\cmd\git.exe` (I like to use short names in commands). Now if I wanted to, I could call git commands via C# using `Process.Start()` but I wasn't too keen on having that done all in code, as I would have to start processes for all these things

<br />

- git config X2
- git clone
- git add
- git commit
- git push

<br />

So I decided to put these commands in a `.cmd` file and run that file from C# (same process). Here is what the 2 files look like
<br /><br />

```cmd
# clone.cmd - replace with my stuff
D:\PROGRA~1\Git\cmd\git.exe config --global user.name 'name'
D:\PROGRA~1\Git\cmd\git.exe config --global user.email 'email'

D:\PROGRA~1\Git\cmd\git.exe clone repo-url

# commit.cmd
cd temp
cd LevinBlog
D:\PROGRA~1\Git\cmd\git.exe add .
D:\PROGRA~1\Git\cmd\git.exe commit -m "Update Speaking Events from Email"
D:\PROGRA~1\Git\cmd\git.exe push
```

<br />
If I run both these files, my function will clone my repo, and add any changes, commit and push. This would than trigger a CI build on my repo and my blog would update. Now I just need to write some code to update the data.json file.

<br />

Since I am in C#, updating a json file is easy, thanks to Json.NET. After my repo is cloned, some code like this will Deserialize the string into an object, where I can make updates to, and than Serialize back into json format to save the file.

<br />

```csharp
string json = File.ReadAllText($"{tempFolder}/{config.DataPath}");
var events = JsonConvert.DeserializeObject<List<Event>>(json);
events.AddRange(newEvents);
File.WriteAllText($"{tempFolder}/{config.DataPath}", JsonConvert.SerializeObject(events, Formatting.Indented));
```

<br />
So now I can clone my repo, update the json file, and commit and push those changes. Final step is to actually update the content. I decided on an email as the "trigger" for all this, and may possibly move to a SMS message or PowerApp in the future. So I want to send an email to an email address, and my function picks up that email and processes it. This again is quite easy in C#, if you take advantage of the [MailKit](https://github.com/jstedfast/MailKit) project. Since the email I am sending to is an O365 Mailbox, I can use IMAP to easily parse the inbox, look for a particular message, parse it, and serialize into an object.

<br />

```cshrarp
private List<Event> ParseEmail()
{
    List<Event> events = new List<Event>();
    using (var client = new ImapClient())
    {
        // For demo-purposes, accept all SSL certificates
        client.ServerCertificateValidationCallback = (s, c, h, e) => true;
        client.Connect(config.IMAPServer, Convert.ToInt32(config.IMAPPort), Convert.ToBoolean(config.IMAPUseSSL));
        client.Authenticate(config.IMAPUsername, config.IMAPPassword);

        var inbox = client.Inbox;
        inbox.Open(FolderAccess.ReadWrite);
        var query = SearchQuery.SubjectContains("Update Speaking Engagement");
        foreach (var uid in inbox.Search(query))
        {
            var message = inbox.GetMessage(uid);
            events.Add(ParseMessage(message.GetTextBody(MimeKit.Text.TextFormat.Plain)));
            log.LogInformation("Subject: {0}", message.Subject);
            inbox.AddFlags(uid, MessageFlags.Deleted, true);
        }
        client.Inbox.Expunge();
        client.Disconnect(true);
    }
    return events;
}

private Event ParseMessage(string message)
{
    Dictionary<string, string> keyValuePairs = message.Split("\r\n")
                                                      .Where(a => a != "")
                                                      .Select(value => value.Split('|'))
                                                      .ToDictionary(pair => pair[0].Trim(), pair => pair[1].Trim());

    return JsonConvert.DeserializeObject<Event>(JsonConvert.SerializeObject(keyValuePairs));
}
```

<br />

So what this code does is read my inbox, look for a message and parses the message. The email will be in the following format
<br />
<br />
````
url| https://www.dotnetconf.net/
eventName| .NET Conf 2019
location| All Around the World
talks| Application Insights
startDate| 2019-09-25
endDate| 2019-09-25
done| false
````

<br />

Wiring all this up, I have a function that checks my email, and if there is a certain message, it parses that message. It than does a clone of my blog's git repo and updates the data.json file with the event that was sent from email. Finally a commit and push is done to trigger a blog update. This was interesting as I discovered some new wrinkles to the App Service Sandbox. Take a look at the [GitHub repo](https://github.com/isaac2004/UpdateBlogAzureFunction) of the function if you like.
