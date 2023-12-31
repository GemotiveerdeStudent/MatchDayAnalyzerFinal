﻿using MatchDayAnalyzerFinal.Models.ClassModels;

namespace MatchDayAnalyzerFinal.Interfaces
{
    public interface ITeamRepository
    {
        // Interface for all method of this class, write interfac first then generate Repository class based on Interface.
        ICollection<Team> GetTeams();
        Team GetTeam(int id);
        IEnumerable<Team> GetPlayersByTeam(int teamId);
        bool TeamExists(int id);
        bool CreateTeam(Team team);
        bool UpdateTeam(Team team);
        bool DeleteTeam(Team team);
        bool Save();
    }
}
