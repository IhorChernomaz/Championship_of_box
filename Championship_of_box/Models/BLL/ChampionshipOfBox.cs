using Championship_of_box.DataModel;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Runtime.InteropServices;
using System.Web;

namespace Championship_of_box.Models
{
    public class Boxer
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Rank { get; set; }

    }

/*    public class BattleBll
    {
        public void Save(BattleModel data)
        {
            using (ChampionshipOfBoxContext db = new ChampionshipOfBoxContext())
            {

                if(data.Id.HasValue)
                {
                    db.Entry(data).State = EntityState.Modified;
                    db.SaveChanges();
                }
                else 
                {
                    db.Battles.Add(data);
                    db.SaveChanges();
                }
                
            }
        }
    }*/

    public class RatingBll
    {
        public int Id { get; set; }
        public int CurrentRanking  { get; set; }
        public string Boxer { get; set; }
        public int AmountOfBattles { get; set; }
    }
}