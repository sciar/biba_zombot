using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;
using BibaFramework.BibaGame;

public class TimerCircle : MonoBehaviour {

	public delegate void OnTimerEnd();
	public OnTimerEnd onTimerEndListeners;
	public AudioServices audio;

	private bool hasTimerEnded = false;
	[SerializeField] private Text timerText;
	private int timerStart = 45;

	public void DecreaseOneSecond() {
		if (hasTimerEnded)
			return;
		if (timerStart > 0) {
			timerStart--;
			audio.PlaySFX("SFX_CountBeep");
			string resultText = "";
			if (timerStart <= 10) {
				resultText += "<color=#F35F5FFF>";
			}
			resultText += timerStart.ToString ("0");
			if (timerStart <= 10) {
				resultText += "</color>";
			}
			timerText.text = resultText;
		} else {
			TimerEnd();
		}
	}

	public void StartTimer(int seconds) {
		timerStart = seconds;
		timerText.text = seconds.ToString ("0");
		hasTimerEnded = false;
	}

	void TimerEnd() {
		hasTimerEnded = true;
		if (onTimerEndListeners != null) {
			onTimerEndListeners();
		}
	}
}
