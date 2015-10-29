using System.Collections.Generic;
using BibaFramework.BibaGame;
using System.Collections;
using UnityEngine;
using BibaFramework.Utility;
using LitJson;
using System;

namespace BibaFramework.BibaAnalytic
{
    public class BibaWeatherService
    {
        private const string WEATHER_API_CALL_FORMATTED = "http://api.openweathermap.org/data/2.5/weather?lat={0}&lon={1}&units=imperial&appid=" + BibaAnalyticConstants.WEATHER_API_KEY;

        private const string WEATHER = "weather";
        private const string MAIN = "main";
        private const string TEMP = "temp";
        private const string WIND = "wind";
        private const string SPEED = "speed";

        private BibaWeatherInfo _weatherInfo;
        public BibaWeatherInfo WeatherInfo { get { return _weatherInfo; } }

        public void RetrieveWeatherInfo()
        {
            new Task(RetrieveWeatherAsync(), true);
        }

        IEnumerator RetrieveWeatherAsync()
        {
            var locInfo = Input.location.lastData;
            var www = new WWW(string.Format(WEATHER_API_CALL_FORMATTED, locInfo.latitude, locInfo.longitude));
          
            yield return www;
          
            ProcessResponseJSON(www.text);
        }

        void ProcessResponseJSON(string text)
        {
            var jsonData = JsonMapper.ToObject(text);

            _weatherInfo = new BibaWeatherInfo();
            _weatherInfo.TimeStamp = DateTime.UtcNow;
            _weatherInfo.Temperature = (float) jsonData[MAIN][TEMP];
            _weatherInfo.WeatherDescription = jsonData[WEATHER][MAIN].ToString();
            _weatherInfo.WindSpeed = (float) jsonData[WIND][SPEED];

            Debug.Log(_weatherInfo);
        }
    }
}