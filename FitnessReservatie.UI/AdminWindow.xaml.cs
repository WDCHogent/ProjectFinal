using FitnessReservatieBL.Domeinen;
using FitnessReservatieBL.Domeinen.Enums;
using FitnessReservatieBL.DTO;
using FitnessReservatieBL.Interfaces;
using FitnessReservatieBL.Managers;
using FitnessReservatieBL.Managers.Eigenschappen;
using FitnessReservatieDL.ADO.NET;
using System.Configuration;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

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
        private KlantManager _klantManager;

        public AdminWindow(Admin admin)
        {
            InitializeComponent();
            this._ingelogdeAdmin = admin;
            var connectiestring = ConfigurationManager.ConnectionStrings["FinalDBConnection"].ToString();

            LabelWelkomAdmin.Content += $"{_ingelogdeAdmin.Voornaam} {_ingelogdeAdmin.Naam}";

            //IRepositories instancieren
            IToestelTypeRepository toesteltypeRepo = new ToestelTypeRepoADO(ConfigurationManager.ConnectionStrings["FinalDBConnection"].ToString());
            _toestelTypeManager = new ToestelTypeManager(toesteltypeRepo);

            IToestelRepository toestelRepo = new ToestelRepoADO(ConfigurationManager.ConnectionStrings["FinalDBConnection"].ToString());
            _toestelManager = new ToestelManager(toestelRepo,toesteltypeRepo);

            IKlantRepository klantRepo = new KlantRepoADO(connectiestring);
            _klantManager = new KlantManager(klantRepo);
            //

            foreach (var toesteltype in _toestelTypeManager.SelecteerToestelType())
            {
                ComboBoxToestelType.Items.Add(toesteltype.ToestelNaam);
            }
        }

        //Customer panel Tab
        private void TextBoxKlantNummer_TextChanged(object sender, TextChangedEventArgs e)
        {
            ButtonCustomerSearch.IsEnabled = true;
            if (!string.IsNullOrWhiteSpace(TextBoxKlantNummer.Text)) TextBoxKlantNaam.IsEnabled = false;
            else
            {
                TextBoxKlantNaam.IsEnabled = true;
                ButtonCustomerSearch.IsEnabled = false;
            }
        }
        private void TextBoxKlantNaam_TextChanged(object sender, TextChangedEventArgs e)
        {
            ButtonCustomerSearch.IsEnabled = true;
            if (!string.IsNullOrWhiteSpace(TextBoxKlantNaam.Text)) TextBoxKlantNummer.IsEnabled = false;
            else
            {
                TextBoxKlantNummer.IsEnabled = true;
                ButtonCustomerSearch.IsEnabled = false;
            }
        }
        private void ButtonCustomerSearch_Click(object sender, RoutedEventArgs e)
        {
            ListViewCustomerTracker.Items.Clear();
            bool x = int.TryParse(TextBoxKlantNummer.Text, out int klantnummer);
            string klantnaam = TextBoxKlantNaam.Text.Trim();
            foreach (var klant in _klantManager.ZoekKlanten(klantnummer, klantnaam))
            {
                ListViewCustomerTracker.Items.Add(klant);
            }
        }

        private void RadioButtonCustomerAll_Checked(object sender, RoutedEventArgs e)
        {
            ListViewCustomerTracker.Items.Clear();
            foreach (var klant in _klantManager.ZoekKlanten(0, null))
            {
                ListViewCustomerTracker.Items.Add(klant);
            }
        }
        //

        //Device Panel
        private void TextBoxToestelNummer_TextChanged(object sender, TextChangedEventArgs e)
        {
            ButtonDeviceSearch.IsEnabled = true;
            if (!string.IsNullOrWhiteSpace(TextBoxToestelNummer.Text))
            {
                TextBoxToestelNaam.IsEnabled = false;
                ComboBoxToestelType.IsEnabled = false;
            }
            else
            {
                TextBoxToestelNaam.IsEnabled = true;
                ComboBoxToestelType.IsEnabled = true;
                ButtonCustomerSearch.IsEnabled = false;
            }
        }
        private void TextBoxToestelNaam_TextChanged(object sender, TextChangedEventArgs e)
        {
            ButtonDeviceSearch.IsEnabled = true;
            if (!string.IsNullOrWhiteSpace(TextBoxToestelNaam.Text))
            {
                TextBoxToestelNummer.IsEnabled = false;
                ComboBoxToestelType.IsEnabled = false;
            }
            else
            {
                TextBoxToestelNummer.IsEnabled = true;
                ComboBoxToestelType.IsEnabled = true;
                ButtonCustomerSearch.IsEnabled = false;
            }
        }
        private void ComboBoxToestelType_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ButtonDeviceSearch.IsEnabled = true;
            if (ComboBoxToestelType.SelectedIndex != -1)
            {
                TextBoxToestelNaam.IsEnabled = false;
                TextBoxToestelNummer.IsEnabled = false;
            }
            else
            {
                TextBoxToestelNaam.IsEnabled = true;
                TextBoxToestelNummer.IsEnabled = true;
                ButtonCustomerSearch.IsEnabled = false;
            }
        }
        private void ButtonDeviceSearch_Click(object sender, RoutedEventArgs e)
        {
            ListViewDeviceTracker.Items.Clear();
            bool x = int.TryParse(TextBoxToestelNummer.Text, out int toestelnummer);
            string toestelnaam = TextBoxToestelNaam.Text.Trim();
            var toesteltype = "";
            if (ComboBoxToestelType.SelectedIndex == -1)
            {
                toesteltype = null;
            }
            else toesteltype = ComboBoxToestelType.SelectedValue.ToString();
            foreach (var toestel in _toestelManager.ZoekToestellen(null, toestelnummer, toestelnaam, toesteltype))
            {
                ListViewDeviceTracker.Items.Add(toestel);
            }
            TextBoxToestelNummer.Clear();
            TextBoxToestelNaam.Clear();
            ComboBoxToestelType.SelectedIndex = -1;
            ButtonDeviceSearch.IsEnabled = false;
        }

        private void RadioButtonDeviceAll_Checked(object sender, RoutedEventArgs e)
        {
            ListViewDeviceTracker.Items.Clear();
            foreach (var toestel in _toestelManager.ZoekToestellen(Status.operatief, 0, null, null))
            {
                ListViewDeviceTracker.Items.Add(toestel);
            }
            foreach (var toestel in _toestelManager.ZoekToestellen(Status.onderhoud, 0, null, null))
            {
                ListViewDeviceTracker.Items.Add(toestel);
            }
            foreach (var toestel in _toestelManager.ZoekToestellen(Status.verwijderd, 0, null, null))
            {
                ListViewDeviceTracker.Items.Add(toestel);
            }
        }
        private void RadioButtonDeviceAvailable_Checked(object sender, RoutedEventArgs e)
        {
            ListViewDeviceTracker.Items.Clear();
            foreach (var toestel in _toestelManager.ZoekToestellen(Status.operatief, 0, null, null))
            {
                ListViewDeviceTracker.Items.Add(toestel);
            }
        }
        private void RadioButtonDeviceService_Checked(object sender, RoutedEventArgs e)
        {
            ListViewDeviceTracker.Items.Clear();
            foreach (var toestel in _toestelManager.ZoekToestellen(Status.onderhoud, 0, null, null))
            {
                ListViewDeviceTracker.Items.Add(toestel);
            }
        }
        private void RadioButtonDeviceDeleted_Checked(object sender, RoutedEventArgs e)
        {
            ListViewDeviceTracker.Items.Clear();
            foreach (var toestel in _toestelManager.ZoekToestellen(Status.verwijderd, 0, null, null))
            {
                ListViewDeviceTracker.Items.Add(toestel);
            }
        }
        private void ButtonWijziging_Opened(object sender, RoutedEventArgs e)
        {
            if (ListViewDeviceTracker.SelectedValue == null)
            {
                ComboBoxStatusUpdate.IsEnabled = false;
            }
            else if (!ListViewDeviceTracker.SelectedValue.ToString().Contains("verwijderd"))
            {
                ComboBoxStatusUpdate.IsEnabled = true;
            }
            else ComboBoxStatusUpdate.IsEnabled = false;
        }

        private void ButtonStatusUpdate_Click(object sender, RoutedEventArgs e)
        {
            ComboBoxItem nieuweStatus = (ComboBoxItem)ComboBoxStatusUpdate.SelectedItem;
            MessageBoxResult result = MessageBox.Show($"U staat op het punt de status te wijzigen van \r\r >>> {ListViewDeviceTracker.SelectedItem.ToString()} naar {nieuweStatus.Content.ToString()} <<< \r\r" +
                $"Bent u zeker dat u verder wil gaan?", "Opgelet!", MessageBoxButton.OKCancel, MessageBoxImage.Warning);

            if (result == MessageBoxResult.OK)
            {
                string updateToestel = _toestelManager.UpdateToestelStatus((DTOToestelInfo)ListViewDeviceTracker.SelectedValue, nieuweStatus.Content.ToString());
                MessageBox.Show(updateToestel, "");
            }

            ListViewDeviceTracker.Items.Clear();
            RadioButtonDeviceAll_Checked(sender, e);

            ComboBoxStatusUpdate.SelectedIndex = -1;
        }

        private void TextBoxNieuwToestelToestelnaam_TextChanged(object sender, TextChangedEventArgs e)
        {
            ComboBoxNieuwToestelToesteltype.SelectedIndex=-1;
            ComboBoxNieuwToestelToesteltype.IsEnabled = true;
            ButtonNieuwToestel.IsEnabled = false;
        }

        private void ComboBoxNieuwToestelToesteltype_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ButtonNieuwToestel.IsEnabled = true;
        }

        private void ButtonNieuwToestel_Click(object sender, RoutedEventArgs e)
        {
            ComboBoxItem toesteltype = (ComboBoxItem)ComboBoxNieuwToestelToesteltype.SelectedItem;
            string maakToestel = _toestelManager.SchrijfToestelInDB(TextBoxNieuwToestelToestelnaam.Text, toesteltype.Content.ToString());
            MessageBox.Show(maakToestel, "");

            ListViewDeviceTracker.Items.Clear();
            RadioButtonDeviceAll_Checked(sender, e);
        }
        //



        //Logout
        private void ButtonLogOut_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainwindow = new MainWindow();
            this.Close();
            mainwindow.Show();
        }
        //

    }
}
