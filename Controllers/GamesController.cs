using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MatchDayAnalyzerFinal.Data;
using MatchDayAnalyzerFinal.Models.ClassModels;
using MatchDayAnalyzerFinal.Models.ViewModels;


namespace MatchDayAnalyzerFinal.Controllers
{
    public class GamesController : Controller
    {
        private readonly MatchAnalyzerDbContext _context;

        public GamesController(MatchAnalyzerDbContext context)
        {
            _context = context;
        }

        // GET: Games
        public async Task<IActionResult> Index()
        {
              return _context.Games != null ? 
                          View(await _context.Games.ToListAsync()) :
                          Problem("Entity set 'MatchAnalyzerDbContext.Games'  is null.");
        }



        // GET: Games/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Games == null)
            {
                return NotFound();
            }

            var game = await _context.Games
                .Include(t => t.TeamsPlayedGame)
                .Include(t => t.AttendanceSheets)
                    .ThenInclude(t => t.Player)
                .FirstOrDefaultAsync(m => m.Id == id);
                
            if (game == null)
            {
                return NotFound();
            }

            return View(game);
        }

        // GET: Games/Create
        public IActionResult Create()
        {
            var gameViewModel = new GameViewModel { };
            return View();
        }

        // POST: Games/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,DateTime,OpponentTeam,HomeTeamScore,AwayTeamScore")] GameViewModel gameUpdate)
        {
            if (ModelState.IsValid)
            {
                Game CreateGame = new Game()
                {
                    Id = gameUpdate.Id,
                    DateTime = gameUpdate.DateTime,
                    OpponentTeam = gameUpdate.OpponentTeam,
                    HomeTeamScore = gameUpdate.HomeTeamScore,
                    AwayTeamScore = gameUpdate?.AwayTeamScore
                };
                AddOrUpdateTeams(CreateGame, gameUpdate.TeamsPlayedGame);
                _context.Add(CreateGame);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
                    
                
            }
            
            return View(gameUpdate);
        }

        // GET: Games/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Games == null)
            {
                return NotFound();
            }

            var game = await _context.Games.FindAsync(id);
            if (game == null)
            {
                return NotFound();
            }
            GameViewModel viewmodel = new()
            {
                Id = game.Id,
                DateTime = game.DateTime,
                OpponentTeam = game.OpponentTeam,
                HomeTeamScore = game.HomeTeamScore,
                AwayTeamScore = game?.AwayTeamScore
            };

            return View(game);
        }

        // POST: Games/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,DateTime,OpponentTeam,HomeTeamScore,AwayTeamScore")] GameViewModel gameUpdate)
        {
            if (id != gameUpdate.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    Game? gameEdit = await _context.Games.FindAsync(id);
                    gameEdit.DateTime = gameUpdate.DateTime;
                    gameEdit.OpponentTeam= gameUpdate.OpponentTeam;
                    gameEdit.HomeTeamScore = gameUpdate.HomeTeamScore;
                    gameEdit.AwayTeamScore = gameUpdate.AwayTeamScore;
                    AddOrUpdateTeams(gameEdit, gameUpdate.TeamsPlayedGame);
                    _context.Update(gameEdit);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!GameExists(gameUpdate.Id))
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
            return View(gameUpdate);
        }

        // GET: Games/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Games == null)
            {
                return NotFound();
            }

            var game = await _context.Games
                .FirstOrDefaultAsync(m => m.Id == id);
            if (game == null)
            {
                return NotFound();
            }

            return View(game);
        }

        // POST: Games/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Games == null)
            {
                return Problem("Entity set 'MatchAnalyzerDbContext.Games'  is null.");
            }
            var game = await _context.Games.FindAsync(id);
            if (game != null)
            {
                _context.Games.Remove(game);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool GameExists(int id)
        {
          return (_context.Games?.Any(e => e.Id == id)).GetValueOrDefault();
        }

        private void AddOrUpdateTeams(Game game, IEnumerable<AssignedTeamViewModel> assignedTeams)
        {
            if (assignedTeams != null)
            {
                foreach (var assignedTeam in assignedTeams)
                {
                    if (assignedTeam.Assigned)
                    {
                        var newTeam = new Team { Id = assignedTeam.Id, Name = assignedTeam.OpponentTeam };
                        _context.Teams.Attach(newTeam);
                        game.TeamsPlayedGame.Add(newTeam);
                    }
                }
            }
        }
    }
}
