// -----------------------------------------------------------------------
// <copyright file="SpellComponents.cs" organization="Pathfinder-Fr">
// Copyright (c) Pathfinder-fr. Tous droits reserves.
// </copyright>
// -----------------------------------------------------------------------

using System.ComponentModel;
using System.Xml.Serialization;

namespace PathfinderDb.Schema
{
    [XmlType("spellComponents")]
    public class SpellComponents
    {
        [XmlAttribute("kinds")]
        [DefaultValue(typeof (SpellComponentKinds), "None")]
        public SpellComponentKinds Kinds { get; set; }

        [XmlText]
        public string Description { get; set; }

        [EditorBrowsable(EditorBrowsableState.Never)]
        public bool ShouldSerializeDescription()
        {
            return !string.IsNullOrWhiteSpace(Description);
        }
    }
}