using System;
using MatchDayAnalyzerFinal.Models.ViewModels;
using MatchDayAnalyzerFinal.Models.ClassModels;

// This class helps me transform the domain view model and vice versa. 
public static class ViewModelHelpers
{
    public static TeamViewModel ToViewModel(this Team team)
    {
        var teamViewModel = new TeamViewModel
        {
            Name = team.Name,
            Id = team.Id
        };

        return teamViewModel;
    }

    public static Team ToDomainModel(this TeamViewModel teamViewModel)
    {
        var team = new Team();
        team.Name = team.Name;
        team.Id = team.Id;

        return team;
    }
}