using System;
using System.Collections.Generic;
using UnityEngine;

namespace BibaFramework.BibaGame
{
    public class BibaLocalizationSettings : ScriptableObject
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