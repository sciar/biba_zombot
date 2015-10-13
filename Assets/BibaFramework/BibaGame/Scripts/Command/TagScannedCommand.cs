using BibaFramework.BibaMenu;
using strange.extensions.command.impl;
using BibaFramework.BibaTag;

namespace BibaFramework.BibaGame
{
    public class TagScannedCommand : Command
    {
        [Inject]
        public BibaEquipment BibaEquipmentToScan { get; set; }

        [Inject]
        public BibaTagType TagScanned { get; set; }

        [Inject]
        public SetMenuStateTriggerSignal SetMenuStateTriggerSignal { get; set; }

        public override void Execute ()
        {
            if (TagScanned == BibaEquipmentToScan.TagType)
            {
                SetMenuStateTriggerSignal.Dispatch(MenuStateTrigger.Yes);
            }
        }
    }
}