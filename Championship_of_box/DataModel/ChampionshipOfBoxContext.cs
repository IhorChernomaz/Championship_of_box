namespace Championship_of_box.DataModel
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Linq;

    public class ChampionshipOfBoxContext : DbContext
    {
        public ChampionshipOfBoxContext()
            : base("DBConnection")
        {
        }
        public DbSet<Battle> Battles { get; set; }

    }

    public class Battle
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public int AmountOfRounds { get; set; }
        public string Boxer1 { get; set; }
        public string Boxer2 { get; set; }
        public int RefereePoints { get; set; }
    }
    public class Rating
    {
        public int CurrentRanking { get; set; }
        public string Boxer { get; set; }
        public int AmountOfBattles { get; set; }
    }

    public class BattleRepository : IRepository<Battle>
    {
        ChampionshipOfBoxContext db;
        public BattleRepository()
        {
            db = new ChampionshipOfBoxContext();
        }

        public IEnumerable<Rating> GetAllBoxerRating()
        {
            var rating = db.Battles.GroupBy(r => r.Boxer1)
                .Select(g => new Rating { Boxer = g.Key, CurrentRanking = g.Sum(s => s.RefereePoints), AmountOfBattles = g.Count() })
                .Union(db.Battles.GroupBy(r => r.Boxer2)
                .Select(g => new Rating { Boxer = g.Key, CurrentRanking = 0, AmountOfBattles = g.Count() }))
                .GroupBy(r => r.Boxer).Select(g => new Rating { Boxer = g.Key, CurrentRanking = g.Sum(s => s.CurrentRanking), AmountOfBattles = g.Sum(s => s.AmountOfBattles) }).ToList();
            return rating;
        }
        
        public IEnumerable<Battle> GetAll()
        {
            return db.Battles.ToList();
        }

        public Battle Get(int id)
        {
            return db.Battles.Find(id);
        }

        public void Create(Battle item)
        {
            db.Battles.Add(item);
        }

        public void Update(Battle item)
        {
            db.Entry(item).State = EntityState.Modified;
        }

        public void Delete(int id)
        {
            Battle user = db.Battles.Find(id);
            if (user != null)
                db.Battles.Remove(user);
        }

        public void Save()
        {
            db.SaveChanges();
        }

        private bool disposed = false;

        public virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    db.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
    interface IRepository<T> : IDisposable where T : class
    {
        IEnumerable<T> GetAll();
        T Get(int id);
        void Create(T item);
        void Update(T item);
        void Delete(int id);
        void Save();
    }
}