using BoatDataObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using BoatLogicLayer;

namespace WpfPresentationLayer
{
    /// <summary>
    /// Interaction logic for frmChangePassword.xaml
    /// </summary>
    public partial class frmChangePassword : Window
    {
        User _user;
        public frmChangePassword(User user)
        {
            InitializeComponent();
            _user = user;
            this.Title += " for " + _user.FirstName + " " + _user.LastName;
        }

        private void btnSubmit_Click(object sender, RoutedEventArgs e)
        {
            var oldPassword = txtOldPassword.Password;
            var newPassword = txtNewPassword.Password;
            var confirmPassword = txtConfirmPassword.Password;

            if(newPassword == oldPassword) 
            {
                MessageBox.Show("You need to choose a new password.");
                return;
            }
            if(newPassword != confirmPassword)
            {
                MessageBox.Show("New Password and Confirm must match.");
                return;
            }
            try
            {
                var usrMgr = new UserManager();
                usrMgr.UpdatePassword(_user.EmployeeID, oldPassword, newPassword);
                MessageBox.Show("Password updated.");
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Password not updated.\n" + ex.Message);
            }

        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
