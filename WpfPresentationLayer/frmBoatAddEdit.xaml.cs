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
    /// Interaction logic for frmBoatAddEdit.xaml
    /// </summary>
    public partial class frmBoatAddEdit : Window
    {
        Boat _boat = null;
        BoatManager _boatManager = null;

        public frmBoatAddEdit(BoatManager boatManager)
        {
            InitializeComponent();
            _boatManager = boatManager;
        }

        public frmBoatAddEdit(Boat boat, BoatManager boatManager)
        {
            InitializeComponent();
            _boatManager = boatManager;
            _boat = boat;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            if(_boat == null)
            {
                this.Title = "Add a New Boat to Inventory";
            }
            else
            {
                this.Title = "Edit the " + _boat.Name + "'s Record";

                txtBoatID.Text = _boat.BoatID.ToString();
                txtName.Text = _boat.Name;
                txtColor.Text = _boat.Color;
                txtStatus.Text = _boat.BoatStatusID.ToString();
                txtType.Text = _boat.BoatTypeID.ToString();

                txtHours.Text = _boat.Hours.ToString();
                txtModelYear.Text = _boat.ModelYear.ToString();
                datePurchaseDate.DisplayDate = _boat.PurchaseDate;
                datePurchaseDate.Text = datePurchaseDate.DisplayDate.ToShortDateString();
                txtDockSlip.Text = _boat.DockSlipID.ToString();
                txtActive.Text = _boat.Active.ToString();
            }
        }

        private void btnConfirm_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
