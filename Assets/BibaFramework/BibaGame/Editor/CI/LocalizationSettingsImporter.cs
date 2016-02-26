using System;
using BibaFramework.BibaGame;
using BibaFramework.Utility;
using Google.GData.Client;
using Google.GData.Spreadsheets;
using UnityEditor;
using UnityEngine;
using BibaFramework.BibaNetwork;

namespace BibaFramework.BibaEditor
{
    public class LocalizationSettingsImporter
	{
        private const string LOCALIZATION_SETTINGS_SPREADSHEET_NAME = "Biba Localization";
        private const string LOCALIZATION_SETTINGS_WORKSHEET_NAME = BibaContentConstants.CI_GAME_ID;

        [MenuItem("Biba/Google Drive/Load Localization Settings")]
        public static void CreateLocalizationSettings ()
        {
            ImportSettings();

            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
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
            var settings = new BibaLocalizationSettings();
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

                        settings.Localizations.Add(localization);
                    }
                    catch(Exception)
                    {
                        Debug.LogWarning(cell.XmlName + "is not a SystemLanguage Enum.");
                        continue;
                    }
                }
            }

            var jsonDataService = new JSONDataService();
            jsonDataService.WriteToDisk<BibaLocalizationSettings>(settings, BibaDataConstants.LOCALIZATION_SETTINGS_PATH);
        }
    }
}