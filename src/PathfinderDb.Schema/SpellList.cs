namespace PathfinderDb.Schema
{
    using System.Xml.Serialization;

    [XmlType("spellList")]
    public class SpellList
    {
        public string Id { get; set; }

        public class Ids
        {
            public const string AntiPaladin = "antipaladin";

            public const string Bard = "bard";

            public const string SorcererWizard = "sorcerer-wizard";

            public const string Ranger = "ranger";

            public const string Paladin = "paladin";

            public const string Druid = "druid";

            public const string Cleric = "cleric";

            public const string Inquisitor = "inquisitor";

            public const string Summoner = "summoner";

            public const string Oracle = "oracle";

            public const string Witch = "witch";

            public const string Alchemist = "alchemist";

            public const string Magus = "magus";

            public const string ElementalistWizard = "elementalistWizard";
        }
    }
}