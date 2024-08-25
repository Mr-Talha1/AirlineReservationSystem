using Microsoft.AspNetCore.Mvc;

namespace AirlineReservationSystem.Controllers
{
    public class DashboardController : Controller
    {
        public IActionResult Index()
        {
            return View();
        } 
        public IActionResult MyBooking()
        {
            return View();
        }
        public IActionResult MyProfile()
        {
            return View();
        }public IActionResult Setting()
        {
            return View();
        }
    }
}
