
using MatchDayAnalyzerFinal.Models.ClassModels;
using System.ComponentModel.DataAnnotations;

namespace MatchDayAnalyzerFinal.Models.ViewModels
{
    public class TeamViewModel
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public Season? Season { get; set; }
        public int? SeasonId { get; set; }
        public virtual ICollection<Player>? PlayersInTeam { get; set; }
        public virtual ICollection<Game>? Games { get; set; }
        public byte? TeamPicture { get; set; }
    }
}
