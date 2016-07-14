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
		public BibaSession BibaSession { get; set; }

		[Inject]
		public BibaAccount BibaAccount { get; set; }

		[Inject]
		public BibaSystem BibaSystem { get; set; }

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
			var parameters = TrackingParams;
			parameters.Add (BibaAnalyticConstants.SESSION_START_TIME, BibaSession.Start.ToString());
			_service.LogEvent(BibaAnalyticConstants.START_SESSION_EVENT, parameters);
        }

        public void TrackEndSession ()
        {
            var parameters = TrackingParams;
			parameters.Add (BibaAnalyticConstants.SESSION_END_TIME, BibaSession.End.ToString());
			parameters.Add (BibaAnalyticConstants.TAG_ENABLED, BibaSession.TagEnabled.ToString());
			parameters.Add (BibaAnalyticConstants.TAG_SCANNED, BibaSession.TagScanned.ToString());

			foreach (var profile in BibaAccount.BibaProfiles) 
			{
				parameters.Add(BibaAnalyticConstants.PROFILE_ID, profile.Id);
				if (BibaAccount.SelectedProfile.LScore > 0 ||
					BibaAccount.SelectedProfile.MScore > 0 ||
					BibaAccount.SelectedProfile.VScore > 0) 
				{
					parameters.Add (BibaAnalyticConstants.LIGHT_TIME + "_" + profile.Id, BibaAccount.SelectedProfile.LScore.ToString ());
					parameters.Add (BibaAnalyticConstants.MODERATE_TIME + "_" + profile.Id, BibaAccount.SelectedProfile.MScore.ToString ());
					parameters.Add (BibaAnalyticConstants.VIGOROUS_TIME + "_" + profile.Id, BibaAccount.SelectedProfile.VScore.ToString ());
				}
			}

			foreach (var equipment in BibaSession.SelectedEquipments) 
			{
				parameters.Add(BibaAnalyticConstants.EQUIPMENT_SELECTED, equipment.EquipmentType.ToString());
			}

			foreach (var equipment in BibaAccount.SelectedProfile.PlayedEquipments)
            {
				parameters.Add(string.Format("{0}_{1}", BibaAnalyticConstants.EQUIPMENT_PLAYED, equipment.EquipmentType.ToString()), equipment.NumberOfTimeSelected.ToString());
            }

            _service.LogEvent(BibaAnalyticConstants.END_SESSION_EVENT, parameters);
        }

		public void TrackActivites()
		{
			if (BibaAccount.SelectedProfile.LScore > 0 ||
				BibaAccount.SelectedProfile.MScore > 0 ||
				BibaAccount.SelectedProfile.VScore > 0) 
			{
				var parameters = TrackingParams;
				parameters.Add (BibaAnalyticConstants.LIGHT_TIME, BibaAccount.SelectedProfile.LScore.ToString ());
				parameters.Add (BibaAnalyticConstants.MODERATE_TIME, BibaAccount.SelectedProfile.MScore.ToString ());
				parameters.Add (BibaAnalyticConstants.VIGOROUS_TIME, BibaAccount.SelectedProfile.VScore.ToString ());

				_service.LogEvent (BibaAnalyticConstants.ACTIVITIES_EVENT, parameters);
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
					
				if(BibaSession != null && !string.IsNullOrEmpty(BibaSession.QuadTileId))
                {
					param.Add(BibaAnalyticConstants.QUADTILE_ID, BibaSession.QuadTileId);
                }

				return param;
            }
        }
    }
}