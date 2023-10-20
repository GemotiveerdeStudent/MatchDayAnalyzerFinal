using MatchDayAnalyzerFinal.Models.ClassModels;
using System.ComponentModel.DataAnnotations;

namespace MatchDayAnalyzerFinal.Dto
{
    public class PlayerDto
    {
        public int Id { get; set; }
        [Required]
        [StringLength(100)]
        public string? Name { get; set; }
        [Required]
        [StringLength(50)]
        public string? Position { get; set; }
        public int? TeamId { get; set; }
        public byte? PlayerPicture { get; set; }
        public int? TotalGoals { get; set; }
    }
}
