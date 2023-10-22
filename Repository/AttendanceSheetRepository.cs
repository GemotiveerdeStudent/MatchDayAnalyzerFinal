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

        public bool CreateAttendanceSheet(AttendanceSheet attendanceSheet)
        {
            //Change tracker
            // Adding updating modyfying
            // connected vs disconnected
            // EntityState.Added
            _context.Add(attendanceSheet);
            _context.SaveChanges();
            return Save();
        }

        // Convert into sequel an put it in the databe. 
        // This is when the actual SQL is going to be generated and added to database.
        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool UpdateAttendanceSheet(AttendanceSheet attendanceSheet)
        {
            _context.Update(attendanceSheet);
                return Save();
        }

        public bool DeleteAttendanceSheet(AttendanceSheet attendanceSheet)
        {
            _context.Remove(attendanceSheet);
                return Save();
        }
    }
}
