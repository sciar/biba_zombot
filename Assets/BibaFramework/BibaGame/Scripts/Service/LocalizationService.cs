using System;
using System.Collections.Generic;
using UnityEngine;
using Vuforia;
using BibaFramework.BibaNetwork;

namespace BibaFramework.BibaGame
{
    public class LocalizationService
    {
        [Inject]
        public IDataService DataService { get; set; }

        [Inject]
        public ICDNService CDNService { get; set; }

        private Dictionary<string, Dictionary<SystemLanguage, string>> _localizationDict;
        private Dictionary<string, Dictionary<SystemLanguage, string>> LocalizationDict {
            get {
                if(_localizationDict == null)
                {
                    _localizationDict = new Dictionary<string, Dictionary<SystemLanguage, string>>();

                    var filePath = CDNService.ShouldLoadFromResources ? 
                        BibaContentConstants.GetResourceContentFilePath(BibaContentConstants.ACHIEVEMENT_SETTINGS_FILE) :
                            BibaContentConstants.GetPersistedContentFilePath(BibaContentConstants.ACHIEVEMENT_SETTINGS_FILE);

                    var localizationSettings = DataService.ReadFromDisk<BibaLocalizationSettings>(filePath);

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
                return _localizationDict;
            }
        }

        public string GetText(string key)
        {
            if (LocalizationDict.ContainsKey(key))
            {
                var keyDict = LocalizationDict [key];
                return keyDict.ContainsKey(Application.systemLanguage) ? keyDict [Application.systemLanguage] : keyDict [SystemLanguage.English];
            } 
            else
            {
                return key;
            }
        }
    }
}