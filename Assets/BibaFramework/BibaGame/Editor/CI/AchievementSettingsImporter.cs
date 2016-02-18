using System;
using System.Globalization;
using System.Text.RegularExpressions;
using BibaFramework.BibaGame;
using Google.GData.Client;
using Google.GData.Spreadsheets;
using UnityEditor;

namespace BibaFramework.BibaMenuEditor
{
    public class AchievementSettingsImporter
	{
        private const string ACHIEVEMENT_SETTINGS_SPREADSHEET_NAME = "Fun Metrics";
        private const string ACHIEVEMENT_SETTINGS_WORKSHEET_NAME = "1-2 Theme Feats";
        private const string SEASONAL_ACHIEVEMENT_SETTINGS_WORKSHEET_NAME = "1-3 Themed Feats for Holiday Season";

        private static BibaAchievementSettings _achievementSettings;

        [MenuItem("Biba/Google Drive/Load Achievement Settings")]
        public static void CreateAchievementAsset ()
        {
            _achievementSettings = new BibaAchievementSettings();

            ImportBasicAchievementSettings();
            ImportSeasonalAchievementSettings();

            var jsonDataService = new JSONDataService();
            jsonDataService.WriteToDisk<BibaAchievementSettings>(_achievementSettings, BibaDataConstants.ACHIEVEMENT_SETTINGS_PATH);

            AssetDatabase.Refresh();
        }

        #region Seasonal Achievement
        static void ImportSeasonalAchievementSettings()
        {
            var entries = GoogleSpreadsheetImporter.GetListEntries(ACHIEVEMENT_SETTINGS_SPREADSHEET_NAME, SEASONAL_ACHIEVEMENT_SETTINGS_WORKSHEET_NAME);
            if (entries == null)
            {
                return;
            }

            ParseForSeasonalAchievementSettings(entries);
        }

        static void ParseForSeasonalAchievementSettings(AtomEntryCollection entries) 
        {
            foreach (ListEntry row in entries)
            {
                var durationText = row.Elements[1].Value;
                if(string.IsNullOrEmpty(durationText))
                {
                    continue;
                }

                var dateMatchGroups = Regex.Match(durationText, BibaEditorConstants.REGEX_STARTDATE_ENDDATE).Groups;
                if(dateMatchGroups.Count == 0)
                {
                    continue;
                }

                for(var i = 2; i < row.Elements.Count; i++)
                {
                    var cell = row.Elements[i];
                    ParseListEntryForSeasonalAchievement(row.Title.Text, 
                                                         Regex.Match(cell.LocalName, BibaEditorConstants.REGEX_TIME_PLAYED).Groups[1].Value, 
                                                         cell.Value, 
                                                         dateMatchGroups[BibaEditorConstants.REGEX_GROUP_STARTDATE].Value, 
                                                         dateMatchGroups[BibaEditorConstants.REGEX_GROUP_ENDDATE].Value);
                }
            }
        }
        
        static void ParseListEntryForSeasonalAchievement(string equipmentString, string timePlayedString, string description, string startDateString, string endDateString)
        {
            if(string.IsNullOrEmpty(equipmentString) || 
               string.IsNullOrEmpty(timePlayedString) || 
               string.IsNullOrEmpty(description))
            {
                return;
            }

            var equipmentType = (BibaEquipmentType)Enum.Parse(typeof(BibaEquipmentType), equipmentString);
            var timePlayed = Convert.ToInt32(timePlayedString);

            var startDate = DateTime.ParseExact(startDateString, BibaEditorConstants.DATETIME_PARSE_EXACT_FORMAT, CultureInfo.InvariantCulture);
            var endDate = DateTime.ParseExact(endDateString, BibaEditorConstants.DATETIME_PARSE_EXACT_FORMAT, CultureInfo.InvariantCulture);

            var configToWrite = new BibaSeasonalAchievementConfig() {
                EquipmentType = equipmentType,
                TimePlayed = timePlayed,
                StartDate = startDate,
                EndDate = endDate,
                DescriptionSuffix = description
            };

            _achievementSettings.AchievementSettings.Add(configToWrite);
        }
        #endregion

        #region Basic Achievement
        static void ImportBasicAchievementSettings()
        {
            var entries = GoogleSpreadsheetImporter.GetListEntries(ACHIEVEMENT_SETTINGS_SPREADSHEET_NAME, ACHIEVEMENT_SETTINGS_WORKSHEET_NAME);
            if (entries == null)
            {
                return;
            }

            ParseBasicAchievementSttings(entries);
        }

        static void ParseBasicAchievementSttings(AtomEntryCollection entries) 
        {
            foreach (ListEntry row in entries)
            {
                for(var i = 1; i < row.Elements.Count; i++)
                {
                    var cell = row.Elements[i];
                    ParseListEntryForBasicAchievement(row.Title.Text, Regex.Match(cell.LocalName, BibaEditorConstants.REGEX_TIME_PLAYED).Groups[1].Value, cell.Value);
                }
            }
        }
        
        static void ParseListEntryForBasicAchievement(string equipmentString, string timePlayedString, string description)
        {
            if(string.IsNullOrEmpty(equipmentString) || 
               string.IsNullOrEmpty(timePlayedString) || 
               string.IsNullOrEmpty(description))
            {
                return;
            }

            var equipmentType = (BibaEquipmentType)Enum.Parse(typeof(BibaEquipmentType), equipmentString);
            var timePlayed = Convert.ToInt32(timePlayedString);
            
            var configToWrite = new BibaSeasonalAchievementConfig() {
                EquipmentType = equipmentType,
                TimePlayed = timePlayed,
                DescriptionSuffix = description
            };

            _achievementSettings.AchievementSettings.Add(configToWrite);
        }
        #endregion
	}
}