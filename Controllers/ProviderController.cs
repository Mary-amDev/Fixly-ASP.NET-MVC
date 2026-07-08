using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Fixly.Models;
using Fixly.Data;

namespace Fixly.Controllers;

public class ProviderController : Controller
{
    private readonly AppDbContext _context; 

    public ProviderController(AppDbContext context)  //يسوي رفرش (دالة بناء)
    {
        _context = context;
    }
    [Route("request")]
    public IActionResult Index()
    {
      
         var requests = _context.ServiceRequests.ToList();
        return View(requests);
    }

}
