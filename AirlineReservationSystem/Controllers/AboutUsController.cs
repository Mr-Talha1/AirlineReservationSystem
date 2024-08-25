using Microsoft.AspNetCore.Mvc;

namespace AirlineReservationSystem.Controllers
{
	public class AboutUsController : Controller
	{
		public IActionResult Index()
		{
			return View();
		}
	}
}
