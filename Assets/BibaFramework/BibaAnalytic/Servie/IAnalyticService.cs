using System.Collections.Generic;
using BibaFramework.BibaGame;

namespace BibaFramework.BibaAnalytic
{
    public interface IAnalyticService
    {
		//Setup keys and start tracking
		void SetupTracking(string iosKey, string androidKey);
        //Track the start of an app session, and other relevant informations
        void TrackStartSession();
        //Track the end of an app session, session length, the last game scene the user is on
        void TrackEndSession();
        //Track weather information
        void TrackWeatherInfo(BibaWeatherInfo weatherInfo);
    }
}