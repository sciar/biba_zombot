using System;
using System.Globalization;
using System.Text.RegularExpressions;
using BibaFramework.BibaGame;
using BibaFramework.BibaNetwork;
using Google.GData.Client;
using Google.GData.Spreadsheets;
using UnityEditor;
using UnityEngine;

namespace BibaFramework.BibaEditor
{
    public class BibaSpecialSceneImporter
	{
        private const string SETTINGS_SPREADSHEET_NAME = "Biba Timed Scenes";
        private const string SETTINGS_WORKSHEET_NAME = BibaContentConstants.CI_GAME_ID;

        [MenuItem("Biba/Load Settings/Load Special Scene Settings")]
        public static void LoadSpecialSceneSettings ()
        {
            ImportSpecialSceneSettings();

            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
        }

        static void ImportSpecialSceneSettings()
        {
            var entries = GoogleSpreadsheetImporter.GetListEntries(SETTINGS_SPREADSHEET_NAME, SETTINGS_WORKSHEET_NAME);
            if (entries == null)
            {
                return;
            }

            ParseSpecialSceneSettings(entries);
        }

        static void ParseSpecialSceneSettings(AtomEntryCollection entries) 
        {
            var settings = new BibaSpecialSceneSettings();
            foreach (ListEntry row in entries)
            {
                var idText = row.Elements[0].Value;
                if(string.IsNullOrEmpty(idText))
                {
                    continue;
                }

                var sceneText = row.Elements[1].Value;
                if(string.IsNullOrEmpty(sceneText))
                {
                    continue;
                }

                var durationText = row.Elements[2].Value;
                if(string.IsNullOrEmpty(durationText))
                {
                    continue;
                }

                var dateMatchGroups = Regex.Match(durationText, BibaEditorConstants.REGEX_STARTDATE_ENDDATE).Groups;
                if(dateMatchGroups.Count == 0)
                {
                    continue;
                }

                var startDate = DateTime.ParseExact(dateMatchGroups[BibaEditorConstants.REGEX_GROUP_STARTDATE].Value, BibaEditorConstants.DATETIME_PARSE_EXACT_FORMAT, CultureInfo.InvariantCulture);     
                var endDate = DateTime.ParseExact(dateMatchGroups[BibaEditorConstants.REGEX_GROUP_ENDDATE].Value, BibaEditorConstants.DATETIME_PARSE_EXACT_FORMAT, CultureInfo.InvariantCulture);

                var sceneSetting = new TimedSceneSetting();

                sceneSetting.Id = idText;
                sceneSetting.SceneName = sceneText;
                sceneSetting.StartDate = startDate;
                sceneSetting.EndDate = endDate;

                settings.TimeSpecialSceneSettings.Add(sceneSetting);
            }

            var jsonDataService = new JSONDataService();
            jsonDataService.WriteToDisk<BibaSpecialSceneSettings>(settings, BibaEditorConstants.GetContentOutputPath(BibaContentConstants.SPECIAL_SCENE_SETTINGS_FILE));
        }
	}
}