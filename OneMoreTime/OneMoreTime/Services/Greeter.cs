namespace OneMoreTime.Services
{
    using Microsoft.Extensions.Configuration;

    public class Greeter : IGreeter
    {
        private string greeting;

        public Greeter(IConfiguration configuration)
        {
            greeting = configuration["greeting"];
        }

        public string GetGreeting()
        {
            return greeting;
        }
    }
}
