using System;
using BibaFramework.BibaAnalytic;

namespace BibaFramework.BibaTest
{
	public class StubAnalyticService : IAnalyticService
	{
		public void SetupTracking (string iosKey, string androidKey)
		{
		}

		public void TrackStartSession ()
		{
		}

		public void TrackEndSession ()
		{
		}
			
		public void TrackWeatherInfo (BibaWeatherInfo weatherInfo)
		{
		}
	}
}