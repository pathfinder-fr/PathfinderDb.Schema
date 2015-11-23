// -----------------------------------------------------------------------
// <copyright file="CharacterAttribute.cs" organization="Pathfinder-Fr">
// Copyright (c) Pathfinder-fr. Tous droits reserves.
// </copyright>
// -----------------------------------------------------------------------

using System.Xml.Serialization;

namespace PathfinderDb.Schema
{
    [XmlType("attribute")]
    public enum CharacterAttribute
    {
        [XmlEnum("none")]
        None = 0,

        [XmlEnum(CharacterAttributes.Strength)]
        Strength = 1,

        [XmlEnum(CharacterAttributes.Dexterity)]
        Dexterity = 2,

        [XmlEnum(CharacterAttributes.Constitution)]
        Constitution = 3,

        [XmlEnum(CharacterAttributes.Intelligence)]
        Intelligence = 4,

        [XmlEnum(CharacterAttributes.Wisdom)]
        Wisdom = 5,

        [XmlEnum(CharacterAttributes.Charisma)]
        Charisma = 6
    }
}