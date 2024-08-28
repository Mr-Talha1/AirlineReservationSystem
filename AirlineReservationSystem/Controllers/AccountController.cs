using System.Security.Claims;
using AirlineReservationSystem.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AirlineReservationSystem.Controllers
{
    public class AccountController : Controller
    {
        private readonly AirlineReservationSystemContext _context;

        public AccountController(AirlineReservationSystemContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<IActionResult> SignUp(User user)
        {
            if (ModelState.IsValid)
            {
                if (await _context.Users.AnyAsync(u => u.Email == user.Email))
                {
                    return RedirectToAction("Index", "Home");
                }

                user.ImagePath = "user_placeholder.jpg";
                user.CreatedAt = DateTime.Now;
                _context.Users.Add(user);
                await _context.SaveChangesAsync();

                await SignInUser(user);//
                return RedirectToAction("Index", "Home");
            }

            return RedirectToAction("Index", "Home");
        }


        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index", "Home");
        }
        [HttpPost]
        public async Task<IActionResult> SignIn(string email, string password)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == email && u.Password == password);

            if (user != null)
            {
                await SignInUser(user);
                return RedirectToAction("Index", "Home");
            }


            return RedirectToAction("Index", "Home");
        }

        private async Task SignInUser(User user)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.Username),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.NameIdentifier, user.UserId.ToString()) // Add User ID claim
            };

            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var authProperties = new AuthenticationProperties
            {
                IsPersistent = true,
                ExpiresUtc = DateTimeOffset.UtcNow.AddMinutes(30)
            };

            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity), authProperties);
        }
    }
}
