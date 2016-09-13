using System;
using System.Collections.Generic;
using Analytics;
using BibaFramework.BibaGame;
using BibaFramework.BibaMenu;

namespace BibaFramework.BibaAnalytic
{
    public class FlurryDeviceAnalyticService : IDeviceAnalyticService
    {
        [Inject]
		public BibaDeviceSession BibaDeviceSession { get; set; }

		[Inject]
		public BibaAccount BibaAccount { get; set; }

        private Flurry _service;

		#region IAnalyticService
		public void SetupTracking (string iosKey, string androidKey)
    	{
			_service = Flurry.Instance;
			_service.SetLogLevel(LogLevel.All);
			_service.StartSession(iosKey, androidKey);
    	}

        public void TrackStartSession ()
        {
			var parameters = TrackingParams;
			parameters.Add (BibaAnalyticConstants.SESSION_START_TIME, BibaDeviceSession.Start.ToString());
			_service.LogEvent(BibaAnalyticConstants.START_SESSION_EVENT, parameters);
        }

        public void TrackEndSession ()
        {
			var parameters = TrackingParams;
			parameters.Add (BibaAnalyticConstants.SESSION_END_TIME, DateTime.UtcNow.ToString());
			_service.LogEvent(BibaAnalyticConstants.END_SESSION_EVENT, parameters);
        }

		public void TrackStartRound ()
    	{
			var parameters = TrackingParams;
			parameters.Add (BibaAnalyticConstants.START_ROUND_EVENT, DateTime.UtcNow.ToString());
			_service.LogEvent(BibaAnalyticConstants.START_ROUND_EVENT, parameters);
    	}

    	public void TrackEndRound ()
		{
			var parameters = TrackingParams;
			parameters.Add (BibaAnalyticConstants.END_ROUND_EVENT, DateTime.UtcNow.ToString());
			_service.LogEvent(BibaAnalyticConstants.END_ROUND_EVENT, parameters);
    	}

    	public void TrackActivities ()
    	{
			var lScore = 0.0;
			var mScore = 0.0;
			var vScore = 0.0;

			foreach(var profile in BibaAccount.BibaProfiles)
			{
				lScore += profile.BibaProfileSession.LMVSessionDict[LMVScoreType.light_score].SessionScore;
				mScore += profile.BibaProfileSession.LMVSessionDict[LMVScoreType.moderate_score].SessionScore;
				vScore += profile.BibaProfileSession.LMVSessionDict[LMVScoreType.vigorous_score].SessionScore;
			}

			var parameters = TrackingParams;

			if (lScore > 0 || mScore > 0 || vScore > 0) 
			{
				parameters.Add (BibaAnalyticConstants.LIGHT_TIME, lScore.ToString ());
				parameters.Add (BibaAnalyticConstants.MODERATE_TIME, mScore.ToString ());
				parameters.Add (BibaAnalyticConstants.VIGOROUS_TIME, vScore.ToString ());
			}

			_service.LogEvent(BibaAnalyticConstants.ACTIVITIES_EVENT, parameters);
    	}

		public void TrackEquipmentSelected (BibaEquipmentType equipmentType)
		{
			var parameters = TrackingParams;
			parameters.Add (BibaAnalyticConstants.EQUIPMENT_SELECTED, equipmentType.ToString ());
			_service.LogEvent (BibaAnalyticConstants.EQUIPMENT_SELECTED, parameters);
		}

		public void TrackEquipmentPlayed (BibaEquipmentType equipmentType)
		{
			var parameters = TrackingParams;
			parameters.Add (BibaAnalyticConstants.EQUIPMENT_PLAYED, equipmentType.ToString ());
			_service.LogEvent (BibaAnalyticConstants.EQUIPMENT_PLAYED, parameters);
    	}

    	public void TrackTagEnabled ()
    	{
			var parameters = TrackingParams;
			parameters.Add (BibaAnalyticConstants.TAG_ENABLED, BibaDeviceSession.TagEnabled.ToString());

			_service.LogEvent (BibaAnalyticConstants.TAG_ENABLED, parameters);
    	}

    	public void TrackTagScanned ()
    	{
			var parameters = TrackingParams;
			parameters.Add (BibaAnalyticConstants.TAG_SCANNED, BibaDeviceSession.TagScanned.ToString());

			_service.LogEvent (BibaAnalyticConstants.TAG_SCANNED, parameters);
    	}
		
        public void TrackWeatherInfo(BibaWeatherInfo weatherInfo)
        {
            var parameters = TrackingParams;
            
            parameters.Add(BibaAnalyticConstants.WEATHER_TEMPERATURE, weatherInfo.Temperature.ToString("F2"));
            parameters.Add(BibaAnalyticConstants.WEATHER_DESCRIPTION, weatherInfo.WeatherDescription);
            parameters.Add(BibaAnalyticConstants.WEATHER_WIND_SPEED, weatherInfo.WindSpeed.ToString("F2"));

            _service.LogEvent(BibaAnalyticConstants.WEATHER_EVENT, parameters);
		}

		~ FlurryDeviceAnalyticService()
		{
			TrackEndSession ();
		}
		#endregion
			
        Dictionary<string, string> TrackingParams {
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