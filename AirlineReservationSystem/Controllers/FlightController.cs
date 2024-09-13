using AirlineReservationSystem.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.CodeAnalysis.Options;
using Microsoft.EntityFrameworkCore;

namespace AirlineReservationSystem.Controllers
{
    public class FlightController : Controller
    {
        private readonly AirlineReservationSystemContext _context;

        public FlightController(AirlineReservationSystemContext context)
        {
            _context = context;
        }

        //        public IActionResult FlightSearchResult(string FlyingFrom, string Flyingto, DateTime flightdate)
        //        {

        //            var flights = (from flight in _context.Flights
        //                       join originCity in _context.Cities on flight.OriginCityId equals originCity.CityId
        //                       join destCity in _context.Cities on flight.DestinationCityId equals destCity.CityId
        //                       where originCity.CityName == FlyingFrom &&
        //                             destCity.CityName == Flyingto &&
        //                             flight.DepartureTime.Date == flightdate.Date
        //                       select flight).ToList();


        //            if (!flights.Any())
        //            {
        //                ViewBag.NoFlightsFound = true; // Set flag if no flights are found
        //            }
        //            return View(flights);
        //}





        [HttpGet]
        public IActionResult FlightSearchResult(string FlyingFrom, string Flyingto, DateTime flightdate, int? couches)
        {
            if (couches != null)
            {
                // Find flights based on input criteria
                var cflights = _context.Flights
                    .Include(f => f.Airline)
                    .Include(f => f.OriginCity)
                    .Include(f => f.DestinationCity)
                    .Include(f => f.Coach)
                    .Where(f => f.OriginCity.CityName == FlyingFrom &&
                                f.DestinationCity.CityName == Flyingto &&
                                f.DepartureTime.Date == flightdate.Date&&
                                f.Coach.CoachId==couches &&
                            f.FlightType == "One Way")
                    .ToList();
                ViewData["CoachID"] = new SelectList(_context.Coaches, "CoachId", "CoachName");
                ViewData["OriginName"] = FlyingFrom;
                ViewData["DestinationName"] = Flyingto;
                ViewData["DestinationDate"] = flightdate;
                ViewData["DestinationFlightCount"] = cflights.Count;
                if (cflights.Count == 0)
                {
                    ViewBag.Error = "No flights found.";
                }

                return View(cflights);
            }
            // Find flights based on input criteria
            var flights = _context.Flights
                .Include(f => f.Airline)
                .Include(f => f.OriginCity)
                .Include(f => f.DestinationCity)
                .Include(f => f.Coach)
                .Where(f => f.OriginCity.CityName == FlyingFrom &&
                            f.DestinationCity.CityName == Flyingto &&
                            f.DepartureTime.Date == flightdate.Date&&
                            f.FlightType== "One Way")
                .ToList();
            ViewData["CoachID"] = new SelectList(_context.Coaches, "CoachId", "CoachName");
            ViewData["OriginName"] = FlyingFrom;
            ViewData["DestinationName"]= Flyingto;
            ViewData["DestinationDate"]= flightdate;
            ViewData["DestinationFlightCount"]= flights.Count;
            if (flights.Count == 0)
            {
                ViewBag.Error = "No flights found.";
            }

            return View(flights);
        }


        //[HttpGet]
        //public IActionResult RoundTripFlightSearch(string FlyingFrom, string Flyingto, DateTime DepartingDate, int? couches)
        //{
        //    if (couches != null)
        //    {
        //        // Find flights based on input criteria
        //        var cflights = _context.Flights
        //            .Include(f => f.Airline)
        //            .Include(f => f.OriginCity)
        //            .Include(f => f.DestinationCity)
        //            .Include(f => f.Coach)
        //            .Where(f => f.OriginCity.CityName == FlyingFrom &&
        //                        f.DestinationCity.CityName == Flyingto &&
        //                        f.DepartureTime.Date == flightdate.Date &&
        //                        f.Coach.CoachId == couches &&
        //                    f.FlightType == "One Way")
        //            .ToList();
        //        ViewData["CoachID"] = new SelectList(_context.Coaches, "CoachId", "CoachName");
        //        ViewData["OriginName"] = FlyingFrom;
        //        ViewData["DestinationName"] = Flyingto;
        //        ViewData["DestinationDate"] = flightdate;
        //        ViewData["DestinationFlightCount"] = cflights.Count;
        //        if (cflights.Count == 0)
        //        {
        //            ViewBag.Error = "No flights found.";
        //        }

        //        return View(cflights);
        //    }
        //    // Find flights based on input criteria
        //    var flights = _context.Flights
        //        .Include(f => f.Airline)
        //        .Include(f => f.OriginCity)
        //        .Include(f => f.DestinationCity)
        //        .Include(f => f.Coach)
        //        .Where(f => f.OriginCity.CityName == FlyingFrom &&
        //                    f.DestinationCity.CityName == Flyingto &&
        //                     f.FlightType == "One Way" &&
        //                    f.ReturnDepartureTime.Date == DepartingDate.Date 
        //                   )
        //        .ToList();
        //    ViewData["CoachID"] = new SelectList(_context.Coaches, "CoachId", "CoachName");
        //    ViewData["OriginName"] = FlyingFrom;
        //    ViewData["DestinationName"] = Flyingto;
        //    ViewData["DestinationDate"] = flightdate;
        //    ViewData["DestinationFlightCount"] = flights.Count;
        //    if (flights.Count == 0)
        //    {
        //        ViewBag.Error = "No flights found.";
        //    }

        //    return View(flights);
        //}



        //[HttpGet]
        //public IActionResult SearchFlights(string flyingFrom, string flyingTo, DateTime flightDate)
        //{
        //    var flights = (from flight in _context.Flights
        //                   join originCity in _context.Cities on flight.OriginCityId equals originCity.CityId into originJoin
        //                   from originCity in originJoin.DefaultIfEmpty()
        //                   join destCity in _context.Cities on flight.DestinationCityId equals destCity.CityId into destJoin
        //                   from destCity in destJoin.DefaultIfEmpty()
        //                   join airline in _context.Airlines on flight.AirlineId equals airline.AirlineId into airlineJoin
        //                   from airline in airlineJoin.DefaultIfEmpty()
        //                   join coach in _context.Coaches on flight.CoachId equals coach.CoachId into coachJoin
        //                   from coach in coachJoin.DefaultIfEmpty()
        //                   where originCity.CityName == flyingFrom &&
        //                         destCity.CityName == flyingTo &&
        //                         flight.DepartureTime.Date == flightDate.Date
        //                   select new Flight
        //                   {
        //                       FlightId = flight.FlightId,
        //                       FlightNumber = flight.FlightNumber,
        //                       DepartureTime = flight.DepartureTime,
        //                       ArrivalTime = flight.ArrivalTime,
        //                       Price = flight.Price,
        //                       AirlineName = airline.AirlineName,
        //                       AirlineImagePath = airline.ImagePath,
        //                       CoachName = coach.CoachName,
        //                       OriginCityName = originCity.CityName,
        //                       DestinationCityName = destCity.CityName
        //                   }).ToList();

        //    if (!flights.Any())
        //    {
        //        ViewBag.NoFlightsFound = true;
        //    }

        //    return View(flights);
        //}



    }
}
