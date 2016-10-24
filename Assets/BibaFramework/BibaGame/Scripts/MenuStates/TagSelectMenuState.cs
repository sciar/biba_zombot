using BibaFramework.BibaMenu;
using UnityEngine;
using System.Collections.Generic;

namespace BibaFramework.BibaGame
{
	public class TagSelectMenuState : SceneMenuState 
    {
        public override string SceneName {
            get {
                return BibaScene.TagSelect;
            }
        }
    }
}