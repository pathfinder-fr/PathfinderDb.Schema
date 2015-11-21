namespace PathfinderDb.Schema
{
    using System.Diagnostics;
    using System.Xml.Serialization;

    [XmlType("feat", Namespace = Namespaces.PathfinderDb)]
    [DebuggerDisplay("{Name}")]
    public class Feat : Element
    {
        /// <summary>
        /// Gets or sets the unique id of this feat.
        /// </summary>
        [XmlAttribute("id")]
        public string Id { get; set; }

        /// <summary>
        /// Gets or sets the name of this feat.
        /// </summary>
        [XmlElement("name")]
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the types of the feat.
        /// </summary>
        [XmlArray("types")]
        [XmlArrayItem("type")]
        public FeatType[] Types { get; set; }

        /// <summary>
        /// Gets or sets all prerequisites for this feat.
        /// </summary>
        [XmlArray("prerequisites")]
        [XmlArrayItem("prerequisite", Type = typeof(FeatPrerequisite))]
        [XmlArrayItem("choice", Type = typeof(FeatPrerequisiteChoice))]
        public object[] Prerequisites { get; set; }

        [XmlElement("summary")]
        public string Summary { get; set; }

        [XmlElement("description")]
        public string Description { get; set; }

        [XmlElement("benefit")]
        public string Benefit { get; set; }

        [XmlElement("normal")]
        public string Normal { get; set; }

        [XmlElement("special")]
        public string Special { get; set; }
    }
}