using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseTesting
{
    public class Pass
    {
        public int ID { get; set; }

        public string Purchaser { get; set; }

        public bool Premium { get; set; }

        public int Capacity { get; set; }

        public virtual ICollection<Vehicle> Vehicles { get; set; }

        public Pass()
        {
            Vehicles = new List<Vehicle>();
        }
    }
}
