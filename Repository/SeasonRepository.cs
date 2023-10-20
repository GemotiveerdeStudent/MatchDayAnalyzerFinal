using MatchDayAnalyzerFinal.Data;
using MatchDayAnalyzerFinal.Interfaces;
using MatchDayAnalyzerFinal.Models.ClassModels;
using Microsoft.EntityFrameworkCore;

namespace MatchDayAnalyzerFinal.Repository
{
    // This is like the DAL in the Example.
    // Database calls from repository
    public class SeasonRepository : ISeasonRepository
    {
        private readonly MatchAnalyzerDbContext _context;

        public SeasonRepository(MatchAnalyzerDbContext context)
        {
            _context = context;
        }

        public Season GetSeasonById(int id)
        {
            return _context.Seasons.
                Where(s => s.Id == id)
                .FirstOrDefault();
        }

        public ICollection<Season> GetSeasons()
        {
            return _context.Seasons
                .ToList();
        }

        public IEnumerable<Team> GetTeamsPlayingInSeason(int seasonId)
        {
                var season = _context.Seasons
                .Include(s => s.TeamsInSeason)
                .FirstOrDefault(s => s.Id == seasonId);

            return season?.TeamsInSeason.ToList() ?? new List<Team>();
        }


        public bool SeasonExists(int id)
        {
            return _context.Seasons.Any(p => p.Id == id);
        }
    }
}
