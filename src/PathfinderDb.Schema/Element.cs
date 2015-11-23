// -----------------------------------------------------------------------
// <copyright file="Element.cs" organization="Pathfinder-Fr">
// Copyright (c) Pathfinder-fr. Tous droits reserves.
// </copyright>
// -----------------------------------------------------------------------

using System.ComponentModel;
using System.Xml.Serialization;

namespace PathfinderDb.Schema
{
    public abstract class Element
    {
        protected Element()
        {
            Source = new ElementSource();
        }

        [XmlElement("source")]
        public ElementSource Source
        {
            get
            {
                if (Sources != null && Sources.Length != 0)
                {
                    return Sources[0];
                }

                return ElementSource.Empty;
            }

            set
            {
                if (value == null)
                {
                    Sources = null;
                }
                else
                {
                    Sources = new[] {value};
                }
            }
        }

        [XmlArray("sources")]
        [XmlArrayItem("source")]
        public ElementSource[] Sources { get; set; }

        [XmlElement("localization")]
        public ElementLocalization Localization { get; set; }

        public ElementLocalization OpenLocalization()
        {
            return Localization ?? (Localization = new ElementLocalization());
        }

        [EditorBrowsable(EditorBrowsableState.Never)]
        public bool ShouldSerializeSource()
        {
            if (Sources != null && Sources.Length > 1)
                return false;

            if (Source == null)
                return false;

            return (Source.Id != null || Source.ShouldSerializeReferences());
        }

        [EditorBrowsable(EditorBrowsableState.Never)]
        public bool ShouldSerializeSources()
        {
            return !ShouldSerializeSource() && Sources != null && Sources.Length > 1;
        }

        [EditorBrowsable(EditorBrowsableState.Never)]
        public bool ShouldSerializeLocalization()
        {
            return Localization != null && Localization.ShouldSerialize();
        }
    }
}