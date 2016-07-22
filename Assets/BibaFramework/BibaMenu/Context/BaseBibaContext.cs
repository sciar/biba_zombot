using System.Collections;
using UnityEngine;
using strange.extensions.command.api;
using strange.extensions.command.impl;
using strange.extensions.context.api;
using strange.extensions.context.impl;
using BibaFramework.BibaGame;
using BibaFramework.BibaNetwork;
using BibaFramework.BibaAnalytic;

namespace BibaFramework.BibaMenu
{
	public abstract class BaseBibaContext : MVCSContext 
	{
		public BaseBibaContext (MonoBehaviour view) : base(view)
		{
		}
		
		public BaseBibaContext (MonoBehaviour view, ContextStartupFlags flags) : base(view, flags)
		{
		}
		
		// Unbind the default EventCommandBinder and rebind the SignalCommandBinder
		protected override void addCoreComponents()
		{
			base.addCoreComponents();
			injectionBinder.Unbind<ICommandBinder>();
			injectionBinder.Bind<ICommandBinder>().To<SignalCommandBinder>().ToSingleton();
		}
		
		// Override Start so that we can fire the StartSignal 
		override public IContext Start()
		{
			base.Start();
			StartSignal startSignal= (StartSignal)injectionBinder.GetInstance<StartSignal>();
			startSignal.Dispatch();
			return this;
		}

        public override void OnRemove ()
        {
            EndSignal endSignal = (EndSignal)injectionBinder.GetInstance<EndSignal>();
            endSignal.Dispatch();
            base.OnRemove ();
        }
		
		protected override void mapBindings()
		{
            BindBaseComponents();

            BindModels();
            BindServices();
            BindViews();
            BindCommands();
            BindSignals();
		}

        protected virtual void BindBaseComponents()
        {   
            injectionBinder.Bind<StartSignal>().To<StartSignal>().ToSingleton();
            injectionBinder.Bind<EndSignal>().To<EndSignal>().ToSingleton();
			injectionBinder.Bind<ResetModelsSignal>().To<ResetModelsSignal>().ToSingleton();
			  
			//Common
            commandBinder.Bind<OpenURLSignal>().To<OpenURLCommand>();
			commandBinder.Bind<CheckForPointsGainSignal>().To<CheckForPointsEventCommand>();  
			commandBinder.Bind<SetProfileSignal>().To<SetProfileCommand>();

			//Menu
            commandBinder.Bind<SetMenuStateTriggerSignal>().To<SetMenuStateTriggerCommand>();
            commandBinder.Bind<SetMenuStateConditionSignal>().To<SetMenuStateConditionCommand>();

            //Audio
            commandBinder.Bind<PlayBibaBGMSignal>().To<PlayBGMCommand>();
			commandBinder.Bind<PlayBibaSFXSignal>().To<PlaySFXCommand>();
			commandBinder.Bind<PlayBibaSFXLoopSignal>().To<PlaySFXLoopCommand>();
			commandBinder.Bind<StopBibaSFXLoopsSignal>().To<StopSFXLoopsCommand>();

            //BibaNetwork
            commandBinder.Bind<ContentUpdatedFromCDNSignal>().To<ContentUpdatedFromCDNCommand>();

			//Analytic
			commandBinder.Bind<ToggleTrackModerateActivitySignal> ().To<ToggleTrackModerateActivityCommand> ();
			commandBinder.Bind<ToggleTrackLightActivitySignal> ().To<ToggleTrackLightActivityCommand> ();
			commandBinder.Bind<ToggleTrackVigorousActivitySignal> ().To<ToggleTrackVigorousActivityCommand> ();
        }

        protected abstract void BindModels();
        protected abstract void BindServices();
        protected abstract void BindViews();
        protected abstract void BindCommands();
        protected abstract void BindSignals();
	}
}