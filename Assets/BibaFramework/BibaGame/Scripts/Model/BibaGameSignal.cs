using strange.extensions.signal.impl;
using System.Collections.Generic;
using UnityEngine;

namespace BibaFramework.BibaGame
{
    //Device
    public class StartSignal : Signal{ }
    public class EndSignal : Signal{ }
    public class ApplicationPausedSignal : Signal{ }
    public class ApplicationUnPausedSignal : Signal{ }
	public class DeviceUpdatedSignal : Signal { }
	public class SessionUpdatedSignal : Signal { }
	public class SetProfileSignal : Signal<string>{ }
	public class StartNewSessionSignal : Signal { }
    public class EquipmentSelectedSignal : Signal <BibaEquipmentType, bool> { }
    public class EquipmentPlayedSignal : Signal<BibaEquipmentType> { }
	public class CheckForPointsGainSignal : Signal<string>{ };
	public class PointsGainedSignal : Signal<int, int>{ };

	//Gameplay
	public class TryToSetHighScoreSignal : Signal<int> { }
	public class CheckForAchievementsSignal : Signal { }

	//Audio
    public class PlayBibaBGMSignal : Signal<string>{ };
    public class PlayBibaSFXSignal : Signal<string>{ };
	public class PlayBibaSFXLoopSignal : Signal<string>{ };
	public class StopBibaSFXLoopsSignal : Signal{ };

    //Settings
    public class OpenURLSignal : Signal<string> { }
    public class EnableHowToSignal : Signal<bool> { }
    public class EnableHelpBubblesSignal : Signal<bool> { }
    public class ResetDeviceSignal : Signal { }
	public class SetLanguageOverwriteSignal : Signal<SystemLanguage>{ }

    //Tag
	public class EnableTagSignal : Signal<bool> { }
	public class StartTagScanSignal : Signal { }
	public class ToggleARCameraSignal : Signal<bool> { }
	public class ToggleTagSignal : Signal<bool> { }
    public class SetTagToScanAtViewSignal : Signal { }
    public class TagInitFailedSignal : Signal { } 
	public class TagFoundSignal : Signal<BibaTagType, GameObject> { }
	public class TagScanCompletedSignal : Signal { }
}