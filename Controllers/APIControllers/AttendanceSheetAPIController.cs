using AutoMapper;
using MatchDayAnalyzerFinal.Dto;
using MatchDayAnalyzerFinal.Interfaces;
using MatchDayAnalyzerFinal.Models.ClassModels;
using MatchDayAnalyzerFinal.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel;

namespace MatchDayAnalyzerFinal.Controllers.APIControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AttendanceSheetAPIController : Controller
    {
        // Reference to Repository for database calls
        private readonly IAttendanceSheetRepository _attendanceSheetRepository;
        private readonly IMapper? _mapper;

        public AttendanceSheetAPIController(IAttendanceSheetRepository attendanceSheetRepository, IMapper mapper)
        {
            _attendanceSheetRepository = attendanceSheetRepository;
            _mapper = mapper;
        }
        // GET: api/<ValuesController>
        // Reference to repository to make the database calls.
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<AttendanceSheet>))]
        public IActionResult GetAttendanceSheets()
        {
            // Linking to automapper tog et rid of nullable values.
            var attendanceSheet = _mapper.Map<List<AttendanceSheetDto>>(_attendanceSheetRepository.GetAttendanceSheets());

            if (!ModelState.IsValid)
                return BadRequest(attendanceSheet);

            return Ok(attendanceSheet);
        }

        // GET api/<ValuesController>/5
        [HttpGet("{attendanceSheetId}")]
        [ProducesResponseType(200, Type = typeof(AttendanceSheet))]
        [ProducesResponseType(400)]
        public IActionResult GetAttendanceSheetsId(int attendanceSheetId)
        {
            if (!_attendanceSheetRepository.AttendanceSheetExists(attendanceSheetId))
                return NotFound();
            // Linking to automapper tog et rid of nullable values.
            var attendanceSheet = _mapper.Map<AttendanceSheetDto>(_attendanceSheetRepository.GetAttendanceSheetsId(attendanceSheetId));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(attendanceSheet);
        }

        [HttpGet("Game/{AttendanceSheetId}")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<AttendanceSheet>))]
        [ProducesResponseType(400)]
        public IActionResult GetGamesSheetsByAttendanceSheet(int attendanceSheetId)
        {
            var games = _mapper.Map<List<AttendanceSheetDto>>(
                _attendanceSheetRepository.GetGamesSheetsByAttendanceSheet(attendanceSheetId));

            if (!ModelState.IsValid)
                return BadRequest();

            return Ok(games);
        }

        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CreateAttendanceSheet([FromBody] AttendanceSheetDto attendanceSheetCreate)
        {
            if (attendanceSheetCreate == null)
                return BadRequest(ModelState);

            var attendanceSheet = _attendanceSheetRepository.GetAttendanceSheets()
                .FirstOrDefault(a => a.Player.Name.Trim().ToUpper() == attendanceSheetCreate.Player.Name.TrimEnd().ToUpper());

            if (attendanceSheet != null)
            {
                ModelState.AddModelError("", "Player already attended to the match");
                return StatusCode(422, ModelState);
            }

            var attendanceSheetMap = _mapper.Map<AttendanceSheet>(attendanceSheetCreate);

            if (!_attendanceSheetRepository.CreateAttendanceSheet(attendanceSheetMap))
            {
                ModelState.AddModelError("", "Something went wrong while saving");
                return StatusCode(500, ModelState);
            }

            return Ok("Successfully created");
        }




    }
}
