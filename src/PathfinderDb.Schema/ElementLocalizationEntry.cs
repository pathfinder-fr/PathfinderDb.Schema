// -----------------------------------------------------------------------
// <copyright file="ElementLocalizationEntry.cs" organization="Pathfinder-Fr">
// Copyright (c) Pathfinder-fr. Tous droits reserves.
// </copyright>
// -----------------------------------------------------------------------

using System.Xml.Serialization;

namespace PathfinderDb.Schema
{
    [XmlType("entry")]
    public class ElementLocalizationEntry
    {
        [XmlText]
        public string Value { get; set; }

        [XmlAttribute("href")]
        public string Href { get; set; }
    }
}