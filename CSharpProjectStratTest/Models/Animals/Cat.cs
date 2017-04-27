using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PawInc.Models.Animals
{
    public class Cat : Animal
    {
        private int intelligenceCoefficient;

        public Cat(string name, int age, string adoptionCenterName, int intelligenceCoefficient)
            : base(name, age, adoptionCenterName)
        {
            this.IntelligenceCoefficient = intelligenceCoefficient;
        }

        public int IntelligenceCoefficient
        {
            get
            {
                return this.intelligenceCoefficient;
            }
            private set
            {
                this.intelligenceCoefficient = value;
            }
        }
    }
}
