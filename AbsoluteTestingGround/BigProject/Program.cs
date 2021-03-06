﻿namespace ACTester.Controller
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using ACTester.Database;
    using ACTester.Enumerations;
    using ACTester.Interfaces;
    using ACTester.Models;
    using ACTester.Utilities;

    public class AirConditionerTesterSystem : IAirConditionerTesterSystem
    {
        public AirConditionerTesterSystem(IAirConditionerTesterDatabase database)
        {
            this.Database = database;
        }

        public AirConditionerTesterSystem() : this(new AirConditionerTesterDatabase())
        {
        }

        public IAirConditionerTesterDatabase Database { get; private set; }

        public string RegisterStationaryAirConditioner(string manufacturer, string model, string energyEfficiencyRating, int powerUsage)
        {
            EnergyEfficiencyRating rating;
            try
            {
                rating =
                    (EnergyEfficiencyRating)Enum.Parse(typeof(EnergyEfficiencyRating), energyEfficiencyRating);
            }
            catch (ArgumentException ex)
            {
                throw new ArgumentException(Constants.IncorrectEnergyEfficiencyRating, ex);
            }

            IAirConditioner airConditioner = new StationaryAirConditioner(manufacturer, model, rating, powerUsage);
            this.Database.AirConditioners.Add(airConditioner);
            return string.Format(Constants.RegisterAirConditioner, airConditioner.Model, airConditioner.Manufacturer);
        }

        public string RegisterCarAirConditioner(string manufacturer, string model, int volumeCoverage)
        {
            IAirConditioner airConditioner = new CarAirConditioner(manufacturer, model, volumeCoverage);
            this.Database.AirConditioners.Add(airConditioner);
            return string.Format(Constants.RegisterAirConditioner, airConditioner.Model, airConditioner.Manufacturer);
        }

        /// <summary>
        /// A method that registers a new PlaneAirConditioner in the database from a given manufacturer, model and volume coverage and electricity used if they pass validation.
        /// </summary>
        /// <param name="manufacturer">The Manufacturer of the air conditioner.</param>
        /// <param name="model">The model of the air conditioner.</param>
        /// <param name="volumeCoverage">An integer value specifying the volume coverage of the air conditioner.</param>
        /// <param name="electricityUsed">An integer value specifying the electricity used of the air conditioner.</param>
        /// <returns>If the input values pass validation the method returns a string with a success message, otherwise it throws an appropriate exception.</returns>
        public string RegisterPlaneAirConditioner(string manufacturer, string model, int volumeCoverage, int electricityUsed)
        {
            IAirConditioner airConditioner = new PlaneAirConditioner(manufacturer, model, volumeCoverage, electricityUsed);
            this.Database.AirConditioners.Add(airConditioner);
            return string.Format(Constants.RegisterAirConditioner, airConditioner.Model, airConditioner.Manufacturer);
        }

        public string TestAirConditioner(string manufacturer, string model)
        {
            IAirConditioner airConditioner = this.Database.AirConditioners.GetItem(manufacturer, model);
            var mark = airConditioner.Test() ? Mark.Passed : Mark.Failed;
            this.Database.Reports.Add(new Report(airConditioner.Manufacturer, airConditioner.Model, mark));
            return string.Format(Constants.TestAirConditioner, model, manufacturer);
        }

        /// <summary>
        /// A method that finds an Air Conditioner in the database from a given manufacturer and model.
        /// </summary>
        /// <param name="manufacturer">The Manufacturer of the searched air conditioner.</param>
        /// <param name="model">The model of the searched air conditioner.</param>
        /// <returns>If the air conditioner exists in the database returns it's string represention, othrerwise it throws an appropriate exception.</returns>
        public string FindAirConditioner(string manufacturer, string model)
        {
            IAirConditioner airConditioner = this.Database.AirConditioners.GetItem(manufacturer, model);
            return airConditioner.ToString();
        }

        public string FindReport(string manufacturer, string model)
        {
            IReport report = this.Database.Reports.GetItem(manufacturer, model);
            return report.ToString();
        }

        public string FindAllReportsByManufacturer(string manufacturer)
        {
            IList<IReport> reports = this.Database.Reports.GetReportsByManufacturer(manufacturer);
            if (reports.Count == 0)
            {
                return Constants.NoReports;
            }

            reports = reports.OrderBy(x => x.Model).ToList();
            StringBuilder reportsPrint = new StringBuilder();
            reportsPrint.AppendLine(string.Format("Reports from {0}:", manufacturer));
            reportsPrint.Append(string.Join(Environment.NewLine, reports));
            return reportsPrint.ToString();
        }

        /// <summary>
        /// A method which displays the system status as a percentage representing the number of tested air conditioners.
        /// </summary>
        /// <returns>Returns a string displaying the percentage of tested air conditioners.</returns>
        public string Status()
        {
            int reports = this.Database.Reports.Count;
            double airConditioners = this.Database.AirConditioners.Count;
            if (reports == 0)
            {
                return string.Format(Constants.Status, 0);
            }

            double percent = reports / airConditioners;
            percent = percent * 100;
            return string.Format(Constants.Status, percent);
        }
    }
}

