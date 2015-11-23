// -----------------------------------------------------------------------
// <copyright file="CreatureType.cs" organization="Pathfinder-Fr">
// Copyright (c) Pathfinder-fr. Tous droits reserves.
// </copyright>
// -----------------------------------------------------------------------

using System.Xml.Serialization;

namespace PathfinderDb.Schema
{
    [XmlType("creatureType")]
    public enum CreatureType
    {
        [XmlEnum("")]
        Other,

        [XmlEnum("aberration")]
        Aberration,

        [XmlEnum("animal")]
        Animal,

        [XmlEnum("construct")]
        Construct,

        [XmlEnum("dragon")]
        Dragon,

        [XmlEnum("fey")]
        Fey,

        [XmlEnum("humanoid")]
        Humanoid,

        [XmlEnum("magicalBeast")]
        MagicalBeast,

        [XmlEnum("monstrousHumanoid")]
        MonstrousHumanoid,

        [XmlEnum("ooze")]
        Ooze,

        [XmlEnum("outsider")]
        Outsider,

        [XmlEnum("plant")]
        Plant,

        [XmlEnum("undead")]
        Undead,

        [XmlEnum("vermin")]
        Vermin
    }
}