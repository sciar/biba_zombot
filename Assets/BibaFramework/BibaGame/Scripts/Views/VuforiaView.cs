using strange.extensions.mediation.impl;
using Vuforia;
using UnityEngine;

namespace BibaFramework.BibaGame
{
	public class VuforiaView:View
	{
		private VuforiaBehaviour _vuforiaBehaviour;
		public VuforiaBehaviour VuforiaBehaviour { 
			get {
				if (_vuforiaBehaviour == null) 
				{
					_vuforiaBehaviour = GetComponent<VuforiaBehaviour> ();
				}
				return _vuforiaBehaviour;
			}
		}
	}
}