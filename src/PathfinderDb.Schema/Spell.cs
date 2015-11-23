// -----------------------------------------------------------------------
// <copyright file="Spell.cs" organization="Pathfinder-Fr">
// Copyright (c) Pathfinder-fr. Tous droits reserves.
// </copyright>
// -----------------------------------------------------------------------

using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace PathfinderDb.Schema
{
    [XmlType("spell", Namespace = Namespaces.PathfinderDb)]
    [DebuggerDisplay("{Name}")]
    public class Spell : Element
    {
        /// <summary>
        /// Gets or sets the unique id of this spell.
        /// It's often generated using its title, replacing spaces by '-' and removing any extra character.
        /// </summary>
        [XmlAttribute("id")]
        public string Id { get; set; }

        /// <summary>
        /// Gets or sets the title (name) of this spell.
        /// </summary>
        [XmlElement("name")]
        public string Name { get; set; }

        [XmlAttribute("school")]
        public SpellSchool School { get; set; }

        [XmlAttribute("descriptor")]
        [DefaultValue(typeof (SpellDescriptors), "None")]
        public SpellDescriptors Descriptor { get; set; }

        [XmlArray("levels")]
        [XmlArrayItem("level")]
        public SpellListLevel[] Levels { get; set; }

        [XmlElement("components")]
        public SpellComponents Components { get; set; }

        [XmlElement("range")]
        public SpellRange Range { get; set; }

        [XmlElement("target")]
        public SpellTarget Target { get; set; }

        [XmlElement("castingTime")]
        public SpellCastingTime CastingTime { get; set; }

        [XmlElement("savingThrow")]
        public SpellSavingThrow SavingThrow { get; set; }

        [XmlElement("spellResistance")]
        public SpellResistance SpellResistance { get; set; }

        [XmlElement("summary")]
        public string Summary { get; set; }

        [EditorBrowsable(EditorBrowsableState.Never)]
        public bool ShouldSerializeSpellResistance()
        {
            return SpellResistance != null && SpellResistance.ShouldSerialize();
        }
    }
}