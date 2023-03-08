using DStore.Models;
using DStore.Service;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace DStore.Areas.Dashboard.Controllers
{
    [Area("Dashboard")]
    public class HomeController : Controller
    {
        
            public IActionResult Index()
        {
            return View();
        }
    }
}
    