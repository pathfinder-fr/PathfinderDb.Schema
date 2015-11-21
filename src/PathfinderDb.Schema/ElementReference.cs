namespace PathfinderDb.Schema
{
    using System;
    using System.ComponentModel;
    using System.Xml.Serialization;

    [XmlType("itemReference")]
    public class ElementReference
    {
        [XmlText]
        public string Name { get; set; }

        [XmlAttribute("lang", Namespace = "http://www.w3.org/XML/1998/namespace")]
        public string Lang { get; set; }

        [XmlIgnore]
        public Uri Href { get; set; }

        [XmlAttribute("href")]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public string HrefString
        {
            get { return this.Href == null ? null : this.Href.ToString(); }
            set { this.Href = value == null ? null : new Uri(value); }
        }
    }
}