namespace ACTester.Core
{
    using System;
    using ACTester.Controller;
    using ACTester.Interfaces;
    using ACTester.Utilities;

    public class ActionManager : IActionManager
    {
        public ActionManager(IAirConditionerTesterSystem controller)
        {
            this.Controller = controller;
        }

        public ActionManager() : this(new AirConditionerTesterSystem())
        {
        }

        private IAirConditionerTesterSystem Controller { get; set; }

        public string ExecuteCommand(ICommand command)
        {
            try
            {
                switch (command.Name)
                {
                    case "RegisterStationaryAirConditioner":
                        this.ValidateParametersCount(command, Constants.RegisterStationaryAcParametersCount);
                        return this.Controller.RegisterStationaryAirConditioner(
                            command.Parameters[0],
                            command.Parameters[1],
                            command.Parameters[2],
                            int.Parse(command.Parameters[3]));
                    case "RegisterCarAirConditioner":
                        this.ValidateParametersCount(command, Constants.RegisterCarAcParametersCount);
                        return this.Controller.RegisterCarAirConditioner(
                            command.Parameters[0],
                            command.Parameters[1],
                            int.Parse(command.Parameters[2]));
                    case "RegisterPlaneAirConditioner":
                        this.ValidateParametersCount(command, Constants.RegisterPlaneAcParametersCount);
                        return this.Controller.RegisterPlaneAirConditioner(
                            command.Parameters[0],
                            command.Parameters[1],
                            int.Parse(command.Parameters[2]),
                            int.Parse(command.Parameters[3]));
                    case "TestAirConditioner":
                        this.ValidateParametersCount(command, Constants.TestAcParametersCount);
                        return this.Controller.TestAirConditioner(
                            command.Parameters[0],
                            command.Parameters[1]);
                    case "FindAirConditioner":
                        this.ValidateParametersCount(command, Constants.FindAcParametersCount);
                        return this.Controller.FindAirConditioner(
                            command.Parameters[0],
                            command.Parameters[1]);
                    case "FindReport":
                        this.ValidateParametersCount(command, Constants.FindReportParametersCount);
                        return this.Controller.FindReport(
                            command.Parameters[0],
                            command.Parameters[1]);
                    case "FindAllReportsByManufacturer":
                        this.ValidateParametersCount(command, Constants.FindReportsByManufacturerParametersCount);
                        return this.Controller.FindAllReportsByManufacturer(
                            command.Parameters[0]);
                    case "Status":
                        this.ValidateParametersCount(command, Constants.StatusParametersCount);
                        return this.Controller.Status();
                    default:
                        throw new InvalidOperationException(Constants.InvalidCommand);
                }
            }
            catch (FormatException ex)
            {
                throw new InvalidOperationException(Constants.InvalidCommand, ex.InnerException);
            }
            catch (IndexOutOfRangeException ex)
            {
                throw new InvalidOperationException(Constants.InvalidCommand, ex.InnerException);
            }
        }

        private void ValidateParametersCount(ICommand command, int count)
        {
            if (command.Parameters.Length != count)
            {
                throw new InvalidOperationException(Constants.InvalidCommand);
            }
        }
    }
}

namespace ACTester.Core
{
    using System;
    using ACTester.Interfaces;
    using ACTester.Utilities;

