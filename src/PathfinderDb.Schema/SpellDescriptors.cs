namespace PathfinderDb.Schema
{
    using System;
    using System.Xml.Serialization;

    [Flags]
    [XmlType("spellDescriptor")]
    public enum SpellDescriptors
    {
        [XmlEnum("")]
        None = 0,

        [XmlEnum("acid")]
        Acid = 1,

        [XmlEnum("air")]
        Air = 1 << 1,

        [XmlEnum("chaotic")]
        Chaotic = 1 << 2,

        [XmlEnum("cold")]
        Cold = 1 << 3,

        [XmlEnum("darkness")]
        Darkness = 1 << 4,

        [XmlEnum("death")]
        Death = 1 << 5,

        [XmlEnum("earth")]
        Earth = 1 << 6,

        [XmlEnum("electricity")]
        Electricity = 1 << 7,

        [XmlEnum("evil")]
        Evil = 1 << 8,

        [XmlEnum("fear")]
        Fear = 1 << 9,

        [XmlEnum("fire")]
        Fire = 1 << 10,

        [XmlEnum("force")]
        Force = 1 << 12,

        [XmlEnum("good")]
        Good = 1 << 13,

        [XmlEnum("healing")]
        Healing = 1 << 14,

        [XmlEnum("languageDependent")]
        LanguageDependent = 1 << 15,

        [XmlEnum("lawful")]
        Lawful = 1 << 16,

        [XmlEnum("light")]
        Light = 1 << 17,

        [XmlEnum("mindAffecting")]
        MindAffecting = 1 << 18,

        /// <remarks>
        /// Added in Ultimate Magic.
        /// </remarks>
        [XmlEnum("shadow")]
        Shadow = 1 << 19,

        [XmlEnum("sonic")]
        Sonic = 1 << 20,

        [XmlEnum("teleportation")]
        Teleportation = 1 << 21,

        [XmlEnum("water")]
        Water = 1 << 22,

        /// <remarks>
        /// Added in Ultimate Magic.
        /// </remarks>
        [XmlEnum("curse")]
        Curse = 1 << 23,

        /// <remarks>
        /// Added in Ultimate Magic.
        /// </remarks>
        [XmlEnum("disease")]
        Disease = 1 << 24,

        /// <remarks>
        /// Added in Ultimate Magic.
        /// </remarks>
        [XmlEnum("emotion")]
        Emotion = 1 << 25,

        /// <remarks>
        /// Added in Ultimate Magic.
        /// </remarks>
        [XmlEnum("pain")]
        Pain = 1 << 26,

        /// <remarks>
        /// Added in Ultimate Magic.
        /// </remarks>
        [XmlEnum("poison")]
        Poison = 1 << 27,
    }
}