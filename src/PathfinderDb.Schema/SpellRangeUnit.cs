// -----------------------------------------------------------------------
// <copyright file="SpellRangeUnit.cs" organization="Pathfinder-Fr">
// Copyright (c) Pathfinder-fr. Tous droits reserves.
// </copyright>
// -----------------------------------------------------------------------

using System.Xml.Serialization;

namespace PathfinderDb.Schema
{
    [XmlType("spellRangeUnit")]
    public enum SpellRangeUnit
    {
        [XmlEnum("specific")]
        Specific = 0,

        [XmlEnum("personal")]
        Personal,

        [XmlEnum("touch")]
        Touch,

        [XmlEnum("close")]
        Close,

        [XmlEnum("medium")]
        Medium,

        [XmlEnum("long")]
        Long,

        [XmlEnum("unlimited")]
        Unlimited,

        [XmlEnum("squares")]
        Squares
    }
}