// -----------------------------------------------------------------------
// <copyright file="ElementSource.cs" organization="Pathfinder-Fr">
// Copyright (c) Pathfinder-fr. Tous droits reserves.
// </copyright>
// -----------------------------------------------------------------------

using System.Collections.Generic;
using System.ComponentModel;
using System.Xml.Serialization;

namespace PathfinderDb.Schema
{
    [XmlType("itemSource")]
    public class ElementSource
    {
        public static readonly ElementSource Empty = new ElementSource();

        public ElementSource()
        {
            References = new List<ElementReference>();
        }

        /// <summary>
        /// Gets or set the id of the source this item comes from.
        /// </summary>
        [XmlAttribute("id")]
        public string Id { get; set; }

        [XmlArray("references")]
        [XmlArrayItem("reference")]
        public List<ElementReference> References { get; set; }

        [EditorBrowsable(EditorBrowsableState.Never)]
        public bool ShouldSerializeReferences()
        {
            return References != null && References.Count != 0;
        }
    }
}