    public class Command : ICommand
    {
        public Command(string line)
        {
            try
            {
                this.Name = line.Substring(0, line.IndexOf(' '));
                this.Parameters = line.Substring(line.IndexOf(' ') + 1)
                    .Split(new char[] { '(', ')', ',' }, StringSplitOptions.RemoveEmptyEntries);
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException(Constants.InvalidCommand, ex);
            }
        }

        public string Name { get; private set; }

        public string[] Parameters { get; private set; }
    }
}

namespace ACTester.Core
{
    using System;
    using ACTester.Interfaces;
    using ACTester.UI;

    public class Engine
    {
        public Engine(IActionManager actionManager, IUserInterface userInterface)
        {
            this.ActionManager = actionManager;
            this.UserInterface = userInterface;
        }

        public Engine()
            : this(new ActionManager(), new ConsoleUserInterface())
        {
        }

        public IActionManager ActionManager { get; private set; }

        public IUserInterface UserInterface { get; private set; }

        public void Run()
        {
            while (true)
            {
                string line = this.UserInterface.ReadLine();
                if (string.IsNullOrEmpty(line))
                {
                    break;
                }

                line = line.Trim();
                try
                {
                    var command = new Command(line);
                    string commandResult = this.ActionManager.ExecuteCommand(command);
                    this.UserInterface.WriteLine(commandResult);
                }
                catch (Exception ex)
                {
                    this.UserInterface.WriteLine(ex.Message);
                }
            }
        }
    }
}

namespace ACTester.Database
{
    using ACTester.Interfaces;

    public class AirConditionerTesterDatabase : IAirConditionerTesterDatabase
    {
        public AirConditionerTesterDatabase()
        {
            this.AirConditioners = new Repository<IAirConditioner>();
            this.Reports = new ReportsRepository();
        }

        public IRepository<IAirConditioner> AirConditioners { get; private set; }

        public IReportsRepository Reports { get; private set; }
    }
}

namespace ACTester.Database
{
    using System.Collections.Generic;
    using ACTester.Interfaces;

    public class ReportsRepository : Repository<IReport>, IReportsRepository
    {
        public ReportsRepository()
        {
            this.ItemsByManufacturer = new Dictionary<string, IList<IReport>>();
        }

        protected Dictionary<string, IList<IReport>> ItemsByManufacturer { get; set; }

        public override void Add(IReport item)
        {
            base.Add(item);
            if (!this.ItemsByManufacturer.ContainsKey(item.Manufacturer))
            {
                this.ItemsByManufacturer.Add(item.Manufacturer, new List<IReport>());
            }

            this.ItemsByManufacturer[item.Manufacturer].Add(item);
        }

        public IList<IReport> GetReportsByManufacturer(string manufacturer)
        {
            if (!this.ItemsByManufacturer.ContainsKey(manufacturer))
            {
                return new List<IReport>();
            }

            return new List<IReport>(this.ItemsByManufacturer[manufacturer]);
        }
    }
}
namespace ACTester.Database
{
    using System.Collections.Generic;
    using ACTester.Exceptions;
    using ACTester.Interfaces;
    using ACTester.Utilities;

    public class Repository<T> : IRepository<T> where T : IManufacturable, IModelable
    {
        public Repository()
        {
            this.ItemsByManufacturerAndModel = new Dictionary<string, T>();
        }

        public int Count { get; protected set; }

        protected Dictionary<string, T> ItemsByManufacturerAndModel { get; set; }

        public virtual void Add(T item)
        {
            string manufacturerAndModel = item.Manufacturer + item.Model;
            if (this.ItemsByManufacturerAndModel.ContainsKey(manufacturerAndModel))
            {
                throw new DuplicateEntryException(Constants.DuplicateEntry);
            }

            this.ItemsByManufacturerAndModel.Add(manufacturerAndModel, item);
            this.Count++;
        }

        public virtual T GetItem(string manufacturer, string model)
        {
            if (!this.ItemsByManufacturerAndModel.ContainsKey(manufacturer + model))
            {
                throw new NonExistantEntryException(Constants.NonExistantEntry);
            }

            return this.ItemsByManufacturerAndModel[manufacturer + model];
        }
    }
}
namespace ACTester.Enumerations
{
    public enum EnergyEfficiencyRating
    {
        A = 999,
        B = 1250,
        C = 1500,
        D = 2000,
        E = 2001
    }
}
namespace ACTester.Enumerations
{
    public enum Mark
    {
        Passed,
        Failed
    }
}
namespace ACTester.Exceptions
{
    using System;

