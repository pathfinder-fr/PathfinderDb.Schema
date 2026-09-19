// -----------------------------------------------------------------------
// <copyright file="DataSetLabel.cs" organization="Pathfinder-Fr">
// Copyright (c) Pathfinder-fr. Tous droits reserves.
// </copyright>
// -----------------------------------------------------------------------

using System.Collections.Generic;
using System.Xml.Serialization;

namespace PathfinderDb.Schema
{
    [XmlType("label")]
    public class DataSetLabel
    {
        [XmlAttribute("domain")]
        public string Domain { get; set; }

        [XmlAttribute("key")]
        public string Key { get; set; }

        [XmlElement("translation")]
        public List<DataSetLabelTranslation> Translations { get; set; }
    }
}
