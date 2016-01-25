using BibaFramework.BibaMenu;
using strange.extensions.signal.impl;
using UnityEngine.EventSystems;
using UnityEngine;
using UnityEngine.UI;

namespace BibaFramework.BibaGame
{
    public class ChartBoostView : SceneMenuStateView
    {
        public GameObject AgeGateObject;
        public Text QuestionText;
        public Text IncorrectText;
        public InputField InputField;

        public Signal<string> AgeGateResponsedSignal = new Signal<string>();

        protected override void Start()
        {
            base.Start();
            IncorrectText.gameObject.SetActive(false);
            AgeGateObject.SetActive(false);

            InputField.onEndEdit.AddListener(AgeGateSubmitted);
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();
            InputField.onEndEdit.RemoveListener(AgeGateSubmitted);
        }

        public void ShowAgeGate(string answerInEnglish)
        { 
            AgeGateObject.SetActive(true);
            QuestionText.text = answerInEnglish;
        }

        void AgeGateSubmitted(string value)
        {
            AgeGateResponsedSignal.Dispatch(value);
        }
    }
}