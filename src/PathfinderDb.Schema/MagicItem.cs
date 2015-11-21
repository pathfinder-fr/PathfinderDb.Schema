namespace PathfinderDb.Schema
{
    using System.Xml.Serialization;

    [XmlType("magicItem")]
    public class MagicItem
    {
        [XmlElement("price")]
        public MoneyAmount Price { get; set; }

        [XmlAttribute("slot")]
        public BodySlot Slot { get; set; }

        [XmlElement("weight")]
        public WeightAmount Weight { get; set; }

        [XmlElement("aura")]
        public MagicAura Aura { get; set; }

        [XmlAttribute("casterLevel")]
        public int? CasterLevel { get; set; }

        [XmlElement("cost")]
        public MoneyAmount Cost { get; set; }
    }
}