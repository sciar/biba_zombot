using System;
using Vuforia;
using UnityEngine;
using System.Collections.Generic;

namespace BibaFramework.BibaGame
{
    public class LocalizationService
    {
        [Inject]
        public IDataService DataService { get; set; }

        private Dictionary<string, Dictionary<SystemLanguage, string>> _localizationDict;

        public LocalizationService()
        {
            _localizationDict = new Dictionary<string, Dictionary<SystemLanguage, string>>();
            var localizationSettings = DataService.ReadFromDisk<BibaLocalizationSettings>(BibaDataConstants.LOCALIZATION_SETTINGS_PATH);
            
            foreach (var localization in localizationSettings.Localizations)
            {
                if(!_localizationDict.ContainsKey(localization.Key))
                {
                    _localizationDict.Add(localization.Key, new Dictionary<SystemLanguage, string>());
                }
                
                var localizationKeyDictionary = _localizationDict[localization.Key];
                if(!localizationKeyDictionary.ContainsKey(localization.Language))
                {
                    localizationKeyDictionary.Add(localization.Language, localization.Text);
                }
                else
                {
                    localizationKeyDictionary[localization.Language] = localization.Text;
                }
            }
        }

        public string GetText(string key)
        {
            if (_localizationDict.ContainsKey(key))
            {
                var keyDict = _localizationDict [key];
                return keyDict.ContainsKey(Application.systemLanguage) ? keyDict [Application.systemLanguage] : keyDict [SystemLanguage.English];
            } 
            else
            {
                return key;
            }
        }
    }
}