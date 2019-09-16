using System;
namespace Assistant.Features.Greeting.Models
{
    public interface IGetGreetingRequest
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

    public class GetGreetingRequest : IGetGreetingRequest
    {
        public string Zip { get; set; }
        public string Name { get; set; }
        public bool OpeningMsg { get; set; }
        public bool CurrentDate { get; set; }
        public bool Weather { get; set; }
        public bool CurrentSurf { get; set; }
        public bool WaterTemp { get; set; }
        public bool NewsHeadlines { get; set; }
    }
}
