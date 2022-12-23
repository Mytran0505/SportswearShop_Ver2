using System;

namespace SportswearShop_Ver2.Models
{
    public class Statistic
    {
        //private int statisticId;
        private DateTime statisticDate;
        private int profit;

        public Statistic()
        {
        }

        public Statistic(/*int statisticId, */DateTime statisticDate, int profit)
        {
            //this.statisticId = statisticId;
            this.statisticDate = statisticDate;
            this.profit = profit;
        }

        //public int StatisticId { get => statisticId; set => statisticId = value; }
        public DateTime StatisticDate { get => statisticDate; set => statisticDate = value; }
        public int Profit { get => profit; set => profit = value; }
    }
}
