using System.Collections;
using UnityEngine;
using BibaFramework.BibaMenu;
using BibaFramework.Utility;
using ChartboostSDK;
using strange.extensions.command.impl;

namespace BibaFramework.BibaGame
{
    public class ShowInterstitialCommand : Command
    {
        [Inject]
        public SetMenuStateTriggerSignal SetMenuStateTriggerSignal { get; set; }

        [Inject]
        public BibaSceneStack BibaSceneStack { get; set; }

        public override void Execute ()
        {
#if UNITY_EDITOR
            SetMenuStateTriggerSignal.Dispatch(MenuStateTrigger.Next);
            return;
#endif
            Retain();
            new Task(WaitForScreenOrientation(), true);

            if (BibaUtility.CheckForInternetConnection())
            {
                Chartboost.setShouldPauseClickForConfirmation(true);
                Chartboost.showInterstitial(CBLocation.Default);
            }
            else
            {
                SetMenuStateTriggerSignal.Dispatch(MenuStateTrigger.Next);
            }
        }

        IEnumerator WaitForScreenOrientation()
        {
            var menuState = BibaSceneStack.Peek();
            if(menuState is SceneMenuState)
            {
                while(Screen.orientation != ((SceneMenuState) menuState).Orientation)
                {
                    yield return null;
                }
            }

            Release();
        }
    }
}