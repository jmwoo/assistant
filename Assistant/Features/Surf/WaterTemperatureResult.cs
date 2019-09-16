namespace Assistant.Features.Surf
{
    public class WaterTemperatureResult
    {
        public int buoy_id { get; set; }
        public double celcius { get; set; }
        public string county { get; set; }
        public int fahrenheit { get; set; }
        public string recorded { get; set; }
        public string wetsuit { get; set; }
    }
}