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
            RegisterMenuStateDependentSignals();
        }
        
        public override void OnRemove ()
        {
            ToggleObjectMenuStateSignal.RemoveListener(ShowObjectBasedMenuState);
            UnRegisterMenuStateDependentSignals();
        }
        
        void ShowObjectBasedMenuState(ObjectMenuState menuState, bool status)
        {
            if (this != null && menuState.MenuStateGameObject != null && menuState.SceneName == this.name)
            {
                BaseObjectMenuStateView.gameObject.SetActive(status);
                
                if (status)
                {
                    MenuStateObjectEnabled();
                } else
                {
                    MenuStateObjectDisabled();
                }
            }
        }
        
        protected abstract void MenuStateObjectEnabled();
        protected abstract void MenuStateObjectDisabled();
        protected virtual void RegisterMenuStateDependentSignals() { }
        protected virtual void UnRegisterMenuStateDependentSignals() { }
	} 
}