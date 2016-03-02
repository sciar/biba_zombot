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
        private const string TIMEDSCENE_SPREADSHEET_NAME = "Biba Timed Scenes";
        private const string LOCALESCENE_SPREADSHEET_NAME = "Biba Locale Based Scenes";
        private const string WORKSHEET_NAME = BibaContentConstants.CI_GAME_ID;

        private const string REGEX_VECTOR2 = "(?<latitude>-?[0-9.]*),[ ]?(?<longtitude>-?[0-9.]*)";
        private const string REGEX_LONGTITUDE = "longtitude";
        private const string REGEX_LATITUDE = "latitude";

        [MenuItem("Biba/Load Settings/Load Special Scene Settings")]
        public static void CreateSpecialSceneSettings ()
        {
            ImportSpecialSceneSettings();

            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
        }

        static void ImportSpecialSceneSettings()
        {
            var settings = new BibaSpecialSceneSettings();

            var timedEntries = GoogleSpreadsheetImporter.GetListEntries(TIMEDSCENE_SPREADSHEET_NAME, WORKSHEET_NAME);
            if (timedEntries == null)
            {
                return;
            }

            ParseTimedSceneSettings(timedEntries, ref settings);

            var localeBasedEntries = GoogleSpreadsheetImporter.GetListEntries(LOCALESCENE_SPREADSHEET_NAME, WORKSHEET_NAME);
            if (localeBasedEntries == null)
            {
                return;
            }
            
            ParseLocaleBasedSceneSettings(localeBasedEntries, ref settings);

            var jsonDataService = new JSONDataService();
            jsonDataService.WriteToDisk<BibaSpecialSceneSettings>(settings, BibaEditorConstants.GetContentOutputPath(BibaContentConstants.SPECIAL_SCENE_SETTINGS_FILE));
        }

        static void ParseTimedSceneSettings(AtomEntryCollection entries, ref BibaSpecialSceneSettings settings) 
        {
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

                settings.TimedSceneSettings.Add(sceneSetting);
            }
        }

        static void ParseLocaleBasedSceneSettings(AtomEntryCollection entries, ref BibaSpecialSceneSettings settings) 
        {
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

                var centerText = row.Elements[2].Value;
                if(string.IsNullOrEmpty(centerText))
                {
                    continue;
                }

                var centerMatchGroups = Regex.Match(centerText, REGEX_VECTOR2).Groups;
                if(centerMatchGroups.Count == 0)
                {
                    continue;
                }

                var latitude = Convert.ToDouble(centerMatchGroups[REGEX_LATITUDE].Value);
                var longtitude = Convert.ToDouble(centerMatchGroups[REGEX_LONGTITUDE].Value);

                var radiusText = row.Elements[3].Value;
                if(string.IsNullOrEmpty(radiusText))
                {
                    continue;
                }

                var sceneSetting = new LocaleSceneSetting();
                
                sceneSetting.Id = idText;
                sceneSetting.SceneName = sceneText;
                sceneSetting.Center = new Vector2((float)latitude, (float)longtitude);
                sceneSetting.Radius = Convert.ToInt32(radiusText);

                settings.LocaleSceneSettings.Add(sceneSetting);
            }
        }
	}
}