    public class DuplicateEntryException : Exception
    {
        public DuplicateEntryException(string message) : base(message)
        {
        }
    }
}
namespace ACTester.Exceptions
{
    using System;

    public class NonExistantEntryException : Exception
    {
        public NonExistantEntryException(string message) : base(message)
        {
        }
    }
}
namespace ACTester.Interfaces
{
    public interface IActionManager
    {
        string ExecuteCommand(ICommand action);
    }
}
namespace ACTester.Interfaces
{
    /// <summary>
    /// An interface representing the abstract class that carries information common for all air conditioners.
    /// </summary>
    public interface IAirConditioner : IManufacturable, IModelable
    {
        /// <summary>
        /// A method that tests if an Air Conditioner meets the requierments for a passing mark.
        /// </summary>
        /// <returns>Returns a boolean value specifying if the test was sucessfull, true for a passing mark and false for a failing one.</returns>
        bool Test();
    }
}
namespace ACTester.Interfaces
{
    public interface IAirConditionerTesterDatabase
    {
        IRepository<IAirConditioner> AirConditioners { get; }

        IReportsRepository Reports { get; }
    }
}
namespace ACTester.Interfaces
{
    public interface IAirConditionerTesterSystem
    {
        string RegisterStationaryAirConditioner(string manufacturer, string model, string energyEfficiencyRating, int powerUsage);

        string RegisterCarAirConditioner(string manufacturer, string model, int volumeCoverage);

        string RegisterPlaneAirConditioner(string manufacturer, string model, int volumeCoverage, int electricityUsed);

        string TestAirConditioner(string manufacturer, string model);

        string FindAirConditioner(string manufacturer, string model);

        string FindReport(string manufacturer, string model);

        string FindAllReportsByManufacturer(string manufacturer);

        string Status();
    }
}
namespace ACTester.Interfaces
{
    public interface ICommand
    {
        string Name { get; }

        string[] Parameters { get; }
    }
}
namespace ACTester.Interfaces
{
    public interface IEngine
    {
        void Run();
    }
}
namespace ACTester.Interfaces
{
    public interface IManufacturable
    {
        /// <summary>
        /// The Manufacturer of the Air Conditioner.
        /// </summary>
        string Manufacturer { get; }
    }
}
namespace ACTester.Interfaces
{
    public interface IModelable
    {
        /// <summary>
        /// The model of the Air Conditioner.
        /// </summary>
        string Model { get; }
    }
}
namespace ACTester.Interfaces
{
    using ACTester.Enumerations;

    /// <summary>
    /// An interface representing the class that carries information common for all reports.
    /// </summary>
    public interface IReport : IManufacturable, IModelable
    {
        /// <summary>
        /// A mark representing if the associated air conditioner passed or failed it's test.
        /// </summary>
        Mark Mark { get; }
    }
}
namespace ACTester.Interfaces
{
    using System.Collections.Generic;

    public interface IReportsRepository : IRepository<IReport>
    {
        IList<IReport> GetReportsByManufacturer(string manufacturer);
    }
}
namespace ACTester.Interfaces
{
    public interface IRepository<T> where T : IManufacturable, IModelable
    {
        int Count { get; }

        void Add(T item);

        T GetItem(string manufacturer, string model);
    }
}
namespace ACTester.Interfaces
{
    public interface IUserInterface
    {
        string ReadLine();

        void WriteLine(string message);
    }
}
namespace ACTester.Models
{
    using System;
    using System.Text;
    using ACTester.Interfaces;
    using ACTester.Utilities;

    public abstract class AirConditioner : IAirConditioner
    {
        private string manufacturer;

        private string model;

        protected AirConditioner(string manufacturer, string model)
        {
            this.Manufacturer = manufacturer;
            this.Model = model;
        }

        public string Manufacturer
        {
            get
            {
                return this.manufacturer;
            }

            set
            {
                if (string.IsNullOrEmpty(value) || value.Length < Constants.ManufacturerMinLength)
                {
                    throw new ArgumentException(string.Format(Constants.IncorrectPropertyLength, "Manufacturer", Constants.ManufacturerMinLength));
                }

                this.manufacturer = value;
            }
        }

