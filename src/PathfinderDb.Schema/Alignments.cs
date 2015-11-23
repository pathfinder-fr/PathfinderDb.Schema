// -----------------------------------------------------------------------
// <copyright file="Alignments.cs" organization="Pathfinder-Fr">
// Copyright (c) Pathfinder-fr. Tous droits reserves.
// </copyright>
// -----------------------------------------------------------------------

using System;

namespace PathfinderDb.Schema
{
    [Flags]
    public enum Alignments
    {
        /// <summary>
        ///     None.
        /// </summary>
        None = 0,

        /// <summary>
        ///     Lawful Good.
        /// </summary>
        LawfulGood = 1,

        /// <summary>
        ///     Lawful Neutral.
        /// </summary>
        LawfulNeutral = 1 << 1,

        /// <summary>
        ///     Lawful Evil.
        /// </summary>
        LawfulEvil = 1 << 2,

        /// <summary>
        ///     Neutral Good.
        /// </summary>
        NeutralGood = 1 << 3,

        /// <summary>
        ///     Neutral Strict.
        /// </summary>
        NeutralStrict = 1 << 4,

        /// <summary>
        ///     Neutral Evil.
        /// </summary>
        NeutralEvil = 1 << 5,

        /// <summary>
        ///     Chaotic Good.
        /// </summary>
        ChaoticGood = 1 << 6,

        /// <summary>
        ///     Chaotic Neutral.
        /// </summary>
        ChaoticNeutral = 1 << 7,

        /// <summary>
        ///     Chaotic Evil.
        /// </summary>
        ChaoticEvil = 1 << 9,

        /// <summary>
        ///     Lawful.
        /// </summary>
        Lawful = LawfulGood | LawfulNeutral | LawfulEvil,

        /// <summary>
        ///     Chaotic.
        /// </summary>
        Chaotic = ChaoticGood | ChaoticNeutral | ChaoticEvil,

        /// <summary>
        ///     Neutral.
        /// </summary>
        Neutral = NeutralGood | NeutralStrict | NeutralEvil | LawfulNeutral | ChaoticNeutral,

        /// <summary>
        ///     Good.
        /// </summary>
        Good = LawfulGood | NeutralGood | ChaoticGood,

        /// <summary>
        ///     Evil.
        /// </summary>
        Evil = LawfulEvil | NeutralEvil | ChaoticEvil,

        /// <summary>
        ///     All
        /// </summary>
        All = Lawful | Neutral | Chaotic
    }
}