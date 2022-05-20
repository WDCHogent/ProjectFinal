using FitnessReservatieBL.Domeinen;
using FitnessReservatieBL.Domeinen.Eigenschappen;
using FitnessReservatieBL.Domeinen.Enums;
using FitnessReservatieBL.Interfaces;
using FitnessReservatieBL.Managers;
using FitnessReservatieBL.Managers.Eigenschappen;
using FitnessReservatieDL.ADO.NET;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Configuration;
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

namespace FitnessReservatie.UI
{
    /// <summary>
    /// Interaction logic for AdminWindow.xaml
    /// </summary>
    public partial class AdminWindow : Window
    {
        private Admin _ingelogdeAdmin;
        private ToestelManager _toestelManager;
        private ToestelTypeManager _toestelTypeManager;

        public AdminWindow(Admin admin)
        {
            InitializeComponent();          
            this._ingelogdeAdmin = admin;
            var connectiestring = ConfigurationManager.ConnectionStrings["FinalDBConnection"].ToString();
            
            LabelWelkomAdmin.Content += $"{_ingelogdeAdmin.Voornaam} {_ingelogdeAdmin.Naam}";

            //IRepositories instancieren
            IToestelRepository toestelRepo = new ToestelRepoADO(ConfigurationManager.ConnectionStrings["FinalDBConnection"].ToString());
            _toestelManager = new ToestelManager(toestelRepo);

            IToestelTypeRepository toesteltypeRepo = new ToestelTypeRepoADO(ConfigurationManager.ConnectionStrings["FinalDBConnection"].ToString());
            _toestelTypeManager = new ToestelTypeManager(toesteltypeRepo);
            //

            foreach (var toesteltype in _toestelTypeManager.SelecteerToestelType())
            {
                ComboBoxToestelType.Items.Add(toesteltype.ToestelNaam);
            }
        }

        private void ButtonLogOut_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainwindow = new MainWindow();
            this.Close();
            mainwindow.Show();
        }

        private void RadioButtonDeviceAll_Checked(object sender, RoutedEventArgs e)
        {
            ListViewDeviceTracker.Items.Clear();
            foreach (var toestel in _toestelManager.GeefToestellenADHVStatus(Status.operatief))
            {
                ListViewDeviceTracker.Items.Add(toestel);
            }
            foreach (var toestel in _toestelManager.GeefToestellenADHVStatus(Status.onderhoud))
            {
                ListViewDeviceTracker.Items.Add(toestel);
            }
            foreach (var toestel in _toestelManager.GeefToestellenADHVStatus(Status.verwijderd))
            {
                ListViewDeviceTracker.Items.Add(toestel);
            }
        }

        private void RadioButtonDeviceAvailable_Checked(object sender, RoutedEventArgs e)
        {
            ListViewDeviceTracker.Items.Clear();
            foreach (var toestel in _toestelManager.GeefToestellenADHVStatus(Status.operatief))
            {
                ListViewDeviceTracker.Items.Add(toestel);
            }
        }

        private void RadioButtonDeviceService_Checked(object sender, RoutedEventArgs e)
        {
            ListViewDeviceTracker.Items.Clear();
            foreach (var toestel in _toestelManager.GeefToestellenADHVStatus(Status.onderhoud))
            {
                ListViewDeviceTracker.Items.Add(toestel);
            }
        }

        private void RadioButtonDeviceDeleted_Checked(object sender, RoutedEventArgs e)
        {
            ListViewDeviceTracker.Items.Clear();
            foreach (var toestel in _toestelManager.GeefToestellenADHVStatus(Status.verwijderd))
            {
                ListViewDeviceTracker.Items.Add(toestel);
            }
        }
        private void ButtonDeviceSearch_Click(object sender, RoutedEventArgs e)
        {
            ListViewDeviceTracker.Items.Clear();
        }

        private void ButtonCustomerSearch_Click(object sender, RoutedEventArgs e)
        {

        }

    }
}
