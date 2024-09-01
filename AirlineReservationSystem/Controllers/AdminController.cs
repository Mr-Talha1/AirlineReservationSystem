using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AirlineReservationSystem.Controllers
{
    public class AdminController : Controller
    {
        private readonly Models.AirlineReservationSystemContext _context;
     
        public AdminController(Models.AirlineReservationSystemContext context)
        {
            _context = context;
        
        }

        public IActionResult Index()
        {
            var usercount = _context.Users.Count();
            return View(usercount);
        }
        public IActionResult Add_Flight()
        {
            return View();
        }
        public IActionResult List_Of_User()
        {
            var userdata = _context.Users.OrderByDescending(x=> x.UserId);
            return View(userdata);
        }
        public IActionResult List_Of_Flighs()
        {
            return View();
        }

        public IActionResult List_Of_Cancel_Flights()
        {
            return View();
        }


        //Delete user
        public IActionResult DeleteUser(int id)
        {
            var user_dats = _context.Users.Find(id);
            if (user_dats == null)
            {
                return NotFound();
            }
            _context.Users.Remove(user_dats);
            _context.SaveChanges(true);
            return RedirectToAction("List_Of_User", "Admin");

        }
    }
}
