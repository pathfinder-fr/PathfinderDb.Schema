// -----------------------------------------------------------------------
// <copyright file="SpellList.cs" organization="Pathfinder-Fr">
// Copyright (c) Pathfinder-fr. Tous droits reserves.
// </copyright>
// -----------------------------------------------------------------------

using System.Xml.Serialization;

namespace PathfinderDb.Schema
{
    [XmlType("spellList")]
    public class SpellList
    {
        public string Id { get; set; }

        public class Ids
        {
            public const string Antipaladin = "antipaladin";

            [System.Obsolete("Use Antipaladin.")]
            public const string AntiPaladin = Antipaladin;

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

            public const string ElementalWizard = "elementalistWizard";

            [System.Obsolete("Use ElementalWizard.")]
            public const string ElementalistWizard = ElementalWizard;

            public const string Shaman = "shaman";

            public const string Bloodrager = "bloodrager";

            public const string Hypnotiseur = "hypnotiseur";

            public const string Medium = "medium";

            public const string Occultiste = "occultiste";

            public const string Psychiste = "psychiste";

            public const string SahirAfiyun = "sahir-afiyun";

            public const string Spirite = "spirite";

            public const string UnchainedSummoner = "summoner-unchained";

            [System.Obsolete("Use UnchainedSummoner.")]
            public const string SummonerUnchained = UnchainedSummoner;
        }
    }
}