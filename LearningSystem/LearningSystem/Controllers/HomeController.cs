using System.Collections.Generic;
using System.IO;
using System.Web;
using System.Web.Mvc;
using LearningSystem.Models.ViewModels.Courses;
using LearningSystem.Services;
using LearningSystem.Services.Interfaces;

namespace LearningSystem.Web.Controllers
{
    [Authorize(Roles = "Student")]
    public class HomeController : Controller
    {
        private IHomeService service;

        public HomeController(IHomeService service)
        {
            this.service = service;
        }

        [AllowAnonymous]
        public ActionResult Index()
        {
            IEnumerable<CourseVm> vms = this.service.GetAllCourses();
            return this.View(vms);
        }

        [Route("upload")]
        public ActionResult UploadFile()
        {
            return this.View();
        }

        [HttpPost]
        [Route("upload")]
        [ActionName("UploadFile")]
        public ActionResult Upload()
        {
            HttpPostedFileBase file = this.Request.Files[0];
            string fileName = Path.GetFileName(file.FileName);
            string path = Path.Combine(Server.MapPath("~/UploadedFiles"), fileName);
            file.SaveAs(path);
            return this.RedirectToAction("Index");
        }


    }
}