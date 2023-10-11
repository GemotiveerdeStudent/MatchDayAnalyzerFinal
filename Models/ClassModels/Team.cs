using System.ComponentModel.DataAnnotations;

namespace MatchDayAnalyzerFinal.Models.ClassModels
{
    public class Team
    {
        public int Id { get; set; }
        [Required]
        [StringLength(50)]
        public string? Name { get; set; }
        public Season? Season { get; set; }
        public int? SeasonId { get; set; }
        public virtual ICollection<Player>? PlayersInTeam { get; set; }
        public virtual ICollection<Game>? Games { get; set; }
        public byte? TeamPicture { get; set; }
    }
}
