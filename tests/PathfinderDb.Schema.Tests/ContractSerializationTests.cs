using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Linq;
using System.Xml.Serialization;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using PathfinderDb.Schema;
using SchemaDataSet = PathfinderDb.Schema.DataSet;

namespace Pathfinder.DataSet
{
    [TestClass]
    public class ContractSerializationTests
    {
        private static readonly XmlSerializer SpellSerializer =
            new XmlSerializer(typeof(Spell), Namespaces.PathfinderDb);

        [TestMethod]
        public void SpellSerializesPublicAttributesCombinedDescriptorSourcesAndLocalization()
        {
            var spell = new Spell
            {
                Id = "storm",
                Name = "Storm",
                School = SpellSchool.Evocation,
                Descriptor = SpellDescriptors.Electricity | SpellDescriptors.Fire,
                Levels = new[] { new SpellListLevel { List = SpellList.Ids.Bard, Level = 3 } },
                Components = new SpellComponents { Kinds = SpellComponentKinds.Verbal, Description = "a feather" },
                Sources = new[]
                {
                    new ElementSource { Id = "pfrpg" },
                    new ElementSource { Id = "um" }
                },
                Localization = new ElementLocalization()
            };
            spell.Localization.AddLocalizedEntry("en", "name", "Storm");

            string xml;
            using (var writer = new StringWriter())
            {
                SpellSerializer.Serialize(writer, spell);
                xml = writer.ToString();
            }

            var root = XElement.Parse(xml);
            Assert.AreEqual("spell", root.Name.LocalName);
            Assert.AreEqual("urn:pathfinderDb", root.Name.NamespaceName);
            Assert.AreEqual("evocation", (string)root.Attribute("school"));
            Assert.AreEqual("electricity fire", (string)root.Attribute("descriptor"));
            var sources = root.Element(XName.Get("sources", "urn:pathfinderDb")).Elements(XName.Get("source", "urn:pathfinderDb")).ToArray();
            Assert.AreEqual(2, sources.Length);
            Assert.AreEqual("pfrpg", (string)sources[0].Attribute("id"));
            Assert.AreEqual("um", (string)sources[1].Attribute("id"));
            Assert.AreEqual("Storm", (string)root.Descendants(XName.Get("entry", "urn:pathfinderDb")).Single());
        }

        [TestMethod]
        public void EmptyAndNullOptionalSpellPropertiesAreAbsentFromXml()
        {
            var spell = new Spell
            {
                Id = "empty",
                Levels = Array.Empty<SpellListLevel>(),
                Components = new SpellComponents { Kinds = SpellComponentKinds.None, Description = " " },
                Sources = new[] { new ElementSource() }
            };

            string xml;
            using (var writer = new StringWriter())
            {
                SpellSerializer.Serialize(writer, spell);
                xml = writer.ToString();
            }

            var root = XElement.Parse(xml);
            Assert.IsNull(root.Attribute("descriptor"));
            var levels = root.Element(XName.Get("levels", "urn:pathfinderDb"));
            Assert.IsNotNull(levels);
            Assert.IsFalse(levels.Elements().Any());
            var components = root.Element(XName.Get("components", "urn:pathfinderDb"));
            Assert.IsNotNull(components);
            Assert.AreEqual(string.Empty, components.Value);
            Assert.IsNull(root.Element(XName.Get("source", "urn:pathfinderDb")));
            Assert.IsNull(root.Element(XName.Get("sources", "urn:pathfinderDb")));
            Assert.IsNull(root.Element(XName.Get("localization", "urn:pathfinderDb")));
        }

        [TestMethod]
        public void FeatSerializesMultipleTypesAndPolymorphicPrerequisites()
        {
            var feat = new Feat
            {
                Id = "combat-feat",
                Name = "Combat Feat",
                Types = new[] { FeatType.General, FeatType.Combat },
                Prerequisites = new object[]
                {
                    new FeatPrerequisite(),
                    new FeatPrerequisiteChoice { Items = new[] { new FeatPrerequisite() } }
                }
            };
            var serializer = new XmlSerializer(typeof(Feat), Namespaces.PathfinderDb);

            string xml;
            using (var writer = new StringWriter())
            {
                serializer.Serialize(writer, feat);
                xml = writer.ToString();
            }

            var root = XElement.Parse(xml);
            Assert.AreEqual(2, root.Element(XName.Get("types", "urn:pathfinderDb")).Elements().Count());
            Assert.AreEqual("general", (string)root.Element(XName.Get("types", "urn:pathfinderDb")).Elements().First());
            Assert.AreEqual("combat", (string)root.Element(XName.Get("types", "urn:pathfinderDb")).Elements().Skip(1).First());
            Assert.AreEqual(1, root.Element(XName.Get("prerequisites", "urn:pathfinderDb"))
                .Elements(XName.Get("prerequisite", "urn:pathfinderDb")).Count());
            Assert.AreEqual(1, root.Element(XName.Get("prerequisites", "urn:pathfinderDb"))
                .Elements(XName.Get("choice", "urn:pathfinderDb")).Count());
        }

