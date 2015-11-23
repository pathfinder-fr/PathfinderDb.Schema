// -----------------------------------------------------------------------
// <copyright file="DataSet.cs" organization="Pathfinder-Fr">
// Copyright (c) Pathfinder-fr. Tous droits reserves.
// </copyright>
// -----------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace PathfinderDb.Schema
{
    [XmlType("dataSet", Namespace = Namespaces.PathfinderDb)]
    [XmlRoot("dataSet", Namespace = Namespaces.PathfinderDb)]
    public class DataSet
    {
        public DataSet()
        {
            Sources = new List<Source>();
            Spells = new List<Spell>();
            SpellLists = new List<SpellList>();
            Feats = new List<Feat>();
            Monsters = new List<Monster>();
        }

        [XmlElement("header")]
        public DataSetHeader Header { get; set; }

        /// <summary>
        ///     Gets or sets all sources used in this dataset.
        /// </summary>
        [XmlArray("sources")]
        public List<Source> Sources { get; set; }

        [XmlArray("spells")]
        public List<Spell> Spells { get; set; }

        [XmlArray("spellLists")]
        public List<SpellList> SpellLists { get; set; }

        [XmlArray("feats")]
        public List<Feat> Feats { get; set; }

        [XmlArray("monsters")]
        public List<Monster> Monsters { get; set; }

        [XmlAttribute("lang", Namespace = Namespaces.Xml)]
        public string Lang { get; set; }

        public static DataSet Load(Stream stream)
        {
            using (var xmlReader = XmlReader.Create(stream))
            {
                return Load(xmlReader);
            }
        }

        public static DataSet Load(XmlReader xmlReader)
        {
            var serializer = new XmlSerializer(typeof (DataSet));
            return (DataSet) serializer.Deserialize(xmlReader);
        }

        public static DataSet Load(TextReader textReader)
        {
            var serializer = new XmlSerializer(typeof (DataSet));
            return (DataSet) serializer.Deserialize(textReader);
        }

        public void Add(DataSet other)
        {
            Copy(() => other.Sources, () => Sources, t => Sources = t);
            Copy(() => other.Feats, () => Feats, t => Feats = t);
            Copy(() => other.Spells, () => Spells, t => Spells = t);
        }

        private void Copy<T>(Func<List<T>> getSource, Func<List<T>> getTarget, Action<List<T>> setTarget)
        {
            var source = getSource();
            if (source == null)
                return;

            var target = getTarget();
            if (target == null)
            {
                target = new List<T>();
                setTarget(target);
            }

            target.AddRange(source);
        }
    }
}