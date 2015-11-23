// -----------------------------------------------------------------------
// <copyright file="Ids.cs" organization="Pathfinder-Fr">
// Copyright (c) Pathfinder-fr. Tous droits reserves.
// </copyright>
// -----------------------------------------------------------------------

namespace PathfinderDb.Schema
{
    public static class Ids
    {
        /// <summary>
        /// Normalization rules : lower invariant, remove all ',' and replace spaces with '-'.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static string Normalize(string id)
        {
            return (id ?? string.Empty).ToLowerInvariant().Replace(",", string.Empty).Replace(' ', '-');
        }
    }
}