namespace PathfinderDb.Schema
{
    using System.Xml.Serialization;

    [XmlType("entry")]
    public class ElementLocalizationEntry
    {
        [XmlText]
        public string Value { get; set; }

        [XmlAttribute("href")]
        public string Href { get; set; }
    }
}