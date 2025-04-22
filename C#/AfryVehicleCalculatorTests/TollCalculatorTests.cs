using AfryTollCalculator.Enums;
using AfryTollCalculator.Models;
using AfryTollCalculator.Utilities;

namespace AfryVehicleCalculatorTests
{
    public class TollCalculatorTests
    {
        private TollCalculator _calculator;
        public TollCalculatorTests()
        {
            _calculator = new TollCalculator();
        }
        [Fact]
        public void ReturnsDiffToDailyMax_WhenFeeExceedsMaxOutsideTimeframe()
        {
            var model = new Vehicle(VehicleType.Car);
            model.AddTollRegistration(53, new DateTime(2025,2,3,5,0,0));
            int fee = _calculator.CalculateTollFee(model, new DateTime(2025,2, 3, 6, 59, 0));

            Assert.Equal(7,fee);
        }
        [Fact]
        public void ReturnsZero_WhenFeeIsEqualToOrExceedsMax()
        {
            var model = new Vehicle(VehicleType.Car);
            model.AddTollRegistration(61, new DateTime(2025, 2, 3, 7, 0, 0));
            int fee = _calculator.CalculateTollFee(model, new DateTime(2025, 2, 3, 8, 0, 0)); 

            Assert.Equal(0, fee);
        }
        [Fact]
        public void ReturnsRegularFee_WhenWithinTimeFrameAndFeeIsHigherThanLast()
        {
            var model = new Vehicle(VehicleType.Car);
            model.AddTollRegistration(1, new DateTime(2025, 2, 3, 7, 0, 0));
            int fee = _calculator.CalculateTollFee(model, new DateTime(2025, 2, 3, 7, 10, 0));

            Assert.True(fee > 1);
        }
    }
}
