using System.ComponentModel.DataAnnotations;

namespace MatchDayAnalyzerFinal.Dto
{
    // A DTO is used to limit the amount of DATA my API returns to people. This was not part of the assigned but it still was an interesting topic, so i added it to the project.

    // The Data i'm excluding are the ICollections.
    public class GameDto
    {
        public int Id { get; set; }
        public DateTime DateTime { get; set; } = DateTime.Now;
        public string? OpponentTeam { get; set; }
        public int? HomeTeamScore { get; set; }
        public int? AwayTeamScore { get; set; }
    }
}
