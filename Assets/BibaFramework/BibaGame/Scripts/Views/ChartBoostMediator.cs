using BibaFramework.BibaMenu;
using BibaFramework.BibaAnalytic;
using ChartboostSDK;

namespace BibaFramework.BibaGame
{
    public class ChartBoostMediator : SceneMenuStateMediator
	{
        [Inject]
        public ChartBoostView ChartBoostView { get; set; }

        [Inject]
        public ChartBoostService ChartBoostService { get; set; }

        [Inject]
        public SetMenuStateTriggerSignal SetMenuStateTriggerSignal { get; set; }

        public override SceneMenuStateView View { get { return ChartBoostView; } }

        public override void SetupMenu (BaseMenuState menuState)
        {
        }

        public override void RegisterSceneDependentSignals ()
        {
            ChartBoostService.didDismissInterstitial += CharBoostDismissed;
        }

        public override void UnRegisterSceneDependentSignals ()
        {
            ChartBoostService.didDismissInterstitial -= CharBoostDismissed;
        }

        void CharBoostDismissed(CBLocation location)
        {
            SetMenuStateTriggerSignal.Dispatch(MenuStateTrigger.Next);
        }
	}
}