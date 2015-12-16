using System;
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
            if(GUI.changed)
            {
                if(ButtonView.MenuStateTrigger.ToString() != ButtonView.MenuStateTriggerString)
                {
                    ButtonView.MenuStateTriggerString = ButtonView.MenuStateTrigger.ToString();
                }

                if(ButtonView.BibaSFX.ToString() != ButtonView.BibaSFXString)
                {
                    ButtonView.BibaSFXString = ButtonView.BibaSFX.ToString();
                }
                EditorUtility.SetDirty(target);
            } 
            else
            {
                if(!string.IsNullOrEmpty(ButtonView.MenuStateTriggerString) && ButtonView.MenuStateTrigger.ToString() != ButtonView.MenuStateTriggerString)
                {
                    ButtonView.MenuStateTrigger = (MenuStateTrigger)Enum.Parse(typeof(MenuStateTrigger), ButtonView.MenuStateTriggerString);
                    EditorUtility.SetDirty(target);
                }

                if(!string.IsNullOrEmpty(ButtonView.BibaSFXString) && ButtonView.BibaSFX.ToString() != ButtonView.BibaSFXString)
                {
                    ButtonView.BibaSFX = (BibaSFX)Enum.Parse(typeof(BibaSFX), ButtonView.BibaSFXString);
                    EditorUtility.SetDirty(target);
                }
            }
        }
    }
}