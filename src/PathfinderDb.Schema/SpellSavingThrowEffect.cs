// -----------------------------------------------------------------------
// <copyright file="SpellSavingThrowEffect.cs" organization="Pathfinder-Fr">
// Copyright (c) Pathfinder-fr. Tous droits reserves.
// </copyright>
// -----------------------------------------------------------------------

using System.Xml.Serialization;

namespace PathfinderDb.Schema
{
    [XmlType("spellSavingThrowEffect")]
    public enum SpellSavingThrowEffect
    {
        [XmlEnum("none")]
        None = 0,

        [XmlEnum("negates")]
        Negates,

        [XmlEnum("partial")]
        Partial,

        [XmlEnum("half")]
        Half,

        [XmlEnum("disbelief")]
        Disbelief,

        [XmlEnum("special")]
        Special
    }
}