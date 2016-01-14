using System;
using BibaFramework.BibaGame;
using BibaFramework.Utility;
using Google.GData.Client;
using Google.GData.Spreadsheets;
using UnityEditor;
using UnityEngine;

namespace BibaFramework.BibaMenuEditor
{
    public class LocalizationSettingsImporter
	{
        private const string LOCALIZATION_SETTINGS_SPREADSHEET_NAME = "Biba Localization";
        private const string LOCALIZATION_SETTINGS_WORKSHEET_NAME = "Common";

        private const string LOCALIZATION_SETTINGS_PROJECT_PATH = "Assets/Resources/" + BibaDataConstants.RESOURCE_LOCALIZATION_FILE_PATH + ".asset";


        [MenuItem("Biba/CI/Load Localization Settings")]
        public static void CreateLocalizationSettings ()
        {
            ImportSettings();
        }

        static void ImportSettings()
        {
            var entries = GoogleSpreadsheetImporter.GetListEntries(LOCALIZATION_SETTINGS_SPREADSHEET_NAME, LOCALIZATION_SETTINGS_WORKSHEET_NAME);
            if (entries == null)
            {
                return;
            }

            ParseLocalizationSettings(entries);
        }

        static void ParseLocalizationSettings(AtomEntryCollection entries)
        {
            var localizationSettings = Resources.Load<BibaLocalizationSettings>(BibaDataConstants.RESOURCE_LOCALIZATION_FILE_PATH);
            if (localizationSettings == null)
            {
                localizationSettings = (BibaLocalizationSettings)ScriptableObjectUtility.CreateAsset<BibaLocalizationSettings>(LOCALIZATION_SETTINGS_PROJECT_PATH);
            }

            foreach (ListEntry row in entries)
            {
                var key = row.Elements[0].Value;
                if(string.IsNullOrEmpty(key))
                {
                    continue;
                }

                for(var i = 1; i < row.Elements.Count; i++)
                {
                    var cell = row.Elements[i];
                    var text = cell.Value;

                    if(string.IsNullOrEmpty(cell.LocalName) || string.IsNullOrEmpty(text))
                    {
                        continue;
                    }

                    try
                    {
                        var language = (SystemLanguage) Enum.Parse(typeof(SystemLanguage), cell.LocalName, true);
                        var localization = new Localization(){
                            Key = key,
                            Language = language,
                            Text = text
                        };

                        localizationSettings.Localizations.Add(localization);
                    }
                    catch(Exception)
                    {
                        Debug.LogWarning(cell.XmlName + "is not a SystemLanguage Enum.");
                        continue;
                    }
                }
            }

            AssetDatabase.Refresh();
        }
	}
}