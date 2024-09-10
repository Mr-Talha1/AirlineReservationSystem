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
                    TempData["RegisterAccount"] = "signupPopupForm";
                    return RedirectToAction("Index", "Home", user);
                }

                user.ImagePath = "user_placeholder.jpg";
                user.CreatedAt = DateTime.Now;
                _context.Users.Add(user);
                await _context.SaveChangesAsync();

                await SignInUser(user);//
                return RedirectToAction("Index", "Home");
            }
            else
            {
                //TempData["RegisterAccount"] = "signupPopupForm";
                TempData["Regmodelstate"] = "modelstate";
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
            if(email=="abc@gamil.com" && password == "123")
            {
                var adminEmail = email;
                await SignInAdmin(adminEmail);
                return RedirectToAction("Index", "Home");
            }


            if (ModelState.IsValid)
            {

                var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == email && u.Password == password);

                if (user != null)
                {
                    await SignInUser(user);
                    return RedirectToAction("Index", "Home");
                }
                else
                {

                    TempData["SingInAccount"] = "signupPopupForm";
                }

            }
            else
            {
                TempData["singmodelstate"] = "modelstate";
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
            //// Check if the user is the admin
            //if (user.Email == "flynowadmin@gmail.com")
            //{
            //    claims.Add(new Claim(ClaimTypes.Role, "Admin")); // Add Admin role claim
            //}

            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var authProperties = new AuthenticationProperties
            {
                IsPersistent = true,
                ExpiresUtc = DateTimeOffset.UtcNow.AddMinutes(30)
            };

            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity), authProperties);
        }



        private async Task SignInAdmin(string AdminEmail)
        {
            var claims = new List<Claim>
          {
       
        new Claim(ClaimTypes.Email, AdminEmail),
         new Claim(ClaimTypes.Role,"Admin") // Add admin ID claim
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
