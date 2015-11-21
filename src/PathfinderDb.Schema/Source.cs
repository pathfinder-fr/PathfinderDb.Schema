namespace PathfinderDb.Schema
{
    using System;
    using System.ComponentModel;
    using System.Diagnostics;
    using System.Xml.Serialization;

    /// <summary>
    /// Represents a product source, such as an official book (player's guide, bestiary) or a web product.
    /// </summary>
    [DebuggerDisplay("{Name}")]
    [XmlType("source")]
    public class Source
    {
        /// <summary>
        /// Gets or sets the unique id for this product source.
        /// It can be one of the values of <see cref="Ids"/> or any custom string.
        /// It should be culture insensitive (all pathfinder rpg rulebook are referenced using the "pfrpg" id, ignoring the language used).
        /// </summary>
        [XmlAttribute("id")]
        public string Id { get; set; }

        /// <summary>
        /// Gets or sets the display name for this product source.
        /// </summary>
        [XmlAttribute("name")]
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the unique url for this product.
        /// </summary>
        [XmlIgnore]
        public Uri Url { get; set; }

        /// <summary>
        /// Gets or sets the unique url for this product, in a text format.
        /// </summary>
        [XmlAttribute("url")]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public string UrlString
        {
            get { return this.Url == null ? null : this.Url.ToString(); }
            set { this.Url = value == null ? null : new Uri(value); }
        }

        /// <summary>
        /// Contains most officials ids.
        /// </summary>
        public static class Ids
        {
            /// <summary>
            /// Source id for pathfinder rpg rules.
            /// </summary>
            public const string PathfinderRpg = "pfrpg";

            /// <summary>
            /// Source id for bestiary.
            /// </summary>
            public const string Bestiary = "bestiary";

            /// <summary>
            /// Source id for bestiary 2.
            /// </summary>
            public const string Bestiary2 = "bestiary2";

            /// <summary>
            /// Source id for bestiary 3.
            /// </summary>
            public const string Bestiary3 = "bestiary3";

            /// <summary>
            /// Source id for bestiary 4.
            /// </summary>
            public const string Bestiary4 = "bestiary4";

            /// <summary>
            /// Source id for pathfinder advanced player guide.
            /// </summary>
            public const string AdvancedPlayerGuide = "apg";

            /// <summary>
            /// Source id for pathfinder ultimate combat.
            /// </summary>
            public const string UltimateCombat = "uc";

            /// <summary>
            /// Source id for pathfinder ultimate magic.
            /// </summary>
            public const string UltimateMagic = "um";

            /// <summary>
            /// Source id for the paizo blog.
            /// </summary>
            public const string PaizoBlog = "paizoBlog";

            public static string AdventurePath(int number)
            {
                return "ap#" + number;
            }
        }
    }
}