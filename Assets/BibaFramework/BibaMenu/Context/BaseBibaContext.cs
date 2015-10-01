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
		
		protected override void mapBindings()
		{
            BindBaseComponents();

            BindModels();
            BindServices();
            BindViews();
            BindCommands();
            BindSignals();

            /*
            injectionBinder.Bind<IExampleModel>().To<ExampleModel>().ToSingleton();
            injectionBinder.Bind<IExampleService>().To<ExampleService>().ToSingleton();
            
            
            mediationBinder.Bind<ExampleView>().To<ExampleMediator>();
            
            
            commandBinder.Bind<CallWebServiceSignal>().To<CallWebServiceCommand>();
            
            //StartSignal is now fired instead of the START event.
            //Note how we've bound it "Once". This means that the mapping goes away as soon as the command fires.
			
			//In MyFirstProject, there's are SCORE_CHANGE and FULFILL_SERVICE_REQUEST Events.
			//Here we change that to a Signal. The Signal isn't bound to any Command,
			//so we map it as an injection so a Command can fire it, and a Mediator can receive it
			injectionBinder.Bind<ScoreChangedSignal>().ToSingleton();
			injectionBinder.Bind<FulfillWebServiceRequestSignal>().ToSingleton();
			*/
		}

        protected virtual void BindBaseComponents()
        {   
            injectionBinder.Bind<StartSignal>().ToSingleton();
        }

        protected abstract void BindModels();
        protected abstract void BindServices();
        protected abstract void BindViews();
        protected abstract void BindCommands();
        protected abstract void BindSignals();
	}
}