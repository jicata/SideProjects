using System.Linq;
using System.Web.Mvc;

namespace WebApplication2.Models
{
    public class CatsController : Controller
    {

        private ApplicationDbContext context;

        public CatsController()
            :this(new ApplicationDbContext())
        {
        }

        public CatsController(ApplicationDbContext context)
        {
            this.context = context;
        }

        // GET: Cats
        public ActionResult Index()
        {
            var cats = context.Cats.ToList();
            return View(cats);
        }

        // GET: Cats/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Cats/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Cats/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Cats/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Cats/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Cats/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Cats/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
