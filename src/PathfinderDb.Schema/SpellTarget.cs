// -----------------------------------------------------------------------
// <copyright file="SpellTarget.cs" organization="Pathfinder-Fr">
// Copyright (c) Pathfinder-fr. Tous droits reserves.
// </copyright>
// -----------------------------------------------------------------------

using System.Xml.Serialization;

namespace PathfinderDb.Schema
{
    /// <summary>
    ///     Describe a target for a spell.
    /// </summary>
    [XmlType("spellTarget")]
    public class SpellTarget
    {
        /// <summary>
        ///     Gets or sets the value describing the target.
        /// </summary>
        [XmlText]
        public string Value { get; set; }
    }
}