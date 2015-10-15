using strange.extensions.signal.impl;

namespace BibaFramework.BibaMenu
{
    //BibaMenu
    public class SetMenuStateTriggerSignal : Signal<MenuStateTrigger>{ }
    public class SetMenuStateConditionSignal : Signal<MenuStateCondition, bool>{ }

    public class ProcessNextMenuStateSignal : Signal<BaseMenuState>{ }

    //MenuState - Scene based
	public class SetupSceneMenuStateSignal : Signal<SceneMenuState>{ }
    public class LoadSceneMenuStateSignal : Signal<SceneMenuState>{ }
	public class PushSceneMenuStateSignal : Signal<SceneMenuState>{ }
	public class PopSceneMenuStateSignal : Signal<SceneMenuState>{ }
	public class ReplaceSceneMenuStateSignal : Signal<SceneMenuState>{ }
    
    public class PlaySceneMenuStateEntryAnimationSignal : Signal { } 
    public class PlaySceneMenuStateExitAnimationSignal : Signal { } 
    
    public class SceneMenuStateEntryAnimationEndedSignal : Signal { }
    public class SceneMenuStateExitAnimationEndedSignal : Signal { }

    //MenuState - Object based
	public class PushObjectMenuStateSignal : Signal<ObjectMenuState>{ };
	public class ReplaceObjectMenuStateSignal : Signal<ObjectMenuState>{ };
	public class ToggleObjectMenuStateSignal : Signal<ObjectMenuState, bool>{ };
}