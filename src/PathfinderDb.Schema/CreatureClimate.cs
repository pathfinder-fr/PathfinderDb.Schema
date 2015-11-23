// -----------------------------------------------------------------------
// <copyright file="CreatureClimate.cs" organization="Pathfinder-Fr">
// Copyright (c) Pathfinder-fr. Tous droits reserves.
// </copyright>
// -----------------------------------------------------------------------

using System.Xml.Serialization;

namespace PathfinderDb.Schema
{
    [XmlType("creatureClimate")]
    public enum CreatureClimate
    {
        [XmlEnum("")]
        Other,

        [XmlEnum("planar")]
        Planar,

        [XmlEnum("cold")]
        Cold,

        [XmlEnum("temperate")]
        Temperate,

        [XmlEnum("warm")]
        Warm
    }
}