using BoatDataObjects;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoatDataAccess
{
    public class EmployeeAccessor
    {
        public static List<Employee> RetrieveEmployeeList(bool active = true)
        {
            var employees = new List<Employee>();

            // start with a SqlConnection
            var conn = DBConnection.GetDBConnection();

            // for the where clause
            var whereActive = active ? 1 : 0;

            // need some command text
            var cmdText = @"SELECT EmployeeID, FirstName, " +
                          @"LastName, PhoneNumber, Email, " +
                          @"UserName, Active " +
                          @"FROM Employee " +
                          @"WHERE ACTIVE = @active";

            // get a SqlCommand object
            var cmd = new SqlCommand(cmdText, conn);
            // add a parameter
            cmd.Parameters.Add("@active", SqlDbType.Bit);
            cmd.Parameters["@active"].Value = whereActive;

            // try to execute the command
            try
            {
                // first, open the connection
                conn.Open();

                // create a data reader by executing the command
                var reader = cmd.ExecuteReader();

                // check to see if anything was returned
                if (reader.HasRows)
                {
                    // loop through the rows
                    while (reader.Read())
                    {
                        // read the values from each row and use them
                        // to create a c# object we can use
                        var emp = new Employee()
                        {
                            EmployeeID = reader.GetInt32(0),
                            FirstName = reader.GetString(1),
                            LastName = reader.GetString(2),
                            PhoneNumber = reader.GetString(3),
                            Email = reader.GetString(4),
                            UserName = reader.GetString(5),
                            Active = reader.GetBoolean(6)
                        };
                        // don't leave the loop iteration without saving
                        employees.Add(emp);
                    }
                }
                reader.Close();
            }
            catch (SqlException)
            {
                throw;
            }
            finally
            {
                // housekeeping cleanup
                conn.Close();
            }

            return employees;
        }

        public static int RetrieveEmployeeCount(bool active = true)
        {
            var count = 0;

            // db connection
            var conn = DBConnection.GetDBConnection();

            // command text
            var cmdText = @"SELECT COUNT(EmployeeId) " +
                          @"FROM Employee " +
                          @"WHERE Active = @active ";

            // set up our parameter
            var whereActive = active ? 1 : 0;

            // set up the command and its parameter
            var cmd = new SqlCommand(cmdText, conn);
            cmd.Parameters.Add("@active", SqlDbType.Bit);
            cmd.Parameters["@active"].Value = whereActive;

            // now to get the data
            try
            {
                // open the connection
                conn.Open();

                // execute the command
                count = (int)cmd.ExecuteScalar();
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                conn.Close();   // good housekeeping
            }

            return count;
        }

        public static int UpdateEmployeeEmail(int employeeID, string email)
        {
            var count = 0;

            // db connection
            var conn = DBConnection.GetDBConnection();

            // command text
            var cmdText = @"sp_update_employee_email";

            // need a command
            var cmd = new SqlCommand(cmdText, conn);

            // set the command type to stored procedure
            cmd.CommandType = CommandType.StoredProcedure;

            // need parameters
            cmd.Parameters.Add("@EmployeeID", SqlDbType.Int);
            cmd.Parameters.Add("@EmailAddress", SqlDbType.VarChar, 100);

            // parameters need values
            cmd.Parameters["@EmployeeID"].Value = employeeID;
            cmd.Parameters["@EmailAddress"].Value = email;


            // now, to business....
            try
            {
                // open a connection
                conn.Open();

                // execute the command!
                count = cmd.ExecuteNonQuery();
            }
            catch (Exception)
            {
                
                throw;
            }
            finally
            {
                conn.Close();
            }

            return count;
        }
    }
}
