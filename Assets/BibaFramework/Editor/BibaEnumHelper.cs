using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace BibaFramework.BibaMenuEditor
{
	public abstract class BibaEnumHelper : EditorWindow
	{
        protected abstract List<string> EnumStrings { get; }
        protected abstract string OutputFileName { get; }
        protected abstract string OutputClassName { get; }
        protected abstract string OutputNameSpaceName { get; }

        protected string _inputDir = string.Empty;
        private string _outputDir = string.Empty;
        
        void OnGUI()
        {
            //Input path
            GUILayout.Label ("Folder Path: " + _inputDir);
            EditorGUILayout.BeginHorizontal ();
            GUILayout.Label ("Locate the Directory for the Input " + OutputClassName +" Files.");
            if (GUILayout.Button ("Select")) 
            {
                _inputDir = EditorUtility.OpenFolderPanel ("Select the Directory", "", "");
            }
            EditorGUILayout.EndHorizontal ();
            
            GUILayout.Space (10);
            
            //Output path
            GUILayout.Label ("Folder Path: " + _outputDir);
            EditorGUILayout.BeginHorizontal ();
            GUILayout.Label ("Locate the Directory for the Output " + OutputFileName + " File.");
            if (GUILayout.Button ("Select")) 
            {
                _outputDir = EditorUtility.OpenFolderPanel ("Select the Directory", "", "");
            }
            EditorGUILayout.EndHorizontal ();
            
            var validPath = Directory.Exists(_inputDir) && _outputDir.StartsWith(Application.dataPath); 
            GUI.enabled = validPath;
            if (GUILayout.Button ("Generate " + OutputFileName)) 
            {
                ReformatFilenames();

                GenerateConstantFile();
                GenerateAdditionalSettings();
            }
            GUI.enabled = true;
        }

        void ReformatFilenames()
        {
            var allFiles = Directory.GetFiles(_inputDir);
            for (int i = 0; i < allFiles.Length; i++)
            {
                var file = allFiles[i];
                var directoryPath = Directory.GetParent(file).FullName;

                var oldFilename = Path.GetFileName(file);
                var oldFilePath = Path.Combine(directoryPath, oldFilename);

                var newFilename = oldFilename.Replace(" ", "_").Replace("-", "_");
                var newFilePath = Path.Combine(directoryPath, newFilename);

                File.Move(oldFilePath, newFilePath);
            }
            AssetDatabase.Refresh();
        }

        void GenerateConstantFile()
        {
            WriteToFile(Path.Combine(_outputDir, OutputFileName));
            AssetDatabase.Refresh();
        }

        protected abstract void WriteToFile(string outputPath);

        protected void WriteEnumToFile(string outputPath)
        {
            HelperMethods.WriteEnumFile(OutputNameSpaceName, OutputClassName, EnumStrings, outputPath);
        }

        protected void WriteConstStringToFile(string outputPath)
        {
            HelperMethods.WriteConstStringFile(OutputNameSpaceName, OutputClassName, EnumStrings, outputPath);
        }

        protected virtual void GenerateAdditionalSettings() { }
	}
}
