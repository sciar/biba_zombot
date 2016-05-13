using System;
using UnityEditor;
using BibaFramework.BibaMenu;
using BibaFramework.BibaGame;

namespace BibaFramework.BibaEditor
{
	[CustomEditor(typeof(BibaPointEventServiceView))]
	public class BibaPointEventServiceViewInspector : Editor
	{
		private BibaPointEventServiceView BibaPointEventServiceView { get { return (BibaPointEventServiceView) target; } }

		public override void OnInspectorGUI()
		{
			base.OnInspectorGUI();
			BibaPointEventServiceView.PointEventKey = BibaInspectorHelper.DisplayConstantStringArrayDropdown<BibaPointEvents>(BibaPointEventServiceView.PointEventKey, "Point Event to Check");
			EditorUtility.SetDirty(target);
		}
	}
}