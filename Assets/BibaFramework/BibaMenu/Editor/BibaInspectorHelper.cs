using System;
using System.Collections.Generic;
using UnityEditor;

namespace BibaFramework.BibaMenuEditor
{
    public class BibaInspectorHelper
    {
        public static string DisplayStringArrayDropdown<T>(string currentValue, string titleToDisplay, string defaultValue = "")
        {
            var constantStrings = GetConstantFieldNames<T>().ToArray();
            var _indexSelected = Array.IndexOf(constantStrings, currentValue);

            _indexSelected = EditorGUILayout.Popup(titleToDisplay, _indexSelected, constantStrings);
            if (_indexSelected == -1)
            {
                return defaultValue;
            }
            else
            {
                return constantStrings[_indexSelected];
            }
        }

        static List<string> GetConstantFieldNames<T>()
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