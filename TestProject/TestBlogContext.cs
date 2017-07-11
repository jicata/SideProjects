﻿namespace EntityFrameworkCore
{
    using System;
    using System.Linq;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.DependencyInjection;
    using NUnit.Framework;

    [TestFixture]
    public class TestBlogContext
    {
        private IServiceProvider serviceProvider;

        [SetUp]
        public void SetupSomeStuff()
        {
            //string connectionString = @"Data Source=.;Initial Catalog=EFCore;Integrated Security=True";

            //var options = new DbContextOptionsBuilder<BloggingContext>()
            //    .UseInMemoryDatabase(databaseName: "Whatever man")
            //    .Options;

            //var standardOptions = new DbContextOptionsBuilder<BloggingContext>()
            //    .UseSqlServer(@"Data Source=.;Initial Catalog=EFCore;Integrated Security=True")
            //    .Options;

            var services = new ServiceCollection()
                .AddDbContext<BloggingContext>(b => b.UseInMemoryDatabase());

            serviceProvider = services.BuildServiceProvider();
        }

        [Test]
        public void SampleTest()
        {
            var serviced = serviceProvider.GetService<BloggingContext>();

            serviced.Blogs.Add(new Blog() { Rating = 5 });
            serviced.SaveChanges();



            var servicedAgain = serviceProvider.GetService<BloggingContext>();

            var blog = servicedAgain.Blogs.FirstOrDefault(b => b.Rating == 5);
            Assert.IsNotNull(blog);

        }
    }
}
