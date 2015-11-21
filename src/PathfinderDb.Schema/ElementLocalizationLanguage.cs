namespace PathfinderDb.Schema
{
    using System.Collections.Generic;
    using System.Xml.Serialization;

    [XmlType("language")]
    public class ElementLocalizationLanguage
    {
        [XmlElement("entry")]
        public List<ElementLocalizationEntry> Entries { get; set; }

        [XmlAttribute("sourceId")]
        public string SourceId { get; set; }

        [XmlAttribute("lang", Namespace = "http://www.w3.org/XML/1998/namespace")]
        public string Lang { get; set; }
    }
}