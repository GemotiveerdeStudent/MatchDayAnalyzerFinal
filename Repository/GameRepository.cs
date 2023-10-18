using MatchDayAnalyzerFinal.Data;
using MatchDayAnalyzerFinal.Interfaces;
using MatchDayAnalyzerFinal.Models.ClassModels;

namespace MatchDayAnalyzerFinal.Repository
{
    // This is like the DAL in the Example.
    // Database calls from repository
    public class GameRepository : IGameRepository
    {
        private readonly MatchAnalyzerDbContext _context;

        public GameRepository(MatchAnalyzerDbContext context)
        {
            _context = context;
        }

        public ICollection<Game> GetGames()
        {
            return _context.Games.OrderBy(p => p.Id).ToList();
        }
    }
}
