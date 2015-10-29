using System;
using System.Collections.Generic;
using Analytics;
using BibaFramework.BibaGame;
using BibaFramework.BibaMenu;

namespace BibaFramework.BibaAnalytic
{
    public class FlurryAnalyticService : IBibaAnalyticService
    {
        [Inject]
        public BibaGameModel BibaGameModel { get; set; }

        [Inject]
        public BibaSceneStack BibaSceneStack { get; set; }

        [Inject]
        public BibaWeatherService BibaWeatherService { get; set; }

        private Flurry _service;

        public FlurryAnalyticService(string iosKey, string androidKey)
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

            foreach (var equipment in BibaGameModel.TotalPlayedEquipments)
            {
                parameters.Add(string.Format("{0}{1}", equipment.EquipmentType.ToString(), BibaAnalyticConstants.EQUIPMENT_PLAYED), equipment.TimeSelected.ToString());
            }

            _service.LogEvent(BibaAnalyticConstants.END_SESSION_EVENT, parameters);
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

        public void TrackSatelliteTagEnabled (bool enabled)
        {
            var parameters = TrackingParams;
            parameters.Add(BibaAnalyticConstants.TAG_ENABLED, enabled.ToString());
            
            _service.LogEvent(BibaAnalyticConstants.TAG_ENABLED_EVENT, parameters);;
        }
       
        public void TrackSatelliteTagScanned (BibaTagType tagType)
        {
            var parameters = TrackingParams;
            parameters.Add(BibaAnalyticConstants.TAG_SCANNED_TYPE, tagType.ToString());

            _service.LogEvent(BibaAnalyticConstants.TAG_SCANNED_EVENT, parameters);
        }

        public void TrackWeatherInfo()
        {
            var parameters = TrackingParams;
            
            parameters.Add(BibaAnalyticConstants.WEATHER_TEMPERATURE, BibaWeatherService.WeatherInfo.Temperature.ToString("F2"));
            parameters.Add(BibaAnalyticConstants.WEATHER_DESCRIPTION, BibaWeatherService.WeatherInfo.WeatherDescription);
            parameters.Add(BibaAnalyticConstants.WEATHER_WIND_SPEED, BibaWeatherService.WeatherInfo.WindSpeed.ToString("F2"));

            _service.LogEvent(BibaAnalyticConstants.WEATHER_EVENT, parameters);
        }

        public Dictionary<string, string> TrackingParams {
            get {
                return new Dictionary<string, string>() { 
                    {BibaAnalyticConstants.TIME_STAMP, DateTime.Now.ToString()}
                };
            }
        }
    }
}

