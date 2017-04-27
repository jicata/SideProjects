using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PawInc.Models.Centers
{
    public class CleansingCenter : Center
    {
        public CleansingCenter(string name)
            : base(name)
        {
        }

        public IEnumerable<Animal> AnimalsAwaitingCleansing()
        {
            List<Animal> animalsAwaitingCleansing = new List<Animal>();

            foreach (var animal in this.StoredAnimals)
            {
                if (!animal.CleanseStatus)
                {
                    animalsAwaitingCleansing.Add(animal);
                }
            }

            return animalsAwaitingCleansing;
        } 

        public IEnumerable<Animal> Cleanse()
        {
            List<Animal> cleansedAnimals = new List<Animal>();

            foreach (var animal in this.StoredAnimals)
            {
                animal.Cleanse();
                cleansedAnimals.Add(animal);
            }

            this.RemoveAnimals(cleansedAnimals);
            return cleansedAnimals;
        }
    }
}
