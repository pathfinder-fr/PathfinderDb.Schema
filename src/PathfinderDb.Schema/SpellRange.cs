namespace PathfinderDb.Schema
{
    using System.ComponentModel;
    using System.Xml.Serialization;

    [XmlType("spellRange")]
    public class SpellRange
    {
        public SpellRange()
        {
            this.Unit = SpellRangeUnit.Specific;
        }

        [XmlAttribute("unit")]
        [DefaultValue(typeof(SpellRangeUnit), "specific")]
        public SpellRangeUnit Unit { get; set; }

        [XmlText]
        public string SpecificValue { get; set; }

        public override string ToString()
        {
            if (this.Unit == SpellRangeUnit.Specific)
            {
                return this.SpecificValue;
            }
            if (this.Unit == SpellRangeUnit.Squares)
            {
                return string.Format("{0} Squares", this.SpecificValue);
            }
            return this.Unit.ToString();
        }
    }
}