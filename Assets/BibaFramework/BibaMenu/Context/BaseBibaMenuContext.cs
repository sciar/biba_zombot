using UnityEngine;
using strange.extensions.context.api;

namespace BibaFramework.BibaMenu
{
    //The class is just for the BibaMenuContextView for the user to be able to select the right Context
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
            mediationBinder.Bind<BibaLocalizedTextView>().To<BibaLocalizedTextMediator>();
			mediationBinder.Bind<BibaLocalizedServiceView> ().To<BibaLocalizedServiceMediator> ();
            mediationBinder.Bind<URLButtonView>().To<URLButtonMediator>();
            mediationBinder.Bind<ObjectMenuStateView>().To<ObjectMenuStateMediator>();
            mediationBinder.Bind<PanelMenuStateView>().To<PanelMenuStateMediator>();
		}
	}
}