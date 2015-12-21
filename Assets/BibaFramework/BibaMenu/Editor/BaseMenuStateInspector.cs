using UnityEditor;
using BibaFramework.BibaMenu;
using BibaFramework.BibaGame;

namespace BibaFramework.BibaMenuEditor
{
    [CustomEditor(typeof(BaseMenuState), true)]
    public class BaseMenuStateInspector : Editor 
    {
        private BaseMenuState BaseMenuState { get { return (BaseMenuState) target; } }

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            BaseMenuState.EnterBGM = BibaInspectorHelper.DisplayStringArrayDropdown<BibaBGM>(BaseMenuState.EnterBGM, "Enter BGM:", BibaBGM.None);
            BaseMenuState.ExitBGM = BibaInspectorHelper.DisplayStringArrayDropdown<BibaBGM>(BaseMenuState.ExitBGM, "Exit BGM:", BibaBGM.None);
            BaseMenuState.EnterSFX = BibaInspectorHelper.DisplayStringArrayDropdown<BibaSFX>(BaseMenuState.EnterSFX, "Enter SFX:", BibaSFX.None);
            BaseMenuState.ExitSFX = BibaInspectorHelper.DisplayStringArrayDropdown<BibaSFX>(BaseMenuState.ExitSFX, "Exit SFX:", BibaSFX.None);

            EditorUtility.SetDirty(target);
        }
    }
}