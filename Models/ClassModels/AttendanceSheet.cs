namespace MatchDayAnalyzerFinal.Models.ClassModels
{
    public class AttendanceSheet
    {
        public int Id { get; set; }
        public byte Attend { get; set; }
        public int PriceToPayPerPlayer { get; set; }
        public int MatchGoal { get; set; }
        public Player? Player { get; set; }
        public int PlayerId { get; set; }
        public Game? Game { get; set; }
        public int GameId { get; set; }
    }
}