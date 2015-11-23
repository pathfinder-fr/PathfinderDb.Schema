// -----------------------------------------------------------------------
// <copyright file="MagicAuraStrength.cs" organization="Pathfinder-Fr">
// Copyright (c) Pathfinder-fr. Tous droits reserves.
// </copyright>
// -----------------------------------------------------------------------

using System.Xml.Serialization;

namespace PathfinderDb.Schema
{
    [XmlType("magicAuraStrength")]
    public enum MagicAuraStrength
    {
        [XmlEnum("none")]
        None,

        [XmlEnum("faint")]
        Faint,

        [XmlEnum("moderate")]
        Moderate,

        [XmlEnum("strong")]
        Strong,

        [XmlEnum("overwhelming")]
        Overwhelming
    }
}