        public string Model
        {
            get
            {
                return this.model;
            }

            set
            {
                if (string.IsNullOrEmpty(value) || value.Length < Constants.ModelMinLength)
                {
                    throw new ArgumentException(string.Format(Constants.IncorrectPropertyLength, "Model", Constants.ModelMinLength));
                }

                this.model = value;
            }
        }

        public abstract bool Test();

        public override string ToString()
        {
            StringBuilder print = new StringBuilder();
            print.AppendLine("Air Conditioner");
            print.AppendLine("====================");
            print.AppendLine(string.Format("Manufacturer: {0}", this.Manufacturer));
            print.AppendLine(string.Format("Model: {0}", this.Model));
            return print.ToString();
        }
    }
}

namespace ACTester.Models
{
    using System;
    using System.Text;
    using ACTester.Utilities;

    public class CarAirConditioner : VehicleAirConditioner
    {
        public CarAirConditioner(string manufacturer, string model, int volumeCoverage) : base(manufacturer, model, volumeCoverage)
        {
        }

        public override bool Test()
        {
            double sqrtVolume = Math.Sqrt(this.VolumeCovered);
            if (sqrtVolume < Constants.MinCarVolume)
            {
                return false;
            }

            return true;
        }

        public override string ToString()
        {
            StringBuilder print = new StringBuilder(base.ToString());
            print.Append("====================");
            return print.ToString();
        }
    }
}
namespace ACTester.Models
{
    using System;
    using System.Text;
    using ACTester.Utilities;

    public class PlaneAirConditioner : VehicleAirConditioner
    {
        private int electricityUsed;

        public PlaneAirConditioner(string manufacturer, string model, int volumeCoverage, int electricityUsed) : base(manufacturer, model, volumeCoverage)
        {
            this.ElectricityUsed = electricityUsed;
        }

        public int ElectricityUsed
        {
            get
            {
                return this.electricityUsed;
            }

            set
            {
                if (value <= 0)
                {
                    throw new ArgumentException(string.Format(Constants.NonPositiveNumber, "Electricity Used"));
                }

                this.electricityUsed = value;
            }
        }

        public override bool Test()
        {
            double sqrtVolume = Math.Sqrt(this.VolumeCovered);
            if ((this.ElectricityUsed / sqrtVolume) < Constants.MinPlaneElectricity)
            {
                return true;
            }

            return false;
        }

        public override string ToString()
        {
            StringBuilder print = new StringBuilder(base.ToString());
            print.AppendLine(string.Format("Electricity Used: {0}", this.ElectricityUsed));
            print.Append("====================");
            return print.ToString();
        }
    }
}
namespace ACTester.Models
{
    using System.Text;
    using ACTester.Enumerations;
    using ACTester.Interfaces;

    public class Report : IReport
    {
        public Report(string manufacturer, string model, Mark mark)
        {
            this.Manufacturer = manufacturer;
            this.Model = model;
            this.Mark = mark;
        }

        public string Manufacturer { get; private set; }

        public string Model { get; private set; }

        public Mark Mark { get; private set; }

        public override string ToString()
        {
            StringBuilder print = new StringBuilder();
            print.AppendLine("Report");
            print.AppendLine("====================");
            print.AppendLine(string.Format("Manufacturer: {0}", this.Manufacturer));
            print.AppendLine(string.Format("Model: {0}", this.Model));
            print.AppendLine(string.Format("Mark: {0}", this.Mark));
            print.Append("====================");
            return print.ToString();
        }
    }
}
namespace ACTester.Models
{
    using System;
    using System.Text;
    using ACTester.Enumerations;
    using ACTester.Utilities;

    public class StationaryAirConditioner : AirConditioner
    {
        private int powerUsage;

        public StationaryAirConditioner(string manufacturer, string model, EnergyEfficiencyRating rating, int powerUsage)
            : base(manufacturer, model)
        {
            this.RequiredEnergyEfficiencyRating = rating;
            this.PowerUsage = powerUsage;
        }

        public EnergyEfficiencyRating RequiredEnergyEfficiencyRating { get; set; }

