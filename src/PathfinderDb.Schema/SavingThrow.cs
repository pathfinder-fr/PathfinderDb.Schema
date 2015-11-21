namespace PathfinderDb.Schema
{
    using System.Xml.Serialization;

    [XmlType("savingThrow")]
    public enum SavingThrow
    {
        [XmlEnum("none")]
        None = 0,

        [XmlEnum("reflex")]
        Reflex = 1,

        [XmlEnum("fortitude")]
        Fortitude = 2,

        [XmlEnum("will")]
        Will = 3,
    }
}