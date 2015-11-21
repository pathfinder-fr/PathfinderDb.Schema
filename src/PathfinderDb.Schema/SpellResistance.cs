namespace PathfinderDb.Schema
{
    using System.ComponentModel;
    using System.Xml.Serialization;

    [XmlType("spellResistance")]
    public class SpellResistance
    {
        [XmlAttribute("resist")]
        [DefaultValue(typeof(SpecialBoolean), "no")]
        public SpecialBoolean Resist { get; set; }

        [XmlAttribute("objects")]
        [DefaultValue(false)]
        public bool Objects { get; set; }

        [XmlAttribute("harmless")]
        [DefaultValue(false)]
        public bool Harmless { get; set; }

        [XmlText]
        public string Text { get; set; }

        public bool ShouldSerialize()
        {
            return this.Resist != SpecialBoolean.No || this.Objects || this.Harmless || !string.IsNullOrWhiteSpace(this.Text);
        }
    }
}