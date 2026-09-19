# Localized Label Catalog Implementation Plan

> **For agentic workers:** REQUIRED SUB-SKILL: Use superpowers:subagent-driven-development (recommended) or superpowers:executing-plans to implement this plan task-by-task. Steps use checkbox (`- [ ]`) syntax for tracking.

**Goal:** Add an optional dataset-level localized label catalog that round-trips through XML and JSON, resolves labels with deterministic fallback, and preserves existing contracts.

**Architecture:** Add focused schema classes for labels, translations, and domain constants. Integrate an optional `Labels` collection and a non-mutating `GetLabel` helper into `DataSet`; update the root XSD and a dataset sample while leaving element-level localization and normalized enum values unchanged.

**Tech Stack:** C#/.NET Standard 2.0, `System.Xml.Serialization`, Newtonsoft.Json, MSTest, XSD fixtures, SDK-style `dotnet` tooling.

**Spec:** `docs/superpowers/specs/2026-09-19-localized-label-catalog-design.md`

## Global Constraints

- Keep `DataSet.Labels` optional so documents without labels remain readable.
- Preserve normalized enum values and existing `Element.Localization` behavior.
- Use `DataSetLanguages.French` (`fr-FR`) and `DataSetLanguages.English` (`en-US`) for the initial catalog entries.
- Resolve labels in this order: requested language, `en-US`, supplied default, technical key.
- Match domains and keys ordinal/case-sensitive; match languages case-insensitively.
- Preserve unknown domain and language strings without validation rejection.
- Do not add external services, package dependencies, or `pf1-tools` changes.
- Preserve collection order during serialization; producers provide domain/key/language ordering.
- Do not push, tag, publish, or create a NuGet release as part of this implementation.

---

### Task 1: Add the label schema types and domain constants

**Files:**
- Create: `src/PathfinderDb.Schema/DataSetLabel.cs`
- Create: `src/PathfinderDb.Schema/DataSetLabelTranslation.cs`
- Create: `src/PathfinderDb.Schema/DataSetLabelDomains.cs`
- Test: `tests/PathfinderDb.Schema.Tests/ContractSerializationTests.cs`

**Interfaces:**
- Produces `DataSetLabel.Domain`, `DataSetLabel.Key`, and `DataSetLabel.Translations`.
- Produces `DataSetLabelTranslation.Language` and `DataSetLabelTranslation.Value`.
- Produces `DataSetLabelDomains.SpellSchool`, `.SpellDescriptor`, `.CreatureType`, `.CreatureClimate`, `.CreatureEnvironment`, `.FeatType`, `.SavingThrow`, `.SpellRangeUnit`, and `.TimeUnit`.
- Uses XML names `label`, `translation`, `domain`, `key`, and `value`; the translation language is serialized as `xml:lang`.

- [ ] **Step 1: Write the failing tests for exact domain values and XML shape**

Add a test method in `ContractSerializationTests`:

```csharp
[TestMethod]
public void LabelSchemaExposesInitialDomainsAndXmlNames()
{
    CollectionAssert.AreEqual(
        new[]
        {
            "spellSchool",
            "spellDescriptor",
            "creatureType",
            "creatureClimate",
            "creatureEnvironment",
            "featType",
            "savingThrow",
            "spellRangeUnit",
            "timeUnit"
        },
        new[]
        {
            DataSetLabelDomains.SpellSchool,
            DataSetLabelDomains.SpellDescriptor,
            DataSetLabelDomains.CreatureType,
            DataSetLabelDomains.CreatureClimate,
            DataSetLabelDomains.CreatureEnvironment,
            DataSetLabelDomains.FeatType,
            DataSetLabelDomains.SavingThrow,
            DataSetLabelDomains.SpellRangeUnit,
            DataSetLabelDomains.TimeUnit
        });

    var label = new DataSetLabel
    {
        Domain = DataSetLabelDomains.SpellSchool,
        Key = "evocation",
        Translations = new List<DataSetLabelTranslation>
        {
            new DataSetLabelTranslation
            {
                Language = DataSetLanguages.French,
                Value = "Évocation"
            }
        }
    };

    var serializer = new XmlSerializer(typeof(DataSetLabel), Namespaces.PathfinderDb);
    using (var writer = new StringWriter())
    {
        serializer.Serialize(writer, label);
        var root = XElement.Parse(writer.ToString());
        Assert.AreEqual("label", root.Name.LocalName);
        Assert.AreEqual("spellSchool", (string)root.Attribute("domain"));
        Assert.AreEqual("evocation", (string)root.Attribute("key"));
        Assert.AreEqual("fr-FR", (string)root.Element(XName.Get("translation", Namespaces.PathfinderDb))
            .Attribute(XNamespace.Xml + "lang"));
        Assert.AreEqual("Évocation", root.Value);
    }
}
```

