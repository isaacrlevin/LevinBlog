---
title: "Video Splitter"
url: "/videosplitter/"
---

<section class="videosplitter-page">

# VideoSplitter

<div class="vs-hero">
	<span class="vs-eyebrow">Product Overview</span>
	<p class="vs-intro">Automatically extract engaging short-form clips from long videos with AI-powered transcription, segment detection, and social publishing.</p>
	<div class="vs-link-row">
		<a class="vs-link-pill" href="https://www.youtube.com/watch?v=rBZvVlglR1k" target="_blank" rel="noopener noreferrer">Watch Demo</a>
		<a class="vs-link-pill" href="/videosplitter/terms/">Terms of Service</a>
		<a class="vs-link-pill" href="/videosplitter/policy/">Privacy Policy</a>
	</div>
</div>

Welcome to VideoSplitter. This application helps you automatically extract engaging short-form video segments from longer videos using AI-powered analysis.

## Demo

Check out VideoSplitter in action: [Watch Demo on YouTube](https://www.youtube.com/watch?v=rBZvVlglR1k)

## Legal

- [Terms of Service](/videosplitter/terms/)
- [Privacy Policy](/videosplitter/policy/)

## Documentation Index

- [Setup Transcription](docs/SetupTranscription.md) - Configure speech-to-text for your videos
- [Setup Segment Generation](docs/SetupSegmentGeneration.md) - Configure AI-powered segment extraction
- [Setup Social Media Publishing](docs/SetupSocialMediaPublishing.md) - Connect your social media accounts
- [Customizing Prompts](docs/CustomizingPrompts.md) - Fine-tune AI behavior with custom prompts
- [Testing Guide](docs/TESTING.md) - Running and writing tests for the project

### Development & Deployment

- [CI/CD Quick Start](docs/CICD-QUICKSTART.md) - Get the pipeline running quickly
- [CI/CD Pipeline](docs/CI-CD.md) - Comprehensive build automation guide
- [Certificate Setup](docs/CERTIFICATE-SETUP.md) - Code signing for Windows Store
- [Secrets Setup](docs/SECRETS-SETUP.md) - Configure GitHub secrets

## Quick Start

1. **Configure Transcription** - Choose between local (Whisper.NET) or cloud-based (Azure Speech) transcription
2. **Configure AI Provider** - Select an LLM provider for intelligent segment detection
3. **Import a Video** - Add your video file to the application
4. **Generate Segments** - Let AI analyze your transcript and suggest the best clips
5. **Export or Publish** - Export clips locally or publish directly to social media

## System Requirements

- Windows 10/11, macOS, or Linux
- .NET 10 Runtime
- FFmpeg (for video processing)
- ~2GB disk space for local Whisper model (if using local transcription)
- Internet connection (for cloud providers)

## Features

- **Multiple Transcription Options**: Local (privacy-focused) or cloud-based (faster for long videos)
- **Multiple AI Providers**: Choose from Ollama (local), OpenAI, Anthropic Claude, Azure OpenAI, or Google Gemini
- **Social Media Integration**: Direct publishing to TikTok, YouTube Shorts, and Instagram Reels
- **Customizable Prompts**: Tailor the AI behavior to your content type

## For Developers

### Building the Project

```bash
# Restore dependencies
dotnet restore

# Build the solution
dotnet build --configuration Release

# Run tests
dotnet test
```

### CI/CD Pipeline

The project uses GitHub Actions for automated builds and releases:

- **PR Validation**: Runs on every pull request (build + test)
- **Continuous Integration**: Runs on main branch (build + test + coverage)
- **Release Builds**: Triggered by Git tags (`v*.*.*`) - creates MSIX packages

See [CI/CD Pipeline Documentation](docs/CI-CD.md) for details.

### Contributing

1. Fork the repository
2. Create a feature branch
3. Make your changes
4. Ensure all tests pass (`dotnet test`)
5. Submit a pull request

The CI pipeline will automatically validate your changes.

</section>
