namespace PathfinderDb.Schema
{
    using System;
    using System.Collections.Generic;
    using System.Xml.Serialization;

    [XmlType("dataSet", Namespace = Namespaces.PathfinderDb)]
    [XmlRoot("dataSet", Namespace = Namespaces.PathfinderDb)]
    public class DataSet
    {
        public DataSet()
        {
            this.Sources = new List<Source>();
            this.Spells = new List<Spell>();
            this.SpellLists = new List<SpellList>();
            this.Feats = new List<Feat>();
            this.Monsters = new List<Monster>();
        }

        [XmlElement("header")]
        public DataSetHeader Header { get; set; }

        /// <summary>
        /// Gets or sets all sources used in this dataset.
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

        [XmlAttribute("lang", Namespace = "http://www.w3.org/XML/1998/namespace")]
        public string Lang { get; set; }

        public static DataSet Load(System.IO.Stream stream)
        {
            using (var xmlReader = System.Xml.XmlReader.Create(stream))
            {
                return Load(xmlReader);
            }
        }

        public static DataSet Load(System.Xml.XmlReader xmlReader)
        {
            var serializer = new XmlSerializer(typeof(DataSet));
            return (DataSet)serializer.Deserialize(xmlReader);
        }

        public static DataSet Load(System.IO.TextReader textReader)
        {
            var serializer = new XmlSerializer(typeof(DataSet));
            return (DataSet)serializer.Deserialize(textReader);
        }

        public void Add(DataSet other)
        {
            this.Copy(() => other.Sources, () => this.Sources, t => this.Sources = t);
            this.Copy(() => other.Feats, () => this.Feats, t => this.Feats = t);
            this.Copy(() => other.Spells, () => this.Spells, t => this.Spells = t);
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