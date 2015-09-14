using UnityEngine;
using strange.extensions.command.impl;
using System.Collections;
using BibaFramework.Utility;

namespace BibaFramework.BibaMenu
{
    public class LoadGameSceneCommand : Command 
    {
        [Inject]
        public BibaMenuState BibaMenuState { get; set; }

        [Inject]
        public SetupMenuSignal SetupMenuSignal { get; set; } 

        [Inject]
        public PlayMenuEntryAnimationSignal PlayMenuEntryAnimationSignal { get; set; }

        [Inject]
        public MenuEntryAnimationEndedSignal MenuEntryAnimationEndedSignal { get; set; }

        [Inject(BibaConstants.BIBA_STATE_MACHINE)]
        public Animator StateMachine { get; set; }

        public override void Execute ()
        {
            Retain();
            LoadNewGameScene();
        }

        void LoadNewGameScene()
        {
            Debug.Log("View Loading Started: " + BibaMenuState.GameScene.ToString());
            if (BibaMenuState.LoadingScreen)
            {
                new Task(LoadLevelAsync());
            } 
            else
            {
                LoadLevel();
            }
        }

        IEnumerator LoadLevelAsync()
        {
            Debug.Log("View GameObject Loading Started: " + BibaMenuState.GameScene);
            
            AsyncOperation asyncOp = Application.LoadLevelAdditiveAsync(BibaMenuState.GameScene.ToString());
            yield return asyncOp;

            LevelLoaded();
        }

        void LoadLevel()
        {
            Application.LoadLevelAdditive(BibaMenuState.GameScene.ToString());
            LevelLoaded();
        }

        void LevelLoaded()
        {
            SetupMenuSignal.Dispatch(BibaMenuState);
            PlayMenuEnteredAnimation();
        }

        void PlayMenuEnteredAnimation()
        {
            if (BibaMenuState.EntryAnimation)
            {
                Debug.Log("Load Animation Started: " + BibaMenuState.GameScene);
                PlayMenuEntryAnimationSignal.Dispatch();
                MenuEntryAnimationEndedSignal.AddListener(MenuEntryAnimationCompleted);
            }
            else
            {
                Release();
            }
        }

        void MenuEntryAnimationCompleted()
        {
            Debug.Log("Load Animation Completed: " + BibaMenuState.GameScene);
            MenuEntryAnimationEndedSignal.RemoveListener(MenuEntryAnimationCompleted);
            Release();
        }
    }
}