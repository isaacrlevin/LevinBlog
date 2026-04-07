---
title: "Privacy Policy"
slug: "policy"
---

<section class="videosplitter-page">

# Privacy Policy

<div class="vs-hero">
	<span class="vs-eyebrow">Legal</span>
	<p class="vs-intro"><strong>Last updated:</strong> April 7, 2026</p>
	<div class="vs-link-row">
		<a class="vs-link-pill" href="/videosplitter/">Video Splitter Home</a>
		<a class="vs-link-pill" href="/videosplitter/terms/">Terms of Service</a>
	</div>
</div>

This Privacy Policy explains how VideoSplitter ("we", "us", or "our") collects, uses, discloses, and protects information when you use the VideoSplitter project and related services. This document is a general template and may be adapted for your specific deployment or platform.

<div class="vs-panel">
	<p>This policy is intended as a general template and does not constitute legal advice.</p>
</div>

## Information We Collect

- **Usage information:** Non-identifying analytics about how the software is used (e.g., feature usage, errors).
- **Uploaded content:** Any video, audio, or transcript files you provide while using the application. These files may contain personal data depending on their content.
- **Telemetry (optional):** If enabled, diagnostic and performance data to improve the application.

## Google API Services Data Disclosure (YouTube)

VideoSplitter allows a logged-in user to publish videos to the user's own YouTube account on the user's behalf. To do this, VideoSplitter requests Google OAuth authorization and uses the following scopes:

- **`/auth/youtube`** (equivalent to `https://www.googleapis.com/auth/youtube`)
- **`/auth/youtube.upload`** (equivalent to `https://www.googleapis.com/auth/youtube.upload`)

### Data Accessed

When a user connects Google/YouTube, VideoSplitter may access and interact with:

- YouTube account authorization information needed to verify upload permissions.
- Video upload payloads you explicitly choose to publish (video file and upload request metadata such as title, description, tags, category, and privacy status).
- Upload operation status and responses returned by the YouTube Data API.

VideoSplitter does not use these scopes to read unrelated Google account data.

### How Google User Data Is Used

VideoSplitter uses Google user data only to:

- Authenticate the logged-in user with Google.
- Upload videos to YouTube as requested by that user.
- Confirm upload completion and return status/errors to the user.

VideoSplitter does not sell Google user data and does not use Google user data for advertising.

### Storage, Retention, and Sharing

- OAuth credentials (for example, access and refresh tokens) may be stored only as required to support authenticated sessions and subsequent user-requested uploads.
- Uploaded video files and metadata are processed for publishing and are retained according to the configured storage/retention settings described in this policy.
- Google user data is shared with Google only through the YouTube API calls required to perform user-requested uploads.
- VideoSplitter does not share Google user data with third parties except when required by law or to protect rights, safety, and security.

Users can revoke VideoSplitter access at any time from their Google account permissions settings.

## How We Use Information

- To provide and maintain the application features.
- To process videos and generate transcripts and segments as requested by users.
- To improve the software and diagnose issues when telemetry is enabled.

## Data Storage and Retention

Uploaded media and generated transcripts are stored where the application is configured to save them (for example, a local database or cloud storage). Retention periods depend on your configuration. If you run a hosted instance, retain data only as long as necessary to provide the service unless otherwise required by law.

## Third-Party Services

The project may use third-party services (e.g., transcription or LLM providers). Those services may receive the data you submit for processing. Review the provider’s privacy policies for details about their data handling and retention practices.

For Google API services requirements, see:

- https://developers.google.com/terms/api-services-user-data-policy
- https://developers.google.com/terms

## Security

We strive to follow reasonable security practices to protect your data. However, no transmission or storage can be guaranteed to be 100% secure. If you operate a hosted instance, implement appropriate access controls and encryption as needed.

## Children's Privacy

The software is not intended for use by children under the applicable age of majority. Do not upload content knowingly containing data from minors without appropriate consent.

## Changes to this Policy

We may update this Privacy Policy from time to time. The "Last updated" date at the top will reflect the most recent revision.

## Contact

If you have questions about this Privacy Policy, open an issue in the repository or contact the project maintainers.

_This Privacy Policy is provided for informational purposes and does not constitute legal advice._

</section>
