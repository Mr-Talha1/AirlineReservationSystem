using System.Security.Claims;
using AirlineReservationSystem.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using static System.Net.Mime.MediaTypeNames;

namespace AirlineReservationSystem.Controllers
{
    public class DashboardController : Controller
    {
        private readonly Models.AirlineReservationSystemContext _context;
        private readonly IWebHostEnvironment environment;
        public DashboardController(Models.AirlineReservationSystemContext context, IWebHostEnvironment environment)
        {
            _context = context;
            this.environment = environment;
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
        public async Task<IActionResult> Setting()
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
            ViewData["CreatedAt"] = userdata.CreatedAt;
            return View(userdata);
        }
        //=================================update user profile
        [HttpPost]
        public IActionResult UpdateProfile(User user)
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
            if (userIdClaim == null)
            {
                return Unauthorized();
            }
            var userId = int.Parse(userIdClaim.Value);

            var userdata = _context.Users.FirstOrDefault(u => u.UserId == userId);

            if (userdata == null)
            {
                return RedirectToAction("Setting", "Dashboard");
            }
            //user.Password = userdata.Password;
            if (!ModelState.IsValid)
            {
                ViewData["userId"] = userId;
                ViewData["ImagePath"] = userdata.ImagePath;
                ViewData["UserName"] = userdata.Username;
                ViewData["CreatedAt"] = userdata.CreatedAt;
                user.ImagePath = userdata.ImagePath;
                return View("setting", user);
            }

            // udpdate image file if you we a have new image file
            string newfileName = userdata.ImagePath;
            if(user.UserImagePath != null)
            {
                newfileName = DateTime.Now.ToString("yyyMMddHHmmssfff");
                newfileName += Path.GetExtension(user.UserImagePath.FileName);
                string imageFullPath = environment.WebRootPath+"/image/"+newfileName;
                using (var stream = System.IO.File.Create(imageFullPath))
                {
                    user.UserImagePath.CopyTo(stream);
                }
                //delete old image file
                if (userdata.ImagePath != "user_placeholder.jpg") {
                    string oldImagePath = environment.WebRootPath + "/image/" + userdata.ImagePath;
                    System.IO.File.Delete(oldImagePath);
                }
                
            }
            //user.ImagePath = newfileName;
            //user.Password = userdata.Password;

            userdata.Username = user.Username;
            userdata.Email = user.Email;
            userdata.Address = user.Address;
            userdata.PhoneNumber = user.PhoneNumber;
            userdata.ImagePath = newfileName;
            userdata.Age = user.Age;
            userdata.Gender=user.Gender;

            //_context.Users.Add(user);
            _context.SaveChanges();
            return RedirectToAction("setting", "Dashboard");
        }

        public async Task<IActionResult> UpdateImage()
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
            if (userIdClaim == null)
            {
                return Unauthorized();
            }
            var userId = int.Parse(userIdClaim.Value);

            var userdata = await _context.Users.FirstOrDefaultAsync(u => u.UserId == userId);

            string imageFullPath = environment.WebRootPath + "/image/" + userdata.ImagePath;
            if (userdata.ImagePath != "user_placeholder.jpg")
            {
                System.IO.File.Delete(imageFullPath);

                userdata.ImagePath = "user_placeholder.jpg";
                _context.SaveChanges();
                return RedirectToAction("setting", "Dashboard");
            }
            return RedirectToAction("setting", "Dashboard");
        }

        
    }
}
