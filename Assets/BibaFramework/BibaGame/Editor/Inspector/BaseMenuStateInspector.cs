using UnityEditor;
using BibaFramework.BibaMenu;
using BibaFramework.BibaGame;

namespace BibaFramework.BibaEditor
{
    [CustomEditor(typeof(BaseMenuState), true)]
    public class BaseMenuStateInspector : Editor 
    {
        private BaseMenuState BaseMenuState { get { return (BaseMenuState) target; } }

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            BaseMenuState.EnterBGM = BibaInspectorHelper.DisplayConstantStringArrayDropdown<BibaBGM>(BaseMenuState.EnterBGM, "Enter BGM:", BibaBGM.None);
            BaseMenuState.ExitBGM = BibaInspectorHelper.DisplayConstantStringArrayDropdown<BibaBGM>(BaseMenuState.ExitBGM, "Exit BGM:", BibaBGM.None);
            BaseMenuState.EnterSFX = BibaInspectorHelper.DisplayConstantStringArrayDropdown<BibaSFX>(BaseMenuState.EnterSFX, "Enter SFX:", BibaSFX.None);
            BaseMenuState.ExitSFX = BibaInspectorHelper.DisplayConstantStringArrayDropdown<BibaSFX>(BaseMenuState.ExitSFX, "Exit SFX:", BibaSFX.None);

            EditorUtility.SetDirty(target);
        }
    }
}