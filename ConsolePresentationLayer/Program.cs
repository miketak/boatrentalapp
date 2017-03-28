using BoatLogicLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsolePresentationLayer
{
    class Program
    {
        static void Main(string[] args)
        {
            var empManager = new EmployeeManager();
            var employees = empManager.GetFilteredEmployeeList(active: true);

            Console.WriteLine("There are " + empManager.EmployeeCount.ToString() + " active employees.");


            Console.WriteLine("\nCurrent employees: \n");
            for (int i = 0; i < employees.Count; i++)
            {
                Console.WriteLine(employees[i].FirstName +
                    " \t" + employees[i].LastName +
                    " \t" + employees[i].Email +
                    " \t" + employees[i].PhoneNumber);
            }

            if (empManager.UpdateEmployeeEmail(employees[0].EmployeeID, "wack@mole.com"))
            {
                employees = empManager.GetFilteredEmployeeList(active: true);
                
                Console.WriteLine("\nChanged email as follows: ");
                Console.WriteLine(employees[0].FirstName +
                    " \t" + employees[0].LastName +
                    " \t" + employees[0].Email +
                    " \t" + employees[0].PhoneNumber);
            }

            var inactiveEmployees = empManager.GetFilteredEmployeeList(active: false);
            Console.WriteLine("\nInactive employees: \n");
            foreach (var emp in inactiveEmployees)
            {
                Console.WriteLine(emp.FirstName + " " + emp.LastName);
            }

            Console.ReadKey();
        }
    }
}
