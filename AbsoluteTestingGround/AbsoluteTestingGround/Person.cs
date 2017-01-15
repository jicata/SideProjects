namespace AbsoluteTestingGround
{
    public class Person
    {
        public string name;

        public Person(string name)
        {
            this.name = name;
        }

        public string Hello()
        {
            return this.name + " says Hello!";
        }
    }
}
