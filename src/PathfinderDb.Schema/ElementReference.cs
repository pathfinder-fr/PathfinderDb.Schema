// -----------------------------------------------------------------------
// <copyright file="ElementReference.cs" organization="Pathfinder-Fr">
// Copyright (c) Pathfinder-fr. Tous droits reserves.
// </copyright>
// -----------------------------------------------------------------------

using System;
using System.ComponentModel;
using System.Xml.Serialization;

namespace PathfinderDb.Schema
{
    [XmlType("itemReference")]
    public class ElementReference
    {
        [XmlText]
        public string Name { get; set; }

        [XmlAttribute("lang", Namespace = "http://www.w3.org/XML/1998/namespace")]
        public string Lang { get; set; }

        [XmlIgnore]
        public Uri Href { get; set; }

        [XmlAttribute("href")]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public string HrefString
        {
            get { return Href == null ? null : Href.ToString(); }
            set { Href = value == null ? null : new Uri(value); }
        }
    }
}