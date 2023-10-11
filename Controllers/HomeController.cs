﻿using MatchDayAnalyzerFinal.Data;
using MatchDayAnalyzerFinal.Models;
using MatchDayAnalyzerFinal.Models.ClassModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace MatchDayAnalyzerFinal.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly MatchAnalyzerDbContext _context;

        public HomeController(ILogger<HomeController> logger, MatchAnalyzerDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        public IActionResult ScoredMoreGoalsThan(int? goalsSearch)
        {
            if (!goalsSearch.HasValue)
            {
                return BadRequest("You must pass a product price in the query string for example, /Home/ProductsThatCostMoreThan?price=50");
            }

            IEnumerable<Player> model = _context.Players
            .Include(p => p.Team)
            .Where(p => p.TotalGoals > goalsSearch)
            .ToList();

            if (!model.Any())
            {
                return NotFound(
                    $"No Player has scored more than {goalsSearch}.");
            }

            ViewData["MaxGoals"] = goalsSearch.Value.ToString("C");

            return View(model);
        }
    }
}