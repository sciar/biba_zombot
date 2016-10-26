using System;
using System.Collections.Generic;
using UnityEngine;

namespace BibaFramework.BibaGame
{
	[Serializable]
    public class BibaLocalizationSettings
    {
        public List<Localization> Localizations = new List<Localization>();
    }

	[Serializable]
    public class Localization
    {
        public string Key;
        public SystemLanguage Language;
        public string Text;
    }
}