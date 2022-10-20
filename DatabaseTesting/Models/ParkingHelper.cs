using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseTesting.Models
{
    public class ParkingHelper
    {
        private ParkingContext parkingContext;

        public ParkingHelper(ParkingContext context)
        {
            this.parkingContext = context;
        }

        public Pass CreatePass(string purchaser, bool premium, int capacity)
        {
            Pass newPass = new Pass();

            newPass.Purchaser = purchaser;

            newPass.Premium = premium;

            newPass.Capacity = capacity;

            parkingContext.Passes.Add(newPass);

            parkingContext.SaveChanges();

            return newPass;
        }

        public ParkingSpot CreateParkingSpot()
        {
            ParkingSpot newSpot = new ParkingSpot();

            newSpot.Occupied = false;

            parkingContext.ParkingSpots.Add(newSpot);

            return newSpot;
        }

        public bool AddVehicleToPass(string passholderName, string vehicleLicence)
        {
            Pass pass = parkingContext.Passes.Include(p => p.Vehicles).FirstOrDefault(p => p.Purchaser == passholderName);

            if (pass == null)
            {
                throw new KeyNotFoundException("No pass found for the given passholder");
            }

            Vehicle vehicle = parkingContext.Vehicles.FirstOrDefault(v => v.Licence == vehicleLicence);

            if (vehicle == null)
            {
                throw new KeyNotFoundException("No vehicle found for the given vehicle");
            }

            if (pass.Capacity == pass.Vehicles.Count)
            {
                throw new Exception("No more vehicles can be added to this pass.");
            }

            pass.Vehicles.Add(vehicle);
            vehicle.PassID = pass.ID;

            parkingContext.SaveChanges();

            return true;
        }
    }
}
