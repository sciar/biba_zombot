using strange.extensions.signal.impl;

namespace BibaFramework.BibaMenu
{
    //BibaMenu
    public class SetMenuStateTriggerSignal : Signal<MenuStateTrigger>{ }
    public class SetMenuStateConditionSignal : Signal<MenuStateCondition, bool>{ }

    public class ProcessNextMenuStateSignal : Signal<BibaMenuState>{ }
    public class SetupMenuSignal : Signal<BibaMenuState>{ }

    public class LoadFullSceneSignal : Signal<BibaMenuState>{ }
    public class PushPopupSceneSignal : Signal<BibaMenuState>{ }
    public class PopPopupSceneSignal : Signal<BibaMenuState>{ }
    public class ReplacePopupSceneSignal : Signal<BibaMenuState>{ }

    public class PlayMenuEntryAnimationSignal : Signal { } 
    public class PlayMenuExitedAnimationSignal : Signal { } 

    public class MenuEntryAnimationEndedSignal : Signal { }
    public class MenuExitAnimationEndedSignal : Signal { }
}