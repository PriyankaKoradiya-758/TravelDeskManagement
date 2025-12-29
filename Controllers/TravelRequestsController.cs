using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TravelDeskManagement.DTOs;
using TravelDeskManagement.Helpers;
using TravelDeskManagement.Models;

namespace TravelDeskManagement.Controllers
{
    public class TravelRequestsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public TravelRequestsController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.TravelRequests.Where(x => !x.IsDeleted).Include(t => t.Employee);
            return View(await applicationDbContext.ToListAsync());
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var travelRequest = await _context.TravelRequests
                .Include(t => t.Employee)
                .FirstOrDefaultAsync(m => m.RequestId == id);
            if (travelRequest == null)
            {
                return NotFound();
            }

            return View(travelRequest);
        }

        public IActionResult Create()
        {
            ViewBag.EmployeeId = new SelectList(
                    _context.Employees,
                    "EmployeeId",
                    "FullName"
                );

            ViewBag.PurposeList = Enum.GetValues(typeof(TravelPurpose))
        .Cast<TravelPurpose>()
        .Select(p => new SelectListItem
        {
            Text = p.ToString().Replace("_", " "),
            Value = ((int)p).ToString()
        })
        .ToList();

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("RequestId,EmployeeId,Purpose,OtherPurpose,TravelFrom,TravelTo,DepartureDate,ReturnDate,Status")] AddTravelRequest travelRequest)
        {
            if (travelRequest.Purpose != TravelPurpose.Other)
            {
                travelRequest.OtherPurpose = null;
            }
            if (ModelState.IsValid)
            {
                ViewBag.PurposeList = Enum.GetValues(typeof(TravelPurpose))
            .Cast<TravelPurpose>()
            .Select(p => new SelectListItem
            {
                Text = p.ToString().Replace("_", " "),
                Value = ((int)p).ToString()
            });
                var adminUserId = HttpContext.Session.GetInt32("AdminUserId");
                TravelRequest obj = new TravelRequest
                {
                    DepartureDate = travelRequest.DepartureDate,
                    EmployeeId = travelRequest.EmployeeId,
                    Purpose = travelRequest.Purpose,
                    OtherPurpose = travelRequest.OtherPurpose,
                    ReturnDate = travelRequest.ReturnDate,
                    Status = TravelRequestStatus.Pending,
                    TravelFrom = travelRequest.TravelFrom,
                    TravelTo = travelRequest.TravelTo,
                    CreatedBy = adminUserId.Value,
                    CreatedDate = DateTime.Now
                };
                _context.TravelRequests.Add(obj);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            ViewBag.EmployeeId = new SelectList(
                _context.Employees,
                "EmployeeId",
                "FullName",
                travelRequest.EmployeeId
            );

            return View(travelRequest);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var travelRequest = await _context.TravelRequests.FindAsync(id);
            if (travelRequest == null)
            {
                return NotFound();
            }
            AddTravelRequest obj = new AddTravelRequest
            {
                EmployeeId = travelRequest.EmployeeId,
                DepartureDate = travelRequest.DepartureDate,
                Status = travelRequest.Status,
                Purpose = travelRequest.Purpose,
                OtherPurpose = travelRequest.OtherPurpose,
                TravelTo = travelRequest.TravelTo,
                TravelFrom = travelRequest.TravelFrom,
                ReturnDate = travelRequest.ReturnDate,
                RequestId = travelRequest.RequestId

            };
            ViewBag.EmployeeId = new SelectList(
                              _context.Employees,
                              "EmployeeId",
                              "FullName"
                          );

            ViewBag.PurposeList = Enum.GetValues(typeof(TravelPurpose))
        .Cast<TravelPurpose>()
        .Select(p => new SelectListItem
        {
            Text = p.ToString().Replace("_", " "),
            Value = ((int)p).ToString()
        })
        .ToList();

            return View(obj);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("RequestId,EmployeeId,Purpose,OtherPurpose,TravelFrom,TravelTo,DepartureDate,ReturnDate,Status")] AddTravelRequest travelRequest)
        {
            if (id != travelRequest.RequestId)
            {
                return NotFound();
            }
            if (travelRequest.Purpose != TravelPurpose.Other)
            {
                travelRequest.OtherPurpose = null;
            }
            if (ModelState.IsValid)
            {
                try
                {
                    ViewBag.PurposeList = Enum.GetValues(typeof(TravelPurpose))
            .Cast<TravelPurpose>()
            .Select(p => new SelectListItem
            {
                Text = p.ToString().Replace("_", " "),
                Value = ((int)p).ToString(),
                Selected = p == travelRequest.Purpose
            })
            .ToList();

                    var adminUserId = HttpContext.Session.GetInt32("AdminUserId");
                    TravelRequest obj = new TravelRequest
                    {
                        RequestId = travelRequest.RequestId,
                        DepartureDate = travelRequest.DepartureDate,
                        EmployeeId = travelRequest.EmployeeId,
                        Purpose = travelRequest.Purpose,
                        OtherPurpose = travelRequest.OtherPurpose,
                        ReturnDate = travelRequest.ReturnDate,
                        Status = travelRequest.Status,
                        TravelFrom = travelRequest.TravelFrom,
                        TravelTo = travelRequest.TravelTo,
                        UpdatedBy = adminUserId.Value,
                        UpdatedDate = DateTime.Now
                    };
                    _context.TravelRequests.Update(obj);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TravelRequestExists(travelRequest.RequestId))
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
            ViewData["EmployeeId"] = new SelectList(_context.Employees, "EmployeeId", "EmployeeId", travelRequest.EmployeeId);
            return View(travelRequest);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var travelRequest = await _context.TravelRequests
                .Include(t => t.Employee)
                .FirstOrDefaultAsync(m => m.RequestId == id);
            if (travelRequest == null)
            {
                return NotFound();
            }

            return View(travelRequest);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var travelRequest = await _context.TravelRequests.FindAsync(id);
            if (travelRequest != null)
            {
                travelRequest.IsDeleted = true;
                _context.TravelRequests.Update(travelRequest);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TravelRequestExists(int id)
        {
            return _context.TravelRequests.Any(e => e.RequestId == id);
        }

        [HttpPost]
        public async Task<IActionResult> Approve(int id)
        {
            var request = await _context.TravelRequests.FindAsync(id);

            if (request == null || request.Status != TravelRequestStatus.Pending)
                return RedirectToAction(nameof(Index));

            request.Status = TravelRequestStatus.Approved;
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public async Task<IActionResult> Reject(int id)
        {
            var request = await _context.TravelRequests.FindAsync(id);

            if (request == null || request.Status != TravelRequestStatus.Pending)
                return RedirectToAction(nameof(Index));

            request.Status = TravelRequestStatus.Rejected;
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

    }
}
