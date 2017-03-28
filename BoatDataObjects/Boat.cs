using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoatDataObjects
{
    public class Boat
    {
        public int BoatID { get; set; }
        public int BoatTypeID { get; set; }
        public string Name { get; set; }
        public int Hours { get; set; }
        public int ModelYear { get; set; }
        public DateTime PurchaseDate { get; set; }
        public string BoatStatusID { get; set; }
        public int DockSlipID { get; set; }
        public string Color { get; set; }
        public bool Active { get; set; }
    }
}
