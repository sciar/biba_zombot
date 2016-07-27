using System;
using System.Collections;
using UnityEngine;
using BibaFramework.BibaAnalytic;
using BibaFramework.Utility;
using LitJson;
using strange.extensions.command.impl;
using BestHTTP;

namespace BibaFramework.BibaGame
{
    public class LogLocationInfoCommand : Command
    {
        private readonly Rect MERCATOR_RECT = new Rect(-180f,-90f,360f,180f);
        private const string WEATHER_API_CALL_FORMATTED = "http://api.openweathermap.org/data/2.5/weather?lat={0}&lon={1}&units=imperial&appid=" + BibaAnalyticConstants.WEATHER_API_KEY;
		private const int MAP_LEVEL_OF_DETAIL = 11;

        private const string WEATHER = "weather";
        private const string MAIN = "main";
        private const string TEMP = "temp";
        private const string WIND = "wind";
        private const string SPEED = "speed";

        [Inject]
        public IAnalyticService BibaAnalyticService { get; set; }

        [Inject]
		public BibaDevice BibaDevice { get; set; } 

        [Inject]
		public BibaDeviceSession BibaSession { get; set; }

        //This call also prompt the enable GPS request for the frist time.
        public override void Execute ()
        { 
            #if !UNITY_EDITOR
			if(BibaDevice.PrivacyEnabled)
            {
                Retain();
                new Task(RetrieveLocationInfo(), true);
            }
            #endif  
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
                StoreSessionLatLong();

                var locInfo = Input.location.lastData;
                var weatherAPIUrl = string.Format(WEATHER_API_CALL_FORMATTED, locInfo.latitude, locInfo.longitude);

                var httpRequest = new HTTPRequest(new Uri(weatherAPIUrl), HTTPMethods.Get, false, RequestCompleted);
                httpRequest.Send();
            }

            // Stop service if there is no need to query location updates continuously
            Input.location.Stop();
        }

        void RequestCompleted(HTTPRequest request, HTTPResponse response)
        {
            if (response != null)
            {
                Debug.Log(response.DataAsText);
                var weatherInfo = ProcessWeatherJSON(response.DataAsText);
                BibaAnalyticService.TrackWeatherInfo(weatherInfo);  
            }

            Release();
        }

        void StoreSessionLatLong()
        {
			BibaSession.QuadTileId = LatLongToQuadKey(Input.location.lastData.longitude, Input.location.lastData.latitude, MAP_LEVEL_OF_DETAIL);
			BibaSession.Location = new Vector2(Input.location.lastData.latitude, Input.location.lastData.longitude);
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
        
        string LatLongToQuadKey(float lon, float lat, int level = MAP_LEVEL_OF_DETAIL)
        {
            Vector2 location = new Vector2 (lon,lat);

            var resultStr = "";
            var currentRect = MERCATOR_RECT;

            for (int i = level; i > 0; i--)
            {
                //Debug.LogWarning(string.Format("Long:{0}, Lat:{1}, Current String:{2}, Current Rect: xMin:{3}, yMin:{4}, xMax:{5}, yMax:{6}", location.x, location.y, resultStr, currentRect.xMin, currentRect.yMin, currentRect.xMax, currentRect.yMax));

                //Unity's rect xMin, yMin is actually the bottom-left in vector2 coordinate
                var halfOfWidth = currentRect.width / 2;
                var halfOfHeight = currentRect.height / 2;
                
                var rect1=new Rect(currentRect.xMin, currentRect.yMin + halfOfHeight,  halfOfWidth, halfOfHeight);
                var rect2=new Rect(currentRect.xMin + halfOfWidth, currentRect.yMin + halfOfHeight, halfOfWidth, halfOfHeight);
                var rect3=new Rect(currentRect.xMin,  currentRect.yMin, halfOfWidth, halfOfHeight);
                var rect4=new Rect(currentRect.xMin + halfOfWidth, currentRect.yMin, halfOfWidth, halfOfHeight);

                if(rect1.Contains(location))
                {
                    currentRect=rect1;
                    resultStr+="00";
                }
                else if(rect2.Contains(location))
                {
                    currentRect=rect2;
                    resultStr+="01";
                }
                else if(rect3.Contains(location))
                {
                    currentRect=rect3; 
                    resultStr+="10";
                }
                else if(rect4.Contains(location))
                {
                    currentRect=rect4;
                    resultStr+="11";
                }
                else
                {
                    return string.Empty;
                }
            }
            return Convert.ToInt32(resultStr, 2).ToString();
        }   
    }
}