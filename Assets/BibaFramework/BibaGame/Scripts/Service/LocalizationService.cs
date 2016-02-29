using System;
using System.Collections.Generic;
using UnityEngine;
using BibaFramework.BibaNetwork;

namespace BibaFramework.BibaGame
{
    public class LocalizationService : IContentUpdated
    {
        [Inject]
        public IDataService DataService { get; set; }

        [Inject]
        public ICDNService CDNService { get; set; }

        private Dictionary<string, Dictionary<SystemLanguage, string>> _localizationDict;
        private Dictionary<string, Dictionary<SystemLanguage, string>> LocalizationDict
        {
            get
            {
                if (_localizationDict == null)
                {
                    ReloadContent();
                }
                return _localizationDict;
            }
        }

        #region - IContentUpdated
        public bool ShouldLoadFromResources 
        {
            get 
            {
                var persistedManifest = DataService.ReadFromDisk<BibaManifest>(BibaContentConstants.GetPersistedPath(BibaContentConstants.MANIFEST_FILENAME));
                var persistedManifestLine = persistedManifest.Lines.Find(line => line.FileName == BibaContentConstants.LOCALIZATION_SETTINGS_FILE);

                var resourceManifest = DataService.ReadFromDisk<BibaManifest>(BibaContentConstants.GetResourceContentFilePath(BibaContentConstants.MANIFEST_FILENAME));
                var resourceManifestLine = resourceManifest.Lines.Find(line => line.FileName == BibaContentConstants.LOCALIZATION_SETTINGS_FILE);

                return persistedManifestLine == null || persistedManifestLine.Version <= resourceManifestLine.Version;
            }
        }

        public string ContentFilePath 
        {
            get 
            {
                return ShouldLoadFromResources ? BibaContentConstants.GetResourceContentFilePath(BibaContentConstants.LOCALIZATION_SETTINGS_FILE) :
                    BibaContentConstants.GetPersistedPath(BibaContentConstants.LOCALIZATION_SETTINGS_FILE);
            }
        }

        public void ReloadContent()
        {
            _localizationDict = new Dictionary<string, Dictionary<SystemLanguage, string>>();
            
            var localizationSettings = DataService.ReadFromDisk<BibaLocalizationSettings>(ContentFilePath);
            foreach (var localization in localizationSettings.Localizations)
            {
                if (!_localizationDict.ContainsKey(localization.Key))
                {
                    _localizationDict.Add(localization.Key, new Dictionary<SystemLanguage, string>());
                }
                
                var localizationKeyDictionary = _localizationDict [localization.Key];
                if (!localizationKeyDictionary.ContainsKey(localization.Language))
                {
                    localizationKeyDictionary.Add(localization.Language, localization.Text);
                } else
                {
                    localizationKeyDictionary [localization.Language] = localization.Text;
                }
            }
        }
        #endregion

        public string GetText(string key)
        {
            if (LocalizationDict.ContainsKey(key))
            {
                var keyDict = LocalizationDict [key];
                return keyDict.ContainsKey(Application.systemLanguage) ? keyDict [Application.systemLanguage] : keyDict [SystemLanguage.English];
            } else
            {
                return key;
            }
        }
    }
}