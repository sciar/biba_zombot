using strange.extensions.signal.impl;
using System.Collections.Generic;
using UnityEngine;

namespace BibaFramework.BibaGame
{
    //Application
    public class StartSignal : Signal{ }
    public class EndSignal : Signal{ }
    public class ApplicationPausedSignal : Signal{ }
    public class ApplicationUnPausedSignal : Signal{ }
	public class LanguageUpdatedSignal : Signal { }
	public class SessionUpdatedSignal : Signal { }

	//Gameplay
	public class ClearEquipmentsSignal : Signal { }
    public class EquipmentSelectedSignal : Signal <BibaEquipmentType, bool> { }
    public class EquipmentPlayedSignal : Signal<BibaEquipmentType> { }
    public class TryToSetHighScoreSignal : Signal<int> { }
	public class CheckForPointsGainSignal : Signal<string>{ };
	public class PointsGainedSignal : Signal<int, int>{ };

	//Audio
    public class PlayBibaBGMSignal : Signal<string>{ };
    public class PlayBibaSFXSignal : Signal<string>{ };
	public class PlayBibaSFXLoopSignal : Signal<string>{ };
	public class StopBibaSFXLoopsSignal : Signal{ };

    //Settings
    public class OpenURLSignal : Signal<string> { }
    public class EnableHowToSignal : Signal<bool> { }
    public class EnableHelpBubblesSignal : Signal<bool> { }
    public class ResetGameModelSignal : Signal { }
	public class SetLanguageOverwriteSignal : Signal<SystemLanguage>{ }

    //Tag
    public class EnableTagSignal : Signal<bool> { }
    public class TagInitFailedSignal : Signal { } 
    public class TagScannedSignal : Signal<BibaTagType> { }
    public class LogCameraReminderTimeSignal : Signal { }
    public class TagScanCompletedSignal : Signal { } 
}