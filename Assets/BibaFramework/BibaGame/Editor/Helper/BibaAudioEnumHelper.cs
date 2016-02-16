using System.IO;
using UnityEngine;
using UnityEditor;
using System.Linq;
using System.Collections.Generic;

namespace BibaFramework.BibaMenuEditor
{
    public class BibaAudioEnumHelper : MonoBehaviour 
	{
        [MenuItem ("Biba/Helper/BibaBGM Constants")]
		static void InitBGMHelper()
		{
            var window = EditorWindow.GetWindow<BibaBGMEnumHelperWindow> ();
			window.Show ();
		}

        [MenuItem ("Biba/Helper/BibaSFX Constants")]
        static void InitSFXHelper()
        {
            var window = EditorWindow.GetWindow<BibaSFXEnumHelperWindow> ();
            window.Show ();
        }
	}

    public abstract class BibaAudioEnumHelperWindow : BibaEnumHelper
	{
        private const string MP3_SUFFIX = ".mp3";
        private const string WAV_SUFFIX = ".wav";

        private List<string> AudioFileNames { 
            get { 
                var mp3s = Directory.GetFiles(_inputDir, "*" + MP3_SUFFIX);
                var wavs = Directory.GetFiles(_inputDir, "*" + WAV_SUFFIX);

                var result = new List<string>();
                result.AddRange(mp3s);
                result.AddRange(wavs);

                return result;
            } 
        }

		protected override List<string> EnumStrings {
            get { 
                return AudioFileNames.Select(file => Path.GetFileNameWithoutExtension(file)).ToList();
            } 
        }
        protected override string OutputNameSpaceName { get { return "BibaFramework.BibaGame"; } }

        protected override void WriteToFile (string outputPath)
        {
            WriteConstStringToFile(outputPath);
        }
	}

    public class BibaBGMEnumHelperWindow: BibaAudioEnumHelperWindow
    {
        protected override string OutputFileName { get { return "BibaBGM.cs"; } }
        protected override string OutputClassName { get { return "BibaBGM"; } }
    }

    public class BibaSFXEnumHelperWindow: BibaAudioEnumHelperWindow
    {
        protected override string OutputFileName { get { return "BibaSFX.cs"; } }
        protected override string OutputClassName { get { return "BibaSFX"; } }
    }
}