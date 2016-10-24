using UnityEngine;
using BibaFramework.BibaMenu;
using strange.extensions.context.api;
using BibaFramework.BibaNetwork;

namespace BibaFramework.BibaGame
{
	public class MenuContext : BaseBibaMenuContext 
	{
		public MenuContext (MonoBehaviour view) : base(view)
		{
		}
		
		public MenuContext (MonoBehaviour view, ContextStartupFlags flags) : base(view, flags)
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
			mediationBinder.Bind<MenuView>().To<MenuMediator>();
		}
		
		protected override void BindCommands ()
		{
			commandBinder.Bind<StartSignal>().To<UpdateFromCDNCommand>();
		}
		
		protected override void BindSignals ()
		{
		}
	}
}