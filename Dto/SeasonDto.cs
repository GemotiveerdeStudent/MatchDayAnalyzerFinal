using MatchDayAnalyzerFinal.Models.ClassModels;
using System.ComponentModel.DataAnnotations;

namespace MatchDayAnalyzerFinal.Dto
{
    public class SeasonDto
    {
        public int Id { get; set; }
        [Required]
        [StringLength(100)]
        public string? Name { get; set; }
        public DateTime? Date { get; set; }
        public int? TotalPrice { get; set; }
        public virtual ICollection<Team>? TeamsInSeason { get; set; }
    }
}
