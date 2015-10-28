using strange.extensions.mediation.impl;
using UnityEngine;

namespace BibaFramework.BibaMenu
{
    public abstract class BaseObjectMenuStateMediator : Mediator 
	{
		[Inject]
		public ToggleObjectMenuStateSignal ToggleObjectMenuStateSignal { get; set; }

        public abstract BaseObjectMenuStateView BaseObjectMenuStateView { get;}

		public override void OnRegister ()
		{
            BaseObjectMenuStateView.gameObject.SetActive(false);
			ToggleObjectMenuStateSignal.AddListener(ShowObjectBasedMenuState);
		}

		public override void OnRemove ()
		{
			ToggleObjectMenuStateSignal.RemoveListener(ShowObjectBasedMenuState);
		}

		void ShowObjectBasedMenuState(ObjectMenuState menuState, bool status)
		{
			if (menuState.MenuStateGameObject != null && menuState.SceneName == this.name)
            {
                BaseObjectMenuStateView.gameObject.SetActive(status);
            }
		}
	} 
}