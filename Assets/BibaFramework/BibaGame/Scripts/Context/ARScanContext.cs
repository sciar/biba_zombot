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
        }
        
        protected override void BindServices ()
        {
            injectionBinder.Bind<IBibaTagService>().To<VuforiaTagService>();
        }
        
        protected override void BindViews ()
        { 
            mediationBinder.Bind<ARScanView>().To<ARScanMediator>();
            mediationBinder.Bind<ARScanStartView>().To<ARScanStartMediator>();
        }
        
        protected override void BindCommands ()
        {   
            commandBinder.Bind<StartSignal>().To<ARScanContextStartedCommand>();
            commandBinder.Bind<LogCameraReminderTimeSignal>().To<LogCameraReminderTimeCommand>();
            commandBinder.Bind<EndSignal>().To<ARScanContextExitedCommand>();
        }
        
        protected override void BindSignals ()
        {
            injectionBinder.Bind<TagServiceInitFailedSignal>().To<TagServiceInitFailedSignal>().ToSingleton();
			injectionBinder.Bind<TagScanCompletedSignal> ().To<TagScanCompletedSignal> ().ToSingleton ();
        }
    }
}