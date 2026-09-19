# Localized Label Catalog Design

## Context

`PathfinderDb.Schema` currently stores normalized enum values such as
`evocation`, `outsider`, and `forest-jungle`. These technical keys are stable
for consumers, but the dataset contract does not carry a shared localized
display label for them. `Element.Localization` is intentionally scoped to an
individual element and is not suitable for repeating shared vocabulary in
every spell, feat, or monster.

Issue #6 adds a dataset-level catalog for localized labels without changing
existing enum values or requiring consumers to parse source content again.

## Scope

The change includes:

- serializable label and translation schema types;
- a `DataSet.Labels` collection;
- domain constants for the initial shared vocabularies;
- a label-resolution helper with explicit fallback behavior;
- XML and JSON serialization coverage;
- XSD and example updates;
- README and changelog documentation.

The change does not translate free-form descriptions, replace technical enum
values, call an external translation service, or update `pf1-tools`/`pf1-data`.

## Contract

### Label model

Each label has:

- `Domain`: the vocabulary namespace;
- `Key`: the normalized technical value;
- `Translations`: localized values keyed by language.

Each translation has:

- `Language`: a BCP 47 language tag, initially using
  `DataSetLanguages.French` (`fr-FR`) and `DataSetLanguages.English`
  (`en-US`);
- `Value`: the localized display text.

The initial domain constants are:

- `spellSchool`
- `spellDescriptor`
- `creatureType`
- `creatureClimate`
- `creatureEnvironment`
- `featType`
- `savingThrow`
- `spellRangeUnit`
- `timeUnit`

The domain container remains extensible: unknown domain strings are valid and
must round-trip without special handling.

### Dataset integration

`DataSet.Labels` is an optional list. A missing `<labels>` element or missing
JSON `labels` property remains valid and readable for backward compatibility.

The declared property order places labels with the other dataset-level
collections. XML uses the `urn:pathfinderDb` namespace. JSON uses the
property names produced by the existing serializer conventions:

```json
{
  "labels": [
    {
      "domain": "spellSchool",
      "key": "evocation",
      "translations": [
        { "language": "fr-FR", "value": "Évocation" },
        { "language": "en-US", "value": "Evocation" }
      ]
    }
  ]
}
```

The equivalent XML is:

```xml
<labels>
  <label domain="spellSchool" key="evocation">
    <translation xml:lang="fr-FR">Évocation</translation>
    <translation xml:lang="en-US">Evocation</translation>
  </label>
</labels>
```

The XSD permits zero or more labels and translations. It keeps the XML
language attribute in the standard XML namespace.

## Resolution behavior

`DataSet.GetLabel(domain, key, language, defaultValue)` resolves in this
order:

1. exact domain/key and requested language;
2. exact domain/key and `en-US`;
3. the supplied `defaultValue`;
4. the technical `key`.

If the catalog is absent, empty, or has no matching translation, the same
fallback order applies. Domain and key matching are ordinal and
case-sensitive, matching the normalized contract values. Language matching is
case-insensitive to align with the existing localization helper behavior.

The helper does not mutate the dataset or create missing catalog entries.

## Deterministic ordering

Serialization preserves collection order. Producers must emit labels ordered by
domain, then key, and translations ordered by language. The schema does not
sort collections implicitly; tests use already ordered collections to make the
required output deterministic without hiding producer mistakes.

## Compatibility and errors

Existing `DataSet` XML and JSON documents without labels remain readable.
Existing normalized values and element-level localization are unchanged.
Unknown domains and languages are preserved as strings. Null optional
collections follow the existing serializer behavior and are not replaced by
success-shaped defaults during resolution.

## Validation

Tests must verify:

1. XML serialization and deserialization of a catalog with French and English
   translations;
2. JSON serialization and deserialization of the same catalog;
3. equivalent XML/JSON domain, key, language, and value data;
4. all four resolution outcomes, including an absent catalog;
5. a dataset without labels remains readable;
6. deterministic output order for domains, keys, and languages;
7. all initial domain constants expose their exact wire values.

The XSD validator and the existing full solution test suite remain required.
