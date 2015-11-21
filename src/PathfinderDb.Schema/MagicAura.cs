namespace PathfinderDb.Schema
{
    using System.Xml.Serialization;

    [XmlType("magicAura")]
    public class MagicAura
    {
        [XmlAttribute("school")]
        public SpellSchool? School { get; set; }

        [XmlAttribute("strength")]
        public MagicAuraStrength Strength { get; set; }
    }
}