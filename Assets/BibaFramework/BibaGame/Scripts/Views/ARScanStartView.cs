using strange.extensions.mediation.impl;
using BibaFramework.BibaMenu;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using System;

namespace BibaFramework.BibaGame
{
	public class ARScanStartView : BaseObjectMenuStateView 
	{
        public List<ARTagInfo> TagInfos;
        public Text TagText;
        public Image TagIcon;

        public GameObject ARCameraPrefab;
        private GameObject _arCamera;

        public void SetupTag(BibaTagType TagType)
        {  
            var tagInfo = TagInfos.Find(info => info.TagType == TagType);
            TagText.text = tagInfo.TagText;
            TagText.color = tagInfo.TagColor;
            TagIcon.sprite = tagInfo.TagIcon;
        }

        public void SetupCamera()
        {
            DestroyCamera();
            _arCamera = Instantiate(ARCameraPrefab);
        }

        public void DestroyCamera()
        {
            if (_arCamera != null)
            {
                GameObject.Destroy(_arCamera);
            }
        }
    }
    
    [Serializable]
    public class ARTagInfo
    {
        public BibaTagType TagType;
        public Sprite TagIcon;
        public Color TagColor;
        public string TagText;
    }
}