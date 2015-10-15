using strange.extensions.signal.impl;

namespace BibaFramework.BibaMenu
{
    //BibaMenu
    public class SetMenuStateTriggerSignal : Signal<MenuStateTrigger>{ }
    public class SetMenuStateConditionSignal : Signal<MenuStateCondition, bool>{ }

    public class ProcessNextMenuStateSignal : Signal<BaseMenuState>{ }
    public class PushMenuStateSignal : Signal<BaseMenuState>{ }
    public class PopMenuStateSignal : Signal<BaseMenuState>{ }
    public class ReplaceMenuStateSignal : Signal<BaseMenuState>{ }

    //MenuState - Scene based
    public class SwitchSceneMenuStateSignal : Signal<BaseMenuState>{ }
    public class SetupSceneMenuStateSignal : Signal<BaseMenuState>{ }

    public class PlayMenuStateEntryAnimationSignal : Signal { } 
    public class PlayMenuStateExitAnimationSignal : Signal { } 
    
    public class MenuStateEntryAnimationEndedSignal : Signal { }
    public class MenuStateExitAnimationEndedSignal : Signal { }

    //MenuState - Object based
	public class ToggleObjectMenuStateSignal : Signal<ObjectMenuState, bool>{ };
}