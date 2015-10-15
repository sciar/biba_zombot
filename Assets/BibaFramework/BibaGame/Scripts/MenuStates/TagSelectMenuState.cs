using BibaFramework.BibaMenu;
using UnityEngine;
using System.Collections.Generic;

namespace BibaFramework.BibaGame
{
    public class ARScanMenuState : SceneMenuState 
    {
        public override BibaScene BibaScene {
            get {
                return BibaScene.ARScan;
            }
        }
    }
}