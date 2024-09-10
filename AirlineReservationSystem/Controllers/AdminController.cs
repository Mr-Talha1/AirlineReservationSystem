using System;
using AirlineReservationSystem.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AirlineReservationSystem.Controllers
{
    public class AdminController : Controller
    {
        private readonly Models.AirlineReservationSystemContext _context;
        private readonly IWebHostEnvironment environment;
        public AdminController(Models.AirlineReservationSystemContext context,IWebHostEnvironment environment)
        {
            _context = context;
            this.environment = environment;
        }
        //============================dashboard========================
        public IActionResult Index()
        {
            var usercount = _context.Users.Count();
            return View(usercount);
        }

        //============================Users========================
        public IActionResult List_Of_User()
        {
            var userdata = _context.Users.OrderByDescending(x=> x.UserId).ToList();
            return View(userdata);
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
            //DeleteAirline user image
            string imageFullPath = environment.WebRootPath + "/image/" + user_dats.ImagePath;
            System.IO.File.Delete(imageFullPath);
            _context.SaveChanges(true);
            return RedirectToAction("List_Of_User", "Admin");

        }



        //============================Classes========================

        public IActionResult Add_Class()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Add_Class(Class Class)
        {
            var airlinedata = _context.Classes.FirstOrDefault(u => u.ClassName == Class.ClassName);
            if (airlinedata != null)
            {
                TempData["ClassError"] = "Class already exist";
                return View(Class);
                //ModelState.AddModelError("ClassName", "Class already exist.");
}

            if (!ModelState.IsValid)
            {

                return View("Add_Class", Class);
            }
            _context.Classes.Add(Class);
            _context.SaveChanges();
            //return View("List_Of_Classes");
            return RedirectToAction("List_Of_Classes", "Admin");


        }
        public IActionResult List_Of_Classes()
        {
            var classData=_context.Classes.OrderByDescending(x=> x.ClassId).ToList();
            return View(classData);
        }

        public IActionResult EditClass(int id)
        {
            var Classdata = _context.Classes.FirstOrDefault(x => x.ClassId == id);
            return View(Classdata);

        }

        [HttpPost]
        public IActionResult EditClass(Class clas)
        {
            var Classedata = _context.Classes.FirstOrDefault(x => x.ClassId == clas.ClassId);
            if (Classedata == null)
            {
                return NotFound();
            }
            if (!ModelState.IsValid)
            {
                //ClassName
                return RedirectToAction("EditClass", clas);

            }
            var checkeClass = _context.Classes.FirstOrDefault(x => x.ClassName == clas.ClassName);

            var newclassName = Classedata.ClassName;
            if(checkeClass == null)
            {
                newclassName = clas.ClassName;
               
            }
            else
            {
                TempData["ClassError"] = "Class already exist";
               return View(clas);
            }
            Classedata.ClassName = newclassName;
            _context.SaveChanges();
            return RedirectToAction("List_Of_Classes", "Admin");

        }

        public IActionResult DeleteClass(int id)
        {
            var class_data = _context.Classes.Find(id);
            if (class_data == null)
            {
                return NotFound();
            }
            _context.Classes.Remove(class_data);
            _context.SaveChanges(true);
            return RedirectToAction("List_Of_Classes", "Admin");

        }

        //============================Airline========================

        public IActionResult Add_Airline()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Add_Airline(Airline airline)
        {
            var airlinedata=_context.Airlines.FirstOrDefault(u=> u.AirlineName==airline.AirlineName);
            if (airlinedata != null)
            {
                TempData["airlineError"] = "airline already exist";
                return View(airline);
            }
            if (!ModelState.IsValid)
            {
          
                return View("Add_Airline", airline);
            }
           var newfileName = DateTime.Now.ToString("yyyMMddHHmmssfff");
            newfileName += Path.GetExtension(airline.AilrineImagePath.FileName);
            string imageFullPath = environment.WebRootPath + "/image/" + newfileName;
            using (var stream = System.IO.File.Create(imageFullPath))
            {
               
                airline.AilrineImagePath.CopyTo(stream);
            }
            airline.ImagePath = newfileName;
            _context.Airlines.Add(airline);
            _context.SaveChanges();
            return RedirectToAction("List_Of_Airlines", "Admin");
        }
        public IActionResult List_Of_Airlines()
        {
            
            var airlinedata = _context.Airlines.OrderByDescending(x => x.AirlineId).ToList();
            return View(airlinedata);
           
        }


        public IActionResult EditAirline(int id)
        {
           var airlinedata=_context.Airlines.FirstOrDefault(x=> x.AirlineId==id);
            return View(airlinedata);

        }
        [HttpPost]
        public IActionResult EditAirline(Airline airline)
        {
            var airlinedata = _context.Airlines.FirstOrDefault(x => x.AirlineId == airline.AirlineId);
            if (airlinedata == null)
            {
                return NotFound();
            }

            var checkeAirline = _context.Airlines.FirstOrDefault(x => x.AirlineName == airline.AirlineName);

            if (!ModelState.IsValid)
            {

                return RedirectToAction("EditAirline", airline);

            }


          
                string newfileName = airlinedata.ImagePath;

                // udpdate ArilineName  if you we a have new ArilineName
                       string newArilineName = airlinedata.AirlineName;

                if (airline.AilrineImagePath != null)
                {
                    newfileName = DateTime.Now.ToString("yyyMMddHHmmssfff");
                    newfileName += Path.GetExtension(airline.AilrineImagePath.FileName);
                    string imageFullPath = environment.WebRootPath + "/image/" + newfileName;
                    using (var stream = System.IO.File.Create(imageFullPath))
                    {
                        airline.AilrineImagePath.CopyTo(stream);
                    }
                    //delete old image file
                  
                        string oldImagePath = environment.WebRootPath + "/image/" + airlinedata.ImagePath;
                        System.IO.File.Delete(oldImagePath);
                }
                if (checkeAirline == null)
                {
                    newArilineName = airline.AirlineName;
                }

            else
            {
                TempData["AilineName"] = airlinedata.AirlineName;
                TempData["AilineError"] = "Ailine Name don't Update already exist";
            }

            airlinedata.AirlineName = newArilineName;

                airlinedata.ImagePath = newfileName;

               
                _context.SaveChanges();
                return RedirectToAction("List_Of_Airlines", "Admin");

        }
        
      
        public IActionResult DeleteAirline(int id)
        {
            var airline_data = _context.Airlines.Find(id);
            if (airline_data == null)
            {
                return NotFound();
            }
            _context.Airlines.Remove(airline_data);
            //DeleteAirline user image
            string imageFullPath = environment.WebRootPath + "/image/" + airline_data.ImagePath;
            System.IO.File.Delete(imageFullPath);
            _context.SaveChanges(true);
            return RedirectToAction("List_Of_Airlines", "Admin");

        }
        //============================Cities========================
        //Add_Cities
        public IActionResult Add_Cities()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Add_Cities(City city)
        {
            var citydata = _context.Cities.FirstOrDefault(u => u.CountryName == city.CountryName && u.CityName==city.CityName);
            if (citydata != null)
            {
                TempData["cityError"] = "This Country Already Have this City";
                return View(city);
            }

            if (!ModelState.IsValid)
            {
                return View("Add_Cities", city);
            }
            _context.Cities.Add(city);
            _context.SaveChanges();
            //return View("List_Of_Classes");
            return RedirectToAction("List_Of_Cities", "Admin");
        } 
        
        public IActionResult List_Of_Cities()
        {
            var CityData = _context.Cities.OrderByDescending(x => x.CityId).ToList();
            return View(CityData);
           
        }

        public IActionResult EditCity(int id)
        {
            var Citydata = _context.Cities.FirstOrDefault(x => x.CityId == id);
            return View(Citydata);

        }
        [HttpPost]
        public IActionResult EditCity(City city)
        {
            var citydata = _context.Cities.FirstOrDefault(x => x.CityId == city.CityId);
            if (citydata == null)
            {
                return NotFound();
            }
            if (!ModelState.IsValid)
            {
                //ClassName
                return RedirectToAction("EditCity", city);

            }
            var checkeCity = _context.Cities.FirstOrDefault(x => x.CountryName == city.CountryName && x.CityName == city.CityName);

            var newCountryName = citydata.CountryName;
            var newCityName = citydata.CityName;
            if (checkeCity == null)
            {
                newCountryName = city.CountryName;
                newCityName = city.CityName;

            }
            else
            {
                TempData["cityError"] = "This Country Already Have this City";
                return View(city);
            }
            citydata.CountryName = newCountryName;
            citydata.CityName = newCityName;

            _context.SaveChanges();
            return RedirectToAction("List_Of_Cities", "Admin");

        }


        //public IActionResult DeleteCity(int id)
        //{
        //    var city_data = _context.Cities.Find(id);
        //    if (city_data == null)
        //    {
        //        return NotFound();
        //    }
        //    _context.Cities.Remove(city_data);
        //    _context.SaveChanges(true);
        //    return RedirectToAction("List_Of_Cities", "Admin");

        //}

        public async Task<IActionResult> DeleteCity(int id)
        {
            // Find the city to delete
            var city = await _context.Cities.FindAsync(id);
            if (city == null)
            {
                return NotFound();
            }

            // Find and remove flights with the city as origin
            var originFlights = _context.Flights.Where(f => f.OriginCityId == id);
            _context.Flights.RemoveRange(originFlights);

            // Find and remove flights with the city as destination
            var destinationFlights = _context.Flights.Where(f => f.DestinationCityId == id);
            _context.Flights.RemoveRange(destinationFlights);

            // Remove the city
            _context.Cities.Remove(city);

            // Save changes to the database
            await _context.SaveChangesAsync();

            return RedirectToAction("List_Of_Cities", "Admin");
        }

        //============================Flights========================

        public IActionResult Add_Flight()
        {
            return View();
        }
        public IActionResult List_Of_Flighs()
        {
            var userdata = _context.Users.OrderByDescending(x => x.UserId).ToList();
            return View(userdata);
        }

        public IActionResult List_Of_Cancel_Flights()
        {
            return View();
        }

        
    }
}
//changes