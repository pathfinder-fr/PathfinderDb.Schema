// -----------------------------------------------------------------------
// <copyright file="SpecialBoolean.cs" organization="Pathfinder-Fr">
// Copyright (c) Pathfinder-fr. Tous droits reserves.
// </copyright>
// -----------------------------------------------------------------------

using System.Xml.Serialization;

namespace PathfinderDb.Schema
{
    /// <summary>
    /// Specifies a boolean which contains a third value named 'special' and used to describe specific conditions.
    /// </summary>
    [XmlType("specialBoolean")]
    public enum SpecialBoolean
    {
        [XmlEnum("false")]
        No = 0,

        [XmlEnum("true")]
        Yes = 1,

        [XmlEnum("special")]
        Special = 2
    }
}