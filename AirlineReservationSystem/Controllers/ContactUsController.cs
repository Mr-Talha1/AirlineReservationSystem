using Microsoft.AspNetCore.Mvc;

namespace AirlineReservationSystem.Controllers
{
	public class ContactUsController : Controller
	{
		public IActionResult Index()
		{
			return View();
		}
	}
}
