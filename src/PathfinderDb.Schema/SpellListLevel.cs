// -----------------------------------------------------------------------
// <copyright file="SpellListLevel.cs" organization="Pathfinder-Fr">
// Copyright (c) Pathfinder-fr. Tous droits reserves.
// </copyright>
// -----------------------------------------------------------------------

using System.Xml.Serialization;

namespace PathfinderDb.Schema
{
    [XmlType("spellListLevel")]
    public class SpellListLevel
    {
        [XmlAttribute("list")]
        public string List { get; set; }

        [XmlAttribute("level")]
        public int Level { get; set; }
    }
}