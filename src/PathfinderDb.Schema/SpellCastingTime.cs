// -----------------------------------------------------------------------
// <copyright file="SpellCastingTime.cs" organization="Pathfinder-Fr">
// Copyright (c) Pathfinder-fr. Tous droits reserves.
// </copyright>
// -----------------------------------------------------------------------

using System.ComponentModel;
using System.Xml.Serialization;

namespace PathfinderDb.Schema
{
    [XmlType("spellCastingTime")]
    public class SpellCastingTime
    {
        public SpellCastingTime()
        {
            Unit = TimeUnit.SimpleAction;
        }

        [XmlAttribute("unit")]
        [DefaultValue(typeof (TimeUnit), "simpleAction")]
        public TimeUnit Unit { get; set; }

        [XmlAttribute("value")]
        [DefaultValue(0)]
        public int Value { get; set; }

        [XmlText]
        public string Text { get; set; }

        public override string ToString()
        {
            switch (Unit)
            {
                case TimeUnit.SimpleAction:
                case TimeUnit.ImmediateAction:
                case TimeUnit.SwiftAction:
                case TimeUnit.FullRoundAction:
                    return Unit.ToString();

                case TimeUnit.Round:
                case TimeUnit.Minute:
                case TimeUnit.Hour:
                case TimeUnit.Day:
                    return string.Format("{0} {1}", Value, Unit);

                default:
                case TimeUnit.Special:
                    return Text;
            }
        }
    }
}