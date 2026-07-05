using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Fixly.Models;

namespace Fixly.Controllers;

public class RequestsController : Controller
{
    public IActionResult Incoming()
    {
        return View();
    }

}
