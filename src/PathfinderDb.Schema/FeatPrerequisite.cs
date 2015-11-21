namespace PathfinderDb.Schema
{
    using System.ComponentModel;
    using System.Xml.Serialization;

    [XmlType("featPrerequisite")]
    public class FeatPrerequisite : IFeatPrerequisiteItem
    {
        [XmlAttribute("type")]
        public FeatPrerequisiteType Type { get; set; }

        [XmlAttribute("otherType")]
        public string OtherType { get; set; }

        [XmlAttribute("value")]
        public string Value { get; set; }

        [XmlAttribute("subValue")]
        public string SubValue { get; set; }

        [XmlAttribute("number")]
        [DefaultValue(0)]
        public int Number { get; set; }

        [XmlText]
        public string Description { get; set; }
    }
}