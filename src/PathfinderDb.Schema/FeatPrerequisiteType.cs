// -----------------------------------------------------------------------
// <copyright file="FeatPrerequisiteType.cs" organization="Pathfinder-Fr">
// Copyright (c) Pathfinder-fr. Tous droits reserves.
// </copyright>
// -----------------------------------------------------------------------

using System.Xml.Serialization;

namespace PathfinderDb.Schema
{
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
        MonsterRace = 9
    }
}