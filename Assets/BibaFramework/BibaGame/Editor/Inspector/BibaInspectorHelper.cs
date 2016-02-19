using System;
using System.Collections.Generic;
using UnityEditor;

namespace BibaFramework.BibaEditor
{
    public class BibaInspectorHelper
    {
        public static string DisplayStringArrayDropdown(string[] stringArray, string currentValue, string titleToDisplay, string defaultValue = "")
        {
            var indexSelected = Array.IndexOf(stringArray, currentValue);
            
            indexSelected = EditorGUILayout.Popup(titleToDisplay, indexSelected, stringArray);
            if (indexSelected == -1)
            {
                return defaultValue;
            }
            else
            {
                return stringArray[indexSelected];
            }
        }

        public static string DisplayConstantStringArrayDropdown<T>(string currentValue, string titleToDisplay, string defaultValue = "")
        {
            return DisplayStringArrayDropdown(GetConstantFieldNames<T>().ToArray(), currentValue, titleToDisplay, defaultValue);
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