namespace PathfinderDb.Schema
{
    using System.Collections.Generic;
    using System.IO;
    using System.Xml;
    using System.Xml.Serialization;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class FeatTests
    {
        private static readonly XmlSerializer serializer = new XmlSerializer(typeof(Feat), Namespaces.PathfinderDb);

        [TestMethod]
        public void Should_Serialize_Prerequisites()
        {
            var feat = new Feat
            {
                Name = "Sample Feat",
                Benefit = "Sample Benefit",
                Description = "Sample Description",
                Id = "sample-feat",
                Normal = "Normal",
                Source = new ElementSource
                {
                    Id = "sample-source",
                    References = new List<ElementReference>
                    {
                        new ElementReference { HrefString = "http://example.com", Name = "Example" },
                        new ElementReference { HrefString = "http://example.com", Name = "Example" }
                    }
                },
                Special = "Special",
                Summary = "Summary",
                Types = new[] { FeatType.General, FeatType.Combat },
                Prerequisites = new object[]
                {
                    new FeatPrerequisite(),
                    new FeatPrerequisiteChoice
                    {
                        Items = new[] { new FeatPrerequisite() }
                    }
                }
            };

            using (var writer = new StringWriter())
            {
                serializer.Serialize(writer, feat);


                var xmlDoc = new XmlDocument();
                xmlDoc.LoadXml(writer.ToString());
            }
        }

        [TestMethod]
        public void Should_Deserialize_Prerequisites()
        {
            const string xml = "<feat xmlns=\"urn:pathfinderDb\"><prerequisites><prerequisite/><choice><prerequisite/></choice></prerequisites></feat>";

            Feat feat;
            using (var reader = new StringReader(xml))
            {
                feat = (Feat)serializer.Deserialize(reader);
            }

            Assert.IsNotNull(feat.Prerequisites);
            Assert.AreEqual(2, feat.Prerequisites.Length);
            Assert.IsInstanceOfType(feat.Prerequisites[0], typeof(FeatPrerequisite));
            Assert.IsInstanceOfType(feat.Prerequisites[1], typeof(FeatPrerequisiteChoice));
        }
    }
}