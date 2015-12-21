using UnityEditor;
using BibaFramework.BibaMenu;
using BibaFramework.BibaGame;

namespace BibaFramework.BibaMenuEditor
{
    [CustomEditor(typeof(BibaButtonView))]
    public class BibaButtonViewInspector : Editor 
    {
        private BibaButtonView ButtonView { get { return (BibaButtonView) target; } }

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            ButtonView.MenuStateTriggerString = BibaInspectorHelper.DisplayStringArrayDropdown<MenuStateTrigger>(ButtonView.MenuStateTriggerString, "MenuStateTrigger to Activate", MenuStateTrigger.None);
            ButtonView.SFXString = BibaInspectorHelper.DisplayStringArrayDropdown<BibaSFX>(ButtonView.SFXString, "SFX to Play", BibaSFX.None);

            EditorUtility.SetDirty(target);
        }
    }
}