// -----------------------------------------------------------------------
// <copyright file="ElementLocalization.cs" organization="Pathfinder-Fr">
// Copyright (c) Pathfinder-fr. Tous droits reserves.
// </copyright>
// -----------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Xml.Serialization;

namespace PathfinderDb.Schema
{
    [XmlType("localization")]
    public class ElementLocalization
    {
        [XmlElement("language")]
        public List<ElementLocalizationLanguage> Languages { get; set; }

        public string GetLocalizedEntry(string language, string href, string defaultValue = null)
        {
            if (Languages == null)
            {
                return defaultValue;
            }

            var elementLanguage = Languages.FirstOrDefault(l => l.Lang == language);

            if (elementLanguage == null || elementLanguage.Entries == null)
            {
                return defaultValue;
            }

            var entry = elementLanguage.Entries.FirstOrDefault(e => e.Href == href);

            if (entry == null)
            {
                return defaultValue;
            }

            return entry.Value;
        }

        public void AddLocalizedEntry(string language, string href, string value)
        {
            var lang = (Languages ?? (Languages = new List<ElementLocalizationLanguage>())).FirstOrDefault(l => l.Lang.Equals(language, StringComparison.OrdinalIgnoreCase));

            if (lang == null)
            {
                lang = new ElementLocalizationLanguage {Lang = language};
                Languages.Add(lang);
            }


            var entries = lang.Entries ?? (lang.Entries = new List<ElementLocalizationEntry>());

            var entry = lang.Entries.FirstOrDefault(e => e.Href.Equals(href, StringComparison.OrdinalIgnoreCase));

            if (entry == null)
            {
                entry = new ElementLocalizationEntry {Href = href};
                entries.Add(entry);
            }

            entry.Value = value;
        }

        [EditorBrowsable(EditorBrowsableState.Never)]
        public bool ShouldSerialize()
        {
            return Languages != null && Languages.Count != 0;
        }
    }
}