        public int PowerUsage
        {
            get
            {
                return this.powerUsage;
            }

            set
            {
                if (value <= 0)
                {
                    throw new ArgumentException(string.Format(Constants.NonPositiveNumber, "Power Usage"));
                }

                this.powerUsage = value;
            }
        }

        public override bool Test()
        {
            if (this.PowerUsage <= (int)this.RequiredEnergyEfficiencyRating || this.RequiredEnergyEfficiencyRating == EnergyEfficiencyRating.E)
            {
                return true;
            }

            return false;
        }

        public override string ToString()
        {
            StringBuilder print = new StringBuilder(base.ToString());
            print.AppendLine(string.Format("Required energy efficiency rating: {0}", this.RequiredEnergyEfficiencyRating));
            print.AppendLine(string.Format("Power Usage(KW / h): {0}", this.PowerUsage));
            print.Append("====================");
            return print.ToString();
        }
    }
}
namespace ACTester.Models
{
    using System;
    using System.Text;
    using ACTester.Utilities;

    public abstract class VehicleAirConditioner : AirConditioner
    {
        private int volumeCovered;

        protected VehicleAirConditioner(string manufacturer, string model, int volumeCoverage) : base(manufacturer, model)
        {
            this.VolumeCovered = volumeCoverage;
        }

        public int VolumeCovered
        {
            get
            {
                return this.volumeCovered;
            }

            set
            {
                if (value <= 0)
                {
                    throw new ArgumentException(string.Format(Constants.NonPositiveNumber, "Volume Covered"));
                }

                this.volumeCovered = value;
            }
        }

        public override string ToString()
        {
            StringBuilder print = new StringBuilder(base.ToString());
            print.AppendLine(string.Format("Volume Covered: {0}", this.VolumeCovered));
            return print.ToString();
        }
    }
}
namespace ACTester.UI
{
    using System;
    using ACTester.Interfaces;

    public class ConsoleUserInterface : IUserInterface
    {
        public string ReadLine()
        {
            return Console.ReadLine();
        }

        public void WriteLine(string message)
        {
            Console.WriteLine(message);
        }
    }
}
namespace ACTester.UI
{
    using System.IO;
    using ACTester.Interfaces;

    public class FileUserInterface : IUserInterface
    {
        public FileUserInterface(string input, string output)
        {
            this.Reader = new StreamReader(File.Open(input, FileMode.Open));
            this.Writer = new StreamWriter(File.Create(output));
        }

        public StreamReader Reader { get; private set; }

        public StreamWriter Writer { get; private set; }

        public string ReadLine()
        {
            return this.Reader.ReadLine();
        }

        public void WriteLine(string message)
        {
            this.Writer.WriteLine(message);
        }

        public void Close()
        {
            this.Writer.Close();
            this.Reader.Close();
        }
    }
}
namespace ACTester.Utilities
{
    public static class Constants
    {
        public const string IncorrectPropertyLength = "{0}'s name must be at least {1} symbols long.";

        public const string NoReports = "No reports.";

        public const string InvalidCommand = "Invalid command";

        public const string Status = "Jobs complete: {0:F2}%";

        public const string IncorrectEnergyEfficiencyRating = "Energy efficiency rating must be between \"A\" and \"E\".";

        public const string NonPositiveNumber = "{0} must be a positive integer.";

        public const string DuplicateEntry = "An entry for the given model already exists.";

        public const string NonExistantEntry = "The specified entry does not exist.";

        public const string RegisterAirConditioner = "Air Conditioner model {0} from {1} registered successfully.";

        public const string TestAirConditioner = "Air Conditioner model {0} from {1} tested successfully.";

        public const int ModelMinLength = 2;

        public const int ManufacturerMinLength = 4;

        public const int MinCarVolume = 3;

        public const int MinPlaneElectricity = 150;

        public const int RegisterStationaryAcParametersCount = 4;

        public const int RegisterCarAcParametersCount = 3;

        public const int RegisterPlaneAcParametersCount = 4;

        public const int TestAcParametersCount = 2;

        public const int FindAcParametersCount = 2;

        public const int FindReportParametersCount = 2;

        public const int FindReportsByManufacturerParametersCount = 1;

        public const int StatusParametersCount = 0;
    }
}

class Program
{
    public static void Main()
    {
        string kur = "ruk";
    }
}