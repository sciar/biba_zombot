﻿using UnityEngine;
using BibaFramework.BibaMenu;
using strange.extensions.context.api;
using BibaFramework.BibaNetwork;
using BibaFramework.BibaAnalytic;

namespace BibaFramework.BibaGame
{
    public class IntroContext : BaseBibaMenuContext 
    {
        public IntroContext (MonoBehaviour view) : base(view)
        {
        }
        
        public IntroContext (MonoBehaviour view, ContextStartupFlags flags) : base(view, flags)
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
            mediationBinder.Bind<IntroView>().To<IntroMediator>();
			mediationBinder.Bind<PointsPopupView> ().To<IntroPointsPopupMediator> ();
        }
			
        protected override void BindCommands ()
        {
			commandBinder.Bind<StartSignal>().To<CheckForSessionEndCommand>();
        }

        protected override void BindSignals ()
        {
        }
    }
}