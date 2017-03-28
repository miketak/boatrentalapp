using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BoatDataAccess;
using BoatDataObjects;

namespace BoatLogicLayer
{
    public class BoatManager
    {
        public List<Boat> RetrieveBoatsForRent()
        {
            List<Boat> boats = null;

            try
            {
                boats = BoatAccessor.RetrieveBoatsByStatus("Available");
            }
            catch (Exception)
            {
                throw;
            }
            return boats;
        }

        public List<Boat> RetrieveActiveBoats()
        {
            List<Boat> boats = null;

            try
            {
                boats = BoatAccessor.RetrieveBoatsByActive(active: true);
            }
            catch (Exception)
            {
                throw;
            }
            return boats;
        }

        public List<Boat> RetrieveInactiveBoats()
        {
            List<Boat> boats = null;

            try
            {
                boats = BoatAccessor.RetrieveBoatsByActive(active: false);
            }
            catch (Exception)
            {
                throw;
            }
            return boats;
        }

        public bool MarkBoatRented(Boat boat)
        {
            var result = false;
            try
            {
                if(1 == BoatAccessor.UpdateBoatStatus(boat.BoatID, 
                                          boat.BoatStatusID, "Rented"))
                {
                    result = true;
                }
                else
                {
                    throw new ApplicationException("Update failed.");
                }
            }
            catch (Exception)
            {
                throw;
            }
            return result;
        }

        public bool CreateBoat(Boat boat)
        {
            return BoatAccessor.CreateNewBoat(boat) == 1;

        }

       
    }
    
}
