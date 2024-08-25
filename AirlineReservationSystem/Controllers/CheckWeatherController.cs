using Microsoft.AspNetCore.Mvc;

namespace AirlineReservationSystem.Controllers
{
    public class CheckWeatherController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
