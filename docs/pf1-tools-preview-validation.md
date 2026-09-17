# `pf1-tools` preview validation

This note records the first local integration of
`PathfinderDb.Schema 2.0.0-preview.1`.

## Method

The `pf1-tools` central package version was changed temporarily, only for the
restore/build process, from `1.0.3` to `2.0.0-preview.1`. A temporary NuGet
configuration exposed the locally generated package under `artifacts/` and
NuGet.org for the remaining dependencies. The consumer repository was restored
and built without a `ProjectReference` to this repository. Its package version
file was restored byte-for-byte after the validation.

## Results

- `WikiExportParser.Console` restored `PathfinderDb.Schema 2.0.0-preview.1`.
- The consumer built for `.NET Framework 4.8`.
- The parser executable displayed its command help successfully.
- The commands `spells`, `feats` and `monsters` executed against the local
  `pf1-xml` corpus.
- The parser exited with code `0`.
- XML and JSON output files were generated for spells, feats and monsters.
- Diagnostics were generated as CSV and JSON.

The corpus run reported 131 errors and 1129 warnings from the parser's existing
data-quality diagnostics. These messages concern source pages and parsing
coverage; they are not build or package restoration failures and must be
compared with the baseline from `pf1-data` before being classified as
regressions.

## Remaining M5 work

- [x] Publish `2.0.0-preview.1` to NuGet.org.
- [ ] Update `pf1-tools/Directory.Packages.props` permanently to the published
  preview.
- [ ] Re-run the parser using the remote package, not the local package source.
- [ ] Compare normalized XML and JSON outputs with the historical baseline.
- [ ] Classify differences caused by corpus revisions, parser changes or schema
  changes.
- [ ] Validate the complete `pf1-data` structure before publishing `2.0.0`.
