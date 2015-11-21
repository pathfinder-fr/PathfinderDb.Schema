namespace PathfinderDb.Schema
{
    using System.Xml.Serialization;

    [XmlType("gearCategory")]
    public enum GearCategory
    {
        [XmlEnum("adventuring")]
        Adventuring,

        [XmlEnum("toolsAndSkillKits")]
        ToolsAndSkillKits,

        [XmlEnum("petsAndFamiliar")]
        PetsAndFamiliar,

        [XmlEnum("guardAndHuntingAnimals")]
        GuardAndHuntingAnimals,

        [XmlEnum("farmAndWorkAnimals")]
        FarmAndWorkAnimals,

        [XmlEnum("mounts")]
        Mounts,

        [XmlEnum("animalRelatedGear")]
        AnimalRelatedGear,

        [XmlEnum("specialSubstancesAndItems")]
        SpecialSubstancesAndItems,

        [XmlEnum("clothing")]
        Clothing,

        [XmlEnum("entertainment")]
        Entertainment,

        [XmlEnum("tradeGoods")]
        TradeGoods,

        [XmlEnum("foodAndDrink")]
        FoodAndDrink,

        [XmlEnum("lodgingAndServices")]
        LodgingAndServices,

        [XmlEnum("transport")]
        Transport,

        [XmlEnum("alchemicalRemedies")]
        AlchemicalRemedies,

        [XmlEnum("alchemicalTools")]
        AlchemicalTools,

        [XmlEnum("alchemicalWeapons")]
        AlchemicalWeapons
    }
}