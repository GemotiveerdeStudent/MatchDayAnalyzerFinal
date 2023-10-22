using AutoMapper;
using MatchDayAnalyzerFinal.Dto;
using MatchDayAnalyzerFinal.Interfaces;
using MatchDayAnalyzerFinal.Models.ClassModels;
using MatchDayAnalyzerFinal.Repository;
using Microsoft.AspNetCore.Mvc;

// Controller has been generated 
namespace MatchDayAnalyzerFinal.Controllers.APIControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GameAPIController : Controller
    {
        // Reference to Repository for database calls
        private readonly IGameRepository _gameRepository;
        private readonly IMapper _mapper;

        public GameAPIController(IGameRepository gameRepository, IMapper mapper)
        {
            this._gameRepository = gameRepository;
            this._mapper = mapper;
        }
        // GET: api/<ValuesController>
        // Reference to repository to make the database calls.
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Game>))]
        public IActionResult GetGames()
        {
            // Linking to automapper tog et rid of nullable values.
            var games = _mapper.Map<List<GameDto>>(_gameRepository.GetGames());

            if(!ModelState.IsValid)
                return BadRequest(games);

            return Ok(games);
        }

        // GET api/<ValuesController>/5
        [HttpGet("{gameId}")]
        [ProducesResponseType(200, Type = typeof(Game))]
        [ProducesResponseType(400)]
        public IActionResult GetGamesId(int gameId)
        {
            if (!_gameRepository.GameExists(gameId))
                return NotFound();
            // Linking to automapper to get rid of nullable values
            var game = _mapper.Map<GameDto>(_gameRepository.GetGamesId(gameId));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(game);
        }


        [HttpGet("Game/{opponentTeam}")]
        [ProducesResponseType(200, Type = typeof(GameDto))]
        [ProducesResponseType(404)]
        [ProducesResponseType(400)]
        public IActionResult GetGame(string opponentTeam)
        {
            if (!_gameRepository.OpponentExists(opponentTeam))
                return NotFound();

            var game = _gameRepository.GetGame(opponentTeam);

            if (game == null)
                return NotFound();

            var gameDto = _mapper.Map<GameDto>(game);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(gameDto);
        }
        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CreateGame([FromBody] GameDto gameCreate)
        {
            if (gameCreate == null)
                return BadRequest(ModelState);

            var game = _gameRepository.GetGames()
                .Where(c => c.OpponentTeam.Trim().ToUpper() == gameCreate.OpponentTeam.TrimEnd().ToUpper())
                .FirstOrDefault();

            if (game != null)
            {
                ModelState.AddModelError("", "Player already attended to the match");
                return StatusCode(422, ModelState);
            }

            var gameMap = _mapper.Map<Game>(gameCreate);


            if (!_gameRepository.CreateGame(gameMap))
            {
                ModelState.AddModelError("", "Something went wrong while saving");
                return StatusCode(500, ModelState);
            }

            return Ok("Successfully created");
        }
    }
}
