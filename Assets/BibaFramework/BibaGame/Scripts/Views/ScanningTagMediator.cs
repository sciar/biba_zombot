using UnityEngine;
using System.Collections;
using strange.extensions.mediation.impl;
using BibaFramework.BibaGame;

public class ScanningTagMediator : Mediator 
{
	[Inject] public IBibaTagService bibaTagService {
		get;
		set;
	}
	
	[Inject] public ScanningTagView ScanningTagView {
		get;
		set;
	}
	
	[Inject]
	public TagFoundSignal TagFoundSignal { get; set; }
	
	[Inject]
	public SetTagToScanAtViewSignal SetTagToScanAtViewSignal { get; set; }
	
	[Inject]
	public SetTagToScanSignal SetTagToScanSignal { get; set; }
	
	[Inject]
	public TagInitFailedSignal TagInitFailedSignal { get; set; }

	[Inject]
	public LocalizationService LocalizationService { get; set; }

	private BibaEquipment equipmentToScan;
	
	public override void OnRegister ()
	{
		ScanningTagView.LocalizationService = LocalizationService;

		ScanningTagView.ScanningTagViewEnabledSignal.AddListener(ScanningTagEnabled);
		
		TagInitFailedSignal.AddListener(TagServiceInitFailed);
		TagFoundSignal.AddListener(TagFound);
	}
	
	public override void OnRemove ()
	{
		ScanningTagView.ScanningTagViewEnabledSignal.RemoveListener(ScanningTagEnabled);
		
		TagInitFailedSignal.RemoveListener(TagServiceInitFailed);
		TagFoundSignal.RemoveListener(TagFound);
	}
	
	void TagServiceInitFailed()
	{
		ScanningTagView.ShowCameraFailedMessage();
	}

	void ScanningTagEnabled()
	{
		SetTagToScanAtViewSignal.AddListener(SetTagToScanAtView);
		SetTagToScanSignal.Dispatch();
		bibaTagService.StartScan();
	}

	void SetTagToScanAtView(BibaEquipment equipment)
	{
		SetTagToScanAtViewSignal.RemoveListener(SetTagToScanAtView);

		equipmentToScan = equipment;
		ScanningTagView.SetupTag(equipment);
	}

	void TagFound(BibaTagType tagType)
	{
		if (tagType == equipmentToScan.TagType)
		{
			bibaTagService.StopScan();
			ScanningTagView.TagScanCompleted();
		}
	}
}