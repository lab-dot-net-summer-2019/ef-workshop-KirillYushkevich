using Microsoft.EntityFrameworkCore;
using SamuraiApp.Domain;

namespace SamuraiApp.Data
{
    public class SamuraiContext:DbContext
    {
        public SamuraiContext()
        {
        }

        public SamuraiContext(DbContextOptions<SamuraiContext> options)
            : base(options)
        { }
        

        public DbSet<Samurai> Samurais { get; set; }
        public DbSet<Quote> Quotes { get; set; }
        public DbSet<Battle> Battles { get; set; }
        public DbSet<Ronin> Ronins { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<SamuraiBattle>()
                .HasKey(s => new { s.BattleId, s.SamuraiId });

            //modelBuilder.Entity<SamuraiBattle>()
            //    .Property(sb => sb.KillStreak);

            modelBuilder.Entity<Samurai>()
                .HasKey(s => new { s.Id});

            modelBuilder.Entity<Ronin>()
                .HasKey(r => new { r.Id });

            modelBuilder.Entity<Quote>()
                .HasKey(q => new { q.Id});

            modelBuilder.Entity<Samurai>()
                .HasMany(s => s.Quotes)
                .WithOne(q => q.Samurai)
                .HasForeignKey(s => new { s.SamuraiId });

            modelBuilder.Entity<Samurai>()
                .HasMany(s => s.SamuraiBattles)
                .WithOne(sb => sb.Samurai)
                .HasForeignKey(s => new {s.SamuraiId });

            modelBuilder.Entity<Quote>()
                .HasOne(q => q.Samurai)
                .WithMany(s => s.Quotes)
                .HasForeignKey(q => new { q.SamuraiId });

            modelBuilder.Entity<SamuraiBattle>()
                .HasOne(sb => sb.Battle)
                .WithMany(b => b.SamuraiBattles)
                .HasForeignKey(sb => new { sb.BattleId });

            base.OnModelCreating(modelBuilder);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseLazyLoadingProxies().UseSqlServer(
                 "Server=(localdb)\\MSSQLLocalDB;Database=SamuraiAppDataCore;Trusted_Connection=True;");
        }
    }
}
