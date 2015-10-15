using strange.extensions.mediation.impl;
using UnityEngine;

namespace BibaFramework.BibaMenu
{
	public class ObjectMenuStateMediator : Mediator 
	{
		[Inject]
		public ToggleObjectMenuStateSignal ShowObjectBasedMenuStateSignal { get; set; }

		[Inject]
		public ObjectMenuStateView ObjectMenuStateView { get; set; }

		public override void OnRegister ()
		{
			ShowObjectBasedMenuStateSignal.AddListener(ShowObjectBasedMenuState);
		}

		public override void OnRemove ()
		{
			ShowObjectBasedMenuStateSignal.RemoveListener(ShowObjectBasedMenuState);
		}

		void ShowObjectBasedMenuState(ObjectMenuState menuState, bool status)
		{
			if(menuState.MenuStateGameObject != null && menuState.SceneName == this.name)
			{
				ObjectMenuStateView.gameObject.SetActive(status);
			}
		}
	} 
}