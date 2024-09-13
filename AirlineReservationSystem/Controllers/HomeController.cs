using System.Diagnostics;
using AirlineReservationSystem.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace AirlineReservationSystem.Controllers
{
    public class HomeController : Controller
    {

        private readonly Models.AirlineReservationSystemContext _context;
  
        public HomeController(Models.AirlineReservationSystemContext context)
        {
            _context = context;
            
        }

        [HttpGet]
        public JsonResult GetCities(string term)
        {
            var cities = _context.Cities
                                .Where(c => c.CityName.Contains(term))
                                .Select(c => c.CityName)
                                .ToList();

            return Json(cities);
        }
        //private readonly ILogger<HomeController> _logger;

        //public HomeController(ILogger<HomeController> logger)
        //{
        //    _logger = logger;
        //}

        public IActionResult Index()
        {
            ViewData["CoachID"] = new SelectList(_context.Coaches, "CoachId", "CoachName");
            return View();
        }

        public IActionResult AccessDenied()
        {
            return View();
        }

        public IActionResult FlightSearchResult()
        {
            return View();
        }
        public IActionResult FlightDetails()
        {
            return View();
        }public IActionResult FlightBooking()
        {
            return View();
        }

        



        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
