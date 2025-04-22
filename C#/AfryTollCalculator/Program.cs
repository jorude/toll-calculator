using AfryTollCalculator.Enums;
using AfryTollCalculator.Interfaces;
using AfryTollCalculator.Models;
using AfryTollCalculator.Services;
using AfryTollCalculator.Utilities;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection.Metadata.Ecma335;
using System.Timers;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace AfryTollCalculator
{
    public class Program
    {
        private static System.Timers.Timer timer = new System.Timers.Timer();
        private static ITollService _tollService;
        private static readonly List<Vehicle> _exampleVehicles = [new Vehicle(VehicleType.Car), new Vehicle(VehicleType.Car), new Vehicle(VehicleType.Car), new Vehicle(VehicleType.Motorbike), new Vehicle(VehicleType.Foreign), new Vehicle(VehicleType.Tractor)];

        static int Main(string[] args)
        {
            // DI setup
            var serviceProvider = new ServiceCollection()
                .AddSingleton<ITollCalculator, TollCalculator>()
                .AddSingleton<ITollService, TollService>()
                .BuildServiceProvider();
            _tollService = serviceProvider.GetService<ITollService>();
            // start time for simulation
            timer.Interval = 1000;
            timer.Elapsed += Timer_Elapsed;
            timer.Start();
            Console.Read();

            return 0;

        }
        private static void Timer_Elapsed(object? sender, ElapsedEventArgs e)
        {
            Random vehicleRand = new Random();
            Random timeIntervalRand = new Random();
            timer.Interval = timeIntervalRand.Next(100, 3000);
            timer.Enabled = true;
            Vehicle v = _exampleVehicles[vehicleRand.Next(0, 5)];
            var randomTime = CreateRandomTime();
            _tollService.RegisterVehicle(v, randomTime);
        }
        private static DateTime CreateRandomTime()
        {
            Random gen = new Random(Guid.NewGuid().GetHashCode());

            DateTime date = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, gen.Next(6,9),gen.Next(59),gen.Next(59));
            int range = (DateTime.Now - date).Days;
            return date.AddDays(gen.Next(range));
        }
       
    }

}
