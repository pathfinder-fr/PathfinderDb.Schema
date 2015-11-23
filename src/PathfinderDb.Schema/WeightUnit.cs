// -----------------------------------------------------------------------
// <copyright file="WeightUnit.cs" organization="Pathfinder-Fr">
// Copyright (c) Pathfinder-fr. Tous droits reserves.
// </copyright>
// -----------------------------------------------------------------------

using System.Xml.Serialization;

namespace PathfinderDb.Schema
{
    /// <summary>
    ///     Specifies the unit used to describe a weight.
    /// </summary>
    [XmlType("weightUnit")]
    public enum WeightUnit
    {
        /// <summary>
        ///     Pounds.
        /// </summary>
        [XmlEnum("lbs")]
        Pounds,

        /// <summary>
        ///     Kilograms.
        /// </summary>
        [XmlEnum("kg")]
        Kilogram
    }
}