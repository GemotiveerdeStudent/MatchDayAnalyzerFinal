using MatchDayAnalyzerFinal.Models.ClassModels;

namespace MatchDayAnalyzerFinal.Interfaces
{
    public interface IGameRepository
    {
        // Interface for all methods of Games 
        ICollection<Game> GetGames();
    }
}
