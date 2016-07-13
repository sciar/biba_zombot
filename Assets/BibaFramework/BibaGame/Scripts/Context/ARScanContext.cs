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
            commandBinder.Bind<StartSignal>().To<CheckToSkipTagScanCommand>();
            commandBinder.Bind<LogCameraReminderTimeSignal>().To<LogCameraReminderTimeCommand>();
            commandBinder.Bind<EndSignal>().To<RemoveVuforiaCommand>();
			commandBinder.Bind<TagScanCompletedSignal>().To<CheckForFirstScanCompletedPointsEventCommand>().To<CheckForScanCompletedPointsEventCommand>();
        }
        
        protected override void BindSignals ()
        {
            injectionBinder.Bind<TagInitFailedSignal>().To<TagInitFailedSignal>().ToSingleton();
			injectionBinder.Bind<TagScanCompletedSignal> ().To<TagScanCompletedSignal> ().ToSingleton ();
        }
    }
}