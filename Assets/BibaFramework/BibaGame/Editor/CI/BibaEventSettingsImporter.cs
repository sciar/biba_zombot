using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using BibaFramework.BibaGame;
using BibaFramework.BibaNetwork;
using Google.GData.Spreadsheets;
using UnityEditor;
using UnityEngine;

namespace BibaFramework.BibaEditor
{
	public class BibaEventSettingsImporter
	{
		[MenuItem ("Biba/Content Generation/Test", false)]
		public static void ImportSettings()
		{
			ImportEventSettings ();
		}

		private const string BIBAGAME_NAMESPACE = "BibaFramework.BibaGame";
		private static readonly string MODEL_PROJECT_FOLDER_PATH = Application.dataPath + "/BibaFramework/BibaGame/Scripts/Model/";
		private static readonly string POINTEVENTS_CONSTANTS_FILEPATH = MODEL_PROJECT_FOLDER_PATH + typeof(BibaPointEvents).Name + ".cs";

		//Googlesheet
		private const string EVENT_SETTINGS_SPREADSHEET_NAME = "Biba Events";
		private const string COMMON_WORKSHEET_NAME = "biba-common";
		private const string EVENT_TYPE_WORKSHEET_NAME = "event_type";
		private const string PARAMETER_TYPE_WORKSHEET_NAME = "parameter_type";

		//Worksheet Headers
		private const string ID = "id";
		private const string POINTS = "points";
		private const string REPEAT = "repeat";
		private const string TYPE1 = "type1";
		private const string TYPE2 = "type2";
		private const string TYPE3 = "type3";
		private const string VALUE1 = "value1";
		private const string VALUE2 = "value2";
		private const string VALUE3 = "value3";
		private const string DESCRIPTION1 = "description1";
		private const string DESCRIPTION2 = "description2";

		//Regex
		private const string REGEX_VECTOR2 = "(?<latitude>-?[0-9.]*),[ ]?(?<longtitude>-?[0-9.]*)";
		private const string REGEX_LONGTITUDE = "longtitude";
		private const string REGEX_LATITUDE = "latitude";

		private static readonly Dictionary<string, Action<Dictionary<string,string>>> EVENT_ACTION_DICT = new Dictionary<string, Action<Dictionary<string,string>>>()
		{
			{ BibaEventType.milestone, ParseMilestone},
			{ BibaEventType.timed_milestone, ParseTimedMilestone},
			{ BibaEventType.timed_content, ParseTimedContent},
			{ BibaEventType.geo_content, ParseGeoContent},
			{ BibaEventType.checkpoint, ParseCheckpoint}
		};

		private static BibaAchievementSettings _achievementSettings;
		private static BibaSpecialSceneSettings _specialSceneSettings;
		private static BibaPointEventSettings _pointEventSettings;
		private static void ImportEventSettings()
		{
			//Reset Settings
			_achievementSettings = new BibaAchievementSettings();
			_specialSceneSettings = new BibaSpecialSceneSettings();
			_pointEventSettings = new BibaPointEventSettings();

			//Import Data
			ImportEventType ();
			ImportParameterType ();

			ImportEventSheet (COMMON_WORKSHEET_NAME);
			ImportEventSheet (BibaContentConstants.CI_GAME_ID);

			//Save Settings
			HelperMethods.WriteConstStringFile (BIBAGAME_NAMESPACE, typeof(BibaPointEvents).Name, _pointEventSettings.BibaPointSettings.Select (setting => setting.Id).ToList (), POINTEVENTS_CONSTANTS_FILEPATH);

			var jsonDataService = new JSONDataService();
			jsonDataService.WriteToDisk<BibaAchievementSettings>(_achievementSettings, BibaEditorConstants.GetContentOutputPath(BibaContentConstants.ACHIEVEMENT_SETTINGS_FILE));
			jsonDataService.WriteToDisk<BibaSpecialSceneSettings>(_specialSceneSettings, BibaEditorConstants.GetContentOutputPath(BibaContentConstants.SPECIAL_SCENE_SETTINGS_FILE));
			jsonDataService.WriteToDisk<BibaPointEventSettings>(_pointEventSettings, BibaEditorConstants.GetContentOutputPath(BibaContentConstants.POINTEVENT_SETTINGS_FILE));

			AssetDatabase.SaveAssets();
			AssetDatabase.Refresh ();
		}

		static void ImportEventType()
		{
			ImportConstSheet (EVENT_TYPE_WORKSHEET_NAME, typeof(BibaEventType).Name);
		}

		static void ImportParameterType()
		{
			ImportConstSheet (PARAMETER_TYPE_WORKSHEET_NAME, typeof(BibaEventParameterType).Name);
		}

		static void ImportConstSheet(string worksheetName, string className, string nameSpace = BIBAGAME_NAMESPACE)
		{
			var entries = GoogleSpreadsheetImporter.GetListEntries (EVENT_SETTINGS_SPREADSHEET_NAME, worksheetName);
			var outputPath = MODEL_PROJECT_FOLDER_PATH + className + ".cs";

			var eventStrings = new List<string> ();
			foreach (ListEntry row in entries) 
			{
				for(int i = 0; i < row.Elements.Count; i++)
				{
					if(!string.IsNullOrEmpty(row.Elements[i].Value))
					{
						eventStrings.Add(row.Elements[i].Value);
					}
				}
			}

			HelperMethods.WriteConstStringFile (nameSpace, className, eventStrings, outputPath);
		}

