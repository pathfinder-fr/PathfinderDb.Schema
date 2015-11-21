namespace PathfinderDb.Schema
{
    using System.ComponentModel;
    using System.Xml.Serialization;

    [XmlType("money")]
    public class MoneyAmount
    {
        /// <summary>
        /// Initializes a new instance of <see cref="MoneyAmount" />.
        /// </summary>
        public MoneyAmount()
        {
            this.Coin = Coin.Gold;
        }

        [XmlAttribute("coin")]
        [DefaultValue(typeof(Coin), "Gold")]
        public Coin Coin { get; set; }

        [XmlAttribute("value")]
        [DefaultValue(0)]
        public int Value { get; set; }

        [XmlText]
        public string Special { get; set; }
    }
}