using UnityEngine;
using BibaFramework.BibaMenu;
using strange.extensions.context.api;

namespace BibaFramework.BibaGame
{
    public class ARScanContext : BaseBibaMenuContext 
    {
        public ARScanContext (MonoBehaviour view) : base(view)
        {
        }
        
        public ARScanContext (MonoBehaviour view, ContextStartupFlags flags) : base(view, flags)
        {
        }
        
        protected override void BindModels ()
        {
            injectionBinder.Bind<BibaEquipment>().To<BibaEquipment>().ToName(BibaGameConstants.SATELLITE_TAG_TO_SCAN);
        }
        
        protected override void BindServices ()
        {
            injectionBinder.Bind<IBibaTagService>().To<VuforiaTagService>();
        }
        
        protected override void BindViews ()
        { 
            mediationBinder.Bind<ARScanView>().To<ARScanMediator>();
        }
        
        protected override void BindCommands ()
        {   
            commandBinder.Bind<StartSignal>().To<SetSatelliteTagToScanCommand>();
        }
        
        protected override void BindSignals ()
        {
        }
    }
}