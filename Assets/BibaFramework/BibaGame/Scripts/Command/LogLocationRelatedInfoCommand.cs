using BibaFramework.BibaAnalytic;
using strange.extensions.command.impl;
using System.Collections;
using UnityEngine;
using BibaFramework.Utility;
using System;

namespace BibaFramework.BibaGame
{
    public class LogLocationRelatedInfoCommand : Command
    {
        [Inject]
        public BibaWeatherService BibaWeatherService { get; set; }

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
            }

            TrackLocationInfo();

            // Stop service if there is no need to query location updates continuously
            Input.location.Stop();
        }

        void TrackLocationInfo()
        {
            BibaWeatherService.RetrieveWeatherInfo();
            if (BibaWeatherService.WeatherInfo != null)
            {
                BibaAnalyticService.TrackWeatherInfo();
            }
        }
    }
}