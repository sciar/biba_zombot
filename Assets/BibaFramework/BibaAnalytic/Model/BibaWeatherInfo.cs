using System;

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
}