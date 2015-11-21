namespace PathfinderDb.Schema
{
    using System.ComponentModel;
    using System.Xml.Serialization;

    [XmlType("spellSavingThrow")]
    public class SpellSavingThrow
    {
        [XmlAttribute("target")]
        public SpellSavingThrowTarget Target { get; set; }

        [XmlAttribute("effect")]
        public SpellSavingThrowEffect Effect { get; set; }

        [XmlAttribute("harmless")]
        [DefaultValue(false)]
        public bool Harmless { get; set; }

        [XmlAttribute("objects")]
        [DefaultValue(false)]
        public bool Objects { get; set; }

        [XmlText]
        public string SpecificValue { get; set; }
    }
}