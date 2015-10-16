using UnityEngine;
using BibaFramework.BibaMenu;
using strange.extensions.context.api;

namespace BibaFramework.BibaGame
{
	public class TagSelectContext : BaseBibaMenuContext 
    {
		public TagSelectContext (MonoBehaviour view) : base(view)
        {
        }
        
		public TagSelectContext (MonoBehaviour view, ContextStartupFlags flags) : base(view, flags)
        {
        }

        protected override void BindModels ()
        {
        }

        protected override void BindServices ()
        {
        }

        protected override void BindViews ()
        {
			mediationBinder.Bind<TagSelectView>().To<TagSelectMediator>();
        }

        protected override void BindCommands ()
        {   
			commandBinder.Bind<EnableTagSignal>().To<EnableTagCommand>();
        }

        protected override void BindSignals ()
        {
        }
    }
}