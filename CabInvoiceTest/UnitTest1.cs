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

        [Test]
        public void GivenDistanceAndTime_Should_Return_Fare()
        {
            double distance = 5; //in km
            int time = 20;   //in minutes

            double fare = invoiceGenerator.CalculateFare(new Ride(distance, time));

            Assert.AreEqual(70, fare);
        }

        [Test]
        public void GivenInvalidDistance_Should_Return_CabInvoiceException()
        {
            double distance = -5; //in km
            int time = 20;   //in minute

            var exception = Assert.Throws<InvoiceException>(() => invoiceGenerator.CalculateFare(new Ride(distance, time)));

            Assert.AreEqual(InvoiceException.ExceptionType.INVALID_DISTANCE, exception.exception);
        }
        [Test]
        public void GivenInvalidTime_Should_Return_CabInvoiceException()
        {
            double distance = 5; //in km
            int time = -20;   //in minute

            var exception = Assert.Throws<InvoiceException>(() => invoiceGenerator.CalculateFare(new Ride(distance, time)));

            Assert.AreEqual(InvoiceException.ExceptionType.INVALID_TIME, exception.exception);
        }
        [Test]
        public void GivenListOfRides_Should_Return_TotalFare()
        {
            rideList = new List<Ride> { new Ride(5, 20), new Ride(3, 15), new Ride(2, 10) };

            double fare = invoiceGenerator.CalculateFareForMultipleRides(rideList);

            Assert.AreEqual(145, fare);

        }
        [Test]
        public void GivenNullRides_Should_Return_CabInvoiceException()
        {
            rideList = new List<Ride> { new Ride(5, 20), null, new Ride(2, 10) };

            var exception = Assert.Throws<InvoiceException>(() => invoiceGenerator.CalculateFareForMultipleRides(rideList));

            Assert.AreEqual(InvoiceException.ExceptionType.NULL_RIDES, exception.exception);
        }
        [Test]
        public void GivenListOfRides_Should_Return_InvoiceData()
        {
            rideList = new List<Ride> { new Ride(5, 20), new Ride(3, 15), new Ride(2, 10) };
            double expectedFare = 145;
            int expectedRides = 3;
            double expectedAverage = expectedFare / expectedRides;

            InvoiceData data = invoiceGenerator.GetInvoiceSummary(rideList);

            Assert.IsTrue(data.noOfRides == expectedRides && data.totalFare == expectedFare && data.averageFare == expectedAverage);
        }
        [Test]
        public void GivenNullRides_WhenAddingToDictionary_Should_Return_CabInvoiceException()
        {
            rideList = new List<Ride> { new Ride(5, 20), null, new Ride(2, 10) };

            var exception = Assert.Throws<InvoiceException>(() => invoiceGenerator.AddRides(1, rideList));

            Assert.AreEqual(InvoiceException.ExceptionType.NULL_RIDES, exception.exception);

        }
        [Test]
        public void GivenUserId_WhenPresent_Should_Return_CabInvoiceSummary()
        {
            rideList = new List<Ride> { new Ride(5, 20), new Ride(3, 15), new Ride(2, 10) };
            double expectedFare = 145;
            int expectedRides = 3;
            double expectedAverage = expectedFare / expectedRides;

            invoiceGenerator.AddRides(1, rideList);
            InvoiceData data = invoiceGenerator.GetUserInvoice(1);

            Assert.IsTrue(data.noOfRides == expectedRides && data.totalFare == expectedFare && data.averageFare == expectedAverage);
        }
        [Test]
        public void GivenUserId_WhenAbsent_Should_Return_CabInvoiceException()
        {
            var exception = Assert.Throws<InvoiceException>(() => invoiceGenerator.GetUserInvoice(1));

            Assert.AreEqual(InvoiceException.ExceptionType.INVALID_USER_ID, exception.exception);
        }

    }
}