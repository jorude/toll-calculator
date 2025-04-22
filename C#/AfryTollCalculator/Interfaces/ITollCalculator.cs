using AfryTollCalculator.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AfryTollCalculator.Interfaces
{
    public interface ITollCalculator
    {
        int CalculateTollFee(Vehicle vehicle, DateTime date);
    }
}
