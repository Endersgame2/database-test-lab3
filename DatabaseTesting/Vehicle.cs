using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseTesting
{
    public class Vehicle
    {
            public int ID { get; set; }

            public int PassID { get; set; }

            public virtual Pass Pass { get; set; }

            public virtual ICollection<Reservation> Reservations { get; set; }

            public bool Parked { get; set; }

            public string Licence { get; set; }
    }
}
