using strange.extensions.signal.impl;

namespace BibaFramework.BibaGame
{
    public class StartSignal : Signal { }

    //BibaTag
    public class ToggleScanSignal : Signal<bool> { }
    public class TagScannedSignal : Signal<string> { }
}