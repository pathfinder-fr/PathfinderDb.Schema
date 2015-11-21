namespace PathfinderDb.Schema
{
    using System.Xml.Serialization;

    /// <summary>
    /// Specifies all spell schools known in pathfinder rpg.
    /// </summary>
    [XmlType("spellSchool")]
    public enum SpellSchool
    {
        [XmlEnum("universal")]
        Universal,

        [XmlEnum("abjuration")]
        Abjuration,

        [XmlEnum("conjuration")]
        Conjuration,

        [XmlEnum("divination")]
        Divination,

        [XmlEnum("enchantment")]
        Enchantment,

        [XmlEnum("evocation")]
        Evocation,

        [XmlEnum("illusion")]
        Illusion,

        [XmlEnum("necromancy")]
        Necromancy,

        [XmlEnum("transmutation")]
        Transmutation
    }
}