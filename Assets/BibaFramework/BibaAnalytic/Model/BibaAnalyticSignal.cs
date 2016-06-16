using strange.extensions.signal.impl;

namespace BibaFramework.BibaAnalytic
{
	public abstract class BaseTrackActivitySignal : Signal <bool> { }
	public class ToggleTrackLightActivitySignal : BaseTrackActivitySignal { }
	public class ToggleTrackModerateActivitySignal : BaseTrackActivitySignal { }
	public class ToggleTrackVigorousActivitySignal : BaseTrackActivitySignal { }
}