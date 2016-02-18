using System;
using System.Collections.Generic;
using UnityEngine;

namespace BibaFramework.BibaGame
{
    public class BibaLocalizationSettings
    {
        public List<Localization> Localizations = new List<Localization>();
    }

    public class Localization
    {
        public string Key;
        public SystemLanguage Language;
        public string Text;
    }
}