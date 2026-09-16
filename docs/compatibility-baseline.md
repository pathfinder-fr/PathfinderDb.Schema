# Compatibility baseline

This document records the contract that must remain identifiable while the
project is migrated from the historical Portable Class Library project to the
SDK-style project.

## Current package and consumer

- NuGet package: `PathfinderDb.Schema`
- Last published version: `1.0.3`
- Historical package target: `portable-net40+sl5+win`
- Current consumer: `pf1-tools/apps/wiki-export-parser`
- Consumer target framework: `.NET Framework 4.8`
- Central package version: `pf1-tools/Directory.Packages.props`
- Historical XML namespace: `urn:pathfinderDb`
- License: Apache-2.0

The first modern package must keep the namespace and the existing serialized
names. Framework migration and package publication must be validated before
changing the data contract.

## Consumer API inventory

The current `pf1-tools` parser consumes the following public surface.

### Aggregate and serialization

- `DataSet`
- `DataSet.Sources`
- `DataSet.Spells`
- `DataSet.SpellLists`
- `DataSet.Feats`
- `DataSet.Monsters`
- `DataSet.Header`
- `DataSet.Load(Stream)`
- `DataSet.Load(TextReader)`
- `DataSet.Load(XmlReader)`
- `DataSet.Add(DataSet)`

The parser creates `DataSet` instances, merges datasets, and sends filtered
datasets to XML, JSON and CSV writers.

### Main model types

- `Spell`
- `SpellList`
- `SpellListLevel`
- `Feat`
- `FeatPrerequisite`
- `FeatPrerequisiteChoice`
- `Monster`
- `Source`
- `ElementSource`
- `ElementReference`
- `ElementLocalization`
- `DataSetHeader`

### Constants and enums used by the parser

- `Source.Ids`
  - `PathfinderRpg`
  - `AdvancedPlayerGuide`
  - `UltimateMagic`
  - `UltimateCombat`
  - `Bestiary`
  - `Bestiary2`
  - `Bestiary3`
  - `Bestiary4`
  - `PaizoBlog`
  - `AdventurePath(int)`
- `SpellList.Ids`
  - `SorcererWizard`
  - `Cleric`
  - `Druid`
  - `Ranger`
  - `Bard`
  - `Paladin`
  - `Alchemist`
  - `Summoner`
  - `Witch`
  - `Inquisitor`
  - `Oracle`
  - `AntiPaladin`
- `FeatType`
- `FeatPrerequisiteType`
- `FeatPrerequisiteOtherTypes`
- `SpellRangeUnit`
- `SpellSchool`
- `SpellDescriptors`
- `SpellComponentKinds`
- `SpellSavingThrowEffect`
- `SpellSavingThrowTarget`
- `SpecialBoolean`
- `DataSetLanguages.English`

The list is based on the current parser sources and must be checked again
against the compiled consumer after the SDK migration.

## Fixtures

### Current repository fixtures

The following files are retained as small, reviewable fixtures:

| Fixture | Intended use | Current status |
|---|---|---|
| `xml/samples/DataSetHeader.xml` | Header and localization reference | Legacy sample; verify against current `DataSet` |
| `xml/samples/Feat.xml` | Feat, source and prerequisite reference | Current contract candidate |
| `xml/samples/Spell.xml` | Spell levels, components, range and localization | Current contract candidate |
| `xml/samples/Monster.xml` | Historical monster contract reference | Not compatible with current `Monster` model |

`xml/samples/Monster.xml` contains attributes and nested sections such as
statistics, attacks, skills and abilities that are not represented by the
current `PathfinderDb.Schema.Monster` class. It must remain available as a
historical reference, but it must not be used as evidence that the current
model can deserialize the complete monster document.

### Historical exports

Representative exports are available in the sibling
`WikiExportParser-Decompile` repository under `Out`:

- `Out/spells.xml`
- `Out/feats.xml`
- `Out/bestiary/monsters.xml`
- source-specific datasets under `Out/apg`, `Out/pfrpg`, `Out/bestiary` and
  other source directories

These exports use the `urn:pathfinderDb` namespace and are the reference for
real parser output. They should be copied into versioned fixtures only when a
small sample is sufficient; large generated datasets should remain external
test inputs or be downloaded by a separately documented compatibility job.

The repository now contains compact, provenance-preserving extracts derived
from those exports:

- `tests/Fixtures/Historical/SpellDataset.xml`
- `tests/Fixtures/Historical/FeatDataset.xml`
- `tests/Fixtures/Historical/MonsterDataset.xml`

They are intentionally small enough to run in every test job while preserving
the source, localization, references, list levels, feat types and monster
attributes that matter to the current model.

## Serialization observations

- XML roots are `dataSet`, `spell`, `feat` or `monster` in the
  `urn:pathfinderDb` namespace.
- `Spell` levels are represented by `spellListLevel` data serialized as
  `<level list="..." level="..." />`.
- `Feat.Prerequisites` is a polymorphic XML array containing `prerequisite` and
  `choice`.
- `Element.Source` is a compatibility facade over `Element.Sources`.
- `Element` supports one or multiple sources and optional localization.
- `Spell.Descriptor` is an enum serialized as a string and may represent
  combined descriptor flags.
- `pf1-tools` uses Newtonsoft.Json with `StringEnumConverter` for JSON output.
- The library itself has no JSON package dependency; JSON compatibility belongs
  to the consumer test layer.

## Comparison rules

The compatibility comparison must distinguish:

1. contract regressions:
   - missing elements or attributes;
   - changed XML names;
   - changed namespace;
   - changed enum text;
   - changed public members used by `pf1-tools`;
2. harmless formatting differences:
   - indentation;
   - XML declaration details;
   - attribute ordering;
   - line endings;
3. intentional differences:
   - explicitly documented model extensions;
   - corrected identifiers with migration aliases;
   - data intentionally excluded by a filtered writer.

The comparison must normalize XML before comparing it and must never compare
the decompiled source text directly with this repository's source text.

## Baseline acceptance criteria

- [x] The current NuGet version and target are recorded.
- [x] The consuming parser and target framework are recorded.
- [x] The public API used by the parser is inventoried.
- [x] The XML namespace and serialization rules are recorded.
- [x] Current and historical fixture locations are recorded.
- [x] Compact historical XML extracts are versioned in this repository.
- [x] The legacy monster fixture is explicitly classified.
- [ ] A machine-executable normalized XML comparison is added in M2.
- [ ] The complete parser output comparison is executed in M5.
