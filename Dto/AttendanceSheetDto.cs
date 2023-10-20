using MatchDayAnalyzerFinal.Models.ClassModels;

namespace MatchDayAnalyzerFinal.Dto
{
    public class AttendanceSheetDto
    {
        public int Id { get; set; }
        public byte Attend { get; set; }
        public int PriceToPayPerPlayer { get; set; }
        public int MatchGoal { get; set; }
        public int PlayerId { get; set; }
        public int GameId { get; set; }
    }
}
