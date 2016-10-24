using BibaFramework.BibaMenu;
using BibaFramework.BibaNetwork;
using strange.extensions.context.api;
using UnityEngine;

namespace BibaFramework.BibaGame
{
    public class DownloadContext : BaseBibaMenuContext 
    {
        public DownloadContext (MonoBehaviour view) : base(view)
        {
        }
        
        public DownloadContext (MonoBehaviour view, ContextStartupFlags flags) : base(view, flags)
        {
        }

        protected override void BindModels ()
        {
        }

        protected override void BindServices ()
        {
        }	

        protected override void BindViews ()
        {
            mediationBinder.Bind<DownloadView>().To<DownloadMediator>();
        }
			
        protected override void BindCommands ()
        {
            commandBinder.Bind<DownloadGeoBasedScenesSignal>().To<DownloadGeoBasedScenesCommand>();
        }

        protected override void BindSignals ()
        {
        }
    }
}