		static void ImportEventSheet(string worksheetName)
		{
			var entries = GoogleSpreadsheetImporter.GetListEntries (EVENT_SETTINGS_SPREADSHEET_NAME, worksheetName);
			foreach (ListEntry row in entries) 
			{
				var eventType = row.Elements[1].Value;
				if (!string.IsNullOrEmpty (eventType) && EVENT_ACTION_DICT.ContainsKey (eventType)) 
				{
					EVENT_ACTION_DICT [eventType].Invoke (GoogleSpreadsheetImporter.GetListEntryDict(row));
				}
			}
		}
			
		static void ParseMilestone(Dictionary<string,string> paramDict)
		{
			var configToWrite = new BibaAchievementConfig ();
			configToWrite.EquipmentType = (BibaEquipmentType)Enum.Parse (typeof(BibaEquipmentType), paramDict [VALUE2]);
			configToWrite.TimePlayed = Convert.ToInt32 (paramDict [VALUE1]);
			configToWrite.DescriptionSuffix = paramDict [DESCRIPTION2];

			_achievementSettings.AchievementSettings.Add (configToWrite);
		}

		static void ParseTimedMilestone(Dictionary<string,string> paramDict)
		{
			//Parse StartDate and EndDate
			var dateMatchGroups = Regex.Match(paramDict[VALUE3], BibaEditorConstants.REGEX_STARTDATE_ENDDATE).Groups;
			var startDate = DateTime.ParseExact(dateMatchGroups[BibaEditorConstants.REGEX_GROUP_STARTDATE].Value, BibaEditorConstants.DATETIME_PARSE_EXACT_FORMAT, CultureInfo.InvariantCulture);
			var endDate = DateTime.ParseExact(dateMatchGroups[BibaEditorConstants.REGEX_GROUP_ENDDATE].Value, BibaEditorConstants.DATETIME_PARSE_EXACT_FORMAT, CultureInfo.InvariantCulture);

			var configToWrite = new BibaSeasonalAchievementConfig() {
				EquipmentType = (BibaEquipmentType)Enum.Parse(typeof(BibaEquipmentType), paramDict[VALUE2]),
				TimePlayed = Convert.ToInt32(paramDict [VALUE1]),
				StartDate = startDate,
				EndDate = endDate,
				DescriptionSuffix = paramDict [DESCRIPTION2]
			};

			_achievementSettings.AchievementSettings.Add(configToWrite);
		}

		static void ParseTimedContent(Dictionary<string,string> paramDict)
		{
			var dateMatchGroups = Regex.Match(paramDict[VALUE2], BibaEditorConstants.REGEX_STARTDATE_ENDDATE).Groups;
			var startDate = DateTime.ParseExact(dateMatchGroups[BibaEditorConstants.REGEX_GROUP_STARTDATE].Value, BibaEditorConstants.DATETIME_PARSE_EXACT_FORMAT, CultureInfo.InvariantCulture);     
			var endDate = DateTime.ParseExact(dateMatchGroups[BibaEditorConstants.REGEX_GROUP_ENDDATE].Value, BibaEditorConstants.DATETIME_PARSE_EXACT_FORMAT, CultureInfo.InvariantCulture);
		
			var sceneSetting = new TimedSceneSetting ();

			sceneSetting.Id = paramDict[ID];
			sceneSetting.SceneName = paramDict[VALUE1];
			sceneSetting.StartDate = startDate;
			sceneSetting.EndDate = endDate;

			_specialSceneSettings.TimedSceneSettings.Add(sceneSetting);
		}

		static void ParseGeoContent(Dictionary<string,string> paramDict)
		{
			var centerMatchGroups = Regex.Match(paramDict[VALUE2], REGEX_VECTOR2).Groups;
			var latitude = Convert.ToDouble(centerMatchGroups[REGEX_LATITUDE].Value);
			var longtitude = Convert.ToDouble(centerMatchGroups[REGEX_LONGTITUDE].Value);

			var sceneSetting = new GeoSceneSetting();
			sceneSetting.Id = paramDict[ID];
			sceneSetting.SceneName = paramDict[VALUE1];
			sceneSetting.Center = new Vector2((float)latitude, (float)longtitude);
			sceneSetting.Radius = Convert.ToInt32(paramDict[VALUE3]);

			_specialSceneSettings.GeoSceneSettings.Add(sceneSetting);
		}

		static void ParseCheckpoint(Dictionary<string,string> paramDict)
		{
			var setting = new BibaPointEvent () {
				Id = paramDict[ID],
				Points = Convert.ToInt32(paramDict[POINTS]),
				Repeat = Convert.ToBoolean(paramDict[REPEAT])
			};

			_pointEventSettings.BibaPointSettings.Add (setting);
		}
	}
}