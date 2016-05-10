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

		private static BibaLocalizationSettings _settings;
		static void ImportSettings()
		{
			_settings = new BibaLocalizationSettings();

			ParseSettingsForSheet (LOCALIZATION_SETTINGS_COMMON_WORKSHEET_NAME);
			ParseSettingsForSheet (LOCALIZATION_SETTINGS_WORKSHEET_NAME);

			var jsonDataService = new JSONDataService();
			jsonDataService.WriteToDisk<BibaLocalizationSettings>(_settings, BibaEditorConstants.GetContentOutputPath(BibaContentConstants.LOCALIZATION_SETTINGS_FILE));
		}

		static void ParseSettingsForSheet(string workSheetName)
		{
			var entries = GoogleSpreadsheetImporter.GetListEntries(LOCALIZATION_SETTINGS_SPREADSHEET_NAME, workSheetName);
			if (entries == null)
			{
				return;
			}
			ParseLocalizationSettings(entries);
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

						_settings.Localizations.Add(localization);
					}
					catch(Exception)
					{
						Debug.LogWarning(cell.XmlName + "is not a SystemLanguage Enum.");
						continue;
					}
				}
			}
		}
	}
}