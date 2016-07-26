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
        }
        
        protected override void BindViews ()
        { 
            mediationBinder.Bind<ARScanView>().To<ARScanMediator>();
            mediationBinder.Bind<ARScanStartView>().To<ARScanStartMediator>();
			mediationBinder.Bind<VuforiaView> ().To<VuforiaMediator> ();
			mediationBinder.Bind<BibaTagEventHandlerView> ().To<BibaTagEventHandlerMediator> ();
        }
        
        protected override void BindCommands ()
        {   
            commandBinder.Bind<StartSignal>().To<CheckToSkipTagScanCommand>();

			commandBinder.Bind<StartTagScanSignal>().To<StartTagScanCommand>();
			commandBinder.Bind<TagInitFailedSignal>().To<TagInitFailedCommand>();
			commandBinder.Bind<TagFoundSignal>().To<TagFoundCommand>();
			commandBinder.Bind<TagScanCompletedSignal> ().
					To<CheckForFirstScanCompletedPointsEventCommand>().
					To<CheckForScanCompletedPointsEventCommand>();

			commandBinder.Bind<EndSignal> ().To<RemoveVuforiaCommand> ();
        }
        
        protected override void BindSignals ()
        {
			injectionBinder.Bind<ToggleTagScanSignal>().To<ToggleTagScanSignal>().ToSingleton();
			injectionBinder.Bind<SetTagToScanAtViewSignal>().To<SetTagToScanAtViewSignal>().ToSingleton();
			injectionBinder.Bind<TagLostSignal>().ToSingleton();
        }
    }
}