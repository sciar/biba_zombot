using BibaFramework.BibaMenu;
using UnityEngine;

namespace BibaFramework.BibaGame
{
    public class ARScanMediator : SceneMenuStateMediator
    {
        public override SceneMenuStateView View { get { return ARScanView; } }

        [Inject]
        public ARScanView ARScanView { get; set; }

        public override void SetupMenu (BaseMenuState menuState)
        {
        }

        public override void RegisterSceneDependentSignals ()
        {
         }
        
        public override void UnRegisterSceneDependentSignals ()
        {
        }
    }
}