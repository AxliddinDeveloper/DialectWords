using System.Diagnostics;
using DialectWords.Models;
using Microsoft.AspNetCore.Mvc;

namespace DialectWords.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult Aloqa()
        {
            return View();
        }

        public IActionResult LoyihaHaqida()
        {
            return View();
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
