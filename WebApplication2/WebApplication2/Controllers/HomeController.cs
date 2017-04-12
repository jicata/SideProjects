using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebApplication2.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public JsonResult GetCats()
        {
            var items = new List<SelectListItem>();
            SelectListGroup slg1 = new SelectListGroup() {Name = "Fluffy"};
            SelectListGroup slg2 = new SelectListGroup() { Name = "Muffy" };
            items.Add(new SelectListItem() { Value = "1", Text = "Cat 1", Group = slg1});
            items.Add(new SelectListItem() { Value = "2", Text = "Cat 2", Group = slg1});
            items.Add(new SelectListItem() { Value = "3", Text = "Cat 3", Group = slg2});

            return this.Json(items, JsonRequestBehavior.AllowGet);
        }
    }
}