namespace PathfinderDb.Schema
{
    using System.Diagnostics;
    using System.Xml.Serialization;

    /// <summary>
    /// Represents a monster.
    /// </summary>
    [XmlType("monster", Namespace = Namespaces.PathfinderDb)]
    [DebuggerDisplay("{Name}")]
    public class Monster : Element
    {
        /// <summary>
        /// Gets or sets the unique id of this monster.
        /// </summary>
        [XmlAttribute("id")]
        public string Id { get; set; }

        /// <summary>
        /// Gets or sets the name of this monster.
        /// </summary>
        [XmlAttribute("name")]
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the combat rank value.
        /// </summary>
        [XmlAttribute("cr")]
        public decimal CR { get; set; }

        [XmlAttribute("climate")]
        public CreatureClimate Climate { get; set; }

        [XmlAttribute("environment")]
        public CreatureEnvironment Environment { get; set; }

        [XmlAttribute("type")]
        public CreatureType Type { get; set; }
    }
}