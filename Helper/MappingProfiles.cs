using AutoMapper;
using MatchDayAnalyzerFinal.Models.ClassModels;
using MatchDayAnalyzerFinal.Dto;
using System.ComponentModel;

namespace MatchDayAnalyzerFinal.Helper
{
    // Created mapping profile for SwaggerUI to prevent nullables.
    // Automapper automatically maps all the attributes. It prevents me from having to do this at every API method : 
                    //Id = gameUpdate.Id,
                    //DateTime = gameUpdate.DateTime,
                    //OpponentTeam = gameUpdate.OpponentTeam,
                    //HomeTeamScore = gameUpdate.HomeTeamScore,
                    //AwayTeamScore = gameUpdate?.AwayTeamScore

    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<Game, GameDto>();
            CreateMap<AttendanceSheet, AttendanceSheetDto>();
            CreateMap<Player, PlayerDto>();
            CreateMap<Team, TeamDto>();
            CreateMap<Season, SeasonDto>();
        }
    }
}
