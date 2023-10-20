using MatchDayAnalyzerFinal.Models.ClassModels;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace MatchDayAnalyzerFinal.Data
{
    public class MatchAnalyzerDbContext : IdentityDbContext
    {
        public DbSet<AttendanceSheet> AttendanceSheets { get; set; }
        public DbSet<Game> Games { get; set; }
        public DbSet<Player> Players { get; set; }
        public DbSet<Season> Seasons { get; set; }
        public DbSet<Team> Teams { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            /*        string connection = @"Data Source=.;Initial Catalog=MatchdayAnalyzer;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
                      optionsBuilder.UseSqlServer(connection); */
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Game>()
                .HasMany(x => x.TeamsPlayedGame)
                .WithMany(y => y.Games)
                .UsingEntity(
                    "GameTeam",
                    g => g.HasOne(typeof(Game)).WithMany().HasForeignKey("GameId").HasPrincipalKey(nameof(Game.Id)),
                    t => t.HasOne(typeof(Team)).WithMany().HasForeignKey("TeamId").HasPrincipalKey(nameof(Team.Id)),
                    j => j.HasKey("GameId", "TeamId")
                );

            base.OnModelCreating(builder);
        }
        public MatchAnalyzerDbContext(DbContextOptions<MatchAnalyzerDbContext> contextOptions) : base(contextOptions)
        {

        }
    }
}