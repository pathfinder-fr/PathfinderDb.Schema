namespace PathfinderDb.Schema
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Xml.Serialization;

    [XmlType("localization")]
    public class ElementLocalization
    {
        [XmlElement("language")]
        public List<ElementLocalizationLanguage> Languages { get; set; }

        public string GetLocalizedEntry(string language, string href, string defaultValue = null)
        {
            if (this.Languages == null)
            {
                return defaultValue;
            }

            var elementLanguage = this.Languages.FirstOrDefault(l => l.Lang == language);

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
            var lang = (this.Languages ?? (this.Languages = new List<ElementLocalizationLanguage>())).FirstOrDefault(l => l.Lang.Equals(language, StringComparison.OrdinalIgnoreCase));

            if (lang == null)
            {
                lang = new ElementLocalizationLanguage { Lang = language };
                this.Languages.Add(lang);
            }


            var entries = lang.Entries ?? (lang.Entries = new List<ElementLocalizationEntry>());

            var entry = lang.Entries.FirstOrDefault(e => e.Href.Equals(href, StringComparison.OrdinalIgnoreCase));

            if (entry == null)
            {
                entry = new ElementLocalizationEntry { Href = href };
                entries.Add(entry);
            }

            entry.Value = value;
        }

        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public bool ShouldSerialize()
        {
            return this.Languages != null && this.Languages.Count != 0;
        }
    }
}