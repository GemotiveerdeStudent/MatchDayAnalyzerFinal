using MatchDayAnalyzerFinal.Models.ClassModels;

namespace MatchDayAnalyzerFinal.Models.ViewModels
{
    public class AssignedTeamViewModel
    {
        public int Id { get; set; }
        public DateTime DateTime { get; set; } = DateTime.Now;
        public string? OpponentTeam { get; set; }
        public int? HomeTeamScore { get; set; }
        public int? AwayTeamScore { get; set; }
        public virtual ICollection<Team>? TeamsPlayedGame { get; set; }
        public bool Assigned { get; set; }
    }
}
