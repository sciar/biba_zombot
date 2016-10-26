using System;
using System.Collections.Generic;

namespace BibaFramework.BibaAnalytic
{
    public class BibaWeatherInfo
    {
        public DateTime TimeStamp { get; set; }
        public float Temperature { get; set; }
        public string WeatherDescription { get; set; }
        public float WindSpeed { get; set; }

        public override string ToString ()
        {
            return string.Format ("[BibaWeatherInfo: TimeStamp={0}, Temperature={1}, WeatherDescription={2}, WindSpeed={3}]", TimeStamp, Temperature, WeatherDescription, WindSpeed);
        }
    }

	[Serializable]
	public class Coord
	{
		public double lon;
		public double lat;
	}

	[Serializable]
	public class Weather
	{
		public string description;
	}

	[Serializable]
	public class Main
	{
		public double temp;
		public double pressure;
		public int humidity;
		public double temp_min;
		public double temp_max;
	}

	[Serializable]
	public class Wind
	{
		public double speed;
		public double deg;
	}

	[Serializable]
	public class BibaWeatherResponse
	{
		public Coord coord;
		public Weather[] weather;
		public string @base;
		public Main main;
		public Wind wind;
		public string name;
	}
}