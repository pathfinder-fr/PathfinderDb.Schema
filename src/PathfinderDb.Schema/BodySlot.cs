// -----------------------------------------------------------------------
// <copyright file="BodySlot.cs" organization="Pathfinder-Fr">
// Copyright (c) Pathfinder-fr. Tous droits reserves.
// </copyright>
// -----------------------------------------------------------------------

using System.Xml.Serialization;

namespace PathfinderDb.Schema
{
    [XmlType("bodySlot")]
    public enum BodySlot
    {
        [XmlEnum("none")]
        None,

        [XmlEnum("armor")]
        Armor,

        [XmlEnum("belts")]
        Belts,

        [XmlEnum("chest")]
        Chest,

        [XmlEnum("eyes")]
        Eyes,

        [XmlEnum("feet")]
        Feet,

        [XmlEnum("hands")]
        Hands,

        [XmlEnum("head")]
        Head,

        [XmlEnum("headband")]
        Headband,

        [XmlEnum("neck")]
        Neck,

        [XmlEnum("ring")]
        Ring,

        [XmlEnum("shield")]
        Shield,

        [XmlEnum("shoulders")]
        Shoulders,

        [XmlEnum("wrist")]
        Wrist
    }
}