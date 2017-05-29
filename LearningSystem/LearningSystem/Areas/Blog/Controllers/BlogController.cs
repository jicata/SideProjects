using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using LearningSystem.Models.BindingModels.Blog;
using LearningSystem.Models.ViewModels.Blog;
using LearningSystem.Services;
using LearningSystem.Services.Interfaces;

namespace LearningSystem.Web.Areas.Blog.Controllers
{
    [RouteArea("blog")]
    [Authorize(Roles = "Student")]
    public class BlogController : Controller
    {
        private IBlogService service;

        public BlogController(IBlogService service)
        {
            this.service = service;
        }

        [Route("articles")]
        public ActionResult Articles()
        {
            IEnumerable<ArticleVm> vms = this.service.GetAllArticles();

            return this.View(vms);
        }

        [HttpGet]
        [Authorize(Roles = "BlogAuthor")]
        [Route("articles/add")]
        public ActionResult Add()
        {
            return this.View();
        }

        [HttpPost]
        [Authorize(Roles = "BlogAuthor")]
        [Route("articles/add")]
        public ActionResult Add(AddArticleBm bind)
        {
            if (this.ModelState.IsValid)
            {
                string username = this.User.Identity.Name;
                this.service.AddNewArticle(bind, username);
                return this.RedirectToAction("Articles");
            }

            return this.View();
        }
    }
}
