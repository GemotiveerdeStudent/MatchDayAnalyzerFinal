using MatchDayAnalyzerFinal.Models.ClassModels;
using System.ComponentModel.DataAnnotations;

namespace MatchDayAnalyzerFinal.Models.ViewModels
{
    public class GameViewModel
    {
        public int Id { get; set; }
        public DateTime DateTime { get; set; } = DateTime.Now;
        public string? OpponentTeam { get; set; }
        public int? HomeTeamScore { get; set; }
        public int? AwayTeamScore { get; set; }
        public virtual ICollection<Team>? TeamsPlayedGame { get; set; }
        public virtual ICollection<AttendanceSheet>? AttendanceSheets { get; set; }
    }
}
