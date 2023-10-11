using System.ComponentModel.DataAnnotations;

namespace MatchDayAnalyzerFinal.Models.ClassModels
{
    public class Player
    {
        public int Id { get; set; }
        [Required]
        [StringLength(100)]
        public string? Name { get; set; }
        [Required]
        [StringLength(50)]
        public string? Position { get; set; }
        public virtual Team? Team { get; set; }
        public int? TeamId { get; set; }
        public virtual ICollection<AttendanceSheet>? AttendanceSheets { get; set; }
        public byte? PlayerPicture { get; set; }
        public int? TotalGoals { get; set; }
    }
}