using AfryTollCalculator.Enums;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AfryTollCalculator.Models
{
    public class Vehicle
    {
        public int Id { get; private set; }
        public VehicleType Type { get; private set; }
        public List<TollRegistration> TollRegistrations { get; set; } = new List<TollRegistration>(); // intending this to be a navigation property through an ORM such as EF

        public Vehicle(VehicleType type)
        {
            Type = type;
            SetId();
        }
        public void AddTollRegistration(int amount, DateTime date)
        {
            TollRegistrations.Add(new TollRegistration(amount, date));  
        }
        public void AddTollRegistration(int amount)
        {
            TollRegistrations.Add(new TollRegistration(amount, DateTime.Now));
        }
        public int GetDayTotalFees(DateTime date)
        {
            return TollRegistrations?
                .Where(x => x.Timestamp.Date == date.Date)
                .Sum(x => x.Amount) ?? 0;
        }

        public TollRegistration GetMostRecentTollRegistration()
        {
            if (!TollRegistrations.Any())
                return null;
            return TollRegistrations?
                .OrderByDescending(x => x.Timestamp)
                .FirstOrDefault();
        }
        private void SetId()
        {
            Random random = new Random();
            Id = random.Next();
        }
    }
}