using System;
using System.Collections.Generic;
using System.Linq;
using BibaFramework.BibaMenu;
using UnityEditor;
using UnityEngine;

namespace BibaFramework.BibaEditor
{
    [CustomEditor(typeof(BibaMenuContextView))]
    public class BibaMenuContextViewInspector : Editor 
    {
        public override void OnInspectorGUI()
        {
            var classStrings = FindSubClassesOf<BaseBibaMenuContext>().Select(type => type.ToString()).ToArray();

            var myTarget = (BibaMenuContextView) target;

            var _indexSelected = Array.IndexOf(classStrings, myTarget.ContextTypeString);
            _indexSelected = EditorGUILayout.Popup("Context to use", _indexSelected, classStrings);

            if (_indexSelected == -1)
            {
                EditorGUILayout.HelpBox("Please select a Context", MessageType.Error);
            }
            else
            {
                myTarget.ContextTypeString = classStrings[_indexSelected];
                EditorUtility.SetDirty(target);
            }
        }

        public List<Type> FindSubClassesOf<TBaseType>()
        {   
            var baseType = typeof(TBaseType);
            var assembly = baseType.Assembly;
            
            return assembly.GetTypes().Where(t => t.IsSubclassOf(baseType)).ToList();
        }
    }
}