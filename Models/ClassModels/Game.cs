using System.ComponentModel.DataAnnotations;

namespace MatchDayAnalyzerFinal.Models.ClassModels
{
    public class Game
    {
        public int Id { get; set; }
        public DateTime DateTime { get; set; } = DateTime.Now;
        [Required]
        [StringLength(100)]
        public string? OpponentTeam { get; set; }
        [Required]
        public int? HomeTeamScore { get; set; }
        [Required]
        public int? AwayTeamScore { get; set; }
        public virtual ICollection<Team>? TeamsPlayedGame { get; set; }
        public virtual ICollection<AttendanceSheet>? AttendanceSheets { get; set; }
    }
}
