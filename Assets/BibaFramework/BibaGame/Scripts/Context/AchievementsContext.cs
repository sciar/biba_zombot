using UnityEngine;
using BibaFramework.BibaMenu;
using strange.extensions.context.api;

namespace BibaFramework.BibaGame
{
    public class AchievementsContext : BaseBibaMenuContext 
    {
        public AchievementsContext (MonoBehaviour view) : base(view)
        {
        }
        
        public AchievementsContext (MonoBehaviour view, ContextStartupFlags flags) : base(view, flags)
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
            mediationBinder.Bind<AchievementsView>().To<AchievementsMediator>();
        }
			
        protected override void BindCommands ()
        {    
            commandBinder.Bind<StartSignal>().To<CheckForAchievementsCommand>();
        }

        protected override void BindSignals ()
        {
        }
    }
}