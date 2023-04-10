using INTEX2.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace INTEX2.Controllers
{
    public class HomeController : Controller
    {
        private BuffaloDbContext context { get; set; }

        public HomeController (BuffaloDbContext temp)
        {
            context = temp;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult BurialList()
        {
            var data = context.Burialmains.ToList();

            return View();
        }

        public IActionResult SupervisedAnalysis()
        {
            return View();
        }

        public IActionResult UnsupervisedAnalysis()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}