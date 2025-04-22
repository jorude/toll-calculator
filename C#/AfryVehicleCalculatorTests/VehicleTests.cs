using AfryTollCalculator.Enums;
using AfryTollCalculator.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Threading.Tasks;

namespace AfryVehicleCalculatorTests
{
    public class VehicleTests
    {
        [Fact]
        public void ReturnsTotalTollAmountForDate_WhenHasTollRegistrations()
        {
            var model = new Vehicle(VehicleType.Car);
            model.TollRegistrations.AddRange(new TollRegistration(10, DateTime.Now), new TollRegistration(20, DateTime.Now));
            double result = model.GetDayTotalFees(DateTime.Now);

            Assert.Equal(30, result);
        }
        [Fact]
        public void ReturnsTollTotalOnlyForGivenDate_WhenHasRegistrationsForMultipleDays()
        {
            var model = new Vehicle(VehicleType.Car);
            model.TollRegistrations.AddRange(new TollRegistration(10, DateTime.Now.AddDays(-1)), new TollRegistration(20, DateTime.Now));
            double result = model.GetDayTotalFees(DateTime.Now);

            Assert.Equal(20, result);
        }
        [Fact]
        public void ReturnsMostRecentRegistration_WhenHasMultipleRegistrations()
        {
            var model = new Vehicle(VehicleType.Car);
            DateTime d1 = DateTime.Now;
            DateTime d2 = DateTime.Now.AddHours(-1);
            model.TollRegistrations.AddRange(new TollRegistration(10, d1), new TollRegistration(20, d2));

            var result = model.GetMostRecentTollRegistration();

            Assert.Equal(d1, result.Timestamp);
        }
        [Fact]
        public void ReturnsNullAsMostRecentRegistration_WhenNoRegistrations()
        {
            var model = new Vehicle(VehicleType.Car);

            var result = model.GetMostRecentTollRegistration();

            Assert.Null(result);
        }

    }
}

