using WebApplication2.Models;
using WebApplication2.Models.Enums;

namespace WebApplication2.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<WebApplication2.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(WebApplication2.Models.ApplicationDbContext context)
        {
            if (context.Cats.Any())
            {
                return;
            }
            var kittyCat = new Cat()
            {
                Breed = "Long Hair",
                Gender = Gender.Male,
                Name = "Sharo"
            };
            var kittyCat1 = new Cat()
            {
                Breed = "Short Hair",
                Gender = Gender.Male,
                Name = "Belcho"
            };
            var kittyCat2 = new Cat()
            {
                Breed = "Fluffination",
                Gender = Gender.Female,
                Name = "Roshla"
            };
            context.Cats.Add(kittyCat);
            context.Cats.Add(kittyCat1);
            context.Cats.Add(kittyCat2);
            context.SaveChanges();
        }
    }
}
