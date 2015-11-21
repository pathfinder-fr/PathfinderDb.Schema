namespace PathfinderDb.Schema
{
    using System;
    using System.Xml.Serialization;

    [Flags]
    public enum SpellComponentKinds
    {
        [XmlEnum("")]
        None = 0,

        [XmlEnum("V")]
        Verbal = 1,

        [XmlEnum("S")]
        Somatic = 2,

        [XmlEnum("M")]
        Material = 4,

        [XmlEnum("F")]
        Focus = 8,

        [XmlEnum("DF")]
        DivineFocus = 16,

        [XmlEnum("M/DF")]
        MaterialOrDivineFocus = 32,

        [XmlEnum("F/DF")]
        FocusOrDivineFocus = 32
    }
}