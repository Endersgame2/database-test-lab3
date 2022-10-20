using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseTesting
{
    public class ParkingSpot
    {
            public int ID { get; set; }

            public bool Occupied { get; set; }

            public virtual ICollection<Reservation> Reservations { get; set; }
    }
}
