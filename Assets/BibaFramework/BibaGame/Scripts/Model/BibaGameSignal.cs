using strange.extensions.signal.impl;
using System.Collections.Generic;
using UnityEngine;

namespace BibaFramework.BibaGame
{
    //Game
    public class StartSignal : Signal{ }
    public class EndSignal : Signal{ }
    public class ApplicationPausedSignal : Signal{ }
    public class ApplicationUnPausedSignal : Signal{ }
	public class GameModelUpdatedSignal : Signal { }
	public class LanguageUpdatedSignal : Signal { }
	public class SetLanguageOverwriteSignal : Signal<SystemLanguage>{ }

	public class ClearEquipmentsSignal : Signal { }
    public class EquipmentSelectedSignal : Signal <BibaEquipmentType, bool> { }
    public class EquipmentPlayedSignal : Signal<BibaEquipmentType> { }
    public class TryToSetHighScoreSignal : Signal<double> { }

    public class PlayBibaBGMSignal : Signal<string>{ };
    public class PlayBibaSFXSignal : Signal<string>{ };

	public class CheckForPointsGainSignal : Signal<string>{ };
	public class PointsGainedSignal : Signal<int>{ };

	//Network
	public class DownloadGeoBasedScenesSignal : Signal { }
	public class ContentUpdatedFromCDNSignal : Signal<string> { }

    //Settings
    public class OpenURLSignal : Signal<string> { }
    public class EnableHowToSignal : Signal<bool> { }
    public class EnableHelpBubblesSignal : Signal<bool> { }
    public class ResetGameModelSignal : Signal { }

    //Tag
    public class EnableTagSignal : Signal<bool> { }
    public class TagServiceInitFailedSignal : Signal { } 
    public class TagScannedSignal : Signal<BibaTagType> { }
    public class LogCameraReminderTimeSignal : Signal { }
    public class TagScanCompletedSignal : Signal { } 
}