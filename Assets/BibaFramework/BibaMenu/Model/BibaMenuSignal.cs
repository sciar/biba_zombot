using strange.extensions.signal.impl;

namespace BibaFramework.BibaMenu
{
    //BibaMenu
    public class SetMenuStateTriggerSignal : Signal<string>{ }
    public class SetMenuStateConditionSignal : Signal<string, bool>{ }

    public class ProcessNextMenuStateSignal : Signal<BaseMenuState>{ }
    public class PushMenuStateSignal : Signal<BaseMenuState>{ }
    public class PopMenuStateSignal : Signal<BaseMenuState>{ }
    public class ReplaceMenuStateSignal : Signal<BaseMenuState>{ }
    public class RemoveLastMenuStateSignal : Signal { }

    //MenuState - Scene based
    public class SwitchSceneMenuStateSignal : Signal<BaseMenuState>{ }
    public class SetupSceneMenuStateSignal : Signal<BaseMenuState>{ }
    
    public class MenuStateEntryAnimationEndedSignal : Signal { }
    public class MenuStateExitAnimationEndedSignal : Signal { }

    //MenuState - Object based
	public class ToggleObjectMenuStateSignal : Signal<ObjectMenuState, bool>{ };
}