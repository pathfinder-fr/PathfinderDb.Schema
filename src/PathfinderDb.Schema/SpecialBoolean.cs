namespace PathfinderDb.Schema
{
    using System.Xml.Serialization;

    /// <summary>
    /// Specifies a boolean which contains a third value named 'special' and used to describe specific conditions.
    /// </summary>
    [XmlType("specialBoolean")]
    public enum SpecialBoolean
    {
        [XmlEnum("false")]
        No = 0,

        [XmlEnum("true")]
        Yes = 1,

        [XmlEnum("special")]
        Special = 2
    }
}