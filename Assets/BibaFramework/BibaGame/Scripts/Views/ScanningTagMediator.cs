using UnityEngine;
using System.Collections;
using strange.extensions.mediation.impl;
using BibaFramework.BibaGame;

public class ScanningTagMediator : Mediator 
{
	[Inject] public ScanningTagView ScanningTagView {
		get;
		set;
	}
	
	[Inject]
	public TagScanCompletedSignal TagScanCompletedSignal { get; set; }
	
	[Inject]
	public SetTagToScanAtViewSignal SetTagToScanAtViewSignal { get; set; }
	
	[Inject]
	public StartTagScanSignal SetTagToScanSignal { get; set; }
	
	[Inject]
	public TagInitFailedSignal TagInitFailedSignal { get; set; }

	[Inject]
	public LocalizationService LocalizationService { get; set; }

	[Inject]
	public BibaDeviceSession BibaDeviceSession { get; set; }

	[Inject]
	public ToggleTagScanSignal ToggleTagScanSignal { get; set; }

	public override void OnRegister ()
	{
		ScanningTagView.LocalizationService = LocalizationService;
		ScanningTagView.ScanningTagViewEnabledSignal.AddListener(TagScanEnabled);
		
		TagScanCompletedSignal.AddListener(TagScanCompleted);
		TagInitFailedSignal.AddListener(TagServiceInitFailed);
	}
	
	public override void OnRemove ()
	{
		ScanningTagView.ScanningTagViewEnabledSignal.RemoveListener(TagScanEnabled);
		
		TagInitFailedSignal.RemoveListener(TagServiceInitFailed);
		TagScanCompletedSignal.RemoveListener(TagScanCompleted);
	}
	
	void TagServiceInitFailed()
	{
		ScanningTagView.ShowCameraFailedMessage();
	}

	void TagScanDisabled()
	{
		ToggleTagScanSignal.Dispatch (false);
	}

	void TagScanEnabled()
	{
		SetTagToScanAtViewSignal.AddListener(SetTagToScanAtView);
		SetTagToScanSignal.Dispatch();
		ToggleTagScanSignal.Dispatch (true);
	}

	void SetTagToScanAtView()
	{
		SetTagToScanAtViewSignal.RemoveListener(SetTagToScanAtView);
		ScanningTagView.SetupTag(BibaDeviceSession.TagToScan);
	}

	void TagScanCompleted()
	{
		ScanningTagView.TagScanCompleted();
	}
}