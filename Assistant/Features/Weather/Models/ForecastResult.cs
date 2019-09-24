using System;
using System.Collections.Generic;
using Jmwoo.Common.Date;
using Assistant.Features.ReadableDateTime.Models;
using Newtonsoft.Json;

namespace Assistant.Features.Weather.Models
{
    // public interface IDt
    // {
    //     int Dt { get; }
    // }

    public abstract class BaseDt
    {
        public abstract int Dt { get; set; }
        public DateTime DateTimeUtc => ((long)Dt).FromUnixTime();
        public DateTime DateTimePst => DateTimeUtc.UtcToPst();
    }

  public class MainForecast
    {
        public double Temp { get; set; }

        [JsonProperty("temp_min")]
        public double TempMin { get; set; }

        [JsonProperty("temp_max")]
        public double TempMax { get; set; }

        public double Pressure { get; set; }

        [JsonProperty("sea_level")]
        public double SeaLevel { get; set; }

        [JsonProperty("grnd_level")]
        public double GroundLevel { get; set; }

        public int Humidity { get; set; }

        [JsonProperty("temp_kf")]
        public double TempKf { get; set; }
    }

    public class WeatherForecast
    {
        public int Id { get; set; }
        public string Main { get; set; }
        public string Description { get; set; }
        public string Icon { get; set; }
    }

    public class CloudsForecast
    {
        public int All { get; set; }
    }

    public class WindForecast
    {
        public double Speed { get; set; }
        public double Deg { get; set; }
    }

    public class SysForecast
    {
        public string Pod { get; set; }
    }

    public class ForecastList : BaseDt
    {
        public override int Dt { get; set; }

        public MainForecast Main { get; set; }

        [JsonProperty("weather")]
        public List<WeatherForecast> Weather { get; set; }

        [JsonProperty("clouds")]
        public CloudsForecast Clouds { get; set; }

        [JsonProperty("wind")]
        public WindForecast Wind { get; set; }

        [JsonProperty("sys")]
        public SysForecast Sys { get; set; }

        [JsonProperty("dt_txt")]
        public string DtTxt { get; set; }
  }

    public class CoordForecast
    {
        public double Lat { get; set; }
        public double Lon { get; set; }
    }

    public class City
    {
        public string Name { get; set; }
        public CoordForecast Coord { get; set; }
        public string Country { get; set; }
        public int Timezone { get; set; }
        public int Sunrise { get; set; }
        public int Sunset { get; set; }
    }

    public class ForecastResult
    {
        public string Cod { get; set; }
        public double Message { get; set; }
        public int Cnt { get; set; }

        [JsonProperty("List")]
        public List<ForecastList> List { get; set; }

        public City City { get; set; }
    }
}
