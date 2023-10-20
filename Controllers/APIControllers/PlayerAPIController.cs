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
        private readonly IMapper _mapper;

        public PlayerAPIController(IPlayerRepository playerRepository, IMapper mapper)
        {
            _playerRepository = playerRepository;
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
    }
}
