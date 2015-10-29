using UnityEngine;
using BibaFramework.BibaMenu;
using strange.extensions.context.api;

namespace BibaFramework.BibaGame
{
    public class EquipmentSelectContext : BaseBibaMenuContext 
    {
        public EquipmentSelectContext (MonoBehaviour view) : base(view)
        {
        }
        
        public EquipmentSelectContext (MonoBehaviour view, ContextStartupFlags flags) : base(view, flags)
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
            mediationBinder.Bind<EquipmentSelectView>().To<EquipmentSelectMediator>();
            mediationBinder.Bind<EquipmentSelectToggleView>().To<EquipmentSelectToggleMediator>();
        }

        protected override void BindCommands ()
        {
            commandBinder.Bind<StartSignal>().To<EquipmentSelectContextStartCommand>();
            commandBinder.Bind<EndSignal>().To<EquipmentSelectContextEndCommand>().To<LogLocationRelatedInfoCommand>();
            commandBinder.Bind<EquipmentSelectedSignal>().To<EquipmentSelectedCommand>();
        }

        protected override void BindSignals ()
        {
        }
    }
}