        [TestMethod]
        public void MonsterSerializesEnumAttributesAndDecimalChallengeRating()
        {
            var monster = new Monster
            {
                Id = "aboleth",
                Name = "Aboleth",
                CR = 7.5m,
                Climate = CreatureClimate.Temperate,
                Environment = CreatureEnvironment.Aquatic,
                Type = CreatureType.Aberration
            };
            var serializer = new XmlSerializer(typeof(Monster), Namespaces.PathfinderDb);

            using (var writer = new StringWriter())
            {
                serializer.Serialize(writer, monster);
                var root = XElement.Parse(writer.ToString());
                Assert.AreEqual("7.5", (string)root.Attribute("cr"));
                Assert.AreEqual("temperate", (string)root.Attribute("climate"));
                Assert.AreEqual("aquatic", (string)root.Attribute("environment"));
                Assert.AreEqual("aberration", (string)root.Attribute("type"));
            }
        }

        [TestMethod]
        public void SourceUrlRoundTripsAndElementSourceOmitsEmptyReferences()
        {
            var source = new Source { Id = Source.Ids.AdventurePath(3), Name = "Adventure", Url = new Uri("https://example.test/source") };
            var sourceSerializer = new XmlSerializer(typeof(Source));
            using (var writer = new StringWriter())
            {
                sourceSerializer.Serialize(writer, source);
                var restored = (Source)sourceSerializer.Deserialize(new StringReader(writer.ToString()));
                Assert.AreEqual("ap#3", restored.Id);
                Assert.AreEqual(source.Url, restored.Url);
                Assert.AreEqual("https://example.test/source", restored.UrlString);
            }

            var elementSource = new ElementSource();
            var elementSerializer = new XmlSerializer(typeof(ElementSource));
            using (var writer = new StringWriter())
            {
                elementSerializer.Serialize(writer, elementSource);
                Assert.IsFalse(XElement.Parse(writer.ToString()).Elements().Any());
            }

            elementSource = new ElementSource
            {
                Id = "uc",
                References = new System.Collections.Generic.List<ElementReference>
                {
                    new ElementReference { Name = "First", HrefString = "https://example.test/1" },
                    new ElementReference { Name = "Second", HrefString = "https://example.test/2" }
                }
            };
            using (var writer = new StringWriter())
            {
                elementSerializer.Serialize(writer, elementSource);
                var root = XElement.Parse(writer.ToString());
                var references = root.Element("references").Elements("reference").ToArray();
                Assert.AreEqual(2, references.Length);
                Assert.AreEqual("First", references[0].Value);
                Assert.AreEqual("https://example.test/2", (string)references[1].Attribute("href"));
            }
        }

