---
date: 2020-01-08T08:25:05-04:00
tags: ["DevOps", "CLI" , "Azure", "personal"]
title: "Building My Blog with GitHub Actions"
---

## So I Changed My DevOps

<br />

As some may know, I already had a fully working [CI/CD process for my blog](/post/building-blog) and it was running on Azure Pipelines. As someone that is always learning and wanting to play with a new tool, I was pretty intrigued when [GitHub Actions](https://github.com/features/actions) was formally announced at GitHub Universe. I wanted to see how challenging it would be to move my DevOps process from Azure Pipelines to GitHub Actions and it was not hard at all.

<br />

## Actions 101

<br />
If you aren't familar, GitHub Actions is the mechanism inside of GitHub for CI/CD (they call them workflows) and is written in [YAML [Yet Another Markup Language]](https://yaml.org/). If you read the [docs](https://help.github.com/en/actions/automating-your-workflow-with-github-actions), you can take a look at the structures that make up a workflow and even choose from a ton of existing Actions that have been developed my Third-Party Vendors, or community members as well, so even though you can create them yourself, you have the ability to leverage hundreds of them to build a workflow that fits your needs.
<br /><br />

## Making a Workflow to Fit My Needs

<br />
The first thing that I did in building my workflow was to identify the steps to get my app from source to a static-site in Azure Blob Storage. It isn't hard but all I have to do is Build and Deploy
<br /><br />

 - Build the app using Hugo Cli
 - Deploy built bits to specific container in Azure Blob Storage

<br />
Way too simple here, so now that I have this, I need to determine what I need in my workflow to complete this. Funny enough both these steps exist as created Actions in the Marketplace, so I then just need to specify when this Action runs and what platform it runs on (in my case Ubuntu).
<br /><br />

- [Hugo-Site](https://github.com/marketplace/actions/hugo-site)
- [Azure Blob Storage Upload](https://github.com/marketplace/actions/azure-blob-storage-upload)

<br />
All in all, my action looks like this.
<br /><br />
```yaml
name: Build and Publish Blog to Azure Blob Storage

on: [push]

jobs:
  build:

    runs-on: ubuntu-latest

    steps:
      - uses: actions/checkout@master
      - uses: chabad360/hugo-actions@master
        with:
          buildPath: 'public'
          hugoVersion: ''
          args: ''
      - uses: bacongobbler/azure-blob-storage-upload@v1.0.0
        with:
          source_dir: public
          container_name: $web
          connection_string: ${{ secrets.ConnString }}
          extra_args: ''

```
<br />
The only things to call out here is that I have a "secret" stored in GitHub (my connection string for blob storage) that I can easily reference from the workflow.

<br />
The workflow itself is saved on the GitHub repo, in a folder called `.github/workflows` and the Actions frame reads that file whenever a commit is made. The `on` is important because it specifies that we should run this function EVERYTIME there is a commit (true CI/CD 😜).
<br /><br />

## Run The Thing

<br />
Similar to other CI tools, GitHub Actions has a dashboard where you can view the status of all the workflows that have been ran (for instance for this blog, it is [here](https://github.com/isaacrlevin/LevinBlog/actions))

{{< figure src="/images/actions/dashboard.png" link="/images/actions/dashboard.png" >}}
<br />

From here, I can drill into the logs and see all the things that take place to get my code published, pretty awesome!

{{< figure src="/images/actions/logs.png" link="/images/actions/logs.png" >}}

## There You Have It

<br />
That was super easy and fun. Remember, GitHub Actions is brand new and there are only going to be more things added to it to make it a complete end-to-end solution for CI/CD, I look forward to seeing where it goes. Feel free to take it for a spin!!!