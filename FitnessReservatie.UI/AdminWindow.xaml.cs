using FitnessReservatieBL.Domeinen;
using FitnessReservatieBL.Domeinen.Enums;
using FitnessReservatieBL.Interfaces;
using FitnessReservatieBL.Managers;
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

        public AdminWindow(Admin admin)
        {
            InitializeComponent();          
            this._ingelogdeAdmin = admin;
            LabelWelkomAdmin.Content += $"{_ingelogdeAdmin.Voornaam} {_ingelogdeAdmin.Naam}";

            IToestelRepository toestelRepo = new ToestelRepoADO(ConfigurationManager.ConnectionStrings["FinalDBConnection"].ToString());
            _toestelManager = new ToestelManager(toestelRepo);
        }

        private void ButtonLogOut_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainwindow = new MainWindow();
            this.Close();
            mainwindow.Show();
        }

        private void RadioButtonAll_Checked(object sender, RoutedEventArgs e)
        {
            ListViewDeviceTracker.Items.Clear();
            foreach (var toestel in _toestelManager.GeefToestellenMetStatus(Status.operatief))
            {
                ListViewDeviceTracker.Items.Add(toestel);
            }
            foreach (var toestel in _toestelManager.GeefToestellenMetStatus(Status.onderhoud))
            {
                ListViewDeviceTracker.Items.Add(toestel);
            }
            foreach (var toestel in _toestelManager.GeefToestellenMetStatus(Status.verwijderd))
            {
                ListViewDeviceTracker.Items.Add(toestel);
            }
        }

        private void RadioButtonAvailable_Checked(object sender, RoutedEventArgs e)
        {
            ListViewDeviceTracker.Items.Clear();
            foreach (var toestel in _toestelManager.GeefToestellenMetStatus(Status.operatief))
            {
                ListViewDeviceTracker.Items.Add(toestel);
            }
        }

        private void RadioButtonService_Checked(object sender, RoutedEventArgs e)
        {
            ListViewDeviceTracker.Items.Clear();
            foreach (var toestel in _toestelManager.GeefToestellenMetStatus(Status.onderhoud))
            {
                ListViewDeviceTracker.Items.Add(toestel);
            }
        }

        private void RadioButtonDeleted_Checked(object sender, RoutedEventArgs e)
        {
            ListViewDeviceTracker.Items.Clear();
            foreach (var toestel in _toestelManager.GeefToestellenMetStatus(Status.verwijderd))
            {
                ListViewDeviceTracker.Items.Add(toestel);
            }
        }
    }
}
