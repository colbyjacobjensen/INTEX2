using INTEX2.Models;
using INTEX2.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Linq;

namespace INTEX2.Controllers
{
    public class HomeController : Controller
    {
        private IBurialRepository repo;

        private BuffaloDbContext context;

        public HomeController (IBurialRepository temp)
        {
            repo = temp;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult BurialList(string burialType, int pageNum = 1)
        {
            int pageSize = 10;
            
            var data = new BurialsViewModel
            {
                Burials = repo.Burials
                .Where(b => b.Sex == burialType || burialType == null)
                .OrderBy(b => b.Id)
                .Skip((pageNum - 1) * pageSize)
                .Take(pageSize),

                PageInfo = new PageInfo
                {
                    TotalBurials =
                        (burialType == null
                            ? repo.Burials.Count()
                            : repo.Burials.Where(b => b.Sex == burialType).Count()),
                    BurialsPerPage = pageSize,
                    CurrentPage = pageNum
                }
            };
            return View(data);
        }

        public IActionResult Supervised()
        {
            return View();
        }

        public IActionResult Unsupervised()
        {
            return View();
        }

        public IActionResult IndividualDetail(long id)
        {
            var blah = new BurialsViewModel
            {
                Burials = repo.Burials
                .Where(b => b.Id == id)
            };

            return View(blah);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}