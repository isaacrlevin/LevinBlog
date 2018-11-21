---
date: 
tags: ["c#", "Azure Functions", "Azure"]
hide: true
title: "Building a Simple Contact Form with Azure Functions"
---

## An Easy Solution for an Easy Task

I write about Azure Functions over and over again because they are the perfect solution for what I am trying to do, small little things that I don't want to spin up a larger unnecessary app to do it. That along with the fact they just run and go away, the price point for the things I do make it basically free. 

I recently remapped my blog to a static website hosted in Azure Blob Storage. During the process, I decided that it would be nice to have a way for people to contact me if they wanted to (no idea why they would). I looked at some options around contact form services and I was not really impressed with the free options and didn't want to create some account to manage. Than I had a thought, why not have an Azure Function that I could http post to and sent an email from there, easy peasy. And boy was it!

## Building the Function with an Http Trigger

In order to make this work, I knew I was going to have to use an html form to do an http post of some sort to get simple data from the client-side (sender's email address, name and body of message). Well Azure Functions have a handy-dandy Http trigger that accepts post http requests. Going through the Azure Function wizard, choosing Http Trigger gets you what you want.

-- Insert Image here

Now all we need is to build out a simple function that parses the http request, gets the fields we need, and build the email to send

-- Insert Gist here

A few things to call out here, we have to create a [SendGrid](https://sendgrid.com/) account (which Azure has a free account version where you can send 25k messages a month, sweet deal) to send the email and add the corresponding SDK to our app. Once we have that, we will need to specify the Api key from SendGrid, and an email address you want to send the message to (In my example, I have a from email address that is seperate as well). When I work with Azure Functions, I typically store configuration information in the Application Settings section of my function in the Azure Portal. 

This makes it easy test locally (there is a localsettings.json file to put these settings, keep it out of your repo) and once you are ready to publish, Visual Studio has a handy tool to push the local settings to the Azure Portal

-- Insert image here

Once we are all set, we can deploy and test with Postman if we want. But in reality, we will need a html form to submit to our function, let's look at this now.

## Using jQuery to Trigger the Function

Why did people stop using jQuery? It is so easy to wire up a little DOM manipulation, make a little Api call, it just works. Not knocking modern JS frameworks (which I am a fan of in most cases) but for simple tasks, KISS is essential to cause analysis paralysis. 

To call my function, all I need is the URL that my Http Trigger Function generates in the Azure Portal

{{< figure src="/images/function-screen.png">}}

Than I build out a simple html form, and wire up a little jQuery to make an ajax() post to it

<script src="https://gist.github.com/isaac2004/1e5f2573c7e1f1653def164f9784a95d.js"></script>

And voila, this just works, and not very many lines of code. The best thing in this case is that since this is just some slight UI changes and ajax calls, it is easy to use this in a modular way, better yet if you reference jQuery in other parts of your app. 

