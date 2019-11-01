---
date: 2019-02-03T11:25:05-04:00
tags: ["Azure", "cli", "DevOps"]
title: "Using Azure CloudShell as a Dev Sandbox"
---

## Have You Heard of CloudShell?

<br />
Back in around the Build 2017 timeframe, the first implementation of [Azure CloudShell](https://docs.microsoft.com/azure/cloud-shell/overview) was rolled out, bringing the ability to run Azure CLI commands from the context of a logged in Azure session, from anywhere the Azure Portal could be loaded (i.e. a browser). This allowed you to do many things via the command line in the Azure world without having to use the GUI or have the tooling installed on your machine. Since that time, there have been a ton of features rolled out to CloudShell, and it can be accessed via it's own url [https://shell.azure.com/](https://shell.azure.com/), though you can still access it via the portal like so.

<br />
[{{< figure src="/images/cloud-shell/get-started.png" >}}](/images/cloud-shell/get-started.png)
<br />

After initiating CloudShell if you haven't before, it will ask you a few things, like what Resource Group you want to install CloudShell in (CloudShell is temporary but needs Azure Files to persist your files). You are also given the option to default what terminal interface you want (PowerShell of Linux Bash). The experience you see when opening CloudShell is a terminal view that is connected to your chosen Azure Directory.

<br />
[{{< figure src="/images/cloud-shell/first-view.png" >}}](/images/cloud-shell/first-view.png)
<br />

From here, you can do some typical Azure things, like maybe create a resource group and see the output.

<br />
[{{< figure src="/images/cloud-shell/resource-group.png" >}}](/images/cloud-shell/resource-group.png)
<br />

That is pretty cool, the idea of being able to do many things Azure related in the browser without the tooling on your matching is pretty powerful.

<br />

## Tooling Built into CloudShell

<br />
I was poking around at the idea of doing some dev tasks on my [Surface Go](https://www.microsoft.com/p/surface-go/8v9dp4lnknsz), which didn't have VS Code on it yet, using what online tools I had. I wanted to take a look at CloudShell and see how far I could go, since there are some cool features and tools built in. I asked around with some of my friends on Twitter, and found out there are a whole slew of [tools pre-installed on CloudShell](https://docs.microsoft.com/azure/cloud-shell/features#tools), like .NET Core, Node.js, Java, Python, git and many more.

<br />
[{{< figure src="/images/cloud-shell/tools.png" >}}](/images/cloud-shell/tools.png)
<br />

## An Idea

<br />
Than I had a fun thought, could I create an ASP.NET Core application, using the .NET CLI, make some coding changes to it via the built-in VS Code support for CloudShell, test it using some method of ip forwarding, check my code into GitHub and finally deploy the code to Azure App Service. I was pretty certain all this could be done.
<br /><br />
I did my first step, creating a ASP.NET Core App and validating the correct bits were put there.

<br />
[{{< figure src="/images/cloud-shell/new-api.png" >}}](/images/cloud-shell/new-api.png)
<br />

If I simply run `code .` in the context of the generated project folder, an instance of VS Code will open inside CloudShell

<br />
[{{< figure src="/images/cloud-shell/code.png" >}}](/images/cloud-shell/code.png)
<br />

At this point, I can make coding changes per my need, and now I want to be able to run the application and test it in a browser. I than saw my friend Anthony Chu mentioned the ability to run [ngrok](https://ngrok.com/)(a way to expose local server to public internet) inside of CloudShell.

<br />
<blockquote class="twitter-tweet" data-lang="en"><p lang="en" dir="ltr">✨ Today&#39;s <a href="https://twitter.com/hashtag/Azure?src=hash&amp;ref_src=twsrc%5Etfw">#Azure</a> <a href="https://twitter.com/hashtag/CloudShell?src=hash&amp;ref_src=twsrc%5Etfw">#CloudShell</a> tip:<br><br>Did you know you can build and test web apps in with Cloud Shell Editor and <a href="https://twitter.com/hashtag/ngrok?src=hash&amp;ref_src=twsrc%5Etfw">#ngrok</a>?<br><br>Learn more about Cloud Shell at <a href="https://twitter.com/docsmsft?ref_src=twsrc%5Etfw">@docsmsft</a>: <a href="https://t.co/zZ35DBhc96">https://t.co/zZ35DBhc96</a> <a href="https://t.co/hgJFeQN2Uj">pic.twitter.com/hgJFeQN2Uj</a></p>&mdash; Anthony Chu #MSIgniteTheTour (@nthonyChu) <a href="https://twitter.com/nthonyChu/status/1075172047246942208?ref_src=twsrc%5Etfw">December 18, 2018</a></blockquote>
<script async src="https://platform.twitter.com/widgets.js" charset="utf-8"></script>

<br />

So what I will need to do is get ngrok running on my CloudShell instance. Well I know if I run a this command, I will get some high-level system info about the CloudShell OS
<br /><br />

```bash
lsb_release -idrc
```

<br />
[{{< figure src="/images/cloud-shell/lsb.png" >}}](/images/cloud-shell/lsb.png)
<br />

Ok, I have x64 Ubuntu, to work with, so I can go download the zip from the [download page](https://ngrok.com/download)(be sure to signup for a free account), unzip the file, and save the application to our path.

<br />
<strong>NOTE: The url and my path may/will be different than yours.</strong>
<br />
<br />
<strong>NOTE: Also you can upload the files directly with CloudShell instead of using curl.</strong>
<br />
<br />
```bash
mkdir DevSandbox
cd DevSandBox
curl https://bin.equinox.io/c/4VmDzA7iaHb/ngrok-stable-linux-amd64.zip -o ngrok.zip
unzip ngrok.zip
export PATH="$PATH:/home/isaac_levin/DevSandBox"
# This line will connect your ngrok account using your auth token
ngrok authtoken <YOUR_AUTH_TOKEN>
```

<br />
<strong>NOTE: Ngrok does not work over https, which is on by default with ASP.NET Core 2.1 and on. Disabling the https redirect in `Startup.cs` will resolve this.</strong>
<br />

<br />
Now I can run ngrok from my terminal, and a tunnel to whatever port I specify and I will get a unique url. One thing that I will need to do is run my application, and than run ngrok against the port that my application is running on. To do this, I will need to have 2 running terminal sessions. Good thing, I can split sessions with [tmux](https://eoinoc.net/tmux-for-noobs). Here is some more information on [tmux support](http://azurepost.com/split-azure-cloud-console-multiple-panes/)
<br /><br />
Than I just run my app in one session, and tunnel with ngrok in the other. Take the url generated and open it in a browser.

<br />
{{< gif gif-src="/images/cloud-shell/ngrok.gif" src="/images/cloud-shell/ngrok.png" >}}
<br />

Pretty darn cool! To add an extra layer, let me open VS Code in CloudShell, and make some changes.

<br />
{{< gif gif-src="/images/cloud-shell/ngrok2.gif" src="/images/cloud-shell/ngrok2.png" >}}
<br />

<strong>WOAH!!!</strong>
<br /><br />
So with this, I can create, develop and test my webapp in CloudShell, no tools on my machine, just the browser. For grins and giggles, better commit this code to a GitHub repo I created.
<br /><br />

```bash
echo "# TestApp" >> README.md
git init
git add README.md
git commit -m "first commit"
git remote add origin https://github.com/isaaclevin/TestApp.git
git push -u origin master
```

<br />
[{{< figure src="/images/cloud-shell/github.png" >}}](/images/cloud-shell/github.png)
<br />

And finally, maybe we should deploy the code to the cloud. So I need to create an Azure App Service and deploy to it. Here is snippet for that
<br /><br />

```bash
# create some variables that will be used all over
resourceGroup="levin-cli-demo"
webAppName="levin-cli-demo-app"
publishFolder="publish"

# create our resource group
az group create --location eastus --name $resourceGroup

# now the app service plan
az appservice plan create --name $webAppName --resource-group $resourceGroup --sku FREE

# and finally the web app
az webapp create --name $webAppName --resource-group $resourceGroup --plan $webAppName

# publish the app with dotnet cli
dotnet publish -c release -o $publishFolder
cd publish

# zip artifacts
zip $publishFolder *

# deploy zip folder to webapp
az webapp deployment source config-zip --resource-group $resourceGroup --name $webAppName --src publish.zip

# get the url of the newly deployed app service and open in the browser
site1=`az webapp show -n $webAppName -g $resourceGroup --query "defaultHostName" -o tsv`
echo $site1
```

<br />

Now you have the url of your webapp in Azure. Putting that in a browser takes you to the site that you just deployed to Azure.

<br />
[{{< gif src="/images/cloud-shell/azure-app.png" >}}](/images/cloud-shell/azure-app.png)
<br />

Pretty great stuff here! Being able to create, develop, test, and deploy an application to Azure <strong>AND</strong> commit to source control with no tooling other than a browser is pretty powerful. This example was with .NET but there are tons of languages supported in CloudShell (look above for all of them).

<br />
## Super Thanks

<br />
Thanks to the folks that enlightened and inspired me to blog about this.

<br />

* [Christos Matskas](https://twitter.com/ChristosMatskas)
* [Anthony Chu](https://twitter.com/nthonyChu)
* [Justin Luk](https://twitter.com/whosjluk)
