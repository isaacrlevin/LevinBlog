---
date: 2017-08-21T11:15:58-04:00
tags: ["git", "cli"]
title: "Resetting Git Working Directory Commands"
---

## I prefer CLI to visual tools for Git

<br />

In doing my day-to-day development, I prefer to do most tasks Git related in a CLI. There are great tools available in Visual Studio 2017, Code, and 3rd party software like Source Tree, but for I like what I like, and the CLI does it for me. One thing that it is super easy to do in the CLI is "resetting" or clearing a working directory and starting fresh. We all make random changes, whether it be add/remove files and make changes that we decide are not ideal for commiting. There are a few ways/options to clean up your directory, and in this post I will show the ones I use often with their caveats.

<br />

## Git Reset HEAD~1

<br />

```bash
git reset HEAD~1
```

<br />

 ✅ Reverts non-pushed commits

<br />

## Git Checkout '.'

<br />

```bash
git checkout . (path specifier. undoes staged local mods)
```

<br />

- Does NOT delete local, non-pushed commits
- Does NOT delete local, non-tracked files
- Restores tracked files you deleted
- Reverts changes you made to tracked files

<br />

## Clean and reset

<br />

```csharp
git clean -f -d -x (force, remove untracked changes)
git reset --hard
```

<br />

- Does NOT delete local, non-pushed commits
- Reverts changes you made to tracked files
- Restores tracked files you deleted
- Deletes files/dirs listed in .gitignore (like build files, node_modules)
- Deletes files/dirs that are not tracked and not in .gitignore