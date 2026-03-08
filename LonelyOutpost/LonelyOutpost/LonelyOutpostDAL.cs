using System;
using System.Collections.Generic;
using System.Linq;

namespace lonelyOutpost
{
    public class LonelyOutpostDAL
    {
        readonly DateTime MY_BIRTHDAY = new DateTime(1972, 02, 29);

        LonelyOutpostEntities ctx;

        public void AddWeight(float weight, TimeSpan time)
        {
            if (ctx == null) ctx = new LonelyOutpostEntities();

            int day = GetDaysAlive();

            Statistic statistic = new Statistic()
            {
                Day = day,
                Weight = weight,
                TimeMeasured = time
            };

            ctx.Statistics.AddObject(statistic);
            ctx.SaveChanges();
        }

        public int GetDaysAlive()
        {
            TimeSpan day = (DateTime.Today - MY_BIRTHDAY);

            return day.Days;
        }

        public List<Statistic> GetStats()
        {
            if (ctx == null) ctx = new LonelyOutpostEntities();

            return ctx.Statistics.ToList();
        }

        public DateTime GetDayFromDaysAlive(int daysAlive)
        {
            return MY_BIRTHDAY + TimeSpan.FromDays(daysAlive);
        }

        public int GetNumberOfUnsortedWords()
        {
            if (ctx == null) ctx = new LonelyOutpostEntities();

            return ctx.NewWords.Count();
        }
    }
}