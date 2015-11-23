// -----------------------------------------------------------------------
// <copyright file="SpellSchool.cs" organization="Pathfinder-Fr">
// Copyright (c) Pathfinder-fr. Tous droits reserves.
// </copyright>
// -----------------------------------------------------------------------

using System.Xml.Serialization;

namespace PathfinderDb.Schema
{
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