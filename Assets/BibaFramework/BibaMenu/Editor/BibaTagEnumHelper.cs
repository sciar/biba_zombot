using System.IO;
using UnityEngine;
using UnityEditor;
using System.Linq;

namespace BibaFramework.BibaMenuEditor
{
	public class BibaTagEnumHelper : MonoBehaviour 
	{
		[MenuItem ("Biba/Enum Helpers/Create BibaTag Enums")]
		static void InitTagHelper()
		{
			var window = EditorWindow.GetWindow<BibaTagEnumHelperWindow> ();
			window.Show ();
		}
	}

	public class BibaTagEnumHelperWindow : BibaEnumHelper
	{
        private const string PATTERN_FILE_SUFFIX = "_scaled";
        private const string PATTERN_FILE_END = PATTERN_FILE_SUFFIX + ".jpg";

		protected override string[] EnumStrings { get { return Directory.GetFiles(_inputDir, "*" + PATTERN_FILE_END).Select(file => Path.GetFileNameWithoutExtension(file).Replace(PATTERN_FILE_SUFFIX, string.Empty)).ToArray(); } }
        protected override string OutputFileName { get { return "BibaTagType.cs"; } }
        protected override string OutputClassName { get { return "BibaTagType"; } }
        protected override string OutputNameSpaceName { get { return "BibaFramework.BibaGame"; } }
	}
}