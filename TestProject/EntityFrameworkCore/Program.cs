namespace EntityFrameworkCore
{
    using System;
    using System.Linq;
    using System.Net;
    using System.Net.Http;
    //using Microsoft.EntityFrameworkCore;
    //using Microsoft.Extensions.DependencyInjection;

    class Program
    {
        private static IServiceProvider serviceProvider;
        static void Main()
        {
            Console.WriteLine("Making API Call...");
            using (var client = new HttpClient(new HttpClientHandler { AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate }))
            {
                client.BaseAddress = new Uri("https://api.stackexchange.com/2.2/");
                HttpResponseMessage response = client.GetAsync("answers?order=desc&sort=activity&site=stackoverflow").Result;
                response.EnsureSuccessStatusCode();
                string result = response.Content.ReadAsStringAsync().Result;
                Console.WriteLine("Result: " + result);
            }
            Console.ReadLine();

            return;
            string connectionString = @"Data Source=.;Initial Catalog=EFCore;Integrated Security=True";

            var options = new DbContextOptionsBuilder<BloggingContext>()
                .UseInMemoryDatabase(databaseName: "Whatever man")
                .Options;

            var standardOptions = new DbContextOptionsBuilder<BloggingContext>()
                .UseSqlServer(@"Data Source=.;Initial Catalog=EFCore;Integrated Security=True")
                .Options;

            var services = new ServiceCollection()
                .AddDbContext<BloggingContext>(b => b.UseInMemoryDatabase());

            serviceProvider = services.BuildServiceProvider();

            var serviced = serviceProvider.GetService<BloggingContext>();
            serviced.Blogs.Add(new Blog() {Rating = 5});
            serviced.SaveChanges();

            //var db = new BloggingContext(options);


            //var blog = new Blog { Url = "http://sample.com" };
            //db.Add(blog);
            //db.SaveChanges();
            //var retrieved = db.Blogs.FirstOrDefault();
            //Console.WriteLine(retrieved.Url);




        }
    }
}
