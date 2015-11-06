using System.Collections;
using UnityEngine;
using BibaFramework.BibaAnalytic;
using BibaFramework.Utility;
using LitJson;
using strange.extensions.command.impl;
using System;

namespace BibaFramework.BibaGame
{
    public class LogLocationInfoCommand : Command
    {
        private const string WEATHER_API_CALL_FORMATTED = "http://api.openweathermap.org/data/2.5/weather?lat={0}&lon={1}&units=imperial&appid=" + BibaAnalyticConstants.WEATHER_API_KEY;
        
        private const string WEATHER = "weather";
        private const string MAIN = "main";
        private const string TEMP = "temp";
        private const string WIND = "wind";
        private const string SPEED = "speed";

        [Inject]
        public IAnalyticService BibaAnalyticService { get; set; }

        [Inject]
        public BibaSessionModel BibaSessionModel { get; set; } 

        public override void Execute ()
        { 
            Debug.Log(LatLongToQuadKey(-123, 49, 11, startRect));
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
                SotreSessionLatLong();

                var locInfo = Input.location.lastData;
                var www = new WWW(string.Format(WEATHER_API_CALL_FORMATTED, locInfo.latitude, locInfo.longitude));
                
                yield return www;

                var weatherInfo = ProcessWeatherJSON(www.text);
                BibaAnalyticService.TrackWeatherInfo(weatherInfo);                
            }

            // Stop service if there is no need to query location updates continuously
            Input.location.Stop();
        }

        void SotreSessionLatLong()
        {
            BibaSessionModel.QuadTileId = LatLongToQuadKey(Input.location.lastData.longitude, Input.location.lastData.latitude, BibaAnalyticConstants.MAP_LEVEL_OF_DETAIL, startRect);//TileSystem.LatLongToQuadKey((double)Input.location.lastData.latitude, (double)Input.location.lastData.longitude, BibaAnalyticConstants.MAP_LEVEL_OF_DETAIL);
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

        private readonly Rect startRect = new Rect(-180f,90f,360f,180f);
        
        private string LatLongToQuadKey(float lon, float lat, int level, Rect currentRect, string currentString="")
        {
            Vector2 location = new Vector2 (lon,lat);
            
            if(level > 0)
            {
                Debug.LogWarning(string.Format("Long: {0}, Lat: {1}, Current String: {2}, Current Rect: X-{3}, Y-{4}, Width-{5}, Height-{6}", lon, lat, currentString, currentRect.x, currentRect.y, currentRect.width, currentRect.height));
                
                level--;
                
                //rect to right down
                var halfOfWidth = currentRect.width / 2;
                var halfOfHeight = currentRect.height / 2;

                Rect rect1=new Rect(currentRect.position.x, currentRect.position.y,  halfOfWidth, halfOfHeight);
                Rect rect2=new Rect(currentRect.position.x + halfOfWidth, currentRect.position.y, halfOfWidth, halfOfHeight);
                Rect rect3=new Rect(currentRect.position.x,  currentRect.position.y - halfOfHeight, halfOfWidth, halfOfHeight);
                Rect rect4=new Rect(currentRect.position.x + halfOfWidth, currentRect.position.y - halfOfHeight, halfOfWidth, halfOfHeight);
                
                if(rect1.Contains(location))
                {
                    currentRect=rect1;
                    currentString+="00";
                    return LatLongToQuadKey(lon,lat,level, currentRect, currentString);
                }
                else if(rect2.Contains(location))
                {
                    currentRect=rect2;
                    currentString+="01";
                    return LatLongToQuadKey(lon,lat,level, currentRect, currentString);
                }
                else if(rect3.Contains(location))
                {
                    currentRect=rect3; 
                    currentString+="10";
                    return LatLongToQuadKey(lon,lat,level, currentRect, currentString);
                }
                else if(rect4.Contains(location))
                {
                    currentRect=rect4;
                    currentString+="11";
                    return LatLongToQuadKey(lon,lat,level, currentRect, currentString);
                }
                else
                {
                    return "error, couldn't find the coord in my rects";
                }
            }
            else
            {
                return currentString;
            }
        }

    }
}