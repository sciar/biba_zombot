using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using BibaFramework.BibaMenu;

namespace BibaFramework.BibaGame
{
    public class ARScanView : SceneMenuStateView
    {
        public List<ARTagInfo> TagInfos;
        public Text TagText;
        public Image TagIcon;

        public void SetupTag(BibaTagType TagType)
        {
            var tagInfo = TagInfos.Find(info => info.TagType == TagType);
            TagText.text = tagInfo.TagText;
            TagText.color = tagInfo.TagColor;
            TagIcon.sprite = tagInfo.TagIcon;
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