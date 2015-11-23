// -----------------------------------------------------------------------
// <copyright file="MoneyAmount.cs" organization="Pathfinder-Fr">
// Copyright (c) Pathfinder-fr. Tous droits reserves.
// </copyright>
// -----------------------------------------------------------------------

using System.ComponentModel;
using System.Xml.Serialization;

namespace PathfinderDb.Schema
{
    [XmlType("money")]
    public class MoneyAmount
    {
        /// <summary>
        /// Initializes a new instance of <see cref="MoneyAmount" />.
        /// </summary>
        public MoneyAmount()
        {
            Coin = Coin.Gold;
        }

        [XmlAttribute("coin")]
        [DefaultValue(typeof (Coin), "Gold")]
        public Coin Coin { get; set; }

        [XmlAttribute("value")]
        [DefaultValue(0)]
        public int Value { get; set; }

        [XmlText]
        public string Special { get; set; }
    }
}