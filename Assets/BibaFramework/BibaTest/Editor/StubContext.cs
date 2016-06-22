using System;
using BibaFramework.BibaAnalytic;
using BibaFramework.BibaGame;
using BibaFramework.BibaMenu;
using BibaFramework.BibaNetwork;
using strange.extensions.command.api;
using strange.extensions.command.impl;
using strange.extensions.context.api;
using strange.extensions.context.impl;
using strange.extensions.dispatcher.eventdispatcher.api;
using strange.extensions.dispatcher.eventdispatcher.impl;
using strange.extensions.injector.api;
using strange.extensions.mediation.api;
using strange.extensions.sequencer.api;
using strange.extensions.sequencer.impl;
using strange.framework.api;
using strange.extensions.dispatcher.api;

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
			injectionBinder.Bind<BibaGameModel>().To<BibaGameModel>().ToSingleton();
			injectionBinder.Bind<BibaSessionModel>().To<BibaSessionModel>().ToSingleton();
		}

		protected void BindServices()
		{
			injectionBinder.Bind<IDataService> ().To<StubDataService> ().ToSingleton ();
			injectionBinder.Bind<IAnalyticService> ().To<StubAnalyticService> ().ToSingleton ();

			injectionBinder.Bind<LocalizationService>().To<LocalizationService>().ToSingleton();
			injectionBinder.Bind<AchievementService>().To<AchievementService>().ToSingleton();
			injectionBinder.Bind<SpecialSceneService>().To<SpecialSceneService>().ToSingleton();
			injectionBinder.Bind<PointEventService>().To<PointEventService>().ToSingleton();
		}

		protected void BindCommands()
		{
			commandBinder.Bind<StartSignal>().
			To<StartTrackingLightActivityCommand>().
			InSequence();

			commandBinder.Bind<ToggleTrackModerateActivitySignal> ().To<ToggleTrackModerateActivityCommand> ();
			commandBinder.Bind<ToggleTrackLightActivitySignal> ().To<ToggleTrackLightActivityCommand> ();
			commandBinder.Bind<ToggleTrackVigorousActivitySignal> ().To<ToggleTrackVigorousActivityCommand> ();

			commandBinder.Bind<ApplicationPausedSignal>().To<EndTrackingAllActivitiesCommand>();
			commandBinder.Bind<ApplicationUnPausedSignal> ().To<StartTrackingLightActivityCommand> ();
			commandBinder.Bind<SetLanguageOverwriteSignal> ().To<SetLanguageOverwriteCommand> ();

			commandBinder.Bind<TestCheckForAchievementCommandSignal> ().To<CheckForAchievementsCommand> ();
			commandBinder.Bind<TestCheckForChartBoostCommandSignal> ().To<CheckForChartBoostCommand> ();
			commandBinder.Bind<TestCheckForInactiveResetCommandSignal> ().To<CheckForInactiveResetCommand> ();   
			commandBinder.Bind<TestCheckToSkipTagScanCommandSignal> ().To<CheckToSkipTagScanCommand> ();
		}

		protected void BindSignals()
		{
			injectionBinder.Bind<SetMenuStateConditionSignal> ().To<SetMenuStateTriggerSignal> ();
			injectionBinder.Bind<SetMenuStateTriggerSignal> ().To<SetMenuStateTriggerSignal> ();
		}
	}
}