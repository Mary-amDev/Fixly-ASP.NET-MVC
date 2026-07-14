using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Fixly.Data;

public class ServiceProvidersController : Controller
{
    private readonly AppDbContext _context;

    public ServiceProvidersController(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IActionResult> Search(string? category, string? city)
    {
        var providers = _context.ServiceProviderProfiles
            .Include(p => p.User)
            .AsQueryable();

        if (!string.IsNullOrWhiteSpace(category))
        {
            providers = providers.Where(p => p.ServiceCategory == category);
        }

        if (!string.IsNullOrWhiteSpace(city))
        {
            providers = providers.Where(p => p.User.City == city);
        }

        return View(await providers.ToListAsync());
    }

}