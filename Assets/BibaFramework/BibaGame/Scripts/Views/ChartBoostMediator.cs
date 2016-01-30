using BibaFramework.BibaMenu;
using BibaFramework.BibaAnalytic;
using ChartboostSDK;
using UnityEngine;

namespace BibaFramework.BibaGame
{
    public class ChartBoostMediator : SceneMenuStateMediator
	{
        [Inject]
        public ChartBoostView ChartBoostView { get; set; }

        [Inject]
        public SetMenuStateTriggerSignal SetMenuStateTriggerSignal { get; set; }

        public override SceneMenuStateView View { get { return ChartBoostView; } }

        private const int ATTEMPT_ALLOW = 1;
        private int _attempt;

        private int _answer = -1;
        private string QuestionInEnglish {
            get {
                if(_answer == -1)
                {
                    _answer = Random.Range(0, 1000);
                }
                return NumberString.GetString(_answer);
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
            ChartBoostView.ShowAgeGate(QuestionInEnglish);
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
                "Zero",
                "One",
                "Two",
                "Three",
                "Four",
                "Five",
                "Six",
                "Seven",
                "Eight",
                "Nine",
                "Ten"
            };
            
            public static string GetString(int value)
            {
                var result = _words[value % 10] + " ";

                var number = value;
                while (number / 10 > 0)
                {
                    number = number / 10;
                    result = result.Insert(0, _words[number % 10] + " ");
                }
                return result;
            }
        }
	}
}