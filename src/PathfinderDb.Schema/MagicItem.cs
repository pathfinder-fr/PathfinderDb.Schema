// -----------------------------------------------------------------------
// <copyright file="MagicItem.cs" organization="Pathfinder-Fr">
// Copyright (c) Pathfinder-fr. Tous droits reserves.
// </copyright>
// -----------------------------------------------------------------------

using System.Xml.Serialization;

namespace PathfinderDb.Schema
{
    [XmlType("magicItem")]
    public class MagicItem
    {
        [XmlElement("price")]
        public MoneyAmount Price { get; set; }

        [XmlAttribute("slot")]
        public BodySlot Slot { get; set; }

        [XmlElement("weight")]
        public WeightAmount Weight { get; set; }

        [XmlElement("aura")]
        public MagicAura Aura { get; set; }

        [XmlAttribute("casterLevel")]
        public int? CasterLevel { get; set; }

        [XmlElement("cost")]
        public MoneyAmount Cost { get; set; }
    }
}