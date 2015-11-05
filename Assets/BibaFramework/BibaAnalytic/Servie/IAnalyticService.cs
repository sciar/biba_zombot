using System.Collections.Generic;
using BibaFramework.BibaGame;

namespace BibaFramework.BibaAnalytic
{
    public interface IAnalyticService
    {
		//Setup keys and start tracking
		void StartTracking(string iosKey, string androidKey);
        //Track the start of a game session, and other relevant informations
        void TrackStartSession();
        //Track the end of a game session, session length, the last game scene the user is on
        void TrackEndSession();
        //Track equipments selected in a sessions
        void TrackEquipmentSelected(BibaEquipmentType equipmentType);
        //Track equipments played in a sessions
        void TrackEquipmentPlayed(BibaEquipmentType equipmentType);
        //Track when the satellite tag is enabled
        void TrackSatelliteTagEnabled(bool enabled);
        //Track when the satellite tag is scanned and recognized
        void TrackSatelliteTagScanned(BibaTagType tagType);
        //Track weather information
        void TrackWeatherInfo(BibaWeatherInfo weatherInfo);
    }
}