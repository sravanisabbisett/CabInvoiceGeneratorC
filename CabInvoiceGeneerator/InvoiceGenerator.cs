using System;
using System.Collections.Generic;
using System.Text;

namespace CabInvoiceGeneerator
{
    public class InvoiceGenerator
    {
        public const double COST_PERKILOMETER = 10.0;
        public const int COST_PERMINUTE = 1;
        public const double MINIMUM_FARE = 5;

        /// <summary>
        /// Calculates the fare of one ride
        /// </summary>
        /// <param name="distance">The distance.</param>
        /// <param name="time">The time.</param>
        /// <returns></returns>
        public double CalculateFare(double distance,int time)
        {
            double totalFare = distance * COST_PERKILOMETER + time * COST_PERMINUTE;
            return Math.Max(totalFare, MINIMUM_FARE);
        }
    }
}
