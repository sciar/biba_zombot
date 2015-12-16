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
            var constantStrings = GetConstantFieldNames<MenuStateTrigger>().ToArray();
            
            var _indexSelected = Array.IndexOf(constantStrings, ButtonView.MenuStateTriggerString);
            _indexSelected = EditorGUILayout.Popup("MenuState to Trigger", _indexSelected, constantStrings);
            
            if (_indexSelected == -1)
            {
                ButtonView.MenuStateTriggerString = MenuStateTrigger.None;
            }
            else
            {
                ButtonView.MenuStateTriggerString = constantStrings[_indexSelected];
            }
        }

        void DisplaySFXDropdown()
        {
            var constantStrings = GetConstantFieldNames<BibaSFX>().ToArray();
            var _indexSelected = Array.IndexOf(constantStrings, ButtonView.SFXString);
            _indexSelected = EditorGUILayout.Popup("SFX to Play", _indexSelected, constantStrings);
            
            if (_indexSelected == -1)
            {
                ButtonView.SFXString = BibaSFX.None;
            }
            else
            {
                ButtonView.SFXString = constantStrings[_indexSelected];
            }
        }
        
        List<string> GetConstantFieldNames<T>()
        {   
            var result = new List<string>();

            var allFields = typeof(T).GetFields();
            foreach (var field in allFields)
            {
                result.Add(field.Name);
            }
            return result;
        }
    }
}