        [TestMethod]
        public void PublicIdentifiersIncludeAllCorpusSourcesAndSpellLists()
        {
            CollectionAssert.AreEquivalent(
                new[]
                {
                    "bestiary5",
                    "bookofthedamned",
                    "codexmonstrueux"
                },
                new[]
                {
                    Source.Ids.Bestiary5,
                    Source.Ids.BookOfTheDamned,
                    Source.Ids.CodexMonstrueux
                });

            CollectionAssert.AreEquivalent(
                new[]
                {
                    "bloodrager",
                    "hypnotiseur",
                    "medium",
                    "occultiste",
                    "psychiste",
                    "sahir-afiyun",
                    "spirite",
                    "summoner-unchained"
                },
                new[]
                {
                    SpellList.Ids.Bloodrager,
                    SpellList.Ids.Hypnotiseur,
                    SpellList.Ids.Medium,
                    SpellList.Ids.Occultiste,
                    SpellList.Ids.Psychiste,
                    SpellList.Ids.SahirAfiyun,
                    SpellList.Ids.Spirite,
                    SpellList.Ids.UnchainedSummoner
                });
        }

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
                Assert.AreEqual(
                    "fr-FR",
                    (string)root.Element(XName.Get("translation", Namespaces.PathfinderDb))
                        .Attribute(XNamespace.Xml + "lang"));
                Assert.AreEqual("Évocation", root.Value);
            }
        }

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

        [TestMethod]
        public void DataSetLabelsRoundTripThroughXmlAndJsonWithEquivalentValues()
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
                            new DataSetLabelTranslation { Language = DataSetLanguages.French, Value = "Évocation" },
                            new DataSetLabelTranslation { Language = DataSetLanguages.English, Value = "Evocation" }
                        }
                    },
                    new DataSetLabel
                    {
                        Domain = DataSetLabelDomains.CreatureType,
                        Key = "outsider",
                        Translations = new List<DataSetLabelTranslation>
                        {
                            new DataSetLabelTranslation { Language = DataSetLanguages.English, Value = "Outsider" }
                        }
                    }
                }
            };

            string xml;
            using (var writer = new StringWriter())
            {
                new XmlSerializer(typeof(SchemaDataSet)).Serialize(writer, dataSet);
                xml = writer.ToString();
            }

            var json = JsonConvert.SerializeObject(dataSet);
            var xmlRestored = SchemaDataSet.Load(new StringReader(xml));
            var jsonRestored = JsonConvert.DeserializeObject<SchemaDataSet>(json);

            Assert.AreEqual(2, xmlRestored.Labels.Count);
            Assert.AreEqual(xmlRestored.Labels[0].Domain, jsonRestored.Labels[0].Domain);
            Assert.AreEqual(xmlRestored.Labels[0].Key, jsonRestored.Labels[0].Key);
            Assert.AreEqual(
                xmlRestored.Labels[0].Translations[0].Language,
                jsonRestored.Labels[0].Translations[0].Language);
            Assert.AreEqual(
                xmlRestored.Labels[0].Translations[0].Value,
                jsonRestored.Labels[0].Translations[0].Value);
            var xmlRoot = XElement.Parse(xml);
            Assert.IsNotNull(xmlRoot.Element(XName.Get("labels", Namespaces.PathfinderDb)));
            Assert.AreEqual(
                "evocation",
                (string)xmlRoot.Element(XName.Get("labels", Namespaces.PathfinderDb))
                    .Element(XName.Get("label", Namespaces.PathfinderDb))
                    .Attribute("key"));
            StringAssert.Contains(json, "\"Labels\"");
            StringAssert.Contains(json, "\"Domain\":\"spellSchool\"");
        }

        [TestMethod]
        public void LocalizationSupportsMultipleLanguagesAndCaseInsensitiveReplacement()
        {
            var localization = new ElementLocalization();
            localization.AddLocalizedEntry("en", "name", "First");
            localization.AddLocalizedEntry("EN", "NAME", "Updated");
            localization.AddLocalizedEntry("fr", "name", "Nom");

            Assert.AreEqual("Updated", localization.GetLocalizedEntry("en", "name"));
            Assert.AreEqual("Nom", localization.GetLocalizedEntry("fr", "name"));
            Assert.AreEqual("fallback", localization.GetLocalizedEntry("de", "name", "fallback"));
            Assert.AreEqual(2, localization.Languages.Count);
            Assert.AreEqual(1, localization.Languages.Single(l => l.Lang == "en").Entries.Count);
        }

        [TestMethod]
        public void JsonSerializationPreservesNullAndEmptyCollectionsAndStringEnums()
        {
            var dataSet = new SchemaDataSet
            {
                Spells = new System.Collections.Generic.List<Spell>
                {
                    new Spell { Id = "json-spell", School = SpellSchool.Conjuration, Levels = new SpellListLevel[0] }
                },
                Feats = new System.Collections.Generic.List<Feat>(),
                Monsters = new System.Collections.Generic.List<Monster>()
            };
            var settings = new JsonSerializerSettings();
            settings.Converters.Add(new StringEnumConverter());

            var json = JsonConvert.SerializeObject(dataSet, settings);
            var restored = JsonConvert.DeserializeObject<SchemaDataSet>(json, settings);

            StringAssert.Contains(json, "\"School\":\"Conjuration\"");
            StringAssert.Contains(json, "\"Levels\":[]");
            Assert.AreEqual("json-spell", restored.Spells[0].Id);
            Assert.AreEqual(SpellSchool.Conjuration, restored.Spells[0].School);
            Assert.AreEqual(0, restored.Feats.Count);
            Assert.AreEqual(0, restored.Monsters.Count);
        }
    }
}
