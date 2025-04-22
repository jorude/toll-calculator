using AfryTollCalculator.Interfaces;
using AfryTollCalculator.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AfryTollCalculator.Services
{
    public class TollService:ITollService
    {
        private ITollCalculator _tollCalculator;

        public TollService(ITollCalculator calculator)
        {
            _tollCalculator = calculator;
        }

       public void RegisterVehicle(Vehicle vehicle, DateTime dateTime)
        {
            Console.WriteLine($"{dateTime}: Vehicle type {vehicle.Type} {vehicle.Id} was registered");
            int fee = _tollCalculator.CalculateTollFee(vehicle, dateTime);
            TollRegistration lastRegistration = vehicle.GetMostRecentTollRegistration();
            if (lastRegistration == null)
            {
                vehicle.AddTollRegistration(fee);
            }
            else
            {
                int timeDiffMinutes = (DateTime.Now - lastRegistration.Timestamp).Minutes;
                if (fee > 0)
                {
                    if (timeDiffMinutes < 60 && fee > lastRegistration.Amount)
                    {
                        // update fee to the new value (is higher)
                        Console.WriteLine($"\t Updated fee from {lastRegistration.Amount} to {fee}");
                        lastRegistration.Amount = fee;
                    }
                    else
                    {
                        vehicle.AddTollRegistration(fee);
                    }
                }
            }
            Console.WriteLine($"\tFee: {fee} | Total for {dateTime.Date.ToShortDateString()}: {vehicle.GetDayTotalFees(dateTime)}");

        }




    }

}
