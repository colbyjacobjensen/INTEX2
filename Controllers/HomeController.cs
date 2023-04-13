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

        public IActionResult Supervised()
        {
            return View();
        }

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