using strange.extensions.signal.impl;
using System.Collections.Generic;
using BibaFramework.BibaTag;

namespace BibaFramework.BibaGame
{
    public class StartSignal : Signal{ }
    public class EndSignal : Signal{ }
    public class ApplicationPausedSignal : Signal{ }
    public class ApplicationUnPausedSignal : Signal{ }

    public class EquipmentSelectedSignal : Signal <List<BibaEquipmentType>> { }

    //BibaTag
    public class ToggleScanSignal : Signal<bool> { }
    public class TagScannedSignal : Signal<BibaTagType> { }
}