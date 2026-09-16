using System;
using System.IO;
using System.Xml.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using PathfinderDb.Schema;
using SchemaDataSet = PathfinderDb.Schema.DataSet;
using System.Xml.Serialization;

namespace Pathfinder.DataSet
{
    [TestClass]
    public class XmlCompatibilityTests
    {
        [TestMethod]
        public void HistoricalSpellDatasetShouldDeserialize()
        {
            var dataSet = LoadHistorical("SpellDataset.xml");

            Assert.AreEqual(1, dataSet.Spells.Count);
            Assert.AreEqual("abondance-de-munitions", dataSet.Spells[0].Id);
            Assert.AreEqual("Abondance de munitions", dataSet.Spells[0].Name);
            Assert.AreEqual(5, dataSet.Spells[0].Levels.Length);
            Assert.AreEqual("uc", dataSet.Spells[0].Source.Id);
            Assert.AreEqual("Abundant Ammunition", dataSet.Spells[0].Localization.Languages[0].Entries[0].Value);
        }

        [TestMethod]
        public void HistoricalFeatDatasetShouldDeserialize()
        {
            var dataSet = LoadHistorical("FeatDataset.xml");

            Assert.AreEqual(1, dataSet.Feats.Count);
            Assert.AreEqual("adepte-de-la-matraque", dataSet.Feats[0].Id);
            Assert.AreEqual(FeatType.Combat, dataSet.Feats[0].Types[0]);
            Assert.AreEqual("uc", dataSet.Feats[0].Source.Id);
            Assert.AreEqual(1, dataSet.Feats[0].Source.References.Count);
        }

        [TestMethod]
        public void HistoricalMonsterDatasetShouldDeserialize()
        {
            var dataSet = LoadHistorical("MonsterDataset.xml");

            Assert.AreEqual(2, dataSet.Monsters.Count);
            Assert.AreEqual("aasimar", dataSet.Monsters[0].Id);
            Assert.AreEqual(CreatureType.Outsider, dataSet.Monsters[0].Type);
            Assert.AreEqual(CreatureEnvironment.Aquatic, dataSet.Monsters[1].Environment);
        }

        [TestMethod]
        public void CurrentSpellSampleShouldPreserveThePublicXmlContract()
        {
            var path = Path.Combine(AppContext.BaseDirectory, "Fixtures", "Current", "Spell.xml");
            var document = XDocument.Load(path);
            var dataSet = SchemaDataSet.Load(File.OpenRead(path));

            Assert.AreEqual("urn:pathfinderDb", document.Root.Name.NamespaceName);
            Assert.AreEqual(1, dataSet.Spells.Count);
            Assert.AreEqual("sort-abri", dataSet.Spells[0].Id);
            Assert.AreEqual(SpellList.Ids.Bard, dataSet.Spells[0].Levels[0].List);
        }

        [TestMethod]
        public void HistoricalSpellDatasetShouldSurviveXmlRoundTrip()
        {
            var dataSet = LoadHistorical("SpellDataset.xml");
            var serializer = new XmlSerializer(typeof(SchemaDataSet));

            string xml;
            using (var writer = new StringWriter())
            {
                serializer.Serialize(writer, dataSet);
                xml = writer.ToString();
            }

            var document = XDocument.Parse(xml);
            using (var reader = new StringReader(xml))
            {
                var roundTripped = SchemaDataSet.Load(reader);
                Assert.AreEqual(dataSet.Spells[0].Id, roundTripped.Spells[0].Id);
                Assert.AreEqual(dataSet.Spells[0].Levels.Length, roundTripped.Spells[0].Levels.Length);
            }

            Assert.AreEqual("urn:pathfinderDb", document.Root.Name.NamespaceName);
        }

        [TestMethod]
        public void HistoricalSpellDatasetShouldUseStringEnumValuesInJson()
        {
            var dataSet = LoadHistorical("SpellDataset.xml");
            var settings = new JsonSerializerSettings();
            settings.Converters.Add(new StringEnumConverter());

            var json = JsonConvert.SerializeObject(dataSet, settings);
            var roundTripped = JsonConvert.DeserializeObject<SchemaDataSet>(json, settings);

            StringAssert.Contains(json, "\"School\":\"Conjuration\"");
            Assert.AreEqual(SpellSchool.Conjuration, roundTripped.Spells[0].School);
        }

        private static SchemaDataSet LoadHistorical(string fileName)
        {
            var path = Path.Combine(AppContext.BaseDirectory, "Fixtures", "Historical", fileName);
            using (var stream = File.OpenRead(path))
            {
                return SchemaDataSet.Load(stream);
            }
        }
    }
}
