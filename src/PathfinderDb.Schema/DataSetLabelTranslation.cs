// -----------------------------------------------------------------------
// <copyright file="DataSetLabelTranslation.cs" organization="Pathfinder-Fr">
// Copyright (c) Pathfinder-fr. Tous droits reserves.
// </copyright>
// -----------------------------------------------------------------------

using System.Xml.Serialization;

namespace PathfinderDb.Schema
{
    [XmlType("translation")]
    public class DataSetLabelTranslation
    {
        [XmlAttribute("lang", Namespace = Namespaces.Xml)]
        public string Language { get; set; }

        [XmlText]
        public string Value { get; set; }
    }
}
