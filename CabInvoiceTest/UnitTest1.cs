using CabInvoiceGeneerator;
using NUnit.Framework;
using System.Collections.Generic;

namespace Tests
{
    public class Tests
    {

        InvoiceGenerator invoiceGenerator;
        List<Ride> rideList;

        [SetUp]
        public void Setup()
        {
            invoiceGenerator = new InvoiceGenerator();
        }

        /// <summary>
        /// Givens the distance and time for normal ride should return fare.
        /// </summary>
        [Test]
        public void GivenDistanceAndTimeForNormalRide_Should_Return_Fare()
        {
            double distance = 5; //in km
            int time = 20;   //in minutes
            invoiceGenerator = new InvoiceGenerator(RideType.NORMAL);

            double fare = invoiceGenerator.CalculateFare(new Ride(distance, time));

            Assert.AreEqual(70, fare);
        }
        /// <summary>
        /// Givens the distance and time for premium ride should return fare.
        /// </summary>
        [Test]
        public void GivenDistanceAndTimeForPremiumRide_Should_Return_Fare()
        {
            double distance = 5; //in km
            int time = 20;   //in minutes
            invoiceGenerator = new InvoiceGenerator(RideType.PREMIUM);

            double fare = invoiceGenerator.CalculateFare(new Ride(distance, time));

            Assert.AreEqual(115, fare);
        }

        /// <summary>
        /// Givens the invalid distance should return CAB invoice exception.
        /// </summary>
        [Test]
        public void GivenInvalidDistance_Should_Return_CabInvoiceException()
        {
            double distance = -5; //in km
            int time = 20;   //in minute
            invoiceGenerator = new InvoiceGenerator(RideType.NORMAL);

            var exception = Assert.Throws<InvoiceException>(() => invoiceGenerator.CalculateFare(new Ride(distance, time)));

            Assert.AreEqual(InvoiceException.ExceptionType.INVALID_DISTANCE, exception.exception);
        }
        /// <summary>
        /// Givens the invalid time should return CAB invoice exception.
        /// </summary>
        [Test]
        public void GivenInvalidTime_Should_Return_CabInvoiceException()
        {
            double distance = 5; //in km
            int time = -20;   //in minute
            invoiceGenerator = new InvoiceGenerator(RideType.NORMAL);

            var exception = Assert.Throws<InvoiceException>(() => invoiceGenerator.CalculateFare(new Ride(distance, time)));

            Assert.AreEqual(InvoiceException.ExceptionType.INVALID_TIME, exception.exception);
        }
        /// <summary>
        /// Givens the list of rides should return total fare.
        /// </summary>
        [Test]
        public void GivenListOfRides_Should_Return_TotalFare()
        {
            rideList = new List<Ride> { new Ride(5, 20), new Ride(3, 15), new Ride(2, 10) };
            invoiceGenerator = new InvoiceGenerator(RideType.NORMAL);

            double fare = invoiceGenerator.CalculateFareForMultipleRides(rideList);

            Assert.AreEqual(145, fare);

        }
        /// <summary>
        /// Givens the null rides should return CAB invoice exception.
        /// </summary>
        [Test]
        public void GivenNullRides_Should_Return_CabInvoiceException()
        {
            rideList = new List<Ride> { new Ride(5, 20), null, new Ride(2, 10) };
            invoiceGenerator = new InvoiceGenerator(RideType.NORMAL);

            var exception = Assert.Throws<InvoiceException>(() => invoiceGenerator.CalculateFareForMultipleRides(rideList));

            Assert.AreEqual(InvoiceException.ExceptionType.NULL_RIDES, exception.exception);
        }
        /// <summary>
        /// Givens the list of rides should return invoice data.
        /// </summary>
        [Test]
        public void GivenListOfRides_Should_Return_InvoiceData()
        {
            rideList = new List<Ride> { new Ride(5, 20), new Ride(3, 15), new Ride(2, 10) };
            double expectedFare = 145;
            int expectedRides = 3;
            double expectedAverage = expectedFare / expectedRides;
            invoiceGenerator = new InvoiceGenerator(RideType.NORMAL);

            InvoiceData data = invoiceGenerator.GetInvoiceSummary(rideList);

            Assert.IsTrue(data.noOfRides == expectedRides && data.totalFare == expectedFare && data.averageFare == expectedAverage);
        }
        /// <summary>
        /// Givens the null rides when adding to dictionary should return CAB invoice exception.
        /// </summary>
        [Test]
        public void GivenNullRides_WhenAddingToDictionary_Should_Return_CabInvoiceException()
        {
            rideList = new List<Ride> { new Ride(5, 20), null, new Ride(2, 10) };
            invoiceGenerator = new InvoiceGenerator(RideType.NORMAL);

            var exception = Assert.Throws<InvoiceException>(() => invoiceGenerator.AddRides(1, rideList));

            Assert.AreEqual(InvoiceException.ExceptionType.NULL_RIDES, exception.exception);

        }
        /// <summary>
        /// Givens the user identifier when present should return CAB invoice summary.
        /// </summary>
        [Test]
        public void GivenUserId_WhenPresent_Should_Return_CabInvoiceSummary()
        {
            rideList = new List<Ride> { new Ride(5, 20), new Ride(3, 15), new Ride(2, 10) };
            double expectedFare = 145;
            int expectedRides = 3;
            double expectedAverage = expectedFare / expectedRides;
            invoiceGenerator = new InvoiceGenerator(RideType.NORMAL);

            invoiceGenerator.AddRides(1, rideList);
            InvoiceData data = invoiceGenerator.GetUserInvoice(1);

            Assert.IsTrue(data.noOfRides == expectedRides && data.totalFare == expectedFare && data.averageFare == expectedAverage);
        }
        /// <summary>
        /// Givens the user identifier when absent should return CAB invoice exception.
        /// </summary>
        [Test]
        public void GivenUserId_WhenAbsent_Should_Return_CabInvoiceException()
        {
            invoiceGenerator = new InvoiceGenerator(RideType.NORMAL);

            var exception = Assert.Throws<InvoiceException>(() => invoiceGenerator.GetUserInvoice(1));

            Assert.AreEqual(InvoiceException.ExceptionType.INVALID_USER_ID, exception.exception);
        }


    }
}