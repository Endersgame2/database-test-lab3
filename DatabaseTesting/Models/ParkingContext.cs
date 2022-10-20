using DatabaseTesting;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;

namespace DatabaseTesting.Models
{
    public class ParkingContext : DbContext
    {
        public virtual DbSet<Vehicle> Vehicles { get; set; }

        public virtual DbSet<Pass> Passes { get; set; }

        public virtual DbSet<ParkingSpot> ParkingSpots { get; set; }

        public virtual DbSet<Reservation> Reservations { get; set; }
    }
}