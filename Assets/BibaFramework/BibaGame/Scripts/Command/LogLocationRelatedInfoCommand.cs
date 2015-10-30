using System.Collections;
using UnityEngine;
using BibaFramework.BibaAnalytic;
using BibaFramework.Utility;
using LitJson;
using strange.extensions.command.impl;
using System;

namespace BibaFramework.BibaGame
{
    public class LogLocationRelatedInfoCommand : Command
    {
        private const string WEATHER_API_CALL_FORMATTED = "http://api.openweathermap.org/data/2.5/weather?lat={0}&lon={1}&units=imperial&appid=" + BibaAnalyticConstants.WEATHER_API_KEY;
        
        private const string WEATHER = "weather";
        private const string MAIN = "main";
        private const string TEMP = "temp";
        private const string WIND = "wind";
        private const string SPEED = "speed";

        [Inject]
        public IBibaAnalyticService BibaAnalyticService { get; set; }

        public override void Execute ()
        { 
            new Task(RetrieveLocationInfo(), true);
        }

        IEnumerator RetrieveLocationInfo()
        {
            // First, check if user has location service enabled
            if (!Input.location.isEnabledByUser)
                yield break;
            
            // Start service before querying location
            Input.location.Start();
            
            // Wait until service initializes
            int maxWait = 20;
            while (Input.location.status == LocationServiceStatus.Initializing && maxWait > 0)
            {
                yield return new WaitForSeconds(1);
                maxWait--;
            }
            
            // Service didn't initialize in 20 seconds
            if (maxWait < 1)
            {
                Debug.Log("Timed out");
                yield break;
            }
            
            // Connection has failed
            if (Input.location.status == LocationServiceStatus.Failed)
            {
                Debug.Log("Unable to determine device location");
                yield break;
            }
            else
            {
                // Access granted and location value could be retrieved
                Debug.Log("Location: " + Input.location.lastData.latitude + " " + Input.location.lastData.longitude + " " + Input.location.lastData.altitude + " " + Input.location.lastData.horizontalAccuracy + " " + Input.location.lastData.timestamp);
              
                var locInfo = Input.location.lastData;
                var www = new WWW(string.Format(WEATHER_API_CALL_FORMATTED, locInfo.latitude, locInfo.longitude));
                
                yield return www;

                var weatherInfo = ProcessWeatherJSON(www.text);
                BibaAnalyticService.TrackWeatherInfo(weatherInfo);                
            }

            // Stop service if there is no need to query location updates continuously
            Input.location.Stop();
        }

        BibaWeatherInfo ProcessWeatherJSON(string text)
        {
            JsonData jsonData = JsonMapper.ToObject(text);
            
            var weatherInfo = new BibaWeatherInfo();
            weatherInfo.TimeStamp = DateTime.UtcNow;
            weatherInfo.Temperature = (float) jsonData[MAIN][TEMP];
            weatherInfo.WeatherDescription = jsonData[WEATHER][0][MAIN].ToString();
            weatherInfo.WindSpeed = (float) jsonData[WIND][SPEED];

            return weatherInfo;
        }
    }
}