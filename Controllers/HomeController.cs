using INTEX2.Models;
using INTEX2.Data;
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
        private mummydbContext _recordContext { get; set; }

        public HomeController (IBurialRepository temp, mummydbContext rContext)
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
        public IActionResult BurialList(string filterType, string filterValue, int pageNum = 1)
        {
            int pageSize = 10;
            
            var data = new BurialsViewModel
            {

                Burials = repo.Burials
                    .Where(b =>
                        filterValue == null ||
                        (
                            //filterType == "Textile Color" && b.Sex == filterValue ||
                            //filterType == "Textile Structure" && b.Sex == filterValue ||
                            filterType == "Sex" && b.Sex == filterValue ||
                            //filterType == "Burial Depth" && b.Age == filterValue ||
                            //filterType == "Estimate Stature" && b.Sex == filterValue ||
                            filterType == "Age At Death" && b.Ageatdeath == filterValue ||
                            filterType == "Head Direction" && b.Headdirection == filterValue ||
                            //filterType == "Burial ID" && b.Burialid == filterValue ||
                            //filterType == "Textile Function" && b.Name == filterValue ||
                            filterType == "Hair Color" && b.Haircolor == filterValue
                        )
                    )
                    .OrderBy(b => b.Id)
                    .Skip((pageNum - 1) * pageSize)
                    .Take(pageSize),

                PageInfo = new PageInfo
                {
                    TotalBurials =
                        (filterValue == null
                            ? repo.Burials.Count()
                            : repo.Burials.Where(b => b.Sex == filterValue).Count()),
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

        public IActionResult FilterBurialList()
        {
            //ViewBag.BurialMainTextileColor = _recordContext.Burialmain.Select(x => x.Sex).Distinct().ToList();
            //ViewBag.BurialMainTextileStructure = _recordContext.Burialmain.Select(x => x.Sex).Distinct().ToList();
            ViewBag.BurialMainSex = _recordContext.Burialmain.Select(x => x.Sex).Distinct().ToList();
            ViewBag.BurialMainBurialDepth = _recordContext.Burialmain.Select(x => x.Depth).Distinct().ToList();
            //ViewBag.BurialMainEstimatedStature = _recordContext.Burialmain.Select(x => x.Sex).Distinct().ToList();
            ViewBag.BurialMainAgeAtDeath = _recordContext.Burialmain.Select(x => x.Ageatdeath).Distinct().ToList();
            ViewBag.BurialMainHeadDirection = _recordContext.Burialmain.Select(x => x.Headdirection).Distinct().ToList();
            ViewBag.BurialMainBurialID = _recordContext.Burialmain.Select(x => x.Burialid).Distinct().ToList();
            //ViewBag.BurialMainTextileFunction = _recordContext.Burialmain.Select(x => x.Sex).Distinct().ToList();
            ViewBag.BurialMainHairColor = _recordContext.Burialmain.Select(x => x.Haircolor).Distinct().ToList();
            ViewBag.BurialMain = _recordContext.Burialmain.Distinct().ToList();

            return View();
        }

        // GET - Record
        [HttpGet]
        public IActionResult Record()
        {
            ViewBag.MummyData = _recordContext.MummyData.ToList();

            return View();
        }

        // POST - Record
        [HttpPost]
        public IActionResult Record(MummyData md)
        {
            if (ModelState.IsValid)
            {
                _recordContext.Add(md);
                _recordContext.SaveChanges();

                return View("Confirmation", md);
            }
            else
            {
                ViewBag.MummyData = _recordContext.MummyData.ToList();

                return View();
            }
        }

        // GET - Table
        [HttpGet]
        public IActionResult Table()
        {
            var records = _recordContext.MummyData
                .OrderBy(data => data.Id)
                .ToList();

            return View(records);
        }

        // GET - Edit
        [HttpGet]
        public IActionResult Edit(long recordid)
        {
            ViewBag.MummyData = _recordContext.MummyData.ToList();

            var record = _recordContext.MummyData.Single(data => data.Id == recordid);

            return View("Record", record);
        }

        // POST - Edit
        [HttpPost]
        public IActionResult Edit(MummyData d, int recordid)
        {
            if (ModelState.IsValid)
            {
                _recordContext.Update(d);
                _recordContext.SaveChanges();

                return RedirectToAction("Table", d);
            }
            else
            {
                ViewBag.MummyData = _recordContext.MummyData.ToList();

                var record = _recordContext.MummyData.Single(data => data.Id == recordid);

                return View("Record", record);
            }
        }

        // GET - Delete
        [HttpGet]
        public IActionResult Delete(int recordid)
        {
            var form = _recordContext.MummyData.Single(data => data.Id == recordid);

            return View(form);
        }

        // POST- Delete
        [HttpPost]
        public IActionResult Delete(MummyData d)
        {
            _recordContext.MummyData.Remove(d);
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