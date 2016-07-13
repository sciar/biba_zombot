using BibaFramework.BibaMenu;

namespace BibaFramework.BibaGame
{
    public class EquipmentSelectMediator : SceneMenuStateMediator
    {
        [Inject]
        public EquipmentSelectView EquipmentSelectView { get; set; }


        public override SceneMenuStateView View {
            get {
                return EquipmentSelectView;
            }
        }

        public override void RegisterSceneDependentSignals ()
        {
        }

        public override void UnRegisterSceneDependentSignals ()
        {
        }

        public override void SetupSceneDependentMenu ()
        {
        }
    }
}