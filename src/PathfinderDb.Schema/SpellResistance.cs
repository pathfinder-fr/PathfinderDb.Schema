// -----------------------------------------------------------------------
// <copyright file="SpellResistance.cs" organization="Pathfinder-Fr">
// Copyright (c) Pathfinder-fr. Tous droits reserves.
// </copyright>
// -----------------------------------------------------------------------

using System.ComponentModel;
using System.Xml.Serialization;

namespace PathfinderDb.Schema
{
    [XmlType("spellResistance")]
    public class SpellResistance
    {
        [XmlAttribute("resist")]
        [DefaultValue(typeof (SpecialBoolean), "no")]
        public SpecialBoolean Resist { get; set; }

        [XmlAttribute("objects")]
        [DefaultValue(false)]
        public bool Objects { get; set; }

        [XmlAttribute("harmless")]
        [DefaultValue(false)]
        public bool Harmless { get; set; }

        [XmlText]
        public string Text { get; set; }

        public bool ShouldSerialize()
        {
            return Resist != SpecialBoolean.No || Objects || Harmless || !string.IsNullOrWhiteSpace(Text);
        }
    }
}