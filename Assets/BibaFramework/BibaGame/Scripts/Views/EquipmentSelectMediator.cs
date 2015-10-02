
using BibaFramework.BibaMenu;

namespace BibaFramework.BibaGame
{
    public class EquipmentSelectMediator : BaseBibaMediator
    {
        [Inject]
        public EquipmentSelectView EquipmentSelectView { get; set; }

        public override BaseBibaView View {
            get {
                return EquipmentSelectView;
            }
        }

        public override void OnRegister ()
        {
            base.OnRegister ();
        }

        public override void OnRemove ()
        {
            base.OnRemove ();
        }

        public override void SetupMenu (BibaMenuState menuState)
        {
        }
    }
}