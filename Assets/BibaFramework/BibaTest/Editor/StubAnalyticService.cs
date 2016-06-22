using System;
using BibaFramework.BibaAnalytic;

namespace BibaFramework.BibaTest
{
	public class StubAnalyticService : IAnalyticService
	{
		public void StartTracking (string iosKey, string androidKey)
		{
		}

		public void TrackStartSession ()
		{
		}

		public void TrackEndSession ()
		{
		}

		public void TrackStartRound ()
		{
		}

		public void TrackEndRound ()
		{
		}

		public void TrackActivites ()
		{
		}

		public void TrackEquipmentSelected (BibaFramework.BibaGame.BibaEquipmentType equipmentType)
		{
		}

		public void TrackEquipmentPlayed (BibaFramework.BibaGame.BibaEquipmentType equipmentType)
		{
		}

		public void TrackSatelliteTagEnabled (bool enabled)
		{
		}

		public void TrackSatelliteTagScanned (BibaFramework.BibaGame.BibaTagType tagType)
		{
		}

		public void TrackWeatherInfo (BibaWeatherInfo weatherInfo)
		{
		}
	}
}