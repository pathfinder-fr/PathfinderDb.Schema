# Changelog

All notable changes to `PathfinderDb.Schema` are documented in this file.

## [2.0.0-preview.1] - Unreleased

### Changed

- Migrated the library and test project to SDK-style projects.
- Changed the library target to `netstandard2.0`.
- Replaced the historical build and manual NuGet packaging flow with
  `dotnet restore`, `dotnet build`, `dotnet test` and `dotnet pack`.
- Preserved the public `PathfinderDb.Schema` namespace and the XML namespace
  `urn:pathfinderDb`.
- Added compact XML compatibility fixtures and serialization tests.
- Added package validation and GitHub Actions workflows for CI and release
  publication.

### Compatibility

- The package remains consumable by .NET Framework 4.8 through
  `netstandard2.0`.
- Existing XML element names, attributes and enum values remain unchanged
  unless explicitly covered by compatibility tests.
- The complete historical monster document remains a reference fixture only:
  its statistics, attacks, skills and abilities are not represented by the
  current `Monster` model.

### Validation status

- Local Release build and tests pass.
- Local preview package and symbol package are produced successfully.
- The package content validator passes.
- Publication to NuGet.org and remote `pf1-tools` validation remain pending
  maintainer credentials and external corpus comparison.

[2.0.0-preview.1]: https://github.com/pathfinder-fr/PathfinderDb.Schema/releases
