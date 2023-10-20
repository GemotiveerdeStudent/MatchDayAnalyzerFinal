using MatchDayAnalyzerFinal.Interfaces;
using MatchDayAnalyzerFinal.Models.ClassModels;
using MatchDayAnalyzerFinal.Data;
using Microsoft.CodeAnalysis.FlowAnalysis.DataFlow;


namespace MatchDayAnalyzerFinal.Repository
{
    // This is like the DAL in the Example.
    // Database calls from repository
    // DATABASE CALLS
    public class AttendanceSheetRepository : IAttendanceSheetRepository
    {
        private readonly MatchAnalyzerDbContext _context;

        public AttendanceSheetRepository(MatchAnalyzerDbContext context)
        {
            _context = context;
        }
        public ICollection<AttendanceSheet> GetAttendanceSheets()
        {
            return _context.AttendanceSheets.ToList();
        }
        public AttendanceSheet GetAttendanceSheetsId(int id)
        {
            // Gotta use FirstorDefault when only calling for 1 from DB.
            return _context.AttendanceSheets.Where(e => e.Id == id).FirstOrDefault();
        }
        public ICollection<Game> GetGamesSheetsByAttendanceSheet(int AttendanceSheetId)
        {
            return _context.AttendanceSheets.Where(e => e.Id == AttendanceSheetId).Select(c => c.Game).ToList();
        }
        public bool AttendanceSheetExists(int id)
        {
            return _context.AttendanceSheets.Any(sheet => sheet.Id == id);
        }
    }
}
