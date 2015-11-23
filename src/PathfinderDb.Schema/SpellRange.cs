// -----------------------------------------------------------------------
// <copyright file="SpellRange.cs" organization="Pathfinder-Fr">
// Copyright (c) Pathfinder-fr. Tous droits reserves.
// </copyright>
// -----------------------------------------------------------------------

using System.ComponentModel;
using System.Xml.Serialization;

namespace PathfinderDb.Schema
{
    [XmlType("spellRange")]
    public class SpellRange
    {
        public SpellRange()
        {
            Unit = SpellRangeUnit.Specific;
        }

        [XmlAttribute("unit")]
        [DefaultValue(typeof (SpellRangeUnit), "specific")]
        public SpellRangeUnit Unit { get; set; }

        [XmlText]
        public string SpecificValue { get; set; }

        public override string ToString()
        {
            if (Unit == SpellRangeUnit.Specific)
            {
                return SpecificValue;
            }
            if (Unit == SpellRangeUnit.Squares)
            {
                return string.Format("{0} Squares", SpecificValue);
            }
            return Unit.ToString();
        }
    }
}