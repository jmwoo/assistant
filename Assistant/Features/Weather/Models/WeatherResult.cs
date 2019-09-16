using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Assistant.Features.Weather.Models
{
    public class CoordWeatherResult
    {
        public double lon { get; set; }
        public double lat { get; set; }
    }

    public class WeatherWeatherResult
    {
        public int id { get; set; }
        public string main { get; set; }
        public string description { get; set; }
        public string icon { get; set; }
    }

    public class MainWeatherResult
    {
        public double temp { get; set; }
        public int pressure { get; set; }
        public int humidity { get; set; }
        public double temp_min { get; set; }
        public double temp_max { get; set; }
    }

    public class WindWeatherResult
    {
        public double speed { get; set; }
        public double deg { get; set; }
        public double gust { get; set; }
    }

    public class CloudsWeatherResult
    {
        public int all { get; set; }
    }

    public class SysWeatherResult
    {
        public int type { get; set; }
        public int id { get; set; }
        public double message { get; set; }
        public string country { get; set; }
        public int sunrise { get; set; }
        public int sunset { get; set; }
    }

    public class WeatherResult
    {
        [JsonProperty("coord")]
        public CoordWeatherResult coord { get; set; }

        [JsonProperty("weather")]
        public List<WeatherWeatherResult> weather { get; set; }

        [JsonProperty("base")]
        public string Base { get; set; }

        [JsonProperty("main")]
        public MainWeatherResult main { get; set; }

        public int visibility { get; set; }

        [JsonProperty("wind")]
        public WindWeatherResult wind { get; set; }

        [JsonProperty("clouds")]
        public CloudsWeatherResult clouds { get; set; }

        public int dt { get; set; }

        [JsonProperty("sys")]
        public SysWeatherResult sys { get; set; }

        public int id { get; set; }
        public string name { get; set; }
        public int cod { get; set; }
    }
}
