using DatabaseTesting;
using DatabaseTesting.Models;
using Microsoft.EntityFrameworkCore;
using Moq;

namespace ParkomaticTest
{
    [TestClass]
    public class ParkingUnitTest
    {
        public ParkingHelper helper { get; set; }
        IQueryable<ParkingSpot> parkingSpotData;
        IQueryable<Vehicle> vehicleData;
        IQueryable<Pass> passData;
        IQueryable<Reservation> reservationData;

        public ParkingUnitTest()
        {
            parkingSpotData = new List<ParkingSpot>{}.AsQueryable();

            var parkingSpotDbSet = new Mock<DbSet<ParkingSpot>>();

            parkingSpotDbSet.As<IQueryable<ParkingSpot>>().Setup(m => m.Provider).Returns(parkingSpotData.Provider);
            parkingSpotDbSet.As<IQueryable<ParkingSpot>>().Setup(m => m.Expression).Returns(parkingSpotData.Expression);
            parkingSpotDbSet.As<IQueryable<ParkingSpot>>().Setup(m => m.ElementType).Returns(parkingSpotData.ElementType);
            parkingSpotDbSet.As<IQueryable<ParkingSpot>>().Setup(m => m.GetEnumerator()).Returns(parkingSpotData.GetEnumerator);

            reservationData = new List<Reservation>{}.AsQueryable();

            var reservationDbSet = new Mock<DbSet<Reservation>>();

            reservationDbSet.As<IQueryable<Reservation>>().Setup(m => m.Provider).Returns(reservationData.Provider);
            reservationDbSet.As<IQueryable<Reservation>>().Setup(m => m.Expression).Returns(reservationData.Expression);
            reservationDbSet.As<IQueryable<Reservation>>().Setup(m => m.ElementType).Returns(reservationData.ElementType);
            reservationDbSet.As<IQueryable<Reservation>>().Setup(m => m.GetEnumerator()).Returns(reservationData.GetEnumerator);

            vehicleData = new List<Vehicle>()
            {
                new Vehicle() { ID = 0, Parked = false, Licence = "WPG 321" },
                new Vehicle() { ID = 1, Parked = false, Licence = "WPG 654" },
                new Vehicle() { ID = 2, Parked = false, Licence = "WPG 987" },
                new Vehicle() { ID = 3, Parked = false, Licence = "WPG 432" },
                new Vehicle() { ID = 4, Parked = false, Licence = "WPG 543" },
                new Vehicle() { ID = 5, Parked = false, Licence = "WPG 654" },
            }.AsQueryable();

            var vehicleDbSet = new Mock<DbSet<Vehicle>>();

            vehicleDbSet.As<IQueryable<Vehicle>>().Setup(m => m.Provider).Returns(vehicleData.Provider);
            vehicleDbSet.As<IQueryable<Vehicle>>().Setup(m => m.Expression).Returns(vehicleData.Expression);
            vehicleDbSet.As<IQueryable<Vehicle>>().Setup(m => m.ElementType).Returns(vehicleData.ElementType);
            vehicleDbSet.As<IQueryable<Vehicle>>().Setup(m => m.GetEnumerator()).Returns(vehicleData.GetEnumerator);

            passData = new List<Pass>()
            {
                new Pass() { ID = 1, Purchaser = "John Smith", Premium = false, Capacity = 5 },
                new Pass() { ID = 1, Purchaser = "Jane Doe", Premium = false, Capacity = 5 },
                new Pass() { ID = 1, Purchaser = "Alice", Premium = false, Capacity = 5 },
                new Pass() { ID = 1, Purchaser = "Bob", Premium = false, Capacity = 5 },
                new Pass() { ID = 1, Purchaser = "Joe", Premium = false, Capacity = 5 }
            }.AsQueryable();

            var passDbSet = new Mock<DbSet<Pass>>();

            passDbSet.As<IQueryable<Pass>>().Setup(m => m.Provider).Returns(passData.Provider);
            passDbSet.As<IQueryable<Pass>>().Setup(m => m.Expression).Returns(passData.Expression);
            passDbSet.As<IQueryable<Pass>>().Setup(m => m.ElementType).Returns(passData.ElementType);
            passDbSet.As<IQueryable<Pass>>().Setup(m => m.GetEnumerator()).Returns(passData.GetEnumerator);

            var mockParkingContext = new Mock<ParkingContext>();

            mockParkingContext.Setup(c => c.ParkingSpots).Returns(parkingSpotDbSet.Object);
            mockParkingContext.Setup(c => c.Reservations).Returns(reservationDbSet.Object);
            mockParkingContext.Setup(c => c.Vehicles).Returns(vehicleDbSet.Object);
            mockParkingContext.Setup(c => c.Passes).Returns(passDbSet.Object);

            helper = new ParkingHelper(mockParkingContext.Object);
        }

        [TestMethod]
        public void TestMethod1()
        {
            string testParameter1 = "Purchaser 1";
            bool testParameter2 = false;
            int testParameter3 = 10;

            Pass testPass = helper.CreatePass(testParameter1, testParameter2, testParameter3);

            Assert.AreNotEqual(testPass, null);

            Assert.AreEqual(testPass.Purchaser, testParameter1);

            Assert.AreEqual(testPass.Premium, testParameter2);

            Assert.AreEqual(testPass.Capacity, testParameter3);
        }

        [TestMethod]
        public void TestMethod2()
        {
            ParkingSpot testParkingSpot = helper.CreateParkingSpot();

            Assert.AreNotEqual(testParkingSpot, null);
        }

        [DataRow("Joe", "WPG 654")]
        [TestMethod]
        public async Task TestMethod3(string purchaserName, string vehicleLicence)
        {
            bool passExists = passData.Any(p => p.Purchaser == purchaserName);
            bool vehicleExists = vehicleData.Any(v => v.Licence == vehicleLicence);
            
            bool vehicleAdded = helper.AddVehicleToPass(purchaserName, vehicleLicence);

            Assert.IsTrue(vehicleAdded);
        }
    }
}