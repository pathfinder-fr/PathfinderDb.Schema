# Changelog

All notable changes to `PathfinderDb.Schema` are documented in this file.

## Unreleased

### Changed

- Removed Archives of Nethys as a source identifier; it is a reference site,
  not a dataset source.
- Added official English spell-list constant names while preserving the
  existing identifiers and obsolete constant aliases.

## [2.0.2] - 2026-09-19

## [2.0.1] - 2026-09-19

### Added

- Added source identifiers for `bestiary5`, `bookofthedamned` and
  `codexmonstrueux`.
- Added spell list identifiers for `bloodrager`, `hypnotiseur`, `medium`,
  `occultiste`, `psychiste`, `sahir-afiyun`, `spirite` and
  `summoner-unchained`.

## [2.0.0-preview.1] - 2026-09-17

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
- Configured the release workflow for NuGet Trusted Publishing with GitHub
  Actions OIDC instead of a persistent API key.

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
- Publication to NuGet.org succeeds through Trusted Publishing OIDC.
- Remote `pf1-tools` validation and external corpus comparison remain pending.

[2.0.0-preview.1]: https://github.com/pathfinder-fr/PathfinderDb.Schema/releases
