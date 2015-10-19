using strange.extensions.command.impl;
using System;
using BibaFramework.BibaMenu;

namespace BibaFramework.BibaGame
{
    public class SetupGameModelCommand : Command
    {
        [Inject]
        public BibaGameModel BibaGameModel { get; set; }

        [Inject]
        public SetMenuStateConditionSignal SetMenuStateConditionSignal { get; set; }

        public override void Execute ()
        {
            SetMenuStateConditionSignal.Dispatch(MenuStateCondition.PrivacyEnabled, BibaGameModel.PrivacyEnabled);
            SetMenuStateConditionSignal.Dispatch(MenuStateCondition.TagEnabled, BibaGameModel.TagEnabled);
            SetMenuStateConditionSignal.Dispatch(MenuStateCondition.HowToEnabled, BibaGameModel.HowToEnabled);
            SetMenuStateConditionSignal.Dispatch(MenuStateCondition.HelpBubblesEnabled, BibaGameModel.HelpBubblesEnabled);
        }
    }
}