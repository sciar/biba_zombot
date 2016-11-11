using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using BibaFramework.BibaGame;
using BibaFramework.BibaMenu;
using strange.extensions.mediation.impl;
using strange.extensions.signal.impl;
using System.Collections;

public class ScanningTagView : View 
{
	private Dictionary<BibaTagType,TagView> TagViewDict;
	
	public Text TagText;
	public Image CheckImage;
	public Image ScanImage;
	
	public GameObject ARCameraPrefab;
	
	public GameObject ScanFailPanel;
	
	public TagView BlueTagView;
	public TagView RedTagView;
	public TagView OrangeTagView;
	public TagView YellowTagView;
	public TagView PurpleTagView;
	public TagView GreenTagView;
	
	public Signal ScanningTagViewEnabledSignal = new Signal();
	public Signal ScanningTagViewDisabledSignal = new Signal ();

	public LocalizationService LocalizationService { get; set; }

    public AudioClip postGameBGMusic;

	protected override void Awake()
	{
		base.Awake();
		TagViewDict = new Dictionary<BibaTagType, TagView>() {
			{BibaTagType.blue, BlueTagView},
			{BibaTagType.red, RedTagView},
			{BibaTagType.orange, OrangeTagView},
			{BibaTagType.yellow, YellowTagView},
			{BibaTagType.green, GreenTagView},
			{BibaTagType.purple, PurpleTagView}
		};
	}
	
	void OnEnable()
	{		
		CheckImage.gameObject.SetActive(false);
		ScanFailPanel.gameObject.SetActive(false);
		ScanImage.gameObject.SetActive(true);
		
		ScanningTagViewEnabledSignal.Dispatch();


        /*/ Stop the victory music and go back to the BG music - Removed for now, music kicks back on next round
        AudioManager.Instance.bgMusic.Stop();
        AudioManager.Instance.bgMusic.clip = postGameBGMusic;
        AudioManager.Instance.bgMusic.Play();
        */
	}

	void OnDisable()
	{
		ScanningTagViewDisabledSignal.Dispatch ();
	}

	public void SetupTag(BibaTagType tagType)
	{
		var tagView = TagViewDict [tagType];
		
		TagText.text = LocalizationService.GetText(tagView.TagName);
		TagText.color = tagView.TagColor;
	}

	public void TagScanCompleted() 
	{
		StartCoroutine(TransitionToSuccessState());
	}
	
	IEnumerator TransitionToSuccessState()
	{
		CheckImage.gameObject.SetActive(true);
		ScanImage.gameObject.SetActive(false);
		
		yield return new WaitForSeconds(1);
		
		GetComponent<BibaCanvasGroup> ().bibaGameStateAnimator.SetTrigger (MenuStateTrigger.Next.ToString());
	}
	
	public void ShowCameraFailedMessage()
	{
		ScanFailPanel.gameObject.SetActive(true);
	}
	
	public void TransitionToFailState()
	{
		GetComponent<BibaCanvasGroup>().bibaGameStateAnimator.SetTrigger("Previous");
	}
}

[Serializable]
public class TagView
{
	public string TagName;
	public Color TagColor;
}
