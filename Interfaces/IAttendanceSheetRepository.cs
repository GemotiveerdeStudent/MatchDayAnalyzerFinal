using MatchDayAnalyzerFinal.Models.ClassModels;

namespace MatchDayAnalyzerFinal.Interfaces
{
    public interface IAttendanceSheetRepository
    {
        // Interface for all method of this class, write interfac first then generate Repository class based on Interface.
        ICollection<AttendanceSheet> GetAttendanceSheets();
        AttendanceSheet GetAttendanceSheetsId(int id);
        ICollection<Game> GetGamesSheetsByAttendanceSheet(int GameId);
        bool AttendanceSheetExists(int id);
        bool CreateAttendanceSheet(AttendanceSheet attendanceSheet);
        bool Save();



    }
}
