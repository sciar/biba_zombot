using strange.extensions.signal.impl;

namespace BibaFramework.BibaMenu
{
    //BibaMenu
    public class TriggerNextMenuStateSignal : Signal<MenuStateTrigger>{ }
    public class ProcessNextMenuStateSignal : Signal<BibaMenuState>{ }
    public class SetupMenuSignal : Signal<BibaMenuState>{ }

    public class LoadFullSceneSignal : Signal<BibaMenuState>{ }
    public class PushPopupSceneSignal : Signal<BibaMenuState>{ }
    public class PopPopupSceneSignal : Signal<BibaMenuState>{ }
    public class ReplacePopupSceneSignal : Signal<BibaMenuState>{ }

    public class PlayMenuEntryAnimationSignal : Signal { } 
    public class PlayMenuExitedAnimationSignal : Signal { } 
    public class PlayMenuLoadAnimationSignal : Signal<bool> { }

    public class MenuEntryAnimationEndedSignal : Signal { }
    public class MenuExitAnimationEndedSignal : Signal { }
}