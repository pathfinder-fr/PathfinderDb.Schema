// -----------------------------------------------------------------------
// <copyright file="Coin.cs" organization="Pathfinder-Fr">
// Copyright (c) Pathfinder-fr. Tous droits reserves.
// </copyright>
// -----------------------------------------------------------------------

using System.Xml.Serialization;

namespace PathfinderDb.Schema
{
    [XmlType("coin")]
    public enum Coin
    {
        [XmlEnum("cp")]
        Copper = 0,

        [XmlEnum("sp")]
        Silver = 1,

        [XmlEnum("gp")]
        Gold = 2,

        [XmlEnum("pp")]
        Platinium = 3
    }
}