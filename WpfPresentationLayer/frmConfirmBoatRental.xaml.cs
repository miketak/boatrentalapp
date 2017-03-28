using BoatDataObjects;
using BoatLogicLayer;
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

namespace WpfPresentationLayer
{
    /// <summary>
    /// Interaction logic for frmConfirmBoatRental.xaml
    /// </summary>
    public partial class frmConfirmBoatRental : Window
    {
        Boat _boat;
        BoatManager _boatManager;
        public frmConfirmBoatRental(Boat boat, User user, BoatManager boatManager)
        {
            InitializeComponent();
            // if the user has no permission, close the form
            var hasPermission = false;
            foreach (var role in user.Roles)
            {
                if(role.RoleName == "Rental")
                {
                    hasPermission = true;
                    break;
                }
            }
            if (hasPermission == false)
            {
                this.Close();
            }
            _boat = boat;
            _boatManager = boatManager;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            this.lblBoatName.Content = "The " + _boat.Name;
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
        }

        private void btnConfirm_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (_boatManager.MarkBoatRented(_boat))
                {
                    MessageBox.Show("Rental Confirmed.");
                    this.DialogResult = true;
                }
                else
                {
                    MessageBox.Show("Boat could not be rented.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
