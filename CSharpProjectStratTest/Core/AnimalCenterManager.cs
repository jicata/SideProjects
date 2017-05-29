using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PawInc.Core
{
    using PawInc.Models;
    using PawInc.Models.Animals;
    using PawInc.Models.Centers;

    public class AnimalCenterManager
    {
        private Dictionary<string, AdoptionCenter> adoptionCenters;

        private Dictionary<string, CleansingCenter> cleansingCenters;

        private Dictionary<string, CastrationCenter> castrationCenters;

        private List<Animal> adoptedAnimals;
        
        private List<Animal> cleansedAnimals;
        
        private List<Animal> castratedAnimals;

        public AnimalCenterManager()
        {
            this.adoptionCenters = new Dictionary<string, AdoptionCenter>();
            this.cleansingCenters = new Dictionary<string, CleansingCenter>();
            this.castrationCenters = new Dictionary<string, CastrationCenter>();
            this.adoptedAnimals = new List<Animal>();
            this.cleansedAnimals = new List<Animal>();
            this.castratedAnimals = new List<Animal>();
        }

        public void RegisterAdoptionCenter(string[] commandArguments)
        {
            string adoptionCenterName = commandArguments[0];
            AdoptionCenter newAdoptionCenter = this.createAdoptionCenter(adoptionCenterName);
            this.adoptionCenters.Add(newAdoptionCenter.Name, newAdoptionCenter);
        }

        public void RegisterCleansingCenter(string[] commandArguments)
        {
            string cleansingCenterName = commandArguments[0];
            CleansingCenter newCleansingCenter = this.createCleansingCenter(cleansingCenterName);
            this.cleansingCenters.Add(newCleansingCenter.Name, newCleansingCenter);
        }

        public void RegisterCastrationCenter(string[] commandArguments)
        {
            string castrationCenterName = commandArguments[0];
            CastrationCenter newCastrationCenter = this.createCastrationCenter(castrationCenterName);
            this.castrationCenters.Add(newCastrationCenter.Name, newCastrationCenter);
        }

        public void RegisterDog(string[] commandArguments)
        {
            string dogName = commandArguments[0];
            int dogAge = int.Parse(commandArguments[1]);
            string dogAdoptionCenterName = commandArguments[3];
            int dogLearnedCommands = int.Parse(commandArguments[2]);

            Dog newDog = this.createDog(dogName, dogAge, dogAdoptionCenterName, dogLearnedCommands);
            this.adoptionCenters[newDog.AdoptionCenterName].RegisterAnimal(newDog);
        }

        public void RegisterCat(string[] commandArguments)
        {
            string catName = commandArguments[0];
            int catAge = int.Parse(commandArguments[1]);
            string catAdoptionCenterName = commandArguments[3];
            int catIntelligenceCoefficient = int.Parse(commandArguments[2]);

            Cat newCat = this.createCat(catName, catAge, catAdoptionCenterName, catIntelligenceCoefficient);
            this.adoptionCenters[newCat.AdoptionCenterName].RegisterAnimal(newCat);
        }

        public void SendForCleansing(string[] commandArguments)
        {
            string adoptionCenterName = commandArguments[0];
            string cleansingCenterName = commandArguments[1];

            foreach (var animal in this.adoptionCenters[adoptionCenterName].SendForCleanse())
            {
                this.cleansingCenters[cleansingCenterName].RegisterAnimal(animal);
            }
        }

        public void SendForCastration(string[] commandArguments)
        {
            string adoptionCenterName = commandArguments[0];
            string castrationCenterName = commandArguments[1];

            foreach (var animal in this.adoptionCenters[adoptionCenterName].SendForCleanse())
            {
                this.castrationCenters[castrationCenterName].RegisterAnimal(animal);
            }
        }

        public void Cleanse(string[] commandArguments)
        {
            string cleansingCenterName = commandArguments[0];

            foreach (var animal in this.cleansingCenters[cleansingCenterName].Cleanse())
            {
                this.adoptionCenters[animal.AdoptionCenterName].RegisterAnimal(animal);
                this.cleansedAnimals.Add(animal);
            }
        }

        public void Adopt(string[] commandArguments)
        {
            string adoptionCenterName = commandArguments[0];

            foreach (var animal in this.adoptionCenters[adoptionCenterName].Adopt())
            {
                this.adoptedAnimals.Add(animal);
            }
        }

        public void Castrate(string[] commandArguments)
        {
            string castrationCenterName = commandArguments[0];

            foreach (var animal in this.castrationCenters[castrationCenterName].Castrate())
            {
                this.adoptionCenters[animal.AdoptionCenterName].RegisterAnimal(animal);
                this.castratedAnimals.Add(animal);
            }
        }

        public string ShowRegularStatistics()
        {
            StringBuilder result = new StringBuilder();

            result.AppendLine("Paw Incorporative Regular Statistics");
            result.AppendLine(string.Format("Adoption Centers: {0}", this.adoptionCenters.Count));
            result.AppendLine(string.Format("Cleansing Centers: {0}", this.cleansingCenters.Count));
            result.AppendLine(string.Format("Adopted Animals: {0}", this.adoptedAnimals.Count > 0 ? 
                string.Join(", ", this.adoptedAnimals.Select(animal => animal.Name)
                .OrderBy(animalName => animalName)) : "None"));
            result.AppendLine(string.Format("Cleansed Animals: {0}", this.cleansedAnimals.Count > 0 ? 
                string.Join(", ", this.cleansedAnimals.Select(animal => animal.Name)
                .OrderBy(animalName => animalName)) : "None"));
            result.AppendLine(string.Format("Animals Awaiting Adoption: {0}", this.adoptionCenters.Sum(adoptionCenter => adoptionCenter.Value.AnimalsAwaitingAdoption().Count())));
            result.Append(string.Format("Animals Awaiting Cleansing: {0}", this.cleansingCenters.Sum(cleansingCenter => cleansingCenter.Value.AnimalsAwaitingCleansing().Count())));

            return result.ToString();
        }

        public string ShowCastrationStatistics()
        {
            StringBuilder result = new StringBuilder();

            result.AppendLine("Paw Inc. Regular Castration Statistics");
            result.AppendLine(string.Format("Castration Centers: {0}", this.castrationCenters.Count));
            result.Append(string.Format("Castrated Animals: {0}", this.castratedAnimals.Count > 0 ? 
                string.Join(", ", this.castratedAnimals.Select(animal => animal.Name)
                .OrderBy(animalName => animalName)) : "None"));

            return result.ToString();
        }

        private AdoptionCenter createAdoptionCenter(string name)
        {
            AdoptionCenter newCenter = new AdoptionCenter(name);
            return newCenter;
        }
        
        private CleansingCenter createCleansingCenter(string name)
        {
            CleansingCenter newCenter = new CleansingCenter(name);
            return newCenter;
        }
        
        private CastrationCenter createCastrationCenter(string name)
        {
            CastrationCenter newCenter = new CastrationCenter(name);
            return newCenter;
        }

        private Dog createDog(string name, int age, string adoptionCenterName, int learnedCommands)
        {
            Dog newDog = new Dog(name, age, adoptionCenterName, learnedCommands);
            return newDog;
        }

        private Cat createCat(string name, int age, string adoptionCenterName, int intelligenceCoefficient)
        {
            Cat newCat = new Cat(name, age, adoptionCenterName, intelligenceCoefficient);
            return newCat;
        }
    }
}
