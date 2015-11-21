namespace PathfinderDb.Schema
{
    using System.Xml.Serialization;

    [XmlType("spellSavingThrowTarget")]
    public enum SpellSavingThrowTarget
    {
        [XmlEnum("none")]
        None = 0,

        [XmlEnum("reflex")]
        Reflex = 1,

        [XmlEnum("fortitude")]
        Fortitude = 2,

        [XmlEnum("will")]
        Will = 3,

        [XmlEnum("special")]
        Special = 4,
    }
}