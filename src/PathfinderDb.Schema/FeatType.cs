// -----------------------------------------------------------------------
// <copyright file="FeatType.cs" organization="Pathfinder-Fr">
// Copyright (c) Pathfinder-fr. Tous droits reserves.
// </copyright>
// -----------------------------------------------------------------------

using System.Xml.Serialization;

namespace PathfinderDb.Schema
{
    [XmlType("featType")]
    public enum FeatType
    {
        [XmlEnum("general")]
        General = 0,

        [XmlEnum("combat")]
        Combat = 1,

        [XmlEnum("critical")]
        Critical = 2,

        [XmlEnum("itemCreation")]
        ItemCreation = 3,

        [XmlEnum("metamagic")]
        Metamagic = 4,

        [XmlEnum("monster")]
        Monster = 5,

        [XmlEnum("team")]
        Teamwork = 6,

        [XmlEnum("grit")]
        Grit = 7,

        [XmlEnum("performance")]
        Performance = 8,

        [XmlEnum("style")]
        Style = 9,

        [XmlEnum("panache")]
        Panache = 10
    }
}