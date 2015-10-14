using strange.extensions.signal.impl;

namespace BibaFramework.BibaMenu
{
    //BibaMenu
    public class SetMenuStateTriggerSignal : Signal<MenuStateTrigger>{ }
    public class SetMenuStateConditionSignal : Signal<MenuStateCondition, bool>{ }

    public class ProcessNextMenuStateSignal : Signal<BibaMenuState>{ }
    public class SetupMenuSignal : Signal<BibaMenuState>{ }

    //MenuState - Scene based
    public class LoadSceneBasedMenuStateSignal : Signal<BibaMenuState>{ }
    public class PushSceneBasedMenuStateSignal : Signal<BibaMenuState>{ }
    public class PopSceneBasedMenuStateSignal : Signal<BibaMenuState>{ }
    public class ReplaceSceneBasedMenuStateSignal : Signal<BibaMenuState>{ }
    
    public class PlaySceneBasedMenuStateEntryAnimationSignal : Signal { } 
    public class PlaySceneBasedMenuStateExitAnimationSignal : Signal { } 
    
    public class SceneBasedMenuStateEntryAnimationEndedSignal : Signal { }
    public class SceneBasedMenuStateExitAnimationEndedSignal : Signal { }

    //MenuState - Object based
    public class EnableObjectBasedMenuStateSignal : Signal<BibaMenuState>{ };
    public class ReplaceObjectBasedMenuStateSignal : Signal<BibaMenuState>{ };
}