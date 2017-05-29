using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using LearningSystem.Data;
using LearningSystem.Models.BindingModels.Blog;
using LearningSystem.Models.EntityModels;
using LearningSystem.Models.ViewModels.Blog;
using LearningSystem.Services.Interfaces;

namespace LearningSystem.Services
{
    public class BlogService : Service, IBlogService
    {
        public BlogService(ILearningSystemContext context) : base(context)
        {
        }

        public IEnumerable<ArticleVm> GetAllArticles()
        {
            IEnumerable<Article> models = this.Context.Articles;
            IEnumerable<ArticleVm> vms = Mapper.Map<IEnumerable<Article>, IEnumerable<ArticleVm>>(models);
            return vms;
        }

        public void AddNewArticle(AddArticleBm bind, string username)
        {
            ApplicationUser currentUser = this.Context.Users.FirstOrDefault(user => user.UserName == username);
            Article model = Mapper.Map<AddArticleBm, Article>(bind);
            model.Author = currentUser;
            model.PublishDate = DateTime.Today;
            this.Context.Articles.Add(model);
            this.Context.SaveChanges();
        }
    }
}
