using AfryTollCalculator.Enums;
using AfryTollCalculator.Interfaces;
using AfryTollCalculator.Models;
using System;
using System.Globalization;

namespace AfryTollCalculator.Utilities
{
    public class TollCalculator : ITollCalculator
    {
        private const int MAXTOLLPERDAY = 60;
        private const int TIMEFRAMEMINUTES = 60;
        private List<VehicleType> _tollFreeVehicles =
            [VehicleType.Motorbike,
       VehicleType.Tractor,
       VehicleType.Emergency,
       VehicleType.Diplomat,
       VehicleType.Foreign,
       VehicleType.Military
         ];
        public TollCalculator()
        {

        }
        public int CalculateTollFee(Vehicle vehicle, DateTime date)
        {
            TollRegistration lastRegistration = vehicle.GetMostRecentTollRegistration();
            int totalFeesForDate = vehicle.GetDayTotalFees(date);
            int regularFeeForDate = GetTollFee(date, vehicle);
            if (totalFeesForDate >= MAXTOLLPERDAY)
                return 0;
            if (lastRegistration == null)
                return regularFeeForDate;
            TimeSpan timeDiffFromLast = date - lastRegistration.Timestamp;
            if (timeDiffFromLast.TotalMinutes < TIMEFRAMEMINUTES
                && regularFeeForDate < lastRegistration.Amount)
                return 0;
            if ((regularFeeForDate + totalFeesForDate) > MAXTOLLPERDAY)
            {
                // return only the amount up to max
                return MAXTOLLPERDAY - totalFeesForDate;
            }
            else
            {
                return regularFeeForDate;
            }
        }
        private int GetTollFee(DateTime date, Vehicle vehicle)
        {
            if (IsTollFreeDate(date) || _tollFreeVehicles.Contains(vehicle.Type)) return 0;

            int hour = date.Hour;
            int minute = date.Minute;

            if (hour == 6 && minute >= 0 && minute <= 29) return 8;
            else if (hour == 6 && minute >= 30 && minute <= 59) return 13;
            else if (hour == 7 && minute >= 0 && minute <= 59) return 18;
            else if (hour == 8 && minute >= 0 && minute <= 29) return 13;
            else if (hour >= 8 && hour <= 14 && minute >= 30 && minute <= 59) return 8;
            else if (hour == 15 && minute >= 0 && minute <= 29) return 13;
            else if (hour == 15 && minute >= 0 || hour == 16 && minute <= 59) return 18;
            else if (hour == 17 && minute >= 0 && minute <= 59) return 13;
            else if (hour == 18 && minute >= 0 && minute <= 29) return 8;
            else return 0;
        }
        private bool IsTollFreeDate(DateTime date)
        {
            int year = date.Year;
            int month = date.Month;
            int day = date.Day;

            if (date.DayOfWeek == DayOfWeek.Saturday || date.DayOfWeek == DayOfWeek.Sunday) return true;

            if (month == 1 && day == 1 ||
                month == 3 && (day == 28 || day == 29) ||
                month == 4 && (day == 1 || day == 30) ||
                month == 5 && (day == 1 || day == 8 || day == 9) ||
                month == 6 && (day == 5 || day == 6 || day == 21) ||
                month == 7 ||
                month == 11 && day == 1 ||
                month == 12 && (day == 24 || day == 25 || day == 26 || day == 31))
            {
                return true;
            }

            return false;
        }


    }
}