using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text.RegularExpressions;
using BibaFramework.BibaGame;
using BibaFramework.Utility;
using Google.GData.Client;
using Google.GData.Spreadsheets;
using UnityEditor;
using UnityEngine;

namespace BibaFramework.BibaMenuEditor
{
    public class AchievementSettingsImporter
	{
        private const string ACHIEVEMENT_SETTINGS_SPREADSHEET_NAME = "Fun Metrics";
        private const string ACHIEVEMENT_SETTINGS_WORKSHEET_NAME = "1-2 Theme Feats";
        private const string REGEX_TIME_PLAYED = "played([1-9][0-9]*)";

        private const string SEASONAL_ACHIEVEMENT_SETTINGS_WORKSHEET_NAME = "1-3 Themed Feats for Holiday Season";
        private const string REGEX_STARTDATE_ENDDATE = "(?<startDate>(0[1-9]|1[0-2])[/](0[1-9]|[1-2][0-9]|3[0-1]))[ ]*-[ ]*(?<endDate>(0[1-9]|1[0-2])[/](0[1-9]|[1-2][0-9]|3[0-1]))";
        private const string REGEX_GROUP_STARTDATE = "startDate";
        private const string REGEX_GROUP_ENDDATE = "endDate";
        private const string DATETIME_PARSE_EXACT = "MM/dd";

        private const string ACHIEVEMENT_CONFIG_FOLDER_PROJECT_PATH = "Assets/Resources/" + BibaDataConstants.RESOURCE_ACHIEVEMENT_CONFIG_FOLDER_PATH + "achievement.asset";
        
        [MenuItem("Biba/CI/Load Achievement Settings")]
        public static void CreateAchievementAsset ()
        {
            ImportBasicAchievementSettings();
            ImportSeasonalAchievementSettings();
            AssetDatabase.Refresh();
        }

        #region Seasonal Achievement
        static void ImportSeasonalAchievementSettings()
        {
            _localConfigDict = null;

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

                var dateMatchGroups = Regex.Match(durationText, REGEX_STARTDATE_ENDDATE).Groups;
                if(dateMatchGroups.Count == 0)
                {
                    continue;
                }

                for(var i = 2; i < row.Elements.Count; i++)
                {
                    var cell = row.Elements[i];  

                    var equipment = row.Title.Text;
                    var timePlayed = Regex.Match(cell.LocalName, REGEX_TIME_PLAYED).Groups[1].Value;
                    var description = cell.Value;
                    
                    ParseListEntryForSeasonalAchievement(equipment, timePlayed, description, dateMatchGroups[REGEX_GROUP_STARTDATE].Value, dateMatchGroups[REGEX_GROUP_ENDDATE].Value);
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

            var startDate = DateTime.ParseExact(startDateString, DATETIME_PARSE_EXACT, CultureInfo.InvariantCulture);
            var startDateVector2 = new Vector2(startDate.Month, startDate.Day);

            var endDate = DateTime.ParseExact(endDateString, DATETIME_PARSE_EXACT, CultureInfo.InvariantCulture);
            var endDateVector2 = new Vector2(endDate.Month, endDate.Day);

            BibaSeasonalAchievementConfig configToWrite;
            var achievementId = BibaSeasonalAchievementConfig.GenerateId(equipmentType, timePlayed, startDateVector2, endDateVector2);
            if (LocalConfigDict.ContainsKey(achievementId))
            {
                configToWrite = (BibaSeasonalAchievementConfig) LocalConfigDict [achievementId];
            }
            else
            {
                configToWrite = CreateAchievementConfig<BibaSeasonalAchievementConfig>(equipmentType, timePlayed);
                configToWrite.StartDate = startDateVector2;
                configToWrite.EndDate = endDateVector2;

                LocalConfigDict.Add(configToWrite.Id, configToWrite);
            }
            configToWrite.DescriptionSuffix = description;
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
                    
                    var equipment = row.Title.Text;
                    var timePlayed = Regex.Match(cell.LocalName, REGEX_TIME_PLAYED).Groups[1].Value;
                    var description = cell.Value;

                    ParseListEntryForBasicAchievement(equipment, timePlayed, description);
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
            
            BibaAchievementConfig configToWrite;
            var achievementId = BibaAchievementConfig.GenerateId(equipmentType, timePlayed);
            if (LocalConfigDict.ContainsKey(achievementId))
            {
                configToWrite = LocalConfigDict [achievementId];
            } 
            else
            {
                configToWrite = CreateAchievementConfig<BibaAchievementConfig>(equipmentType, timePlayed);
                LocalConfigDict.Add(configToWrite.Id, configToWrite);
            }
            configToWrite.DescriptionSuffix = description;
        }
        #endregion

        #region Common
        private static Dictionary<string, BibaAchievementConfig> _localConfigDict;
        static Dictionary<string, BibaAchievementConfig> LocalConfigDict
        {
            get 
            {    
                if(_localConfigDict == null)
                {
                    _localConfigDict = new Dictionary<string, BibaAchievementConfig>();

                    var configs = Resources.LoadAll<BibaAchievementConfig>(BibaDataConstants.RESOURCE_ACHIEVEMENT_CONFIG_FOLDER_PATH);
                    foreach (var config in configs)
                    {
                        _localConfigDict.Add(config.Id, config);
                    }
                }
                return _localConfigDict;
            }
        }

        static TConfig CreateAchievementConfig<TConfig>(BibaEquipmentType equipmentType, int timePlayed) where TConfig : BibaAchievementConfig
        {
            var configToWrite = (TConfig)ScriptableObjectUtility.CreateAsset<TConfig>(ACHIEVEMENT_CONFIG_FOLDER_PROJECT_PATH);
            configToWrite.EquipmentType = equipmentType;
            configToWrite.TimePlayed = timePlayed;
            return configToWrite;
        }
        #endregion
	}
}