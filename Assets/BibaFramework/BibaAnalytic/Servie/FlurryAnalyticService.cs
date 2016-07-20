using System;
using System.Collections.Generic;
using Analytics;
using BibaFramework.BibaGame;
using BibaFramework.BibaMenu;
using UnityEngine;

namespace BibaFramework.BibaAnalytic
{
    public class FlurryAnalyticService : IAnalyticService
    {
        [Inject]
		public BibaDeviceSession BibaDeviceSession { get; set; }

		[Inject]
		public BibaAccount BibaAccount { get; set; }

		[Inject]
		public BibaDevice BibaDevice { get; set; }

        [Inject]
        public BibaSceneStack BibaSceneStack { get; set; }

        private Flurry _service;

		public void SetupTracking (string iosKey, string androidKey)
    	{
			_service = Flurry.Instance;
			_service.SetLogLevel(LogLevel.All);
			_service.StartSession(iosKey, androidKey);
    	}

        public void TrackStartSession ()
        {
			foreach (var profile in BibaAccount.BibaProfiles) 
			{
				var parameters = TrackingParams;
				parameters.Add (BibaAnalyticConstants.SESSION_START_TIME, BibaDeviceSession.Start.ToString());
				_service.LogEvent(BibaAnalyticConstants.START_SESSION_EVENT, parameters);
			}
        }

        public void TrackEndSession ()
        {
			foreach(var profile in BibaAccount.BibaProfiles)
			{
				var playerSession = profile.BibaPlayerSession;
				var parameters = TrackingParams;

				parameters.Add (BibaAnalyticConstants.PROFILE_ID, profile.Id);
				parameters.Add (BibaAnalyticConstants.SESSION_END_TIME, BibaDeviceSession.End.ToString());
				parameters.Add (BibaAnalyticConstants.TAG_SCANNED, BibaDeviceSession.TagScanned.ToString());

				if (profile.LScore > 0 ||
					profile.MScore > 0 ||
					profile.VScore > 0) 
				{
					parameters.Add (BibaAnalyticConstants.LIGHT_TIME + "_" + profile.Id, profile.LScore.ToString ());
					parameters.Add (BibaAnalyticConstants.MODERATE_TIME + "_" + profile.Id, profile.MScore.ToString ());
					parameters.Add (BibaAnalyticConstants.VIGOROUS_TIME + "_" + profile.Id, profile.VScore.ToString ());
				}

				foreach (var equipment in BibaDeviceSession.SelectedEquipments) 
				{
					parameters.Add(BibaAnalyticConstants.EQUIPMENT_SELECTED, equipment.EquipmentType.ToString());
				}

				foreach (var equipment in BibaDevice.TotalEquipments)
	            {
					parameters.Add(string.Format("{0}_{1}", BibaAnalyticConstants.EQUIPMENT_PLAYED, equipment.EquipmentType.ToString()), equipment.NumberOfTimePlayed.ToString());
	            }

	            _service.LogEvent(BibaAnalyticConstants.END_SESSION_EVENT, parameters);
			}
        }
		
        public void TrackWeatherInfo(BibaWeatherInfo weatherInfo)
        {
            var parameters = TrackingParams;
            
            parameters.Add(BibaAnalyticConstants.WEATHER_TEMPERATURE, weatherInfo.Temperature.ToString("F2"));
            parameters.Add(BibaAnalyticConstants.WEATHER_DESCRIPTION, weatherInfo.WeatherDescription);
            parameters.Add(BibaAnalyticConstants.WEATHER_WIND_SPEED, weatherInfo.WindSpeed.ToString("F2"));

            _service.LogEvent(BibaAnalyticConstants.WEATHER_EVENT, parameters);
		}

        public Dictionary<string, string> TrackingParams {
            get {
                var param = new Dictionary<string, string>() {
                    {BibaAnalyticConstants.TIME_STAMP, DateTime.Now.ToString()}
                };
					
				if(!string.IsNullOrEmpty(BibaDeviceSession.QuadTileId))
                {
					param.Add(BibaAnalyticConstants.QUADTILE_ID, BibaDeviceSession.QuadTileId);
                }

				return param;
            }
        }
    }
}