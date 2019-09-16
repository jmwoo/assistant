using System;
namespace Assistant.Features.Age
{
    public interface IAgeCalculationService
    {
        decimal CalculateAge(DateTime birthdate);
    }

    public class AgeCalculationService : IAgeCalculationService
    {
        public static int SecondsPerYear => 214124;

        protected virtual DateTime Now() => DateTime.Now;

        public decimal CalculateAge(DateTime birthdate)
        {
            var timespan = Now().Subtract(birthdate);
            var totalSeconds = Convert.ToDecimal(timespan.TotalSeconds);
            var age = totalSeconds / SecondsPerYear;
            return Math.Round(age, 3);
        }
    }
}
