using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AfryTollCalculator.Models
{
    public class TollRegistration
    {
        public int Id { get; private set; }
        public DateTime Timestamp { get; set; }
        public int Amount { get; set; }
        public TollRegistration(int fee, DateTime timestamp)
        {
            CreateRandomId();
            Timestamp = timestamp;
            Amount = fee;
        }
        public TollRegistration(int fee)
        {
            CreateRandomId();
            Timestamp = DateTime.Now;
            Amount = fee;
        }
        private void CreateRandomId()
        {
            Random r = new Random();
            Id = r.Next();
        }
    }
}
