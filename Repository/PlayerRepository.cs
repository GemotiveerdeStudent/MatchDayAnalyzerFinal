using MatchDayAnalyzerFinal.Interfaces;
using MatchDayAnalyzerFinal.Data;
using MatchDayAnalyzerFinal.Models.ClassModels;
using AutoMapper;

namespace MatchDayAnalyzerFinal.Repository
{
    // This is like the DAL in the Example.
    // Database calls from repository
    public class PlayerRepository : IPlayerRepository
    {
        private readonly MatchAnalyzerDbContext _context;


        public PlayerRepository(MatchAnalyzerDbContext context)
        {
            _context = context;
        }

        public Player GetPlayer(int id)
        {
            return _context.Players.
                Where(p => p.Id == id)
                .FirstOrDefault();
        }

        public IEnumerable<Player> GetPlayersByTeam(int teamId)
        {
            return _context.Players
                .Where(p => p.TeamId == teamId)
                .ToList();
        }


        public ICollection<Player> GetPlayers()
        {
            return _context.Players
                .ToList();
        }

        public bool PlayerExists(int id)
        {
            return _context.Players.Any(p => p.Id == id);
        }

        public bool CreatePlayer(Player player)
        {
            _context.Add(player);
            return Save();
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }
    }
}
