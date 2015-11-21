namespace PathfinderDb.Schema
{
    using System.Xml.Serialization;

    [XmlType("magicAuraStrength")]
    public enum MagicAuraStrength
    {
        [XmlEnum("none")]
        None,

        [XmlEnum("faint")]
        Faint,

        [XmlEnum("moderate")]
        Moderate,

        [XmlEnum("strong")]
        Strong,

        [XmlEnum("overwhelming")]
        Overwhelming
    }
}