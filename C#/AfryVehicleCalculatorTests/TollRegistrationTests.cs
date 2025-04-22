using AfryTollCalculator.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AfryVehicleCalculatorTests
{
    public class TollRegistrationTests
    {
        [Fact]
        public void SetsIdOnCreation()
        {
            var t = new TollRegistration(0);

            Assert.True(t.Id > 0);
        }
        [Fact]
        public void SetsTimestampOnCreation()
        {
            var t = new TollRegistration(0);

            Assert.True(t.Timestamp > DateTime.MinValue);
        }
    }
}
