using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TravelDeskManagement.DTOs;
using TravelDeskManagement.Models;

namespace TravelDeskManagement.Controllers
{
    public class TravelBookingsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public TravelBookingsController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _context.TravelBookings.Where(x => !x.IsDeleted).ToListAsync());
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var travelBooking = await _context.TravelBookings
                .FirstOrDefaultAsync(m => m.BookingId == id);
            if (travelBooking == null)
            {
                return NotFound();
            }

            return View(travelBooking);
        }

        public IActionResult Create()
        {
            var travelRequests = _context.TravelRequests
        .Select(x => new TravelRequestDropdownDto
        {
            RequestId = x.RequestId,
            DisplayText = x.Employee.FullName + " | " +
                          x.TravelFrom + " → " + x.TravelTo
        })
        .ToList();

            ViewBag.TravelRequests = new SelectList(
                travelRequests,
                "RequestId",
                "DisplayText"
            );
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("BookingId,RequestId,FlightNumber,Airline,BookingReference,HotelName")] AddTravelBooking travelBooking)
        {
            if (ModelState.IsValid)
            {
                var adminUserId = HttpContext.Session.GetInt32("AdminUserId");
                TravelBooking obj = new TravelBooking
                {
                    RequestId = travelBooking.RequestId,
                    Airline = travelBooking.Airline,
                    BookingReference = travelBooking.BookingReference,
                    FlightNumber = travelBooking.FlightNumber,
                    HotelName = travelBooking.HotelName,
                    CreatedBy = adminUserId.Value,
                    CreatedDate = DateTime.Now
                };
                _context.TravelBookings.Add(obj);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            return View(travelBooking);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var travelBooking = await _context.TravelBookings.FindAsync(id);
            if (travelBooking == null)
            {
                return NotFound();
            }
            var travelRequests = _context.TravelRequests
        .Select(x => new TravelRequestDropdownDto
        {
            RequestId = x.RequestId,
            DisplayText = x.Employee.FullName + " | " +
                          x.TravelFrom + " → " + x.TravelTo
        })
        .ToList();

            ViewBag.TravelRequests = new SelectList(
                travelRequests,
                "RequestId",
                "DisplayText"
            );

            AddTravelBooking obj = new AddTravelBooking
            {
                Airline = travelBooking.Airline,
                BookingId = travelBooking.BookingId,
                BookingReference = travelBooking.BookingReference,
                FlightNumber = travelBooking.FlightNumber,
                HotelName = travelBooking.HotelName,
                RequestId = travelBooking.RequestId
            };
            return View(obj);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("BookingId,RequestId,FlightNumber,Airline,BookingReference,HotelName")] AddTravelBooking travelBooking)
        {
            if (id != travelBooking.BookingId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var adminUserId = HttpContext.Session.GetInt32("AdminUserId");
                    TravelBooking obj = new TravelBooking
                    {
                        BookingId = travelBooking.BookingId,
                        RequestId = travelBooking.RequestId,
                        Airline = travelBooking.Airline,
                        BookingReference = travelBooking.BookingReference,
                        FlightNumber = travelBooking.FlightNumber,
                        HotelName = travelBooking.HotelName,
                        UpdatedBy = adminUserId.Value,
                        UpdatedDate = DateTime.Now
                    };
                    _context.TravelBookings.Update(obj);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TravelBookingExists(travelBooking.BookingId))
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
            return View(travelBooking);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var travelBooking = await _context.TravelBookings
                .FirstOrDefaultAsync(m => m.BookingId == id);
            if (travelBooking == null)
            {
                return NotFound();
            }

            return View(travelBooking);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var travelBooking = await _context.TravelBookings.FindAsync(id);
            if (travelBooking != null)
            {
                travelBooking.IsDeleted = true;
                _context.TravelBookings.Update(travelBooking);

            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TravelBookingExists(int id)
        {
            return _context.TravelBookings.Any(e => e.BookingId == id);
        }
    }
}
