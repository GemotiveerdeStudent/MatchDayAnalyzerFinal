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
            if (!_teamRepository.TeamExists(teamId))
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
        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CreateTeam([FromBody] TeamDto teamCreate)
        {
            if (teamCreate == null)
                return BadRequest(ModelState);

            var game = _teamRepository.GetTeams()
                .Where(c => c.Name.Trim().ToUpper() == teamCreate.Name.TrimEnd().ToUpper())
                .FirstOrDefault();

            if (game != null)
            {
                ModelState.AddModelError("", "Player already attended to the match");
                return StatusCode(422, ModelState);
            }

            var teamMap = _mapper.Map<Team>(teamCreate);

            if (!_teamRepository.CreateTeam(teamMap))
            {
                ModelState.AddModelError("", "Something went wrong while saving");
                return StatusCode(500, ModelState);
            }

            return Ok("Successfully created");
        }


        [HttpPut("{teamId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult UpdateSeason(int teamId, [FromBody] TeamDto updateTeam)
        {
            if (updateTeam == null)
                return BadRequest(ModelState);

            if (teamId != updateTeam.Id)
                return BadRequest(ModelState);

            if (!_teamRepository.TeamExists(teamId))
                return NotFound();

            if (!ModelState.IsValid)
                return BadRequest();

            var teamMap = _mapper.Map<Team>(updateTeam);

            if (!_teamRepository.UpdateTeam(teamMap))
            {
                ModelState.AddModelError("", "Something went wrong while updating the attendancesheet");
                return StatusCode(500, ModelState);
            }

            return Ok();
        }

        [HttpDelete("teamId")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult DeleteTeam(int teamId)
        {
            if (!_teamRepository.TeamExists(teamId))
            {
                return NotFound();
            }

            var TeamToDelete = _teamRepository.GetTeam(teamId);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (_teamRepository.DeleteTeam(TeamToDelete))
            {
                ModelState.AddModelError("", "Something went wrong deleting Season");
            }

            return NoContent();
        }


    }
}
