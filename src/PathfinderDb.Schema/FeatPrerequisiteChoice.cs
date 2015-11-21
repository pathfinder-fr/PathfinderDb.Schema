namespace PathfinderDb.Schema
{
    using System.Xml.Serialization;

    [XmlType("choice")]
    public class FeatPrerequisiteChoice : IFeatPrerequisiteItem
    {
        [XmlElement("prerequisite")]
        public FeatPrerequisite[] Items { get; set; }
    }
}