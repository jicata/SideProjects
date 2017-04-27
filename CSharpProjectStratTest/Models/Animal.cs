using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PawInc.Models
{
    public abstract class Animal
    {
        private string name;

        private int age;

        private string adoptionCenterName;

        private bool cleanseStatus;

        protected Animal(string name, int age, string adoptionCenterName)
        {
            this.Name = name;
            this.Age = age;
            this.AdoptionCenterName = adoptionCenterName;
            this.CleanseStatus = false;
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

        public int Age
        {
            get
            {
                return this.age;
            }

            private set
            {
                this.age = value;
            }
        }

        public string AdoptionCenterName
        {
            get
            {
                return this.adoptionCenterName;
            }

            private set
            {
                this.adoptionCenterName = value;
            }
        }

        public bool CleanseStatus
        {
            get
            {
                return this.cleanseStatus;
            }

            private set
            {
                this.cleanseStatus = value;
            }
        }

        public void Cleanse()
        {
            this.CleanseStatus = true;
        }
    }
}
