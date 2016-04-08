using System.Linq;
using BibaFramework.BibaGame;
using BibaFramework.BibaMenu;
using UnityEditor;
using UnityEngine;
using BibaFramework.BibaNetwork;

namespace BibaFramework.BibaEditor
{
    [CustomEditor(typeof(BibaLocalizedTextView))]
    public class BibaLocalizedTextViewInspector : Editor 
    {
        private BibaLocalizedTextView TextView { get { return (BibaLocalizedTextView) target; } }

        private static BibaLocalizationSettings settings;
        private static BibaLocalizationSettings Settings {
            get {
                if(settings == null)
                {
					ReloadSettings ();
                }
                return settings;
            }
        }

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            var allKeys = Settings.Localizations.Select(localization => localization.Key).Distinct().ToArray();
            
            TextView.Key = BibaInspectorHelper.DisplayStringArrayDropdown(allKeys, TextView.Key, "Key Selected");
            EditorUtility.SetDirty(target);

			if (GUILayout.Button ("Reload Localization Settings")) 
			{
				ReloadSettings ();
			}
        }

		static void ReloadSettings()
		{
			var dataService = new JSONDataService();
			settings = dataService.ReadFromDisk<BibaLocalizationSettings>(BibaEditorConstants.GetResourceFilePath(BibaContentConstants.LOCALIZATION_SETTINGS_FILE));
		}
    }
}