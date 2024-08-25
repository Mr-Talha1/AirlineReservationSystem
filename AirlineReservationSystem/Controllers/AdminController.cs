using Microsoft.AspNetCore.Mvc;

namespace AirlineReservationSystem.Controllers
{
    public class AdminController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
