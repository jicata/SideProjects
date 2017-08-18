namespace OneMoreTime.Controllers
{
    using Microsoft.AspNetCore.Mvc;

    [Route("[controller]")]
    public class AboutController
    {
        [Route("")]
        public string Phone()
        {
            return "1-1-1-1-133";
        }

        [Route("[action]")]
        public string Country()
        {
            return "USA";
        }
    }
}
