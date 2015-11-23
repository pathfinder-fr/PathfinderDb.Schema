// -----------------------------------------------------------------------
// <copyright file="DataSetHeader.cs" organization="Pathfinder-Fr">
// Copyright (c) Pathfinder-fr. Tous droits reserves.
// </copyright>
// -----------------------------------------------------------------------

using System.Xml.Serialization;

namespace PathfinderDb.Schema
{
    [XmlType("header")]
    public class DataSetHeader
    {
        [XmlElement("title")]
        public string Title { get; set; }

        [XmlElement("description")]
        public string Description { get; set; }

        [XmlElement("author")]
        public string Author { get; set; }

        [XmlElement("generator")]
        public string Generator { get; set; }
    }
}