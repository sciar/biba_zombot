using System.Collections.Generic;
using BibaFramework.BibaGame;

namespace BibaFramework.BibaAnalytic
{
	public interface IDeviceAnalyticService : IBibaAnalyticService
    {
		void SetupTracking(string iosKey, string androidKey);
		void TrackStartRound();
		void TrackEndRound();
		void TrackActivities();
		void TrackEquipmentSelected (BibaEquipmentType equipmentType);
		void TrackEquipmentPlayed (BibaEquipmentType equipmentType);
		void TrackWeatherInfo(BibaWeatherInfo weatherInfo);
		void TrackTagEnabled();
    }
}