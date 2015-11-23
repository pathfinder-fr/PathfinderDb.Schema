// -----------------------------------------------------------------------
// <copyright file="MagicAura.cs" organization="Pathfinder-Fr">
// Copyright (c) Pathfinder-fr. Tous droits reserves.
// </copyright>
// -----------------------------------------------------------------------

using System.Xml.Serialization;

namespace PathfinderDb.Schema
{
    [XmlType("magicAura")]
    public class MagicAura
    {
        [XmlAttribute("school")]
        public SpellSchool? School { get; set; }

        [XmlAttribute("strength")]
        public MagicAuraStrength Strength { get; set; }
    }
}