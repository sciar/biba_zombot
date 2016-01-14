using System.Linq;
using BibaFramework.BibaGame;
using BibaFramework.BibaMenu;
using UnityEditor;
using UnityEngine;

namespace BibaFramework.BibaMenuEditor
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
                    settings = AssetDatabase.LoadAssetAtPath<BibaLocalizationSettings>(BibaEditorConstants.LOCALIZATION_SETTINGS_PROJECT_PATH);
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
        }
    }
}