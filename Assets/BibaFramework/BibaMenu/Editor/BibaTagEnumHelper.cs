using System.IO;
using UnityEngine;
using UnityEditor;
using System.Linq;

namespace BibaFramework.BibaMenuEditor
{
	public class BibaTagEnumHelper : MonoBehaviour 
	{
		[MenuItem ("Biba/Create BibaTag Enum From ARToolKit Generated Patterns")]
		static void InitTagHelper()
		{
			var window = EditorWindow.GetWindow<BibaTagEnumHelperWindow> ();
			window.Show ();
		}
	}

	public class BibaTagEnumHelperWindow : BibaSceneEnumHelperWindow
	{
		protected override string[] EnumStrings {
            get {
                return Directory.GetFiles(_inputDir, "*.txt").Select(file => Path.GetFileNameWithoutExtension(file)).ToArray();;
            }
        }

        protected override string OutputFileName {
            get {
                return "BibaTagType.cs";
            }
        }

        protected override string OutputClassName {
            get {
                return "BibaTagType";
            }
        }

        protected override string OutputNameSpaceName {
            get {
                return "BibaFramework.BibaTag";
            }
        }

        protected override void GenerateAdditionalSettings ()
        {
        }
	}
}
