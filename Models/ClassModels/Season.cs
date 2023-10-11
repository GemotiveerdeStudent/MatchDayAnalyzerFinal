using System.ComponentModel.DataAnnotations;

namespace MatchDayAnalyzerFinal.Models.ClassModels
{
    public class Season
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
