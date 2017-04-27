using System;

namespace PawInc.Models.Animals
{
    public class Dog : Animal
    {
        private int learnedCommands;

        public Dog(string name, int age, string adoptionCenterName, int learnedCommands)
            : base(name, age, adoptionCenterName)
        {
            this.LearnedCommands = learnedCommands;
        }

        public void Bark()
        {
            Console.WriteLine("WOOF WOOF");
        }

        public int LearnedCommands
        {
            get
            {
                return this.learnedCommands;
            }
            private set
            {
                this.learnedCommands = value;
            }
        }

    }
}
