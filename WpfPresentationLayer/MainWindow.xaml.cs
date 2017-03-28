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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfPresentationLayer
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        User _user = null;
        BoatManager _boatManager = new BoatManager();
        List<Boat> _boatsForRent = null;
        List<Boat> _boatInventory = null;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void frmMain_Loaded(object sender, RoutedEventArgs e)
        {
            hideAllTabs();
            txtUsername.Focus();
            _boatsForRent = _boatManager.RetrieveBoatsForRent();
            RefreshBoatInventory();
        }

        private void RefreshBoatInventory(bool active = true)
        {
            if (active)
            {
                _boatInventory = _boatManager.RetrieveActiveBoats();
            }
            else
            {
                _boatInventory = _boatManager.RetrieveInactiveBoats();
            }
        }


        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            var username = txtUsername.Text;
            var password = txtPassword.Password;

            var usrMgr = new UserManager();

            if (_user == null)
            {
                try
                {
                    _user = usrMgr.AuthenticateUser(username, password);
                    MessageBox.Show("Welcome back, " + _user.FirstName);
                    txtPassword.IsEnabled = false;
                    txtPassword.Password = "";
                    txtUsername.IsEnabled = false;
                    txtUsername.Text = "";
                    btnLogin.Content = "Log out";
                    showTabs();
                    btnLogin.IsDefault = false;
                    statusMessage.Content = "Logged in as " + _user.FirstName + " " + _user.LastName + ".";
                }
                catch (Exception ex)
                {
                    var msg = BoatDataObjects.AppSettings.Development ? ex.Message +
                        "\n" + ex.StackTrace : ex.Message;
                    MessageBox.Show(msg, "Login Failure!");
                }
            }
            else
            {
                _user = null;
                txtUsername.IsEnabled = true;
                txtPassword.IsEnabled = true;
                btnLogin.Content = "Login";
                txtUsername.Focus();
                hideAllTabs();
                btnLogin.IsDefault = true;
                statusMessage.Content = "You are not logged in.  Log in to continue.";
            }
        }

        private void showTabs()
        {
            tabconMain.Visibility = Visibility.Visible;

            foreach (var role in _user.Roles)
            {
                switch (role.RoleName)
                {
                    case "Rental":
                        tabRentBoats.Visibility = Visibility.Visible;
                        tabRentBoats.IsSelected = true;
                        break;
                    case "Checkout":
                        tabCheckOut.Visibility = Visibility.Visible;
                        tabCheckOut.IsSelected = true;
                        break;
                    case "Checkin":
                        tabCheckIn.Visibility = Visibility.Visible;
                        tabCheckIn.IsSelected = true;
                        break;
                    case "Inspection":
                        tabCheckIn.Visibility = Visibility.Visible;
                        tabCheckIn.IsSelected = true;
                        break;
                    case "Maintenance":
                        tabMaintenance.Visibility = Visibility.Visible;
                        tabMaintenance.IsSelected = true;
                        break;
                    case "Prep":
                        tabPrepForRent.Visibility = Visibility.Visible;
                        tabPrepForRent.IsSelected = true;
                        break;
                    case "Manager":
                        tabBoatInventory.Visibility = Visibility.Visible;
                        tabBoatInventory.IsSelected = true;
                        tabEmployeeManagement.Visibility = Visibility.Visible;
                        tabEmployeeManagement.IsSelected = true;
                        break;
                    default:
                        break;
                }
            }
            ((TabItem)tabconMain.Items[0]).Focus();
        }

        private void hideAllTabs()
        {
            foreach (var t in tabconMain.Items)
            {
                ((TabItem)t).Visibility = Visibility.Collapsed;
            }
            tabconMain.Visibility = Visibility.Hidden;



            //// hide the tabs
            //tabRentBoats.Visibility = Visibility.Collapsed;
            //tabCheckOut.Visibility = Visibility.Collapsed;
            //tabCheckIn.Visibility = Visibility.Collapsed;
            //tabInspection.Visibility = Visibility.Collapsed;
            //tabMaintenance.Visibility = Visibility.Collapsed;
            //tabPrepForRent.Visibility = Visibility.Collapsed;
            //tabBoatInventory.Visibility = Visibility.Collapsed;
            //tabEmployeeManagement.Visibility = Visibility.Collapsed;
        }



        private void mnuPassword_Click(object sender, RoutedEventArgs e)
        {
            // this is where we'll lauch a password change dialog
            if (_user == null)
            {
                MessageBox.Show("You must be logged in to change your password.");
            }
            else
            {
                var passwordWindow = new frmChangePassword(_user);
                passwordWindow.ShowDialog();
            }
        }

        private void tabRentBoats_GotFocus(object sender, RoutedEventArgs e)
        {
            dgBoatsForRent.ItemsSource = _boatsForRent;
        }

        private void dgBoatsForRent_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var boat = (Boat)dgBoatsForRent.SelectedItem;
            //_boatsForRent.Remove(boat);
            //MessageBox.Show("You selected the " + boat.Name);
            //dgBoatsForRent.Items.Refresh();

            var rentalForm = new frmConfirmBoatRental(boat, _user, _boatManager);
            var result = rentalForm.ShowDialog();
            if (result == false)
            {
                MessageBox.Show("Rental Canceled.");
            }
            _boatsForRent = _boatManager.RetrieveBoatsForRent();
            dgBoatsForRent.Items.Refresh();
            dgBoatsForRent.Focus();
        }

        private void tabBoatInventory_GotFocus(object sender, RoutedEventArgs e)
        {
            dgBoatInventory.ItemsSource = _boatInventory;
            dgBoatInventory.Focus();
            // dgBoatInventory.SelectedIndex = -1;
        }

        private void chkActive_Checked(object sender, RoutedEventArgs e)
        {
            RefreshBoatInventory(active: true);
            dgBoatInventory.Items.Refresh();
            dgBoatInventory.Focus();
            // dgBoatInventory.SelectedIndex = -1;
        }

        private void chkActive_Unchecked(object sender, RoutedEventArgs e)
        {
            RefreshBoatInventory(active: false);
            dgBoatInventory.Items.Refresh();
            dgBoatInventory.Focus();
            // dgBoatInventory.SelectedIndex = -1;
        }

        private void btnAddBoat_Click(object sender, RoutedEventArgs e)
        {
            var addEditForm = new frmBoatAddEdit(_boatManager);
            addEditForm.ShowDialog();
        }

        private void btnEditBoat_Click(object sender, RoutedEventArgs e)
        {
            var boat = (Boat)dgBoatInventory.SelectedItem;

            var addEditForm = new frmBoatAddEdit(boat, _boatManager);
            var result = addEditForm.ShowDialog();

            if (result == true)
            {
                // refresh _boatInventory and dgBoatInventory.Items
            }
            else
            {
                MessageBox.Show("Edit operation aborted.");
            }

            //if(dgBoatInventory.SelectedIndex.Equals(-1))
            //{
            //    MessageBox.Show("Please select a boat to edit!");
            //}
        }
    }
}
