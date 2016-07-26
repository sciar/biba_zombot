using strange.extensions.mediation.impl;
using Vuforia;

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

		private VideoBackgroundManager _videoBackground;
		public VideoBackgroundManager VideoBackgroundManager { 
			get {
				if (_videoBackground == null) 
				{
					_videoBackground = GetComponent<VideoBackgroundManager> ();
				}
				return _videoBackground;
			}
		}
	}
}