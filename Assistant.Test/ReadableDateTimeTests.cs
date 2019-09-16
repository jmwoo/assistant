using System;
using Xunit;
using Assistant.Features.ReadableDateTime.Services;
using Assistant.Features.ReadableDateTime.Models;
using System.Collections.Generic;

namespace Assistant.test
{
    public class ReadableDateTimeTests
    {
        readonly IReadableDateTimeService _service;

        public ReadableDateTimeTests()
        {
            _service = new ReadableDateTimeService();
        }

        [Theory]
        [InlineData("11/29/2019 11:59:59 PM", "11:59 PM|Friday|Twenty Ninth|November|Evening")]
        [InlineData("3/11/2020 07:01:00 AM", "7:01 AM|Wednesday|Eleventh|March|Morning")]
        [InlineData("2/25/2020 00:00:00 AM", "12:00 AM|Tuesday|Twenty Fifth|February|Morning")]
        [InlineData("6/4/2020 02:47:23 PM", "2:47 PM|Thursday|Fourth|June|Afternoon")]
        public void HasCorrectValues(string dtStr, string expectedStr)
        {
            var dt = DateTime.Parse(dtStr);
            var expected = this.Parse(expectedStr);
            var actual = _service.Get(dt);

            Assert.Equal(expected.Time, actual.Time);
            Assert.Equal(expected.DayOfWeek, actual.DayOfWeek);
            Assert.Equal(expected.DayOfMonthEnglish, actual.DayOfMonthEnglish);
            Assert.Equal(expected.Month, actual.Month);
            Assert.Equal(expected.PartOfDay, actual.PartOfDay);
        }

        private ReadableDt Parse(string s)
        {
            var split = s.Split("|");
            return new ReadableDt
            {
                Time = split[0],
                DayOfWeek = split[1],
                DayOfMonthEnglish = split[2],
                Month = split[3],
                PartOfDay = split[4],
            };
        }
    }
}