Add the required `System.Collections.Generic` import if the test file does not already provide it.

- [ ] **Step 2: Run the focused test and verify the expected compile failure**

Run:

```powershell
dotnet test .\tests\PathfinderDb.Schema.Tests\PathfinderDb.Schema.Tests.csproj -c Release --no-restore --nologo --filter FullyQualifiedName~LabelSchemaExposesInitialDomainsAndXmlNames --verbosity minimal
```

Expected: compilation fails because the three schema types and domain constants do not exist.

- [ ] **Step 3: Implement the minimal schema types**

Implement `DataSetLabel` with `[XmlType("label")]`, `[XmlAttribute("domain")]`, `[XmlAttribute("key")]`, and `[XmlElement("translation")]`.

Implement `DataSetLabelTranslation` with `[XmlType("translation")]`, `[XmlAttribute("lang", Namespace = "http://www.w3.org/XML/1998/namespace")]`, and `[XmlText]` on `Value`.

Implement `DataSetLabelDomains` as a public static class with the nine exact string constants listed above. Do not constrain unknown domains.

- [ ] **Step 4: Run the focused test and verify it passes**

Run the same focused `dotnet test` command. Expected: PASS.

- [ ] **Step 5: Commit the schema types**

```powershell
git add src/PathfinderDb.Schema/DataSetLabel.cs src/PathfinderDb.Schema/DataSetLabelTranslation.cs src/PathfinderDb.Schema/DataSetLabelDomains.cs tests/PathfinderDb.Schema.Tests/ContractSerializationTests.cs
git commit -m "Add localized label schema types"
```

### Task 2: Integrate labels into DataSet and add fallback resolution

**Files:**
- Modify: `src/PathfinderDb.Schema/DataSet.cs`
- Test: `tests/PathfinderDb.Schema.Tests/ContractSerializationTests.cs`

**Interfaces:**
- Adds `public List<DataSetLabel> Labels { get; set; }`.
- Adds `public string GetLabel(string domain, string key, string language, string defaultValue = null)`.
- `GetLabel` never mutates `Labels` and returns the technical `key` as the final fallback.

- [ ] **Step 1: Write failing tests for fallback behavior**

Add this test method:

```csharp
[TestMethod]
public void DataSetResolvesLabelsWithRequestedEnglishDefaultAndKeyFallbacks()
{
    var dataSet = new SchemaDataSet
    {
        Labels = new List<DataSetLabel>
        {
            new DataSetLabel
            {
                Domain = DataSetLabelDomains.SpellSchool,
                Key = "evocation",
                Translations = new List<DataSetLabelTranslation>
                {
                    new DataSetLabelTranslation { Language = DataSetLanguages.English, Value = "Evocation" },
                    new DataSetLabelTranslation { Language = DataSetLanguages.French, Value = "Évocation" }
                }
            }
        }
    };

    Assert.AreEqual("Évocation", dataSet.GetLabel(DataSetLabelDomains.SpellSchool, "evocation", "FR-fr"));
    Assert.AreEqual("Evocation", dataSet.GetLabel(DataSetLabelDomains.SpellSchool, "evocation", "de-DE"));
    Assert.AreEqual("Provided", dataSet.GetLabel(DataSetLabelDomains.SpellSchool, "missing", "de-DE", "Provided"));
    Assert.AreEqual("missing", dataSet.GetLabel(DataSetLabelDomains.SpellSchool, "missing", "de-DE"));
}

[TestMethod]
public void DataSetLabelResolutionDoesNotCreateAMissingCatalog()
{
    var dataSet = new SchemaDataSet();

    Assert.AreEqual("evocation", dataSet.GetLabel(DataSetLabelDomains.SpellSchool, "evocation", DataSetLanguages.French));
    Assert.AreEqual(0, dataSet.Labels.Count);
}
```

- [ ] **Step 2: Run the focused tests and verify they fail**

