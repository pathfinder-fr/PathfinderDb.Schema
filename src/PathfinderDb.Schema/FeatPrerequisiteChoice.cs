// -----------------------------------------------------------------------
// <copyright file="FeatPrerequisiteChoice.cs" organization="Pathfinder-Fr">
// Copyright (c) Pathfinder-fr. Tous droits reserves.
// </copyright>
// -----------------------------------------------------------------------

using System.Xml.Serialization;

namespace PathfinderDb.Schema
{
    [XmlType("choice")]
    public class FeatPrerequisiteChoice : IFeatPrerequisiteItem
    {
        [XmlElement("prerequisite")]
        public FeatPrerequisite[] Items { get; set; }
    }
}