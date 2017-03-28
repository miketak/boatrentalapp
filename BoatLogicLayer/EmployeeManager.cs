using BoatDataObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BoatDataAccess;

namespace BoatLogicLayer
{
    public class EmployeeManager
    {
        private int _employeeCount;
        public int EmployeeCount
        {
            get
            {
                try
                {
                    _employeeCount = EmployeeAccessor.RetrieveEmployeeCount();
                }
                catch (Exception)
                {
                    throw;
                }
                return _employeeCount;
            }
            private set
            {
                _employeeCount = value;
            }
        }
        public List<Employee> GetFilteredEmployeeList(bool active = true)
        {
            try
            {
                return EmployeeAccessor.RetrieveEmployeeList(active);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Reply hazy. Try again later.", ex);
            }
        }

        public bool UpdateEmployeeEmail(int employeeID, string newEmail)
        {
            var result = false;
            try
            {
                if ( employeeID >= 100000 && newEmail != null && newEmail != "")
                {
                    var count = EmployeeAccessor.UpdateEmployeeEmail(employeeID, newEmail);
                    result = count == 1 ? true : false;
                }
            }
            catch 
            {
                result = false;
            }
            return result;
        }
    }
}
