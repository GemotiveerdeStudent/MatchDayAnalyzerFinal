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
    public class PlayerAPIController : Controller
    {
        // Reference to Repository for database calls
        private readonly IPlayerRepository _playerRepository;
        private readonly ITeamRepository _teamRepository;
        private readonly IMapper _mapper;

        public PlayerAPIController(IPlayerRepository playerRepository, ITeamRepository teamRepository, IMapper mapper)
        {
            _playerRepository = playerRepository;
            _teamRepository = teamRepository;
            _mapper = mapper;
        }
        // GET: api/<ValuesController>
        // Reference to repository to make the database calls.
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Player>))]
        public IActionResult GetPlayers()
        {
            // Linking to automapper tog et rid of nullable values.
            var player = _mapper.Map<List<PlayerDto>>(_playerRepository.GetPlayers());

            if (!ModelState.IsValid)
                return BadRequest(player);

            return Ok(player);
        }

        // GET api/<ValuesController>/5
        [HttpGet("{playerId}")]
        [ProducesResponseType(200, Type = typeof(AttendanceSheet))]
        [ProducesResponseType(400)]
        public IActionResult GetPlayerId(int playerId)
        {
            if (!_playerRepository.PlayerExists(playerId))
                return NotFound();

            var player = _playerRepository.GetPlayer(playerId);

            if (player == null)
                return NotFound();

            var playerDto = _mapper.Map<PlayerDto>(player);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(playerDto);
        }


        [HttpGet("Game/{teamId}")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<PlayerDto>))]
        [ProducesResponseType(404)] 
        [ProducesResponseType(400)]
        public IActionResult GetPlayersByTeam(int teamId)
        {
            var players = _playerRepository.GetPlayersByTeam(teamId);

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
        public IActionResult CreatePlayer([FromQuery] int teamId , [FromBody] PlayerDto playerCreate)
        {
            if (playerCreate == null)
                return BadRequest(ModelState);

            var game = _playerRepository.GetPlayers()
                .Where(c => c.Name.Trim().ToUpper() == playerCreate.Name.TrimEnd().ToUpper())
                .FirstOrDefault();

            if (game != null)
            {
                ModelState.AddModelError("", "Player already attended to the match");
                return StatusCode(422, ModelState);
            }

            var playerMap = _mapper.Map<Player>(playerCreate);

            if (!_playerRepository.CreatePlayer(teamId, playerMap))
            {
                ModelState.AddModelError("", "Something went wrong while saving");
                return StatusCode(500, ModelState);
            }

            return Ok("Successfully created");
        }

        [HttpPut("{playerId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult UpdatePlayer(int playerId,[FromQuery] int teamId, [FromBody] PlayerDto updateplayer)
        {
            if (updateplayer == null)
                return BadRequest(ModelState);

            if (playerId != updateplayer.Id)
                return BadRequest(ModelState);

            if (!_playerRepository.PlayerExists(playerId))
                return NotFound();

            if (!ModelState.IsValid)
                return BadRequest();

            var playerMap = _mapper.Map<Player>(updateplayer);

            if (!_playerRepository.UpdatePlayer(teamId, playerMap))
            {
                ModelState.AddModelError("", "Something went wrong while updating the player");
                return StatusCode(500, ModelState);
            }

            return Ok();
        }

        [HttpDelete("playerId")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult DeletePlayer(int playerId)
        {
            if (!_playerRepository.PlayerExists(playerId))
            {
                return NotFound();
            }

            var PlayerToDelete = _playerRepository.GetPlayer(playerId);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (_playerRepository.DeletePlayer(PlayerToDelete))
            {
                ModelState.AddModelError("", "Something went wrong deleting Player");
            }

            return NoContent();
        }

    }
}
