namespace PathfinderDb.Schema
{
    using System.Xml.Serialization;

    /// <summary>
    /// Describe a target for a spell.
    /// </summary>
    [XmlType("spellTarget")]
    public class SpellTarget
    {
        /// <summary>
        /// Gets or sets the value describing the target.
        /// </summary>
        [XmlText]
        public string Value { get; set; }
    }
}