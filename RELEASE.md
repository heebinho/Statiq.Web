# 1.0.0-beta.6

# 1.0.0-beta.5

- Some tweaks to the new .NET template to prefer the directory name.

# 1.0.0-beta.4

- Updated Statiq Framework to version [1.0.0-beta.21](https://github.com/statiqdev/Statiq.Framework/releases/tag/v1.0.0-beta.21).
- **Breaking change:** Removed the `IBootstrapper.SetDefaultTemplate()` extension given more general use of templates. The "default" template should now be specified
  by setting a templates for the HTML media type (by default it's still Razor, so this breaking change won't really affect anyone right now).
- **Breaking Change:** Removed the `AssetFiles`, `DataFiles`, and `ContentFiles` settings and replaced with a single `InputFiles` setting for finding all input files.
  The target pipeline and content type are now determined from the media type and metadata of the document instead of via globbing patterns for each pipeline.
  If you previously had asset files that started with an underscore (such as a `_redirects` file), you will need to explicitly add those to the `InputFiles` patterns
  along with the default pattern: `.AddSetting(WebKeys.InputFiles, new [] { "**/{!_,}*", "_redirects" })`.
- Added a `ClearDataContent` document setting that clears content from data documents (for example, to support passing the data file to layouts).
  Set this for a single data document to clear it's content or globally with `.AddSetting(WebKeys.ClearDataContent, true)` to clear the content of all data files.
- Made the concept of "templates" more general. They now essentially use the media type of a document (typically inferred from file extension) to determine which pipeline to
  process the document in and what module to use for processing. Templates can now be defined for assets, data, and content and for the `Process` and `PostProcess` phases for each.
- Added a `ContentType` document setting to override the calculated pipeline and processing for a document (values are `Asset`, `Data`, and `Content`).
  For example, setting the `ContentType` of a file named "foo.json" to `Asset` will treat the file as an asset and will not process it's content as data.
- Added a `MediaType` document setting to override the media type calculated from the file extension.
- Added a `RemoveScriptExtension` document setting that will convert script file names like "foo.json.csx" to "foo.json" and reset their media types so the script output can be seamlessly
  processed by the appropriate pipeline and modules (for example, "foo.json.csx" will get processed by the `Data` pipeline while "foo.md.csx" will get processed by the `Content` pipeline).
  The default value is `true`.
- Removed the `Isolated` flag from the `Assets` pipeline so the set of copied assets can be retrieved from other pipelines (I.e. to generate a list of images in a directory).
- Added support for script files (`.csx` or `.cs`) to the `Archive` pipeline (I.e. to generate JSON APIs from a collection of documents or data).
- Added a `Script` document setting that will treat a file as a C# script, even if the extension is not `.cs` or `.csx`.
- Added a common `Inputs` pipeline that consolidates directory metadata, sidecar, and front matter parsing and supports evaluating scripts with a `.csx` or `.cs` extension.
  Detailed script usage will be documented on the site, but generally if the script returns null the original input document is returned, if the script returns a string the content
  of the document will be changed to the return value, or if the script returns a document(s) those will be added to the appropriate pipeline.
- New `Statiq.Web.Templates` project with a Statiq Web templates for the `dotnet new` CLI command (#915, thanks @devlead).

# 1.0.0-beta.3

- **Breaking change:** Updated Statiq Framework to version [1.0.0-beta.20](https://github.com/statiqdev/Statiq.Framework/releases/tag/v1.0.0-beta.20).
  This version of Statiq Framework contains breaking changes which Statiq Web will inherit.
- The `Content` pipeline no longer creates any metadata-based tree structure (I.e. the metadata key `Children` is no longer set). Instead, consider
  using methods from `Outputs` such as `Outputs.GetChildren(doc)` or the new `OutputPages` property (see the Statiq Framework 1.0.0-beta.20 release notes for more details).
- Added a new `MinimumStatiqWebVersion` key to perform a check for the minimum allowed version of Statiq Web. If this is set to something higher than the current version
  of Statiq Web, an error will be logged and execution will stop. Any setting that starts will this key will be considered, so it's recommended the use of this key be
  suffixed with a unique identifier to avoid conflicts between components (for example `MinimumStatiqWebVersion-MySite`). While not required or typically necessary for sites,
  it's recommended that themes set this in their theme settings file (for example `MinimumStatiqWebVersion-CleanBlog`).

# 1.0.0-beta.2

- Updated Statiq Framework to version [1.0.0-beta.19](https://github.com/statiqdev/Statiq.Framework/releases/tag/v1.0.0-beta.19).
  This version of Statiq Framework includes internal refactoring that provides a big performance boost.
- Added a `AssetFiles` settings to configure the globbing patterns used for copying assets.

# 1.0.0-beta.1

- **Breaking change:** Updated Statiq Framework to version [1.0.0-beta.18](https://github.com/statiqdev/Statiq.Framework/releases/tag/v1.0.0-beta.18).
  This version of Statiq Framework contains breaking changes which Statiq Web will inherit.
- **Breaking change:** The `Content` pipeline no longer nests output documents and instead all documents are now output.
  `IEnumerable<IDocument>.FilterDestinations("*.html")` or `Outputs["*.html"]` can be used to get "root" documents.
- Added a `MakeLinksAbsolute` setting to rewrite relative links to be absolute.
- Added a `MakeLinksRootRelative` setting to rewrite relative links to be root-relative.
- Suppressed archive output when there's no documents to archive.
- Added the `CacheDocuments` module to additional pipelines for faster rebuild times.
- Added an `ArchiveKeyComparer` metadata that allows specifying a specific comparer for use with archive groups (usually with script metadata).
- Added ability for all pipelines to ensure every document gets a `Published` value, either from an existing value or from the file name or modified date.
- Added a `PublishedUsesLastModifiedDate` setting to control whether a file modified date should be used for getting published dates.
- Added `settings` as a default settings file name in themes (with support for JSON, YAML, or XML formats).
- Added support for sidecar files in other input directories at the same relative path (I.e. themes).
- Added support for `themesettings` and `statiq` YAML (`.yml` and `.yaml`) and XML (`.xml`) configuration files in themes.
- Fixed a bug on engine reruns (I.e. the preview command).

# 1.0.0-alpha.21

- Updated Statiq Framework to version [1.0.0-beta.17](https://github.com/statiqdev/Statiq.Framework/releases/tag/v1.0.0-beta.17).

# 1.0.0-alpha.20

- Added `OutputPath` setting so the output path can be set from the command-line or configuration file.
- Added `ExcludedPaths` setting so excluded paths can be set from the command-line or configuration file.
- Added `InputPaths` setting so input paths can be set from the command-line or configuration file.
- Updated Statiq Framework to version [1.0.0-beta.16](https://github.com/statiqdev/Statiq.Framework/releases/tag/v1.0.0-beta.16).

# 1.0.0-alpha.19

- Added the new `Statiq.Web.props` file to a `buildTransitive` folder in the package so it flows transitively to consumers.

# 1.0.0-alpha.18

- Bug fix for unclosed `<ItemGroup>` in the new props file (#909, thanks @drmathias).

# 1.0.0-alpha.17

- Added a `IncludeInSitemap` setting to control whether a document should be included in the sitemap (#907, thanks @drmathias).
- Fixed a bug that required feed items to have URI IDs when the specification indicates they can also be arbitrary strings (#906).
- Added a props file to the Statiq.Web package to automatically set the default theme, extensions, and archetypes MSBuild props.
- Updated Statiq Framework to version [1.0.0-beta.14](https://github.com/statiqdev/Statiq.Framework/releases/tag/v1.0.0-beta.14).
- Added a `GatherHeadingsLevel` key that can be used to adjust the headings level when gathering headings globally or per-document (#904).

# 1.0.0-alpha.16

- Updated Statiq Framework to version [1.0.0-beta.13](https://github.com/statiqdev/Statiq.Framework/releases/tag/v1.0.0-beta.13).
- Added support for reading `themesettings.json` and `statiq.json` from the theme paths.
- Added support for theme paths and configuring them via the bootstrapper and/or settings.

# 1.0.0-alpha.15

- Added redirect support.
- Added deployment support for Azure App Service.
- Added deployment support for Netlify.
- Added deployment support for GitHub Pages.
- Added `Enumerate` support to data files.

# 1.0.0-alpha.14

- Added support for directory metadata for data files.
- Added support for front matter in data files.
- Added support for sidecar files as `_[filename].[json|yaml]`.
- Added `ProcessSidecarFiles` setting to turn sidecar files off.
- Added `ApplyDirectoryMetadata` setting to turn directory metadata off.
- Added better xref error messages.

# 1.0.0-alpha.13

- Fixed a bug in the preview command that exited on failures.
- Changed preview server to listen to any hostname/IP on the specified port (this allows use from services like Gitpod).
- Renamed the root namespaces of the extension libraries brought over from Statiq Framework to match new project names.

# 1.0.0-alpha.12

- Added a new `Bootstrapper.AddWeb()` extension to add Statiq Web functionality to an existing bootstrapper.

# 1.0.0-alpha.11

- Changed resource mirroring to be opt-in instead of opt-out (you now need to set `MirrorResources` to `true` to enable) (#896).
- Fix to filter tree placeholder pages out of the `Sitemap` pipeline (#895).

# 1.0.0-alpha.10

- Changed the default theme input path to "theme/input" in preparation for work on dedicated theme folders (see #891).
- Added a new `RenderPostProcessTemplates` key that prevents running post-processing templates like Razor.
- Added a new `ShouldOutput` key that controls outputting a particular document to disk (but doesn't remove it from the pipeline like `Excluded` does).
- Added support for directory metadata (by default as `_directory.yaml` files).
- Added new `ContentFiles` and `DataFiles` settings to control the file globbing patterns.
- Added a new `GenerateSitemap` setting and `Sitemap` pipeline to generate sitemap files by default.
- Added a new `Excluded` key that indicates a document should be filtered out of the content or data pipeline.
- Fixed a bug with feeds not flattening the content document tree.

# 1.0.0-alpha.9

- Fixed xref resolution to report all errors in a given document at once.
- Changed the xref space placeholder character to a dash to match/roundtrip automatic file name titles.
- Removed the `ChildPages` shortcode (it should really be part of the theme).

# 1.0.0-alpha.8

- Added support for validating links.
- Refactored xref error messages to display for all documents at once (instead of one at a time).

# 1.0.0-alpha.7

- Added xref support for links like "xref:xyz" where "xyz" is the value of the "Xref" metadata, the document title with spaces converted to underscores if no "Xref" value is defined, or the source file name if neither of those are available.
- Added `IExecutionContext.TryGetXrefDocument()` and `IExecutionContext.GetXrefDocument()` extension methods to get a document by xref.
- Added `IExecutionContext.TryGetXrefLink()` and `IExecutionContext.GetXrefLink()` extension methods to get a document link by xref.

# 1.0.0-alpha.6

- Added support for Handlebars for files with a ".hbs" or ".handlebars" extension.
- Added ability to specify a default template via the `Bootstrapper.SetDefaultTemplate()` extension.
- Added a powerful capability to add, modify, and remove template modules like Markdown, Razor, etc. via the `Bootstrapper.ConfigureTemplates()` extension.
- Refactored metadata processing into a new common `ProcessMetadata` module.
- Added the `OptimizeFileName` module with `OptimizeContentFileNames` and `OptimizeDataFileNames` settings to control it.
- Added the `SetDestination` module to the "Data" pipeline.

# 1.0.0-alpha.5

- Refactored the `ReadGitHub` module to take configuration values.
- The "Content" and "Data" pipelines now concatenate all documents from pipelines that declare themselves a dependency using `IPipeline.DependencyOf`.

# 1.0.0-alpha.4

- Added a new DeployGitHubPages module.
- Moved the preview and serve commands into Statiq.Web from Statiq.App.
- Moved Statiq.GitHub into Statiq Web as Statiq.Web.GitHub from Statiq Framework. 
- Moved Statiq.Netifly into Statiq Web as Statiq.Web.Netlify from Statiq Framework. 
- Moved Statiq.Azure into Statiq Web as Statiq.Web.Azure from Statiq Framework. 
- Moved Statiq.Aws into Statiq Web as Statiq.Web.Aws from Statiq Framework. 
- Moved Statiq.Hosting into Statiq Web as Statiq.Web.Hosting from Statiq Framework. 
- Moved HTML-based shortcodes from Statiq.Core.
- Fixed a bug with `ArchiveKey` when using a string-based key.
- Added support for setting archive document source.
- Added the `GatherHeadings` module.
- Added a `ChildPages` shortcode.
- Added shortcode support.
- Added `CreateTree`/`FlattenTree` to the Content pipeline.
- Added support for ordering documents in the Content pipeline using the "Index" metadata value.

# 1.0.0-alpha.3

- Added a new Feeds pipeline that creates RSS and Atom feeds based on a definition file.
- Added a new Data pipeline that reads YAML and JSON files.
- Added excerpt generation (in the "Excerpt" metadata key) to the Content pipeline.

# 1.0.0-alpha.2

- Added an Archives pipeline that can create archive indexes, groups, and pages.

# 1.0.0-alpha.1

- Initial version with Content, Assets, Less, and Sass pipelines.