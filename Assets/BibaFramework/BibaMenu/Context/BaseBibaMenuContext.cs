using UnityEngine;
using strange.extensions.context.api;

namespace BibaFramework.BibaMenu
{
	public abstract class BaseBibaMenuContext : BaseBibaContext 
	{
        public BaseBibaMenuContext (MonoBehaviour view) : base(view)
		{
		}
		
        public BaseBibaMenuContext (MonoBehaviour view, ContextStartupFlags flags) : base(view, flags)
		{
		}

        protected override void BindBaseComponents ()
        {
            base.BindBaseComponents ();

            mediationBinder.Bind<BibaButtonView>().To<BibaButtonMediator>();
            commandBinder.Bind<TriggerNextMenuStateSignal>().To<TriggerNextMenuStateCommand>();
        }
	}
}