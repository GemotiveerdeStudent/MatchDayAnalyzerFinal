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

        public bool GameExists(int id)
        {
            return _context.Games.Any(g => g.Id == id);
        }
        public bool OpponentExists(string opponentTeam)
        {
            return _context.Games.Any(g => g.OpponentTeam == opponentTeam);
        }

        public Game GetGame(string opponentTeam)
        {
            return _context.Games.Where(g => g.OpponentTeam == opponentTeam).FirstOrDefault();
        }

        public ICollection<Game> GetGames()
        {
            // Return a list of games
            return _context.Games.OrderBy(p => p.Id).ToList();
        }

        public Game GetGamesId(int id)
        {
            // Return just one game
            return _context.Games.Where(g => g.Id == id).FirstOrDefault();
        }
    }
}
