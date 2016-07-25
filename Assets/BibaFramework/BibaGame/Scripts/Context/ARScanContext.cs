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
			//For debuggin from the Scene in Unity
			#if UNITY_EDITOR
			var session = injectionBinder.GetInstance<BibaDeviceSession>();
			if(session.SelectedEquipments.Count == 0)
			{
				session.SelectedEquipments.Add(new BibaEquipment(BibaEquipmentType.bridge));
				session.SelectedEquipments.Add(new BibaEquipment(BibaEquipmentType.climber));
				session.SelectedEquipments.Add(new BibaEquipment(BibaEquipmentType.overhang));
				session.TagEnabled = true;
			}
			#endif
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

			commandBinder.Bind<SetTagToScanSignal>().To<SetTagToScanCommand>();
            commandBinder.Bind<LogCameraReminderTimeSignal>().To<LogCameraReminderTimeCommand>();
			commandBinder.Bind<TagScanCompletedSignal> ().
					To<TagScanCompletedCommand>().
					To<RemoveVuforiaCommand>().
					To<CheckForFirstScanCompletedPointsEventCommand>().
					To<CheckForScanCompletedPointsEventCommand>();
        }
        
        protected override void BindSignals ()
        {
			injectionBinder.Bind<SetTagToScanAtViewSignal>().To<SetTagToScanAtViewSignal>().ToSingleton();
            injectionBinder.Bind<TagInitFailedSignal>().To<TagInitFailedSignal>().ToSingleton();
			injectionBinder.Bind<TagFoundSignal>().ToSingleton();
			injectionBinder.Bind<TagLostSignal>().ToSingleton();
        }
    }
}