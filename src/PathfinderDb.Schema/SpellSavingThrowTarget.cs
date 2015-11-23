// -----------------------------------------------------------------------
// <copyright file="SpellSavingThrowTarget.cs" organization="Pathfinder-Fr">
// Copyright (c) Pathfinder-fr. Tous droits reserves.
// </copyright>
// -----------------------------------------------------------------------

using System.Xml.Serialization;

namespace PathfinderDb.Schema
{
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
        Special = 4
    }
}