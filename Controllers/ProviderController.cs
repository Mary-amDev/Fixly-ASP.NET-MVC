using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Fixly.Models;

namespace Fixly.Controllers;

public class ProviderController : Controller
{
    public IActionResult Index()
    {
        return View();
    }

}
