// -----------------------------------------------------------------------
// <copyright file="TimeUnit.cs" organization="Pathfinder-Fr">
// Copyright (c) Pathfinder-fr. Tous droits reserves.
// </copyright>
// -----------------------------------------------------------------------

using System.Xml.Serialization;

namespace PathfinderDb.Schema
{
    /// <summary>
    /// Specifies the units of time which can be used to describe a timed element.
    /// </summary>
    public enum TimeUnit
    {
        /// <summary>
        /// The unit of time can not be described using a unit of time.
        /// It can be a conditional value, or a range.
        /// </summary>
        [XmlEnum("special")]
        Special = 0,

        /// <summary>
        /// A simple action, as described in pathfinder combat rules.
        /// </summary>
        [XmlEnum("simpleAction")]
        SimpleAction,

        /// <summary>
        /// An immediate action, as described in pathfinder combat rules.
        /// </summary>
        [XmlEnum("immediateAction")]
        ImmediateAction,

        /// <summary>
        /// A swift action, as described in pathfinder combat rules.
        /// </summary>
        [XmlEnum("swiftAction")]
        SwiftAction,

        /// <summary>
        /// A full round action, as described in pathfinder combat rules.
        /// </summary>
        [XmlEnum("fullRoundAction")]
        FullRoundAction,

        /// <summary>
        /// One or more rounds.
        /// A round is a unit of time in pathfinder rpg consisting of six seconds.
        /// </summary>
        [XmlEnum("round")]
        Round,

        /// <summary>
        /// One or more minutes.
        /// </summary>
        [XmlEnum("minute")]
        Minute,

        /// <summary>
        /// One or more hours.
        /// </summary>
        [XmlEnum("hour")]
        Hour,

        /// <summary>
        /// One or more days.
        /// </summary>
        [XmlEnum("day")]
        Day
    }
}