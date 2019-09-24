using System;
using Assistant.Features.Surf;

namespace Assistant.Features.Greeting.Models
{
    public interface IGreetingRequest
    {
        string Zip { get; }
        string Name { get; }
        bool Greeting { get; }
        bool Date { get; }
        bool Weather { get; }
        SurfSpot? Surf { get; }
        bool Water { get; }
        bool News { get; }
    }

    public class GreetingRequest : IGreetingRequest
    {
        public string Zip { get; set; } = "92117";
        public string Name { get; set; } = string.Empty;
        public bool Greeting { get; set; } = true;
        public bool Date { get; set; }
        public bool Weather { get; set; }
        public SurfSpot? Surf { get; set; }
        public bool Water { get; set; }
        public bool News { get; set; }
    }
}
