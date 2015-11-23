// -----------------------------------------------------------------------
// <copyright file="EquipmentItem.cs" organization="Pathfinder-Fr">
// Copyright (c) Pathfinder-fr. Tous droits reserves.
// </copyright>
// -----------------------------------------------------------------------

using System.Diagnostics;
using System.Xml.Serialization;

namespace PathfinderDb.Schema
{
    [XmlType("equipmentItem", Namespace = Namespaces.PathfinderDb)]
    [DebuggerDisplay("{Name}")]
    public class EquipmentItem : Element
    {
        [XmlElement("price")]
        public MoneyAmount Price { get; set; }

        [XmlElement("weight")]
        public WeightAmount Weight { get; set; }

        [XmlElement("name")]
        public string Name { get; set; }
    }
}