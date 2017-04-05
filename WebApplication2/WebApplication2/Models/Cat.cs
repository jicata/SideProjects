using WebApplication2.Models.Enums;

namespace WebApplication2.Models
{
    public class Cat
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Breed { get; set; }

        public Gender Gender { get; set; }

        public string ImgUrl { get; set; }
    }
}