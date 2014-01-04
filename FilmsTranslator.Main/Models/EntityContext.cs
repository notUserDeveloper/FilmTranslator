using System.Data.Entity;

namespace FilmsTranslator.Main.Models
{
    public class EntityContext : DbContext
    {
        public DbSet<Film> Films { get; set; }
        public DbSet<Kinopoisk> Kinopoisks { get; set; }
        public DbSet<NewTitle> NewTitles { get; set; }
        public DbSet<SubStatistic> SubStatistics { get; set; }
        public DbSet<Parasite> Parasites { get; set; }
        public DbSet<RightWord> RightWords { get; set; }
    }

    public static class DB
    {
        public static EntityContext Init
        {
            get
            {
                return new EntityContext();
            }
        }
    }
}
