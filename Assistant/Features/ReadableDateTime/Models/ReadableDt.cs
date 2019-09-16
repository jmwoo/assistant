using System;
namespace Assistant.Features.ReadableDateTime.Models
{
    public class ReadableDt
    {
        public DateTime Dt { get; set; }
        public string Time { get; set; }
        public string DayOfWeek { get; set; }
        public string DayOfMonthEnglish { get; set; }
        public string Month { get; set; }
        public string PartOfDay { get; set; }
    }
}
