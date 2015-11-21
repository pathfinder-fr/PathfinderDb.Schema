// -----------------------------------------------------------------------
// <copyright file="Element.cs" organization="Pathfinder-Fr">
// Copyright (c) Pathfinder-fr. Tous droits reserves.
// </copyright>
// -----------------------------------------------------------------------

namespace PathfinderDb.Schema
{
    using System.Xml.Serialization;

    public abstract class Element
    {
        protected Element()
        {
            this.Source = new ElementSource();
        }

        [XmlElement("source")]
        public ElementSource Source
        {
            get
            {
                if (this.Sources != null && this.Sources.Length != 0)
                {
                    return this.Sources[0];
                }

                return ElementSource.Empty;
            }

            set
            {
                if (value == null)
                {
                    this.Sources = null;
                }
                else
                {
                    this.Sources = new[] { value };
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
            return this.Localization ?? (this.Localization = new ElementLocalization());
        }

        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public bool ShouldSerializeSource()
        {
            if (this.Sources != null && this.Sources.Length > 1)
                return false;

            if (this.Source == null)
                return false;

            return (this.Source.Id != null || this.Source.ShouldSerializeReferences());
        }

        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public bool ShouldSerializeSources()
        {
            return !this.ShouldSerializeSource() && this.Sources != null && this.Sources.Length > 1;
        }

        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public bool ShouldSerializeLocalization()
        {
            return this.Localization != null && this.Localization.ShouldSerialize();
        }
    }
}