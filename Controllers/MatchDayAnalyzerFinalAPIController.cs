using Microsoft.AspNetCore.Mvc;

namespace MatchDayAnalyzerFinal.Controllers
{
    public class MatchDayAnalyzerFinalAPIController : Controller
    {

        public MatchDayAnalyzerFinalAPIController()
        {

        }
        public IActionResult Index()
        {
            return View();
        }
    }
}
