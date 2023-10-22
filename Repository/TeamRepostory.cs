using MatchDayAnalyzerFinal.Data;
using MatchDayAnalyzerFinal.Interfaces;
using MatchDayAnalyzerFinal.Models.ClassModels;

namespace MatchDayAnalyzerFinal.Repository
{
    // This is like the DAL in the Example.
    // Database calls from repository
    public class TeamRepostory : ITeamRepository
    {
        private readonly MatchAnalyzerDbContext _context;

        public TeamRepostory(MatchAnalyzerDbContext context)
        {
            _context = context;
        }

        public bool CreateTeam(Team team)
        {
            _context.Add(team);
            return Save();
        }

        public IEnumerable<Team> GetPlayersByTeam(int teamId)
        {
            return _context.Teams
                .Where(p => p.Id == teamId)
                .ToList();
        }

        public Team GetTeam(int id)
        {
            return _context.Teams.
                Where(s => s.Id == id)
                .FirstOrDefault();
        }

        public ICollection<Team> GetTeams()
        {
            return _context.Teams
                .ToList();
        }

        public bool PlayerExists(int id)
        {
            return _context.Players.Any(p => p.Id == id);
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }
    }
}
