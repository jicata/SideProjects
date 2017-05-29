using LearningSystem.Models.EntityModels;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace LearningSystem.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<LearningSystem.Data.LearningSystemContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            this.AutomaticMigrationDataLossAllowed = true;
        }

        protected override void Seed(LearningSystemContext context)
        {
            if (!context.Roles.Any(role => role.Name == "Student"))
            {
                var store = new RoleStore<IdentityRole>(context);
                var manager = new RoleManager<IdentityRole>(store);
                var role = new IdentityRole("Student");
                manager.Create(role);
            }

            if (!context.Roles.Any(role => role.Name == "Admin"))
            {
                var store = new RoleStore<IdentityRole>(context);
                var manager = new RoleManager<IdentityRole>(store);
                var role = new IdentityRole("Admin");
                manager.Create(role);
            }

            if (!context.Roles.Any(role => role.Name == "Trainer"))
            {
                var store = new RoleStore<IdentityRole>(context);
                var manager = new RoleManager<IdentityRole>(store);
                var role = new IdentityRole("Trainer");
                manager.Create(role);
            }

            if (!context.Roles.Any(role => role.Name == "BlogAuthor"))
            {
                var store = new RoleStore<IdentityRole>(context);
                var manager = new RoleManager<IdentityRole>(store);
                var role = new IdentityRole("BlogAuthor");
                manager.Create(role);
            }

            context.Courses.AddOrUpdate(course => course.Name, new Course[]
            {
                new Course()
                {
                    Name =  "Programming basics - March 2016",
                    Description = "Курсът \"Programming Basics\" дава начални умения по програмиране, необходими за всички технологични специалности в СофтУни. Това включва писане на програмен код на начално ниво (basic coding skills), работа със среда за разработка (IDE), използване на променливи и данни, оператори и изрази, работа с конзолата (четене на входни данни и печатане на резултати), използване на условни конструкции (if, if-else, switch-case) и цикли (for, while, do-while, for-each).\r\n\r\nКурсът завършва с практически приемен изпит, като успешно представилите се кандидати стават студенти в СофтУни и продължават напред с подготовката си за софтуерни инженери.",
                    StartDate = new DateTime(2016, 03, 23),
                    EndDate = new DateTime(2016, 05, 23)
                },
                new Course()
                {
                    Name =  "Курс Software Technologies - февруари 2017",
                    Description = "Курсът \"Software Technologies\" дава начални познания по най-използваните софтуерни технологии в практиката и позволява на студентите да се ориентират кои технологии им харесват, за да ги изучават по-задълбочено. Изучават се базови концепции от front-end и back-end разработката. Курсът се състои от четири части: HTML5 разработка (HTML + CSS + JavaScript + AJAX + REST), PHP уеб разработка (PHP + MySQL), C# уеб разработка (ASP.NET MVC + Entity Framework + SQL Server) и Java уеб разработка (Java + Spring MVC + Hibernate + MySQL).",
                    StartDate = new DateTime(2017, 03, 25),
                    EndDate = new DateTime(2017, 05, 30)
                },
                new Course()
                {
                    Name =  "Курс C# MVC Frameworks - ASP.NET - юни 2017",
                    Description = "Курсът \"ASP.NET MVC\" ще ви научи как се изграждат съвременни уеб приложения с архитектурата Model-View-Controller, използвайки HTML5, бази данни, Entity Framework и други технологии. Изучава се технологичната платформа ASP.NET, нейните компоненти и архитектура, създаването на MVC уеб приложения, дефинирането на модели, изгледи и частични изгледи с Razor view engine, дефиниране на контролери, работа с бази данни и интеграция с Entity Framework, LINQ и SQL Server. Курсът включа и по-сложни теми като работа с потребители, роли и сесии, използване на AJAX, кеширане, сигурност на уеб приложенията, уеб сокети и SignalR и работа с библиотеки от MVC контроли. Включват се няколко практически лабораторни упражнения (лабове) и работилници за изграждане на цялостни, пълнофункционални ASP.NET MVC уеб приложения. Курсът включва работа по екипен проект за изграждане на уеб приложение и завършва с практически изпит по уеб разработка с ASP.NET MVC. ",
                    StartDate = new DateTime(2017, 06, 04),
                    EndDate = new DateTime(2017, 09, 10)
                },
                new Course()
                {
                    Name =  "Курс Java OOP Advanced - септември 2017",
                    Description = "Курсът Object Oriented Programming Advanced с Java надгражда курса Object Oriented Programming Basics като залага най-вече на добрите практики в парадигмата обектно-ориентирано програмиране, като абстракция и програмиране с интерфейси, преизползваемост на кода, поддръжка и лесна променяемост, чрез инструменти като събития, Reflection, Generics и разбиране на принципи като Open/Closed Principle и Liskov Substitution Principle.",
                    StartDate = new DateTime(2017, 09, 23),
                    EndDate = new DateTime(2017, 12, 23)
                },
            });
        }
    }
}
