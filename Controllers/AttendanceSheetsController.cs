using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MatchDayAnalyzerFinal.Data;
using MatchDayAnalyzerFinal.Models.ClassModels;

namespace MatchDayAnalyzerFinal.Controllers
{
    public class AttendanceSheetsController : Controller
    {
        private readonly MatchAnalyzerDbContext _context;

        public AttendanceSheetsController(MatchAnalyzerDbContext context)
        {
            _context = context;
        }

        // GET: AttendanceSheets
        public async Task<IActionResult> Index()
        {
            var matchAnalyzerDbContext = _context.AttendanceSheets.Include(a => a.Game).Include(a => a.Player);
            return View(await matchAnalyzerDbContext.ToListAsync());
        }

        // GET: AttendanceSheets/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.AttendanceSheets == null)
            {
                return NotFound();
            }

            var attendanceSheet = await _context.AttendanceSheets
                .Include(a => a.Game)
                .Include(a => a.Player)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (attendanceSheet == null)
            {
                return NotFound();
            }

            return View(attendanceSheet);
        }

        // GET: AttendanceSheets/Create
        public IActionResult Create()
        {
            ViewData["GameId"] = new SelectList(_context.Games, "Id", "OpponentTeam");
            ViewData["PlayerId"] = new SelectList(_context.Players, "Id", "Name");
            return View();
        }

        // POST: AttendanceSheets/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Attend,PriceToPayPerPlayer,MatchGoal,PlayerId,GameId")] AttendanceSheet attendanceSheet)
        {
            if (ModelState.IsValid)
            {
                _context.Add(attendanceSheet);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["GameId"] = new SelectList(_context.Games, "Id", "OpponentTeam", attendanceSheet.GameId);
            ViewData["PlayerId"] = new SelectList(_context.Players, "Id", "Name", attendanceSheet.PlayerId);
            return View(attendanceSheet);
        }

        // GET: AttendanceSheets/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.AttendanceSheets == null)
            {
                return NotFound();
            }

            var attendanceSheet = await _context.AttendanceSheets.FindAsync(id);
            if (attendanceSheet == null)
            {
                return NotFound();
            }
            ViewData["GameId"] = new SelectList(_context.Games, "Id", "OpponentTeam", attendanceSheet.GameId);
            ViewData["PlayerId"] = new SelectList(_context.Players, "Id", "Name", attendanceSheet.PlayerId);
            return View(attendanceSheet);
        }

        // POST: AttendanceSheets/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Attend,PriceToPayPerPlayer,MatchGoal,PlayerId,GameId")] AttendanceSheet attendanceSheet)
        {
            if (id != attendanceSheet.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(attendanceSheet);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AttendanceSheetExists(attendanceSheet.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["GameId"] = new SelectList(_context.Games, "Id", "OpponentTeam", attendanceSheet.GameId);
            ViewData["PlayerId"] = new SelectList(_context.Players, "Id", "Name", attendanceSheet.PlayerId);
            return View(attendanceSheet);
        }

        // GET: AttendanceSheets/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.AttendanceSheets == null)
            {
                return NotFound();
            }

            var attendanceSheet = await _context.AttendanceSheets
                .Include(a => a.Game)
                .Include(a => a.Player)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (attendanceSheet == null)
            {
                return NotFound();
            }

            return View(attendanceSheet);
        }

        // POST: AttendanceSheets/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.AttendanceSheets == null)
            {
                return Problem("Entity set 'MatchAnalyzerDbContext.AttendanceSheets'  is null.");
            }
            var attendanceSheet = await _context.AttendanceSheets.FindAsync(id);
            if (attendanceSheet != null)
            {
                _context.AttendanceSheets.Remove(attendanceSheet);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AttendanceSheetExists(int id)
        {
          return (_context.AttendanceSheets?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
