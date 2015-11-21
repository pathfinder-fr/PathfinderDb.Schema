namespace PathfinderDb.Schema
{
    using System.Collections.Generic;
    using System.Xml.Serialization;

    [XmlType("itemSource")]
    public class ElementSource
    {
        public static readonly ElementSource Empty = new ElementSource();

        public ElementSource()
        {
            this.References = new List<ElementReference>();
        }

        /// <summary>
        /// Gets or set the id of the source this item comes from.
        /// </summary>
        [XmlAttribute("id")]
        public string Id { get; set; }

        [XmlArray("references")]
        [XmlArrayItem("reference")]
        public List<ElementReference> References { get; set; }

        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public bool ShouldSerializeReferences()
        {
            return this.References != null && this.References.Count != 0;
        }
    }
}