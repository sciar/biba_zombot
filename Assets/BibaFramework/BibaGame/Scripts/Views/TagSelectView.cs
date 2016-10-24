using strange.extensions.signal.impl;
using UnityEngine.UI;
using BibaFramework.BibaMenu;

namespace BibaFramework.BibaGame
{
	public class TagSelectView : SceneMenuStateView
    {
		public Button YesButton;
		public Button NoButton;

		public Signal<bool> EnableTagSignal = new Signal<bool>();

		protected override void Start ()
		{
			base.Start();
			YesButton.onClick.AddListener(YesButtonPressed);
			NoButton.onClick.AddListener(NoButtonPressed);
		}

		protected override void OnDestroy()
		{
			base.OnDestroy();
			YesButton.onClick.RemoveListener(YesButtonPressed);
			NoButton.onClick.RemoveListener(NoButtonPressed);
		}

		void YesButtonPressed()
		{
			EnableTagSignal.Dispatch(true);
		}

		void NoButtonPressed()
		{
			EnableTagSignal.Dispatch(false);
		}
    }
}