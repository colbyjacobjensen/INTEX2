using System;
using INTEX2.Models;
using Microsoft.AspNetCore.Mvc;

namespace INTEX2.Components
{
    public class TypesViewComponent : ViewComponent
    {
        private IBurialRepository repo { get; set; }

        public TypesViewComponent(IBurialRepository temp) => repo = temp;

        public IViewComponentResult Invoke()
        {
            ViewBag.SelectedType = RouteData?.Values["burialType"];

            var types = repo.Burials
                .Select(x => x.Sex)
                .Distinct()
                .OrderBy(x => x);

            return View(types);
        }
    }
}