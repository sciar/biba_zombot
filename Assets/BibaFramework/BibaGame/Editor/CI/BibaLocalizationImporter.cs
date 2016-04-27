using System;
using BibaFramework.BibaGame;
using BibaFramework.BibaNetwork;
using BibaFramework.Utility;
using Google.GData.Client;
using Google.GData.Spreadsheets;
using UnityEditor;
using UnityEngine;

namespace BibaFramework.BibaEditor
{
    public class BibaLocalizationImporter
	{
        private const string LOCALIZATION_SETTINGS_SPREADSHEET_NAME = "Biba Localization";
        private const string LOCALIZATION_SETTINGS_COMMON_WORKSHEET_NAME = "biba-common";
        private const string LOCALIZATION_SETTINGS_WORKSHEET_NAME = BibaContentConstants.CI_GAME_ID;

        public static void CreateLocalizationSettings ()
        {
            ImportSettings();

            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
        }

        private static BibaLocalizationSettings settings;
        static void ImportSettings()
        {
            settings = new BibaLocalizationSettings();

            //Parse common localization settings
            var commonEntries = GoogleSpreadsheetImporter.GetListEntries(LOCALIZATION_SETTINGS_SPREADSHEET_NAME, LOCALIZATION_SETTINGS_COMMON_WORKSHEET_NAME);
            if (commonEntries == null)
            {
                return;
            }
            ParseLocalizationSettings(commonEntries);

            //Parse game specific localization settings
            var gameEntries = GoogleSpreadsheetImporter.GetListEntries(LOCALIZATION_SETTINGS_SPREADSHEET_NAME, LOCALIZATION_SETTINGS_WORKSHEET_NAME);
            if (gameEntries == null)
            {
                return;
            }
            ParseLocalizationSettings(gameEntries);
        }

        static void ParseLocalizationSettings(AtomEntryCollection entries)
        {
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
            jsonDataService.WriteToDisk<BibaLocalizationSettings>(settings, BibaEditorConstants.GetContentOutputPath(BibaContentConstants.LOCALIZATION_SETTINGS_FILE));
        }
    }
}