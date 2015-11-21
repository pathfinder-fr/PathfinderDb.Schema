namespace PathfinderDb.Schema
{
    using System;
    using System.ComponentModel;
    using System.Xml.Serialization;

    /// <summary>
    /// Describes a weight, using a value and a unit.
    /// </summary>
    [XmlType("weight")]
    public class WeightAmount
    {
        /// <summary>
        /// Gets or sets the unit used to describe the weight.
        /// </summary>
        [XmlAttribute("unit")]
        public WeightUnit Unit { get; set; }

        /// <summary>
        /// Gets or sets the value sued to describe the weight.
        /// </summary>
        [XmlAttribute("value")]
        [DefaultValue(typeof(decimal), "0")]
        public decimal Value { get; set; }

        [XmlText]
        public string Special { get; set; }

        public WeightAmount ConvertTo(WeightUnit unit)
        {
            if (unit == this.Unit)
            {
                return this;
            }

            var ratio = 1m;
            switch (this.Unit)
            {
                case WeightUnit.Kilogram:
                    switch (unit)
                    {
                        case WeightUnit.Pounds:
                            // kg => lb
                            ratio = 2.20462262m;
                            break;
                    }
                    break;

                case WeightUnit.Pounds:
                    switch (unit)
                    {
                        case WeightUnit.Kilogram:
                            // lb => kg
                            ratio = 0.45359237m;
                            break;
                    }
                    break;
            }

            return new WeightAmount
            {
                Special = this.Special,
                Unit = unit,
                Value = Math.Round(this.Value * ratio, 2)
            };
        }
    }
}