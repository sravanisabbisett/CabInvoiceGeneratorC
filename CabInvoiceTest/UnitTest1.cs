using CabInvoiceGeneerator;
using NUnit.Framework;

namespace Tests
{
    public class Tests
    {

        InvoiceGenerator invoiceGenerator;

        [SetUp]
        public void Setup()
        {
            invoiceGenerator = new InvoiceGenerator();
        }

        /// <summary>
        /// Givens the distance and time should calculate total fare.
        /// </summary>
        [Test]
        public void GivenDistanceAndTime_ShouldCalculateTotalFare()
        {
            double distance = 2.0;
            int time = 5;
            double expectedFare = 25;
            double actualFare = invoiceGenerator.CalculateFare(distance, time);
            Assert.AreEqual(expectedFare, actualFare);
        }

        /// <summary>
        /// Givens the minimum distance and time should Return minimum fare.
        /// </summary>
        [Test]
        public void GivenMinimumDistanceAndTime_ShouldReturnMinimumFare()
        {
            double distance = 0.2;
            int time = 1;
            double expectedFare = 5;
            double actulaFare = invoiceGenerator.CalculateFare(distance, time);
            Assert.AreEqual(expectedFare, actulaFare);
        }
    }
}