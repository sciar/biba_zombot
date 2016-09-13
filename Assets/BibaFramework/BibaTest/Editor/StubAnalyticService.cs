using System;
using BibaFramework.BibaAnalytic;
using BibaFramework.BibaGame;

namespace BibaFramework.BibaTest
{
	public class StubAnalyticService : IDeviceAnalyticService
	{
		public void SetupTracking (string iosKey, string androidKey)
		{
		}

		public void TrackStartRound ()
		{
		}

		public void TrackEndRound ()
		{
		}

		public void TrackActivities ()
		{
		}

		public void TrackEquipmentSelected (BibaEquipmentType equipmentType)
		{
		}

		public void TrackEquipmentPlayed (BibaEquipmentType equipmentType)
		{
		}

		public void TrackWeatherInfo (BibaWeatherInfo weatherInfo)
		{
		}

		public void TrackTagEnabled ()
		{
		}

		public void TrackStartSession ()
		{
		}

		public void TrackEndSession ()
		{
		}

		public void TrackTagScanned ()
		{
		}
	}
}