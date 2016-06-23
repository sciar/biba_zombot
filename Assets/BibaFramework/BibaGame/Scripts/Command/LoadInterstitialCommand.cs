using System.Collections;
using BibaFramework.BibaAnalytic;
using BibaFramework.BibaMenu;
using BibaFramework.Utility;
using ChartboostSDK;
using strange.extensions.command.impl;
using UnityEngine;

namespace BibaFramework.BibaGame
{
    public class LoadInterstitialCommand : Command
    {
        private const float CHART_BOOST_TIME_OUT = 5;

        [Inject]
        public SetMenuStateTriggerSignal SetMenuStateTriggerSignal { get; set; }

        [Inject]
        public BibaSceneStack BibaSceneStack { get; set; }

		[Inject]
		public BibaGameModel BibaGameModel { get; set; }

        private bool _loadedChartboost = false;

        public override void Execute ()
        {
#if UNITY_EDITOR
            SetMenuStateTriggerSignal.Dispatch(MenuStateTrigger.Next);
            return;
#endif
            //On a device and has internet
            Retain();
            new Task(WaitForOrientationAndLoadChartboost(), true);
        }

        IEnumerator WaitForOrientationAndLoadChartboost()
        {
            var menuState = BibaSceneStack.Peek();
            if (menuState is SceneMenuState && BibaUtility.CheckForInternetConnection())
            {
                while(Screen.orientation != ((SceneMenuState) menuState).Orientation)
                {
                    yield return null;
                }

                Chartboost.setShouldPauseClickForConfirmation(true);
				Chartboost.showInterstitial(BibaGameModel.TagEnabled ? CBLocation.locationFromName(BibaAnalyticConstants.HOUSE_AD) : CBLocation.Default);

				Chartboost.didDisplayInterstitial += InterstitialLoaded;
				Chartboost.didFailToLoadInterstitial += FailToLoadInterstitial;
                var timeLapsed = 0f;
                while (timeLapsed < CHART_BOOST_TIME_OUT && !_loadedChartboost)
                {
                    timeLapsed += Time.deltaTime;
                    yield return null;
                }
                
				Chartboost.didDisplayInterstitial -= InterstitialLoaded;
				Chartboost.didFailToLoadInterstitial -= FailToLoadInterstitial;
                
                if (!_loadedChartboost)
                {
                    SetMenuStateTriggerSignal.Dispatch(MenuStateTrigger.Next);
                }
            }
            else
            {
                SetMenuStateTriggerSignal.Dispatch(MenuStateTrigger.Next);
            }
            Release();
        }

        void InterstitialLoaded(CBLocation location)
        {
            _loadedChartboost = true;
        }

		void FailToLoadInterstitial(CBLocation location, CBImpressionError error )
		{
			Debug.LogWarning ("Ad fail to show at: " + location.ToString () + " with error: " + error.ToString());
		}
    }
}