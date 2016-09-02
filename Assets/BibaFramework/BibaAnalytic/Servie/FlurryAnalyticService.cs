using System;
using System.Collections.Generic;
using Analytics;
using BibaFramework.BibaGame;
using BibaFramework.BibaMenu;

namespace BibaFramework.BibaAnalytic
{
    public class FlurryAnalyticService : IAnalyticService
    {
        [Inject]
		public BibaDeviceSession BibaDeviceSession { get; set; }

		[Inject]
		public BibaAccount BibaAccount { get; set; }

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
				var parameters = TrackingParams;

				parameters.Add (BibaAnalyticConstants.PROFILE_ID, profile.Id);
				parameters.Add (BibaAnalyticConstants.SESSION_END_TIME, DateTime.UtcNow.ToString());
				parameters.Add (BibaAnalyticConstants.TAG_ENABLED, BibaDeviceSession.TagEnabled.ToString());
				parameters.Add (BibaAnalyticConstants.TAG_SCANNED, BibaDeviceSession.TagScanned.ToString());

				var lSession = profile.BibaProfileSession.LMVSessionDict[LMVScoreType.light_score];
				var mSession = profile.BibaProfileSession.LMVSessionDict[LMVScoreType.moderate_score];
				var vSession = profile.BibaProfileSession.LMVSessionDict[LMVScoreType.vigorous_score];
				if (lSession.SessionScore > 0 ||
					mSession.SessionScore> 0 ||
					vSession.SessionScore > 0) 
				{
					parameters.Add (BibaAnalyticConstants.LIGHT_TIME, lSession.SessionScore.ToString ());
					parameters.Add (BibaAnalyticConstants.MODERATE_TIME, mSession.SessionScore.ToString ());
					parameters.Add (BibaAnalyticConstants.VIGOROUS_TIME, vSession.SessionScore.ToString ());
				}

				foreach (var equipment in BibaDeviceSession.SelectedEquipments) 
				{
					parameters.Add(string.Format("{0}_{1}", BibaAnalyticConstants.EQUIPMENT_SELECTED, equipment.EquipmentType.ToString()), equipment.EquipmentType.ToString());
				}

				foreach (var equipment in profile.BibaProfileSession.SessionEquipments)
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