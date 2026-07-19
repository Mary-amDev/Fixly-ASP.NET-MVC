using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Fixly.Data;
using Fixly.Models;
using System.Security.Claims;

public class ServiceProvidersController : Controller
{
    private readonly AppDbContext _context;

    public ServiceProvidersController(AppDbContext context)
    {
        _context = context;
    }
    public async Task<IActionResult> Index()
    {
        var providerId = User.FindFirstValue(ClaimTypes.NameIdentifier);

        var requests = await _context.ServiceRequests
            .Include(r => r.Customer)
            .Where(r => r.ProviderId == providerId)
            .Join(
                _context.ServiceProviderProfiles,
                request => request.ProviderId,
                profile => profile.UserId,
                (request, profile) => new MyRequestViewModel
                {
                    Request = request,
                    ServiceCategory = profile.ServiceCategory
                })
            .OrderByDescending(x => x.Request.RequestedDate)
            .ToListAsync();

        return View(requests);
    }

            public IActionResult AcceptRequest(int id)
        {
            var request = _context.ServiceRequests.FirstOrDefault(r => r.Id == id);

            if (request == null)
                return NotFound();

            request.Status = RequestStatus.Accepted;

            _context.SaveChanges();

            return RedirectToAction(nameof(Index));
        }

        public IActionResult RejectRequest(int id)
        {
            var request = _context.ServiceRequests.FirstOrDefault(r => r.Id == id);

            if (request == null)
                return NotFound();

            request.Status = RequestStatus.Rejected;

            _context.SaveChanges();

            return RedirectToAction(nameof(Index));
}
}