namespace PathfinderDb.Schema
{
    using System.Xml.Serialization;

    [XmlType("creatureEnvironment")]
    public enum CreatureEnvironment
    {
        [XmlEnum("")]
        Unknown,

        [XmlEnum("aquatic")]
        Aquatic,

        [XmlEnum("sky")]
        Sky,

        [XmlEnum("hills")]
        Hills,

        [XmlEnum("desert")]
        Desert,

        [XmlEnum("forest-jungle")]
        ForestJungle,

        [XmlEnum("swamp")]
        Swamp,

        [XmlEnum("mountains")]
        Mountains,

        [XmlEnum("plains")]
        Plains,

        [XmlEnum("ruins-dungeons")]
        RuinsDungeons,

        [XmlEnum("underground")]
        Underground,

        [XmlEnum("urban")]
        Urban
    }
}