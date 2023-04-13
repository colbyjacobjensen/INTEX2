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

        // Index
        public IActionResult Index()
        {
            return View();
        }

        // Privacy
        public IActionResult Privacy()
        {
            return View();
        }

        // Burial List
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

        // Individual Details
        public IActionResult IndividualDetail(long id)
        {
            var blah = new BurialsViewModel
            {
                Burials = repo.Burials
                .Where(b => b.Id == id)
            };

            return View(blah);
        }

        // Supervised 
        public IActionResult Supervised()
        {
            return View();
        }

        // Unsupervised 
        public IActionResult Unsupervised()
        {
            return View();
        }

        // GET - Record
        [HttpGet]
        public IActionResult Record()
        {
            ViewBag.Burialmain = _recordContext.Burialmain.ToList();

            return View();
        }

        // POST - Record
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
                ViewBag.Burialmain = _recordContext.Burialmain.ToList();

                return View();
            }
        }

        // GET - Table
        [HttpGet]
        public IActionResult Table()
        {
            var records = _recordContext.Burialmain
                .OrderBy(data => data.Id)
                .ToList();

            return View(records);
        }

        // GET - Edit
        [HttpGet]
        public IActionResult Edit(long recordid)
        {
            ViewBag.Burialmain = _recordContext.Burialmain.ToList();

            var record = _recordContext.Burialmain.Single(data => data.Id == recordid);

            return View("Record", record);
        }

        // POST - Edit
        [HttpPost]
        public IActionResult Edit(Burialmain bm, int movieid)
        {
            if (ModelState.IsValid)
            {
                _recordContext.Update(bm);
                _recordContext.SaveChanges();

                return RedirectToAction("Table", bm);
            }
            else
            {
                ViewBag.Burialmain = _recordContext.Burialmain.ToList();

                var record = _recordContext.Burialmain.Single(data => data.Id == movieid);

                return View("Record", record);
            }
        }

        // GET - Delete
        [HttpGet]
        public IActionResult Delete(int recordid)
        {
            var form = _recordContext.Burialmain.Single(data => data.Id == recordid);

            return View(form);
        }

        // POST- Delete
        [HttpPost]
        public IActionResult Delete(Burialmain bm)
        {
            _recordContext.Burialmain.Remove(bm);
            _recordContext.SaveChanges();

            return RedirectToAction("Table");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}