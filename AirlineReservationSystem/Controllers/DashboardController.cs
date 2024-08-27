using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AirlineReservationSystem.Controllers
{
    public class DashboardController : Controller
    {
        private readonly Models.AirlineReservationSystemContext _context;

        public DashboardController(Models.AirlineReservationSystemContext context)
        {
            _context = context;
        }
         public async Task<IActionResult> Index()
        {
            
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
            if (userIdClaim == null)
            {
                return Unauthorized();
            }
            var userId = int.Parse(userIdClaim.Value);
            var userdata = await _context.Users.FirstOrDefaultAsync(u => u.UserId == userId);
            ViewData["userId"] = userId;
            ViewData["ImagePath"] = userdata.ImagePath;
            ViewData["UserName"] = userdata.Username;
            //var d = userdata.CreatedAt.ToString('MM/dd');
            ViewData["CreatedAt"] = userdata.CreatedAt;
            return View(userdata);
            
        }
        public IActionResult MyBooking()
        {
           
            return View();
        }
        public async Task<IActionResult> MyProfile()

        { var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
            if (userIdClaim == null)
            {
                return Unauthorized();
            }
            var userId = int.Parse(userIdClaim.Value);
            var userdata = await _context.Users.FirstOrDefaultAsync(u => u.UserId == userId);
            ViewData["userId"] = userId;
            ViewData["ImagePath"] = userdata.ImagePath;
            ViewData["UserName"] = userdata.Username;
            ViewData["CreatedAt"] = userdata.CreatedAt;
            return View(userdata);

        }
        public IActionResult Setting()
        {
            return View();
        }
    }
}
