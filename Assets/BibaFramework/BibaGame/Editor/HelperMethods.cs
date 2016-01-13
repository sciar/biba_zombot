using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;

namespace BibaFramework.BibaMenuEditor
{
    public class HelperMethods 
    {
        public static void WriteEnumFile(string nameSpace, string enumName, List<string> enumsToWrite, string outputPath)
        {
            var indent = 4;

            var generatedLines = new List<string> ();

            generatedLines.Add("namespace " + nameSpace);
            generatedLines.Add("{");

            generatedLines.Add(SpaceString(indent) + "public enum " + enumName);
            generatedLines.Add(SpaceString(indent) + "{");

            enumsToWrite.Insert(0, "None");
            enumsToWrite.ForEach(e => generatedLines.Add(SpaceString(indent * 2) + e + ","));

            generatedLines.Add(SpaceString(indent) + "}");

            generatedLines.Add("}");

            File.WriteAllLines (outputPath, generatedLines.ToArray());
        }

        public static void WriteConstStringFile(string nameSpace, string className, List<string> enumsToWrite, string outputPath)
        {
            var indent = 4;
            
            var generatedLines = new List<string> ();
            
            generatedLines.Add("namespace " + nameSpace);
            generatedLines.Add("{");
            
            generatedLines.Add(SpaceString(indent) + "public class " + className);
            generatedLines.Add(SpaceString(indent) + "{");
            
            enumsToWrite.Insert(0, "None");
            enumsToWrite.ForEach(e => generatedLines.Add(SpaceString(indent * 2) + "public const string " + e + " = \"" + e + "\";" ));
            
            generatedLines.Add(SpaceString(indent) + "}");
            
            generatedLines.Add("}");
            
            File.WriteAllLines (outputPath, generatedLines.ToArray());
        }

        static string SpaceString(int numOfSpace)
        {
            return new string(' ', numOfSpace);
        }
    }
}
