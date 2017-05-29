using System.Web.Mvc;
using LearningSystem.Models.ViewModels.Admin;
using LearningSystem.Services;
using LearningSystem.Services.Interfaces;

namespace LearningSystem.Web.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    [RouteArea("admin")]
    public class AdminController : Controller
    {
        private IAdminService service;

        public AdminController(IAdminService service)
        {
            this.service = service;
        }

        [HttpGet]
        [Route]
        public ActionResult Index()
        {
            AdminPageVm vm = this.service.GetAdminPage();
            return this.View(vm);
        }

        [HttpGet]
        [Route("course/add")]
        public ActionResult AddCourse()
        {   
            return this.View();
        }

        [HttpGet]
        [Route("courses/{id}/edit")]
        public ActionResult EditCourse(int id)
        {
            return this.View();
        }

        [HttpGet]
        [Route("users/{id}/edit")]
        public ActionResult EditUser(int id)
        {
            return this.View();
        }

    }
}
