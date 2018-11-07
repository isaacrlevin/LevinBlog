---
date: 2018-07-01T11:25:05-04:00
tags: ["Azure", "cli", DevOps"]
title: "Azure CLIs Are Awesome"

---

## Did you know the Azure CLI was Extendable?

<br />

I did not know this. I was minding my business, when I decided to catch up on some Azure Friday videos, and found this [beauty](https://channel9.msdn.com/Shows/Azure-Friday/Azure-CLI-Extensions) from June 23rd 2018 regarding extending the Azure CLI and how it was completely in the open. In the video one of the extensions was showcased, alias, which allows a developer to create quick commands for Azure CLI commands. So often when working with the Azure CLI, I write these long, impossible to remember commands where only one or two things change. For instance

<br />

```bash
az webapp list --query '[].{Name:name, Location:location}' --output table
```

<br />
This command does something pretty straightforward, but it is complicated. All I want is to be able to get a list of my web apps and see what region they are in, which the Azure Portal gives sdaedsame on the main App Service page. In the past I would have to have this command saved off somewhere and run it when I needed this info.

<br />

## Reducing the Need to Copy/Paste

<br />

Storing the above command isn't hard, but IT IS annoying, becuase most times I forget I have it saved somewhere, so I have to Google (or Bing?) the syntax to write this command again. With Azure CLI alias, I can create the alias, and going forward on my machine, I can use a command I created to do this task.

<br />

```bash
az alias create --name ls-apps --command "webapp list --query '[].{Name:name, Location:location}' --output table"

az ls-apps
```

<br />

Now everytime I run my alias command, it will run the command I have had to rely upon in the past. Leveraging aliases in this way allows us to not only remove complexity, but also keystrokes, which is a huge win in the world of a developer.

<br />

## Getting the Alias Extension

<br />

Like most extensions for cli tools, the process to get an extension involves invoking a command to install that extension from somewhere. The below command will install the alias command

<br />

```bash
az extension add --name alias
```

<br />

The first thing you will want to do is see the available commands with the alias extension. Like all other Azure CLI commands, you can do that using --help

<br />

```bash
az alias --help
```

<br />

#### NOTE: You must have the 2.0.28 version of the CLI to have the alias extension work

<br />

## Sharing your Aliases

<br />

All alias that are created are stored in configuration files in the Azure CLI root directory. The root directory is *%USERPROFILE%\.azure* on Windows and *$HOME/.azure* on macOS/Linux operating systems. The file in question is called alias which has no extension. The file is written in the INI format. For our command from earlier, the alias is stored as

<br />

```bash
[ls-apps]
command = webapp list --query '[].{Name:name, Location:location}' --output table
```

<br />

Since the configuration of aliases are so straightforward, sharing them between team members is simple. All they need is the extension installed and dropping the file in their root directory will enable them to use those shorthand commands. Now onboarding team members that will need to work with the CLI will be far more efficient.

<br />

## Tons of Options for Enhancements

<br />

There are a handful of awesome commands that I use to make my life easier. I am very curious what aliases people have created, so please share if you like.