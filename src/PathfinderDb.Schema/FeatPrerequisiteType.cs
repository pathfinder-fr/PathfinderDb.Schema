namespace PathfinderDb.Schema
{
    using System.Xml.Serialization;

    [XmlType("featPrerequisiteType")]
    public enum FeatPrerequisiteType
    {
        [XmlEnum("other")]
        Other = 0,

        [XmlEnum("attribute")]
        Attribute = 1,

        [XmlEnum("feat")]
        Feat = 2,

        [XmlEnum("bba")]
        BBA = 3,

        [XmlEnum("classLevel")]
        ClassLevel = 4,

        [XmlEnum("spellcasterLevel")]
        SpellcasterLevel = 5,

        [XmlEnum("skillRank")]
        SkillRank = 6,

        [XmlEnum("race")]
        Race = 7,

        [XmlEnum("spellCast")]
        SpellCast = 8,

        [XmlEnum("monsterRace")]
        MonsterRace = 9,
    }
}