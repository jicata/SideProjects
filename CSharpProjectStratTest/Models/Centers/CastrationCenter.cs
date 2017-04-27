using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PawInc.Models.Centers
{
    public class CastrationCenter : Center
    {
        public CastrationCenter(string name)
            : base(name)
        {
        }

        public IEnumerable<Animal> Castrate()
        {
            List<Animal> castratedAnimals = new List<Animal>();

            foreach (var animal in this.StoredAnimals)
            {
                castratedAnimals.Add(animal);
            }

            this.RemoveAnimals(castratedAnimals);
            return castratedAnimals;
        }
    }
}
