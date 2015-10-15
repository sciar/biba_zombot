using BibaFramework.BibaMenu;
using UnityEngine;
using System.Collections.Generic;

namespace BibaFramework.BibaGame
{
    public class ARScanMenuState : SceneMenuState 
    {
        public override BibaScene GameScene {
            get {
                return BibaScene.ARScan;
            }
        }
    }
}