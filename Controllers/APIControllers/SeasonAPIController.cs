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
    public class SeasonAPIController : Controller
    {
        // Reference to Repository for database calls
        private readonly ISeasonRepository _seasonRepository;
        private readonly IMapper? _mapper;

        public SeasonAPIController(ISeasonRepository seasonRepository, IMapper mapper)
        {
            _seasonRepository = seasonRepository;
            _mapper = mapper;
        }
        // GET: api/<ValuesController>
        // Reference to repository to make the database calls.
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Player>))]
        public IActionResult GetSeasons()
        {
            // Linking to automapper tog et rid of nullable values.
            var season = _mapper.Map<List<SeasonDto>>(_seasonRepository.GetSeasons());

            if (!ModelState.IsValid)
                return BadRequest(season);

            return Ok(season);
        }

        // GET api/<ValuesController>/5
        [HttpGet("{seasonId}")]
        [ProducesResponseType(200, Type = typeof(AttendanceSheet))]
        [ProducesResponseType(400)]
        public IActionResult GetSeasonById(int seasonId)
        {
            if (!_seasonRepository.SeasonExists(seasonId))
                return NotFound();

            var season = _seasonRepository.GetSeasonById(seasonId);

            if (season == null)
                return NotFound();

            var playerDto = _mapper.Map<SeasonDto>(season);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(playerDto);
        }


        [HttpGet("Game/{seasonId}")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<TeamDto>))]
        [ProducesResponseType(404)]
        [ProducesResponseType(400)]
        public IActionResult GetTeamsPlayingInSeason(int seasonId)
        {
            var teams = _seasonRepository.GetTeamsPlayingInSeason(seasonId);

            if (teams == null || !teams.Any())
                return NotFound(); // 404 for "Not Found"

            var teamsDto = _mapper.Map<List<TeamDto>>(teams);

            if (!ModelState.IsValid)
                return BadRequest();

            return Ok(teamsDto);
        }


    }
}
