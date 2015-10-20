using strange.extensions.signal.impl;
using System.Collections.Generic;

namespace BibaFramework.BibaGame
{
    public class StartSignal : Signal{ }
    public class EndSignal : Signal{ }
    public class ApplicationPausedSignal : Signal{ }
    public class ApplicationUnPausedSignal : Signal{ }

    public class EquipmentSelectedSignal : Signal <List<BibaEquipmentType>> { }

    //Game
    public class EquipmentPlayedSignal : Signal<BibaEquipmentType> { }
    public class TryToSetHighScoreSignal : Signal<float> { }

    //Settings
    public class OpenAboutBibaURLSignal : Signal { }
    public class EnableHowToSignal : Signal<bool> { }
    public class EnableHelpBubblesSignal : Signal<bool> { }

    //Tag
    public class EnableTagSignal : Signal<bool> { }
    public class TagScannedSignal : Signal<BibaTagType> { }
}