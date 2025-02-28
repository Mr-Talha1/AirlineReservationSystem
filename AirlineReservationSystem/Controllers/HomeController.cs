using System.Diagnostics;
using AirlineReservationSystem.Models;
using Microsoft.AspNetCore.Mvc;

namespace AirlineReservationSystem.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
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
