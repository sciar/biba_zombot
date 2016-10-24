using BibaFramework.BibaMenu;
using BibaFramework.BibaAnalytic;
using ChartboostSDK;
using UnityEngine;
using System.Collections.Generic;

namespace BibaFramework.BibaGame
{
    public class ChartBoostMediator : SceneMenuStateMediator
	{
        [Inject]
        public ChartBoostView ChartBoostView { get; set; }

        [Inject]
        public SetMenuStateTriggerSignal SetMenuStateTriggerSignal { get; set; }

		[Inject]
		public LocalizationService LocalizationService { get; set; }

        public override SceneMenuStateView View { get { return ChartBoostView; } }

        private const int ATTEMPT_ALLOW = 1;
        private int _attempt;

        private int _answer = -1;
        private string QuestionText {
            get {
                if(_answer == -1)
                {
                    _answer = Random.Range(0, 1000);
                }

				var result = "";
				foreach (var key in NumberString.GetTextKeys(_answer)) 
				{
					result += LocalizationService.GetText (key) + " ";
				}
				return result;
            }
        }

        public override void SetupSceneDependentMenu ()
        {

        }

        public override void RegisterSceneDependentSignals ()
        {
            ChartBoostView.AgeGateResponsedSignal.AddListener(AgeGateConfirm);
        }

        public override void UnRegisterSceneDependentSignals ()
        {
            ChartBoostView.AgeGateResponsedSignal.RemoveListener(AgeGateConfirm);
        }
        
        void OnEnable() 
        {
            Chartboost.didCloseInterstitial += SkipChartBoost;
            Chartboost.didPauseClickForConfirmation += ShowParentalGate;
        }

        void OnDisable() 
        {
            Chartboost.didPauseClickForConfirmation -= ShowParentalGate;
            Chartboost.didCloseInterstitial -= SkipChartBoost;
        }

        void ShowParentalGate()
        {
            ChartBoostView.ShowAgeGate(QuestionText);
        }

        void AgeGateConfirm(string inputValue)
        {
            var result = inputValue == _answer.ToString();
            _attempt++;

            if (result)
            {
                ProcessRightAnswer();
            }
            else
            {
                ProcessWrongAnswer();
            }
        }

        void ProcessRightAnswer()
        {
            ChartBoostView.IncorrectText.gameObject.SetActive(false);
            Chartboost.didPassAgeGate(true);
            SetMenuStateTriggerSignal.Dispatch(MenuStateTrigger.Next);
        }

        void ProcessWrongAnswer()
        {
            ChartBoostView.IncorrectText.gameObject.SetActive(true);
            
            if (_attempt > ATTEMPT_ALLOW)
            {
                Chartboost.didPassAgeGate(false);   
                SetMenuStateTriggerSignal.Dispatch(MenuStateTrigger.Next);
            }
        }

        void SkipChartBoost(CBLocation location)
        {
            SetMenuStateTriggerSignal.Dispatch(MenuStateTrigger.Next);
        }

        private static class NumberString
        {
            static string[] _words =
            {
				"0_text",
				"1_text",
				"2_text",
				"3_text",
				"4_text",
				"5_text",
				"6_text",
				"7_text",
				"8_text",
				"9_text"
            };
            
			public static List<string> GetTextKeys(int value)
            {
				var result = new List<string> ();
				result.Insert (0, _words [value % 10]);

                var number = value;
                while (number / 10 > 0)
                {
                    number = number / 10;
                    result.Insert(0, _words[number % 10]);
                }
                return result;
            }
        }
	}
}