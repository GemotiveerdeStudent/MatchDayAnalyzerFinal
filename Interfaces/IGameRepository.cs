using MatchDayAnalyzerFinal.Models.ClassModels;

namespace MatchDayAnalyzerFinal.Interfaces
{
    public interface IGameRepository
    {
        // Interface for all methods of Games 
        ICollection<Game> GetGames();
        Game GetGamesId (int id);
        Game GetGame (string opponentTeam);
        bool GameExists(int id);
        bool OpponentExists(string opponentTeam);
        bool CreateGame(Game game);
        bool UpdateGame(Game game);
        bool DeleteGame(Game game);
        bool Save();
    }
}

