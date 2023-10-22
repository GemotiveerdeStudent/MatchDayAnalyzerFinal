using MatchDayAnalyzerFinal.Models.ClassModels;

namespace MatchDayAnalyzerFinal.Dto
{
    public class AttendanceSheetDto
    {
        public int Id { get; set; }
        public int PriceToPayPerPlayer { get; set; }
        public int MatchGoal { get; set; }
        public int PlayerId { get; set; }
        public int GameId { get; set; }
        public bool Attend { get; set; } 
        public Player Player { get; set; }
    }
}
