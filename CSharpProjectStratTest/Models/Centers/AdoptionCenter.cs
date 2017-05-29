using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PawInc.Models.Centers
{
    public class AdoptionCenter : Center
    {
        public AdoptionCenter(string name)
            : base(name)
        {
        }

        public IEnumerable<Animal> AnimalsAwaitingAdoption()
        {
            List<Animal> animalsAwaitingAdoption = new List<Animal>();

            foreach (var animal in this.StoredAnimals)
            {
                if (animal.CleanseStatus)
                {
                    animalsAwaitingAdoption.Add(animal);
                }
            }

            return animalsAwaitingAdoption;
        } 

        public IEnumerable<Animal> SendForCleanse()
        {
            List<Animal> animalsForCleansing = new List<Animal>();

            foreach (var animal in this.StoredAnimals)
            {
                if (!animal.CleanseStatus)
                {
                    animalsForCleansing.Add(animal);
                }
            }

            this.RemoveAnimals(animalsForCleansing);
            return animalsForCleansing;
        } 

        public IEnumerable<Animal> Adopt()
        {
            List<Animal> adoptedAnimals = new List<Animal>();

            foreach (var animal in this.StoredAnimals)
            {
                if (animal.CleanseStatus)
                {
                    adoptedAnimals.Add(animal);
                }
            }

            this.RemoveAnimals(adoptedAnimals);
            return adoptedAnimals;
        }
    }
}
