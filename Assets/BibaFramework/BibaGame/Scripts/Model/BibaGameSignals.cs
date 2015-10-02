using strange.extensions.signal.impl;

namespace BibaFramework.BibaGame
{
    public class StartSignal : Signal { }

    public class EquipmentSelectToggledSignal : Signal <BibaEquipmentType, bool> { }

    //BibaTag
    public class ToggleScanSignal : Signal<bool> { }
    public class TagScannedSignal : Signal<string> { }
}