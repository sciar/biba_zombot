using System;
using System.Collections;
using System.Collections.Generic;
using BibaFramework.BibaMenu;
using UnityEngine;
using UnityEngine.UI;

namespace BibaFramework.BibaGame
{
	public class ARScanStartView : BaseObjectMenuStateView 
	{
        public float CompleteWaitTime = 1f;

        public List<ARTagInfo> TagInfos;
        public Text TagText;
        public Image TagIcon;
        public GameObject Spinner;
        public GameObject CheckMark;

		public LocalizationService LocalizationService { get; set; }

        protected override void Start()
        {
            base.Start();
            Spinner.gameObject.SetActive(true);
            CheckMark.gameObject.SetActive(false);
        }

        public void SetupTag(BibaTagType TagType)
        {  
            var tagInfo = TagInfos.Find(info => info.TagType == TagType);
			TagText.text = LocalizationService.GetText(tagInfo.TagTextKey);
            TagText.color = tagInfo.TagColor;
            TagIcon.sprite = tagInfo.TagIcon;
        }

        public void CompleteScan(Action onComplete)
        {
            StartCoroutine(WaitAndComplete(onComplete));
        }

        IEnumerator WaitAndComplete(Action onComplete)
        {
            Spinner.gameObject.SetActive(false);
            CheckMark.gameObject.SetActive(true);

            yield return new WaitForSeconds(CompleteWaitTime);
            onComplete();
        }
    }
    
    [Serializable]
    public class ARTagInfo
    {
        public BibaTagType TagType;
        public Sprite TagIcon;
        public Color TagColor;
        public string TagTextKey;
    }
}