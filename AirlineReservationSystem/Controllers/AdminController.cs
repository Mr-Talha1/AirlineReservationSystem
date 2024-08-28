using Microsoft.AspNetCore.Mvc;

namespace AirlineReservationSystem.Controllers
{
    public class AdminController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Add_Flight()
        {
            return View();
        }
        public IActionResult List_Of_User()
        {
            return View();
        }
        public IActionResult List_Of_Flighs()
        {
            return View();
        }

        public IActionResult List_Of_Cancel_Flights()
        {
            return View();
        }
    }
}
