﻿namespace MatchDayAnalyzerFinal.Models.ClassModels
{
    public class AttendanceSheet
    {
        public int Id { get; set; }
        public byte Attend { get; set; }
        public int PriceToPayPerPlayer { get; set; }
        public int MatchGoal { get; set; }
        public virtual Player? Player { get; set; }
        public int PlayerId { get; set; }
        public virtual Game? Game { get; set; }
        public int GameId { get; set; }
    }
}