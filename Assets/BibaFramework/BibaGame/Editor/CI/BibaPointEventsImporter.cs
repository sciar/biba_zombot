using System;
using BibaFramework.BibaNetwork;
using BibaFramework.BibaGame;
using UnityEditor;
using Google.GData.Client;
using Google.GData.Spreadsheets;
using UnityEngine;
using System.Linq;
using System.IO;

namespace BibaFramework.BibaEditor
{
	public class BibaPointEventsImporter
	{
		private const string SETTINGS_SPREADSHEET_NAME = "Biba Point System";
		private const string SETTINGS_COMMON_WORKSHEET_NAME = "biba-common";
		private const string SETTINGS_WORKSHEET_NAME = BibaContentConstants.CI_GAME_ID;
		private const string TRUE = "TRUE";

		private static readonly string POINTEVENTS_CONSTANTS_FILEPATH = Path.Combine(Application.dataPath, "BibaFramework/BibaGame/Scripts/Model/" + POINT_EVENTS + ".cs");
		private const string BIBAGAME_NAMESPACE = "BibaFramework.BibaGame";
		private const string POINT_EVENTS = "BibaPointEvents";

		public static void ImportSettings ()
		{
			ImportPointEventSettings();

			AssetDatabase.SaveAssets();
			AssetDatabase.Refresh();
		}

		private static BibaPointEventSettings _settings;
		static void ImportPointEventSettings()
		{
			_settings = new BibaPointEventSettings();

			ParseSettingsForSheet (SETTINGS_WORKSHEET_NAME);

			var jsonDataService = new JSONDataService();
			jsonDataService.WriteToDisk<BibaPointEventSettings>(_settings, BibaEditorConstants.GetContentOutputPath(BibaContentConstants.POINTEVENT_SETTINGS_FILE));
		}

		static void ParseSettingsForSheet(string workSheetName)
		{
			var entries = GoogleSpreadsheetImporter.GetListEntries(SETTINGS_SPREADSHEET_NAME, workSheetName);
			if (entries == null)
			{
				return;
			}
			ParseSettings(entries);
		}

		static void ParseSettings(AtomEntryCollection entries)
		{
			foreach (ListEntry row in entries)
			{
				var key = row.Elements[0].Value;
				if(string.IsNullOrEmpty(key))
				{
					continue;
				}

				var points = -1;
				int.TryParse (row.Elements [1].Value, out points);
				if (points == -1) 
				{
					continue;
				}

				var repeatStr = row.Elements [2].Value;
				if (string.IsNullOrEmpty (repeatStr)) 
				{
					continue;
				}

				var repeat = repeatStr == TRUE ? true : false;

				var setting = new BibaPointEvent () {
					Key = key,
					Points = points,
					Repeat = repeat
				};

				_settings.BibaPointSettings.Add (setting);
			}

			HelperMethods.WriteConstStringFile (BIBAGAME_NAMESPACE, "BibaPointEvents", _settings.BibaPointSettings.Select (setting => setting.Key).ToList (), POINTEVENTS_CONSTANTS_FILEPATH);
		}
	}
}