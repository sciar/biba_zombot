using UnityEngine;
using UnityEditor;
using System.IO;

namespace BibaFramework.Utility
{
    //Imported from http://wiki.unity3d.com/index.php?title=CreateScriptableObjectAsset by Robin Pan
    public static class ScriptableObjectUtility
    {
        public static T CreateAsset<T> (string projectPath) where T : ScriptableObject
        {
            T asset = ScriptableObject.CreateInstance<T> ();

            string assetPathAndName = AssetDatabase.GenerateUniqueAssetPath (projectPath);

            Directory.CreateDirectory (Path.GetDirectoryName(projectPath));
            AssetDatabase.Refresh();

            AssetDatabase.CreateAsset (asset, assetPathAndName);
            
            AssetDatabase.SaveAssets ();
            AssetDatabase.Refresh();

            return asset;
        }
    }
}