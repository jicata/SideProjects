using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PawInc.Models
{
    public abstract class Center
    {
        private string name;

        private List<Animal> storedAnimals; 

        protected Center(string name)
        {
            this.Name = name;
            this.storedAnimals = new List<Animal>();
        }

        public string Name
        {
            get
            {
                return this.name;
            }
            private set
            {
                this.name = value;
            }
        }

        public IEnumerable<Animal> StoredAnimals
        {
            get
            {
                return this.storedAnimals;
            }
        }

        public void RegisterAnimal(Animal animal)
        {
            this.storedAnimals.Add(animal);
        }

        public void RemoveAnimals(IEnumerable<Animal> animalsToRemove)
        {
            this.storedAnimals.RemoveAll(animalsToRemove.Contains);
        }
    }
}
