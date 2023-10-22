using MatchDayAnalyzerFinal.Models.ClassModels;

namespace MatchDayAnalyzerFinal.Interfaces
{
    public interface IPlayerRepository
    {
        // Interface for all method of this class, write interfac first then generate Repository class based on Interface.
        ICollection<Player> GetPlayers();
        Player GetPlayer(int id);
        IEnumerable<Player> GetPlayersByTeam(int teamId);
        bool PlayerExists(int id);
        bool CreatePlayer(Player player);
        bool Save();
    }
}
