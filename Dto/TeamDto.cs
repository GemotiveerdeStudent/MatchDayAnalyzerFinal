using MatchDayAnalyzerFinal.Models.ClassModels;
using System.ComponentModel.DataAnnotations;

namespace MatchDayAnalyzerFinal.Dto
{
    public class TeamDto
    {
        public int Id { get; set; }
        [Required]
        [StringLength(50)]
        public string? Name { get; set; }
        public Season? Season { get; set; }
        public int? SeasonId { get; set; }
        public byte? TeamPicture { get; set; }

    }
}
