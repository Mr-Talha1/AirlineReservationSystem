using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AirlineReservationSystem.Models;

namespace AirlineReservationSystem.Controllers
{
    public class FlightsController : Controller
    {
        private readonly AirlineReservationSystemContext _context;

        public FlightsController(AirlineReservationSystemContext context)
        {
            _context = context;
        }

        // GET: Flights
        public async Task<IActionResult> Index()
        {
            var airlineReservationSystemContext = _context.Flights.Include(f => f.Airline).Include(f => f.Class).Include(f => f.DestinationCity).Include(f => f.OriginCity);
            return View(await airlineReservationSystemContext.ToListAsync());
        }

        // GET: Flights/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var flight = await _context.Flights
                .Include(f => f.Airline)
                .Include(f => f.Class)
                .Include(f => f.DestinationCity)
                .Include(f => f.OriginCity)
                .FirstOrDefaultAsync(m => m.FlightId == id);
            if (flight == null)
            {
                return NotFound();
            }

            return View(flight);
        }

        // GET: Flights/Create
        public IActionResult Create()
        {
            ViewData["AirlineId"] = new SelectList(_context.Airlines, "AirlineId", "AirlineId");
            ViewData["ClassId"] = new SelectList(_context.Classes, "ClassId", "ClassId");
            ViewData["DestinationCityId"] = new SelectList(_context.Cities, "CityId", "CityId");
            ViewData["OriginCityId"] = new SelectList(_context.Cities, "CityId", "CityId");
            return View();
        }

        // POST: Flights/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Flight flight)
        {
            if (ModelState.IsValid)
            {
                _context.Add(flight);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["AirlineId"] = new SelectList(_context.Airlines, "AirlineId", "AirlineId", flight.AirlineId);
            ViewData["ClassId"] = new SelectList(_context.Classes, "ClassId", "ClassId", flight.ClassId);
            ViewData["DestinationCityId"] = new SelectList(_context.Cities, "CityId", "CityId", flight.DestinationCityId);
            ViewData["OriginCityId"] = new SelectList(_context.Cities, "CityId", "CityId", flight.OriginCityId);
            return View(flight);
        }

        // GET: Flights/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var flight = await _context.Flights.FindAsync(id);
            if (flight == null)
            {
                return NotFound();
            }
            ViewData["AirlineId"] = new SelectList(_context.Airlines, "AirlineId", "AirlineId", flight.AirlineId);
            ViewData["ClassId"] = new SelectList(_context.Classes, "ClassId", "ClassId", flight.ClassId);
            ViewData["DestinationCityId"] = new SelectList(_context.Cities, "CityId", "CityId", flight.DestinationCityId);
            ViewData["OriginCityId"] = new SelectList(_context.Cities, "CityId", "CityId", flight.OriginCityId);
            return View(flight);
        }

        // POST: Flights/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("FlightId,FlightNumber,AirlineId,OriginCityId,DestinationCityId,DepartureTime,ArrivalTime,TotalSeats,AvailableSeats,ClassId,SkyMiles,Stops,Price,CreatedAt")] Flight flight)
        {
            if (id != flight.FlightId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(flight);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FlightExists(flight.FlightId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["AirlineId"] = new SelectList(_context.Airlines, "AirlineId", "AirlineId", flight.AirlineId);
            ViewData["ClassId"] = new SelectList(_context.Classes, "ClassId", "ClassId", flight.ClassId);
            ViewData["DestinationCityId"] = new SelectList(_context.Cities, "CityId", "CityId", flight.DestinationCityId);
            ViewData["OriginCityId"] = new SelectList(_context.Cities, "CityId", "CityId", flight.OriginCityId);
            return View(flight);
        }

        // GET: Flights/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var flight = await _context.Flights
                .Include(f => f.Airline)
                .Include(f => f.Class)
                .Include(f => f.DestinationCity)
                .Include(f => f.OriginCity)
                .FirstOrDefaultAsync(m => m.FlightId == id);
            if (flight == null)
            {
                return NotFound();
            }

            return View(flight);
        }

        // POST: Flights/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var flight = await _context.Flights.FindAsync(id);
            if (flight != null)
            {
                _context.Flights.Remove(flight);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FlightExists(int id)
        {
            return _context.Flights.Any(e => e.FlightId == id);
        }
    }
}
