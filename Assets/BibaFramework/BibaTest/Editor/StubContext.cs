using System;
using BibaFramework.BibaAnalytic;
using BibaFramework.BibaGame;
using BibaFramework.BibaMenu;
using BibaFramework.BibaNetwork;
using strange.extensions.command.api;
using strange.extensions.command.impl;
using strange.extensions.context.api;
using strange.extensions.context.impl;
using strange.extensions.dispatcher.api;
using strange.extensions.dispatcher.eventdispatcher.api;
using strange.extensions.dispatcher.eventdispatcher.impl;
using strange.extensions.injector.api;
using strange.extensions.mediation.api;
using strange.extensions.sequencer.api;
using strange.extensions.sequencer.impl;
using strange.framework.api;
using UnityEngine.Experimental.Director;
using UnityEngine;

namespace BibaFramework.BibaTest
{
	public class StubContext : CrossContext 
	{
		public ICommandBinder commandBinder;
		public IMediationBinder mediationBinder;

		public StubContext (): base() 
		{ 
		}

		public StubContext (object view, bool autoStartup) : base(view, autoStartup)
		{
		}

		protected override void addCoreComponents() 
		{
			base.addCoreComponents();

			injectionBinder.Bind<IInjectionBinder>().ToValue(injectionBinder);
			injectionBinder.Bind<ICommandBinder>().To<SignalCommandBinder>().ToSingleton();
			injectionBinder.Bind<IEventDispatcher>().To<EventDispatcher>().ToSingleton().ToName(ContextKeys.CONTEXT_DISPATCHER);
			injectionBinder.Bind<ISequencer>().To<EventSequencer>().ToSingleton();
			commandBinder = injectionBinder.GetInstance<ICommandBinder>() as ICommandBinder;
		}

		public override void AddView(object view) 
		{
			mediationBinder.Trigger(MediationEvent.AWAKE, view as IView);
		}

		public override IContext Start()
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
			BindModels();
			BindServices();
			BindCommands();
			BindSignals ();
		} 

		protected void BindModels()
		{
			injectionBinder.Bind<BibaDevice>().To<BibaDevice>().ToSingleton();
			injectionBinder.Bind<BibaDeviceSession>().To<BibaDeviceSession>().ToSingleton();
			injectionBinder.Bind<BibaAccount>().To<BibaAccount>().ToSingleton();
			injectionBinder.Bind<BibaProfile>().To<BibaProfile> ().ToSingleton ();
			injectionBinder.Bind<Animator> ().ToName(BibaMenuConstants.BIBA_STATE_MACHINE).To<StubAnimator> ().ToSingleton();
		}

		protected void BindServices()
		{
			injectionBinder.Bind<IDataService> ().To<StubDataService> ().ToSingleton ();
			injectionBinder.Bind<IDeviceAnalyticService> ().To<StubAnalyticService> ().ToSingleton ();

			injectionBinder.Bind<AchievementService>().To<AchievementService>().ToSingleton();
			injectionBinder.Bind<SpecialSceneService>().To<SpecialSceneService>().ToSingleton();
			injectionBinder.Bind<PointEventService>().To<PointEventService>().ToSingleton();
		}

		protected void BindCommands()
		{
			commandBinder.Bind<StartSignal>().To<SetDeviceModelCommand>().To<SetDefaultProfileCommand>().To<StartNewSessionCommand>().InSequence();
			commandBinder.Bind<SetProfileSignal> ().To<SetProfileCommand> ();
			commandBinder.Bind<TestModelResetSignal>().To<SetDeviceModelCommand>().To<StartNewSessionCommand>().InSequence();

			commandBinder.Bind<ToggleTrackModerateActivitySignal> ().To<ToggleTrackModerateActivityCommand> ();
			commandBinder.Bind<ToggleTrackLightActivitySignal> ().To<ToggleTrackLightActivityCommand> ();
			commandBinder.Bind<ToggleTrackVigorousActivitySignal> ().To<ToggleTrackVigorousActivityCommand> ();

			commandBinder.Bind<ApplicationPausedSignal>().To<EndTrackingActivitiesCommand>();
			commandBinder.Bind<ApplicationUnPausedSignal> ().To<StartTrackingActivitiesCommand> ();

			commandBinder.Bind<TestCheckForChartBoostCommandSignal> ().To<CheckForChartBoostCommand> ();
			commandBinder.Bind<TestCheckForSessionEndCommandSignal> ().To<CheckForSessionEndCommand> ();   
			commandBinder.Bind<TestCheckToSkipTagScanCommandSignal> ().To<CheckToSkipTagScanCommand> ();
			
			commandBinder.Bind<EquipmentPlayedSignal>().To<EquipmentPlayedCommand>();
			commandBinder.Bind<EquipmentSelectedSignal>().To<EquipmentSelectedCommand>();
			commandBinder.Bind<TryToSetHighScoreSignal> ().To<TryToSetHighScoreCommand> ();

			//Menu
			commandBinder.Bind<SetMenuStateConditionSignal> ().To<SetMenuStateConditionCommand> ();
		}

		protected void BindSignals()
		{
			injectionBinder.Bind<SetMenuStateTriggerSignal> ().To<SetMenuStateTriggerSignal> ();
			injectionBinder.Bind<SessionUpdatedSignal> ().To<SessionUpdatedSignal> ();
		}
	}
};

 