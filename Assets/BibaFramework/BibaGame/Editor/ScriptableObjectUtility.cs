using UnityEngine;
using UnityEditor;
using System.IO;

namespace BibaFramework.Utility
{
    //Imported from http://wiki.unity3d.com/index.php?title=CreateScriptableObjectAsset by Robin Pan
    public static class ScriptableObjectUtility
    {
        /// <summary>
        //  This makes it easy to create, name and place unique new ScriptableObject asset files.
        /// </summary>
        public static T CreateAsset<T> (string folderPath) where T : ScriptableObject
        {
            T asset = ScriptableObject.CreateInstance<T> ();
            
            string assetPathAndName = AssetDatabase.GenerateUniqueAssetPath (folderPath + typeof(T).Name + ".asset");
            
            AssetDatabase.CreateAsset (asset, assetPathAndName);
            
            AssetDatabase.SaveAssets ();
            AssetDatabase.Refresh();

            return asset;
        }
    }
}