Run:

```powershell
dotnet test .\tests\PathfinderDb.Schema.Tests\PathfinderDb.Schema.Tests.csproj -c Release --no-restore --nologo --filter "FullyQualifiedName~DataSetResolvesLabelsWithRequestedEnglishDefaultAndKeyFallbacks|FullyQualifiedName~DataSetLabelResolutionDoesNotCreateAMissingCatalog" --verbosity minimal
```

Expected: compilation fails because `DataSet.Labels` and `DataSet.GetLabel` do not exist.

- [ ] **Step 3: Implement DataSet integration**

Initialize `Labels` in the `DataSet` constructor consistently with the existing dataset collections, but keep deserialization of a missing `<labels>` element valid. Implement `GetLabel` by:

1. finding a label where `Domain` and `Key` match ordinally;
2. finding the requested language case-insensitively;
3. falling back to `DataSetLanguages.English` case-insensitively;
4. returning `defaultValue` when neither translation exists;
5. returning `key` when `defaultValue` is null.

Do not create a label or translation during lookup. If `Labels` is null, continue directly to the default/key fallback.

- [ ] **Step 4: Run the focused tests and the existing localization tests**

Run:

```powershell
dotnet test .\tests\PathfinderDb.Schema.Tests\PathfinderDb.Schema.Tests.csproj -c Release --no-restore --nologo --filter FullyQualifiedName~DataSetResolvesLabelsWithRequestedEnglishDefaultAndKeyFallbacks|FullyQualifiedName~DataSetLabelResolutionDoesNotCreateAMissingCatalog|FullyQualifiedName~LocalizationSupportsMultipleLanguagesAndCaseInsensitiveReplacement --verbosity minimal
```

Expected: all selected tests pass.

- [ ] **Step 5: Commit DataSet integration**

```powershell
git add src/PathfinderDb.Schema/DataSet.cs tests/PathfinderDb.Schema.Tests/ContractSerializationTests.cs
git commit -m "Integrate localized labels into datasets"
```

### Task 3: Cover XML and JSON round-trip compatibility

**Files:**
- Modify: `tests/PathfinderDb.Schema.Tests/ContractSerializationTests.cs`
- Modify: `tests/PathfinderDb.Schema.Tests/XmlCompatibilityTests.cs`

**Interfaces:**
- Verifies `DataSet.Labels` round-trips through `XmlSerializer` and Newtonsoft.Json.
- Verifies datasets with no labels remain readable.
- Verifies XML and JSON preserve the same domain, key, language, and value data.

- [ ] **Step 1: Add failing XML/JSON round-trip tests**

Add a test that creates one dataset with labels for `spellSchool` and `creatureType`, serializes it to XML and JSON, deserializes both, and asserts:

```csharp
Assert.AreEqual(2, xmlRestored.Labels.Count);
Assert.AreEqual(xmlRestored.Labels[0].Domain, jsonRestored.Labels[0].Domain);
Assert.AreEqual(xmlRestored.Labels[0].Key, jsonRestored.Labels[0].Key);
Assert.AreEqual(xmlRestored.Labels[0].Translations[0].Language, jsonRestored.Labels[0].Translations[0].Language);
Assert.AreEqual(xmlRestored.Labels[0].Translations[0].Value, jsonRestored.Labels[0].Translations[0].Value);
```

Assert that serialized XML contains `<labels>` and `<label domain="spellSchool" key="evocation">`, and JSON contains the serializer’s established `Labels`, `Domain`, `Key`, `Translations`, `Language`, and `Value` property names. Do not change existing JSON naming conventions just for this feature.

Add a compatibility test that deserializes an existing historical dataset and asserts `Labels == null` or an empty collection without throwing.

- [ ] **Step 2: Run the new tests to expose any missing serialization contract**

Run the focused XML/JSON tests. Expected: the tests fail if the new annotations or `DataSet` integration are incomplete; otherwise they provide the first round-trip regression coverage.

- [ ] **Step 3: Adjust only serialization annotations needed by the established conventions**

Keep the model’s XML annotations explicit. If the current JSON serializer cannot round-trip a property because of a naming conflict, add the smallest Newtonsoft.Json attribute needed to the new types only; do not alter existing serialized property names.

- [ ] **Step 4: Run the focused tests and the full test suite**

Run:

