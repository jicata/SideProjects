﻿using System.Web.Mvc;

namespace LearningSystem.Web.Areas.Blog
{
    public class BlogAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "Blog";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.Routes.MapMvcAttributeRoutes();   
        }
    }
}