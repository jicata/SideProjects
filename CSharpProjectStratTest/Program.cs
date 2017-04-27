using System;
using System.IO;
using PawInc.Models.Animals;

namespace PawInc
{
    using PawInc.Core;

    public class Program
    {
        public static void Main(string[] args)
        {
            //TextWriter writer = new StringWriter();
            //Console.SetOut(writer);
            //Dog dog = new Dog("balo",2,"doma",3);
            //dog.Bark();
            //return;
            Engine engine = new Engine();
            engine.Run();
        }
    }
}
