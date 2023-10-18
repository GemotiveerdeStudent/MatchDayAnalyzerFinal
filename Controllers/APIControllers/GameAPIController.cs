using MatchDayAnalyzerFinal.Interfaces;
using MatchDayAnalyzerFinal.Models.ClassModels;
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

        public GameAPIController(IGameRepository gameRepository)
        {
            this._gameRepository = gameRepository;
        }
        // GET: api/<ValuesController>
        // Reference to repository to make the database calls.
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Game>))]
        public IActionResult GetGames()
        {
            var games = _gameRepository.GetGames();

            if(!ModelState.IsValid)
                return BadRequest(games);

            return Ok(games);
        }

        // GET api/<ValuesController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<ValuesController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<ValuesController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<ValuesController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
