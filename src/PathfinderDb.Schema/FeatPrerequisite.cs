// -----------------------------------------------------------------------
// <copyright file="FeatPrerequisite.cs" organization="Pathfinder-Fr">
// Copyright (c) Pathfinder-fr. Tous droits reserves.
// </copyright>
// -----------------------------------------------------------------------

using System.ComponentModel;
using System.Xml.Serialization;

namespace PathfinderDb.Schema
{
    [XmlType("featPrerequisite")]
    public class FeatPrerequisite : IFeatPrerequisiteItem
    {
        [XmlAttribute("type")]
        public FeatPrerequisiteType Type { get; set; }

        [XmlAttribute("otherType")]
        public string OtherType { get; set; }

        [XmlAttribute("value")]
        public string Value { get; set; }

        [XmlAttribute("subValue")]
        public string SubValue { get; set; }

        [XmlAttribute("number")]
        [DefaultValue(0)]
        public int Number { get; set; }

        [XmlText]
        public string Description { get; set; }
    }
}