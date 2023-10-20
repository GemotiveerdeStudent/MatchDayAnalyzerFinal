using AutoMapper;
using MatchDayAnalyzerFinal.Dto;
using MatchDayAnalyzerFinal.Interfaces;
using MatchDayAnalyzerFinal.Models.ClassModels;
using MatchDayAnalyzerFinal.Repository;
using Microsoft.AspNetCore.Mvc;

namespace MatchDayAnalyzerFinal.Controllers.APIControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TeamAPIController : Controller
    {
        // Reference to Repository for database calls
        private readonly ITeamRepository _teamRepository;
        private readonly IMapper? _mapper;

        public TeamAPIController(ITeamRepository teamRepository, IMapper mapper)
        {
            _teamRepository = teamRepository;
            _mapper = mapper;
        }

        // GET: api/<ValuesController>
        // Reference to repository to make the database calls.
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Team>))]
        public IActionResult GetTeams()
        {
            // Linking to automapper tog et rid of nullable values.
            var team = _mapper.Map<List<TeamDto>>(_teamRepository.GetTeams());

            if (!ModelState.IsValid)
                return BadRequest(team);

            return Ok(team);
        }

        // GET api/<ValuesController>/5
        [HttpGet("{teamId}")]
        [ProducesResponseType(200, Type = typeof(AttendanceSheet))]
        [ProducesResponseType(400)]
        public IActionResult GetTeam(int teamId)
        {
            if (!_teamRepository.PlayerExists(teamId))
                return NotFound();

            var team = _teamRepository.GetTeam(teamId);

            if (team == null)
                return NotFound();

            var teams = _mapper.Map<TeamDto>(team);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(teams);
        }


        [HttpGet("Game/{teamId}")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<PlayerDto>))]
        [ProducesResponseType(404)]
        [ProducesResponseType(400)]
        public IActionResult GetPlayersByTeam(int teamId)
        {
            var players = _teamRepository.GetPlayersByTeam(teamId);

            if (players == null || !players.Any())
                return NotFound(); // 404 for "Not Found"

            var playerDtos = _mapper.Map<List<PlayerDto>>(players);

            if (!ModelState.IsValid)
                return BadRequest();

            return Ok(playerDtos);
        }
    }
}
