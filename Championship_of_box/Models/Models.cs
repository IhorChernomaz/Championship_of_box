using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Championship_of_box.Models
{
    public class RatingViewModel
    {
        public int? CurrentRanking { get; set; }
        public string Boxer { get; set; }
        public int? AmountOfBattles { get; set; }
    }
    public class BattleViewModel
    {
        public int? Id { get; set; }
        public DateTime? Date { get; set; }
        public int? AmountOfRounds { get; set; }
        public string Boxer1 { get; set; }
        public string Boxer2 { get; set; }
        public int? RefereePoints { get; set; }
    }
}