```powershell
dotnet test .\PathfinderDb.Schema.sln -c Release --no-restore --nologo --verbosity minimal
```

Expected: all existing and new tests pass, including historical XML fixtures.

- [ ] **Step 5: Commit round-trip coverage**

```powershell
git add tests/PathfinderDb.Schema.Tests/ContractSerializationTests.cs tests/PathfinderDb.Schema.Tests/XmlCompatibilityTests.cs
git commit -m "Test localized label serialization"
```

### Task 4: Update the XSD and versioned XML sample

**Files:**
- Modify: `xml/pathfinderDb.xsd`
- Create or modify: `xml/samples/DataSetLabels.xml`
- Modify: `tests/PathfinderDb.Schema.Tests/XmlCompatibilityTests.cs`
- Modify: `README.md`

**Interfaces:**
- XSD accepts an optional `labels` collection under `dataSet`.
- Each label requires `domain` and `key`, and contains zero or more translations.
- Each translation uses `xml:lang` and text content.
- The sample is loadable through `DataSet.Load`.

- [ ] **Step 1: Add the sample-loading test**

Add a test that loads `Fixtures/Current/DataSetLabels.xml` copied from the repository sample by the existing test project content rules, then asserts:

```csharp
Assert.AreEqual(1, dataSet.Labels.Count);
Assert.AreEqual(DataSetLabelDomains.SpellSchool, dataSet.Labels[0].Domain);
Assert.AreEqual("Évocation", dataSet.GetLabel(DataSetLabelDomains.SpellSchool, "evocation", DataSetLanguages.French));
```

If the test project does not currently copy `xml/samples` into `Fixtures/Current`, add the smallest explicit `<None CopyToOutputDirectory="PreserveNewest">` item needed in the test project.

- [ ] **Step 2: Run the sample test and verify it fails**

Run the focused sample test. Expected: the file or `labels` schema is missing.

- [ ] **Step 3: Extend `pathfinderDb.xsd`**

Add a `labels` choice element alongside `sources`, `spells`, `feats`, `spellLists`, `monsters`, and `gears`. Define nested `label` and `translation` structures with the exact attributes and XML namespace from the spec. Keep the element optional and repeatable at the dataset level.

- [ ] **Step 4: Add the XML sample**

Create `xml/samples/DataSetLabels.xml` with the `urn:pathfinderDb` root, one `spellSchool/evocation` label, French and English translations, and a small spell payload only if required by the existing sample-loading setup.

- [ ] **Step 5: Run XSD and tests**

Run:

```powershell
dotnet test .\PathfinderDb.Schema.sln -c Release --no-restore --nologo --verbosity minimal
```

No package is generated for this issue unless explicitly requested.

- [ ] **Step 6: Document the public contract**

Add a README section describing `DataSet.Labels`, the nine domain constants, the fallback order, and the fact that technical enum values remain unchanged. Add an `Unreleased` changelog entry with the XML/JSON compatibility note.

- [ ] **Step 7: Commit schema and documentation updates**

```powershell
git add xml/pathfinderDb.xsd xml/samples/DataSetLabels.xml tests/PathfinderDb.Schema.Tests/XmlCompatibilityTests.cs README.md CHANGELOG.md
git commit -m "Document localized label catalog contract"
```

### Task 5: Final verification and local handoff

**Files:**
- No source changes expected; inspect all files changed by Tasks 1-4.

- [ ] **Step 1: Review the complete diff**

Run:

```powershell
git diff HEAD~5..HEAD --check
git diff HEAD~5..HEAD --stat
```

Confirm the diff contains only label schema, dataset integration, tests, XSD/sample, README, changelog, and the approved specification/plan commits.

- [ ] **Step 2: Run the complete verification suite**

Run:

```powershell
dotnet restore .\PathfinderDb.Schema.sln --nologo
dotnet build .\PathfinderDb.Schema.sln -c Release --no-restore --nologo
dotnet test .\PathfinderDb.Schema.sln -c Release --no-build --nologo --verbosity minimal
```

Expected: build exits with code 0 and every test passes. Existing XML documentation warnings are acceptable; compilation errors and test failures are not.

- [ ] **Step 3: Check release boundaries**

Run:

```powershell
git status --short --branch
git tag --points-at HEAD
```

Do not create a tag, push a branch, create a package, or invoke GitHub Actions. Report the local commit and verification results only.
