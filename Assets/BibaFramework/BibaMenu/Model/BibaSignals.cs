using strange.extensions.signal.impl;

namespace BibaFramework.BibaMenu
{
    public class StartSignal : Signal { }

    public class TriggerNextMenuStateSignal : Signal<MenuStateTrigger>{ }
    public class LoadGameSceneSignal : Signal<BibaMenuState>{ }
    public class SetupMenuSignal : Signal<BibaMenuState>{ }

    public class PlayMenuEntryAnimationSignal : Signal { } 
    public class PlayMenuExitedAnimationSignal : Signal { } 
    public class PlayMenuLoadAnimationSignal : Signal<bool> { }
   
    public class MenuEntryAnimationEndedSignal : Signal { }
    public class MenuExitAnimationEndedSignal : Signal { }
}