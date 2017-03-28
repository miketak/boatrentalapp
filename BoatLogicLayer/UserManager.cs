using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;
using BoatDataAccess;
using BoatDataObjects;

namespace BoatLogicLayer
{
    public class UserManager
    {
        public User AuthenticateUser(string username, string password)
        {
            User user = null;

            if (username.Length > 20 || username.Length < 5)
            {
                throw new ApplicationException("Invalid Username");
            }
            if(password.Length < 7)
            {   // might use a regex to test complexity rules
                throw new ApplicationException("Invalid Password");
            }



            // need a data access method to check the password
            try
            {
                // check for valid user
                if( 1 == UserAccessor.VerifyUsernameAndPassword(username, HashSha256(password)))
                {
                    password = null;
                    // we need a user object
                    user = UserAccessor.RetrieveUserByUsername(username);

                    // we need to add the roles
                    user.Roles = UserAccessor.RetrieveEmployeeRoles(user.EmployeeID);
                }
                else
                {
                    throw new ApplicationException("Authentication Failed!");
                }
            }
            catch
            {
                throw;
            }

            return user;
        }

        private string HashSha256(string source)
        {
            string result = "";

            // create a byte array
            byte[] data;

            // create a .NET Hash provider object
            using (SHA256 sha256hash = SHA256.Create())
            {
                // hash the input
                data = sha256hash.ComputeHash(Encoding.UTF8.GetBytes(source));
            }
            // create a string builder
            var s = new StringBuilder();

            // loop through the data creating letters for the string
            for (int i = 0; i < data.Length; i++)
            {
                s.Append(data[i].ToString("x2"));
            }
            result = s.ToString();
            return result;
        }

        // bool SetPassword(password)

        public bool UpdatePassword(int employeeID, string oldPassword, string newPassword)
        {
            var result = false;

            try
            {
                UserAccessor.UpdatePasswordHash(employeeID, HashSha256(oldPassword), HashSha256(newPassword));
                result = true;
            }
            catch (Exception)
            {
                throw;
            }
            return result;
        }
    }
}
