using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PawInc.Core
{
    public class Engine
    {
        private bool isRunning;

        private AnimalCenterManager animalCenterManager;

        public Engine()
        {
            this.isRunning = false;
            this.animalCenterManager = new AnimalCenterManager();
        }

        public void Run()
        {
            this.isRunning = true;

            while (this.isRunning)
            {
                string inputLine = this.ReadInput();
                List<string> commandArguments = this.ParseInput(inputLine);
                this.DispatchCommand(commandArguments);
            }
        }

        private List<string> ParseInput(string inputLine)
        {
            List<string> splittedInput = inputLine.Split(new char[] {'|'}, StringSplitOptions.RemoveEmptyEntries).ToList().Select(item => item.Trim()).ToList();
            return splittedInput;
        }

        private void DispatchCommand(List<string> commandArguments)
        {
            string commandName = commandArguments[0];
            commandArguments.Remove(commandName);

            switch (commandName)
            {
                case "RegisterAdoptionCenter":
                    this.animalCenterManager.RegisterAdoptionCenter(commandArguments.ToArray());
                    break;
                case "RegisterCleansingCenter":
                    this.animalCenterManager.RegisterCleansingCenter(commandArguments.ToArray());
                    break;
                case "RegisterCastrationCenter":
                    this.animalCenterManager.RegisterCastrationCenter(commandArguments.ToArray());
                    break;
                case "RegisterDog":
                    this.animalCenterManager.RegisterDog(commandArguments.ToArray());
                    break;
                case "RegisterCat":
                    this.animalCenterManager.RegisterCat(commandArguments.ToArray());
                    break;
                case "SendForCleansing":
                    this.animalCenterManager.SendForCleansing(commandArguments.ToArray());
                    break;
                case "SendForCastration":
                    this.animalCenterManager.SendForCastration(commandArguments.ToArray());
                    break;
                case "Cleanse":
                    this.animalCenterManager.Cleanse(commandArguments.ToArray());
                    break;
                case "Adopt":
                    this.animalCenterManager.Adopt(commandArguments.ToArray());
                    break;
                case "Castrate":
                    this.animalCenterManager.Castrate(commandArguments.ToArray());
                    break;
                case "CastrationStatistics":
                    this.WriteOutput(this.animalCenterManager.ShowCastrationStatistics());
                    break;
                case "Paw Paw Pawah":
                    this.WriteOutput(this.animalCenterManager.ShowRegularStatistics());
                    this.isRunning = false;
                    break;
            }
        }

        private string ReadInput()
        {
            return Console.ReadLine();
        }

        private void WriteOutput(string output)
        {
            Console.WriteLine(output);
        }
    }
}
