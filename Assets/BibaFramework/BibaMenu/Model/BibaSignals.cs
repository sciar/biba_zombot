using strange.extensions.signal.impl;

namespace BibaFramework.BibaMenu
{
    public class StartSignal : Signal { }

    public class TriggerNextMenuStateSignal : Signal<MenuStateTrigger>{ }
    public class LoadGameSceneSignal : Signal<BibaMenuState>{ }
    public class UnloadGameSceneSignal : Signal<BibaMenuState>{ }
    public class SetupMenuSignal : Signal<BibaMenuState>{ }

    public class QueueLoadingAnimationSignal : Signal<GameScene> { }
    public class PlayMenuEntryAnimationSignal : Signal { } 
    public class PlayMenuExitedAnimationSignal : Signal { } 
    public class MenuEntryAnimationEndedSignal : Signal { }
    public class MenuExitAnimationEndedSignal : Signal { } 
}