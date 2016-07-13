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

		public void StartTracking (string iosKey, string androidKey)
    	{
			_service = Flurry.Instance;
			_service.SetLogLevel(LogLevel.All);
			_service.StartSession(iosKey, androidKey);

			TrackStartSession();
    	}

        ~FlurryAnalyticService()
        {
            TrackEndSession();
        }

        public void TrackStartSession ()
        {
            _service.LogEvent(BibaAnalyticConstants.START_SESSION_EVENT, TrackingParams);
        }

        public void TrackEndSession ()
        {
            var parameters = TrackingParams;
            parameters.Add(BibaAnalyticConstants.END_GAME_LAST_MENU_SHOWN, BibaSceneStack.Count > 0 ? BibaSceneStack.Peek().ToString() : "GameScene.Null");

			foreach (var equipment in BibaAccount.SelectedProfile.PlayedEquipments)
            {
                parameters.Add(string.Format("{0}{1}", equipment.EquipmentType.ToString(), BibaAnalyticConstants.EQUIPMENT_PLAYED), equipment.NumberOfTimeSelected.ToString());
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

        public void TrackEquipmentSelected (BibaEquipmentType equipmentType)
        {
            var parameters = TrackingParams;
            parameters.Add(BibaAnalyticConstants.EQUIPMENT_SELECTED_TYPE, equipmentType.ToString());

            _service.LogEvent(BibaAnalyticConstants.EQUIPMENT_SELECTED_EVENT, parameters);
        }

        public void TrackEquipmentPlayed(BibaEquipmentType equipmentType)
        {
            var parameters = TrackingParams;
            parameters.Add(BibaAnalyticConstants.EQUIPMENT_PLAYED_TYPE, equipmentType.ToString());
            
            _service.LogEvent(BibaAnalyticConstants.EQUIPMENT_PLAYED_EVENT, parameters);
        }

		public void TrackTagEnabled (bool enabled)
        {
            var parameters = TrackingParams;
            parameters.Add(BibaAnalyticConstants.TAG_ENABLED, enabled.ToString());
            
            _service.LogEvent(BibaAnalyticConstants.TAG_ENABLED_EVENT, parameters);;
        }
       
		public void TrackTagScanned (BibaTagType tagType)
        {
            var parameters = TrackingParams;
            parameters.Add(BibaAnalyticConstants.TAG_SCANNED_TYPE, tagType.ToString());

            _service.LogEvent(BibaAnalyticConstants.TAG_SCANNED_EVENT, parameters);
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

