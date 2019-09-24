using System;
namespace Assistant.Features.Greeting.Models
{
    public interface IGreetingRequest
    {
        string Zip { get; }
        string Name { get; }
        bool OpeningMsg { get; }
        bool CurrentDate { get; }
        bool Weather { get; }
        bool CurrentSurf { get; }
        bool WaterTemp { get; }
        bool NewsHeadlines { get; }
    }

    public class GreetingRequest : IGreetingRequest
    {
        public string Zip { get; set; } = "92117";
        public string Name { get; set; } = string.Empty;
        public bool OpeningMsg { get; set; } = true;
        public bool CurrentDate { get; set; } = true;
        public bool Weather { get; set; } = true;
        public bool CurrentSurf { get; set; } = true;
        public bool WaterTemp { get; set; } = true;
        public bool NewsHeadlines { get; set; } = true;
    }
}
