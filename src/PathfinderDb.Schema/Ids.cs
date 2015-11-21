// -----------------------------------------------------------------------
// <copyright file="Ids.cs" organization="Pathfinder-Fr">
// Copyright (c) Pathfinder-fr. Tous droits reserves.
// </copyright>
// -----------------------------------------------------------------------

namespace PathfinderDb.Schema
{
    public static class Ids
    {
        public static string Normalize(string id)
        {
            return (id ?? string.Empty).ToLowerInvariant().Replace(",", string.Empty).Replace(' ', '-');
        }
    }
}