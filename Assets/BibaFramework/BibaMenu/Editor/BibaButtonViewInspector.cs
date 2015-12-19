using System;
using System.Collections.Generic;
using System.Linq;
using BibaFramework.BibaGame;
using BibaFramework.BibaMenu;
using UnityEditor;
using UnityEngine;

namespace BibaFramework.BibaMenuEditor
{
    [CustomEditor(typeof(BibaButtonView))]
    public class BibaButtonViewInspector : Editor 
    {
        private BibaButtonView ButtonView { get { return (BibaButtonView) target; } }

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            DisplayMenuStateTriggerDropdown();
            DisplaySFXDropdown();

            EditorUtility.SetDirty(target);
        }
        
        void DisplayMenuStateTriggerDropdown()
        {
            ButtonView.MenuStateTriggerString = BibaInspectorHelper.DisplayStringArrayDropdown<MenuStateTrigger>(ButtonView.MenuStateTriggerString, "MenuStateTrigger to Activate", MenuStateTrigger.None);
        }

        void DisplaySFXDropdown()
        {
            ButtonView.SFXString = BibaInspectorHelper.DisplayStringArrayDropdown<BibaSFX>(ButtonView.SFXString, "SFX to Play", BibaSFX.None);
        }
    }
}