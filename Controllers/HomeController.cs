using INTEX2.Models;
using INTEX2.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Linq;

namespace INTEX2.Controllers
{
    public class HomeController : Controller
    {
        private IBurialRepository repo;
        private BuffaloDbContext _recordContext { get; set; }

        public HomeController (IBurialRepository temp, BuffaloDbContext rContext)
        {
            repo = temp;
            _recordContext = rContext;
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

        [HttpGet]
        public IActionResult Record()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Record(Burialmain bm)
        {
            if (ModelState.IsValid)
            {
                _recordContext.Add(bm);
                _recordContext.SaveChanges();

                return View("Confirmation", bm);
            }
            else
            {
                return View();
            }
        }

        public IActionResult Table()
        {
            var records = _recordContext.Burialmain.ToList();

            return View(records);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}