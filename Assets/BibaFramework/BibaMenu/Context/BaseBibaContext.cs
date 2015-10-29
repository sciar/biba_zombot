using System.Collections;
using UnityEngine;
using strange.extensions.command.api;
using strange.extensions.command.impl;
using strange.extensions.context.api;
using strange.extensions.context.impl;
using BibaFramework.BibaGame;

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

            commandBinder.Bind<SetMenuStateTriggerSignal>().To<SetMenuStateTriggerCommand>();
            commandBinder.Bind<SetMenuStateConditionSignal>().To<SetMenuStateConditionCommand>();
            commandBinder.Bind<ProcessNextMenuStateSignal>().To<ProcessNextMenuStateCommand>();

            mediationBinder.Bind<MenuStateMachineView>().To<MenuStateMachineMediator>();
        }

        protected abstract void BindModels();
        protected abstract void BindServices();
        protected abstract void BindViews();
        protected abstract void BindCommands();
        protected abstract void BindSignals();
	}
}