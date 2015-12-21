using strange.extensions.mediation.impl;
using UnityEngine;
using BibaFramework.BibaGame;

namespace BibaFramework.BibaMenu
{
    public abstract class BaseObjectMenuStateMediator : Mediator 
    {
        [Inject]
        public ToggleObjectMenuStateSignal ToggleObjectMenuStateSignal { get; set; }

        [Inject]
        public PlayBibaBGMSignal PlayBibaBGMSignal { get; set; }

        [Inject]
        public PlayBibaSFXSignal PlayBibaSFXSignal { get; set; }

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
                if (status)
                {
                    if(menuState.EnterBGM != BibaBGM.None)
                    {
                        PlayBibaBGMSignal.Dispatch(menuState.EnterBGM);
                    }

                    if(menuState.EnterSFX != BibaSFX.None)
                    {
                        PlayBibaSFXSignal.Dispatch(menuState.EnterSFX);
                    }

                    BaseObjectMenuStateView.AnimateEntry();
                    MenuStateObjectEnabled();
                } 
                else
                {
                    if(menuState.ExitBGM != BibaBGM.None)
                    {
                        PlayBibaBGMSignal.Dispatch(menuState.ExitBGM);
                    }
                    
                    if(menuState.ExitSFX != BibaSFX.None)
                    {
                        PlayBibaSFXSignal.Dispatch(menuState.ExitSFX);
                    }

                    BaseObjectMenuStateView.AnimateExit();
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