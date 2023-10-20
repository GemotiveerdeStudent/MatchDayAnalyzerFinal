using MatchDayAnalyzerFinal.Models.ClassModels;

namespace MatchDayAnalyzerFinal.Interfaces
{
    public interface ISeasonRepository
    {
        // Interface for all method of this class, write interfac first then generate Repository class based on Interface.
        ICollection<Season> GetSeasons();
        Season GetSeasonById(int id);
        IEnumerable<Team> GetTeamsPlayingInSeason(int seasonId);
        bool SeasonExists(int id);
    }
}
