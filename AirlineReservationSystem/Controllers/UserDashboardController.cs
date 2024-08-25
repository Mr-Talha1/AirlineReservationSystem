using Microsoft.AspNetCore.Mvc;

namespace AirlineReservationSystem.Controllers
{
    public class UserDashboardController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
