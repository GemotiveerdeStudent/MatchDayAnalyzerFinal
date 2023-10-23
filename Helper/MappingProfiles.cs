using AutoMapper;
using MatchDayAnalyzerFinal.Models.ClassModels;
using MatchDayAnalyzerFinal.Dto;
using System.ComponentModel;

namespace MatchDayAnalyzerFinal.Helper
{
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
            CreateMap<GameDto, Game>();
            CreateMap<AttendanceSheet, AttendanceSheetDto>();
            CreateMap<AttendanceSheetDto, AttendanceSheet>();
            CreateMap<Player, PlayerDto>();
            CreateMap<PlayerDto, Player>();
            CreateMap<Team, TeamDto>();
            CreateMap<TeamDto, Team>();
            CreateMap<Season, SeasonDto>();
            CreateMap<SeasonDto, Season>();
        }
    }
}
