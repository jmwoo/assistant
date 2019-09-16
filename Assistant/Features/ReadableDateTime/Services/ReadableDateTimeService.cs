using System;
using Assistant.Features.ReadableDateTime.Models;

namespace Assistant.Features.ReadableDateTime.Services
{
    public interface IReadableDateTimeService
    {
        ReadableDt Get(DateTime datetime);
    }

    public class ReadableDateTimeService : IReadableDateTimeService
    {
        public ReadableDateTimeService()
        {
        }

        public ReadableDt Get(DateTime datetime)
        {
            var partOfDay = "Evening";
            if (datetime.Hour < 12)
            {
                partOfDay = "Morning";
            }
            else if (datetime.Hour >= 12 && datetime.Hour < 17)
            {
                partOfDay = "Afternoon";
            }

            return new ReadableDt
            {
                Dt = datetime,
                Time = datetime.ToString("h:mm tt"),
                DayOfWeek = datetime.ToString("dddd"),
                DayOfMonthEnglish = DaysOfMonthEnglish[datetime.Day - 1],
                Month = datetime.ToString("MMMM"),
                PartOfDay = partOfDay
            };
        }

        private string[] DaysOfMonthEnglish => new[]
        {
           "First",
           "Second",
           "Third",
           "Fourth",
           "Fifth",
           "Sixth",
           "Seventh",
           "Eighth",
           "Ninth",
           "Tenth",
           "Eleventh",
           "Twelfth",
           "Thirteenth",
           "Fourteenth",
           "Fifteenth",
           "Sixteenth",
           "Seventeenth",
           "Eighteenth",
           "Nineteenth",
           "Twentieth",
           "Twenty First",
           "Twenty Second",
           "Twenty Third",
           "Twenty Fourth",
           "Twenty Fifth",
           "Twenty Sixth",
           "Twenty Seventh",
           "Twenty Eighth",
           "Twenty Ninth",
           "Thirtieth",
           "Thirty First"
        };
    }
}
