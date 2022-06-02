using FitnessReservatieBL.Domeinen;
using FitnessReservatieBL.Domeinen.Enums;
using FitnessReservatieBL.DTO;
using FitnessReservatieBL.Interfaces;
using FitnessReservatieBL.Managers;
using FitnessReservatieBL.Managers.Eigenschappen;
using FitnessReservatieDL.ADO.NET;
using System;
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
        #region IRepositories
        private Admin _ingelogdeAdmin;
        private KlantManager _klantManager;
        private ToestelTypeManager _toestelTypeManager;
        private TijdslotManager _tijdslotManager;
        private ToestelManager _toestelManager;
        private ReservatieInfoManager _reservatieInfoManager;
        private ReservatieManager _reservatieManager;
        #endregion

        public AdminWindow(Admin admin)
        {
            InitializeComponent();
            this._ingelogdeAdmin = admin;
            var connectiestring = ConfigurationManager.ConnectionStrings["FinalDBConnection"].ToString();

            LabelWelkomAdmin.Content += $"{_ingelogdeAdmin.Voornaam} {_ingelogdeAdmin.Naam}";

            # region IRepositories instancieren
            IKlantRepository klantRepo = new KlantRepoADO(connectiestring);
            _klantManager = new KlantManager(klantRepo);

            IToestelTypeRepository toesteltypeRepo = new ToestelTypeRepoADO(connectiestring);
            _toestelTypeManager = new ToestelTypeManager(toesteltypeRepo);

            ITijdslotRepository tijdslotRepo = new TijdslotRepoADO(connectiestring);
            _tijdslotManager = new TijdslotManager(tijdslotRepo);

            IToestelRepository toestelRepo = new ToestelRepoADO(connectiestring);
            _toestelManager = new ToestelManager(toestelRepo, toesteltypeRepo);

            IReservatieInfoRepository reservatieInfoRepo = new ReservatieInfoRepoADO(connectiestring);
            _reservatieInfoManager = new ReservatieInfoManager(reservatieInfoRepo);

            IReservatieRepository reservatieRepo = new ReservatieRepoADO(connectiestring);
            _reservatieManager = new ReservatieManager(reservatieRepo, reservatieInfoRepo, klantRepo, toestelRepo);
            #endregion

            foreach (var toesteltype in _toestelTypeManager.SelecteerToestelType())
            {
                ComboBoxToestelType.Items.Add(toesteltype.ToestelNaam);
            }
        }

        #region Klanten Tab
        private void TextBoxKlantNummer_TextChanged(object sender, TextChangedEventArgs e)
        {
            ButtonKlantZoek.IsEnabled = true;
            if (!string.IsNullOrWhiteSpace(TextBoxKlantNummer.Text)) TextBoxKlantNaam.IsEnabled = false;
            else
            {
                TextBoxKlantNaam.IsEnabled = true;
                ButtonKlantZoek.IsEnabled = false;
            }
        }
        private void TextBoxKlantNaam_TextChanged(object sender, TextChangedEventArgs e)
        {
            ButtonKlantZoek.IsEnabled = true;
            if (!string.IsNullOrWhiteSpace(TextBoxKlantNaam.Text)) TextBoxKlantNummer.IsEnabled = false;
            else
            {
                TextBoxKlantNummer.IsEnabled = true;
                ButtonKlantZoek.IsEnabled = false;
            }
        }
        private void ButtonKlantZoek_Click(object sender, RoutedEventArgs e)
        {
            ListViewKlantTracker.Items.Clear();
            bool x = int.TryParse(TextBoxKlantNummer.Text.Trim(), out int klantnummer);
            string klantnaam = TextBoxKlantNaam.Text.Trim();
            foreach (var klant in _klantManager.ZoekKlanten(klantnummer, klantnaam))
            {
                ListViewKlantTracker.Items.Add(klant);
            }
            RadioButtonCustomerAll.IsChecked = false;

        }

        private void RadioButtonCustomerAll_Checked(object sender, RoutedEventArgs e)
        {
            ListViewKlantTracker.Items.Clear();
            foreach (var klant in _klantManager.ZoekKlanten(0, null))
            {
                ListViewKlantTracker.Items.Add(klant);
            }
        }
        #endregion

        #region Toestellen Tab
        private void TextBoxToestelNummer_TextChanged(object sender, TextChangedEventArgs e)
        {
            ButtonToestelZoek.IsEnabled = true;
            if (!string.IsNullOrWhiteSpace(TextBoxToestelNummer.Text))
            {
                TextBoxToestelNaam.IsEnabled = false;
                ComboBoxToestelType.IsEnabled = false;
            }
            else
            {
                TextBoxToestelNaam.IsEnabled = true;
                ComboBoxToestelType.IsEnabled = true;
                ButtonToestelZoek.IsEnabled = false;
            }
        }
        private void TextBoxToestelNaam_TextChanged(object sender, TextChangedEventArgs e)
        {
            ButtonToestelZoek.IsEnabled = true;
            if (!string.IsNullOrWhiteSpace(TextBoxToestelNaam.Text))
            {
                TextBoxToestelNummer.IsEnabled = false;
                ComboBoxToestelType.IsEnabled = false;
            }
            else
            {
                TextBoxToestelNummer.IsEnabled = true;
                ComboBoxToestelType.IsEnabled = true;
                ButtonToestelZoek.IsEnabled = false;
            }
        }
        private void ComboBoxToestelType_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ButtonToestelZoek.IsEnabled = true;
            if (ComboBoxToestelType.SelectedIndex != -1)
            {
                TextBoxToestelNaam.IsEnabled = false;
                TextBoxToestelNummer.IsEnabled = false;
            }
            else
            {
                TextBoxToestelNaam.IsEnabled = true;
                TextBoxToestelNummer.IsEnabled = true;
                ButtonToestelZoek.IsEnabled = false;
            }
        }
        private void ButtonToestelZoek_Click(object sender, RoutedEventArgs e)
        {
            ListViewToestelTracker.Items.Clear();
            bool x = int.TryParse(TextBoxToestelNummer.Text.Trim(), out int toestelnummer);
            string toestelnaam = TextBoxToestelNaam.Text.Trim();
            var toesteltype = "";
            if (ComboBoxToestelType.SelectedIndex == -1)
            {
                toesteltype = null;
            }
            else toesteltype = ComboBoxToestelType.SelectedValue.ToString();
            foreach (var toestel in _toestelManager.ZoekToestellen(null, toestelnummer, toestelnaam, toesteltype))
            {
                ListViewToestelTracker.Items.Add(toestel);
            }
            TextBoxToestelNummer.Clear();
            TextBoxToestelNaam.Clear();
            ComboBoxToestelType.SelectedIndex = -1;
            ButtonToestelZoek.IsEnabled = false;

            RadioButtonDeviceAll.IsChecked = false;
            RadioButtonDeviceService.IsChecked = false;
            RadioButtonDeviceAvailable.IsChecked = false;
            RadioButtonDeviceDeleted.IsChecked = false;
        }

        private void RadioButtonDeviceAll_Checked(object sender, RoutedEventArgs e)
        {
            ListViewToestelTracker.Items.Clear();
            foreach (var toestel in _toestelManager.ZoekToestellen(Status.operatief, 0, null, null))
            {
                ListViewToestelTracker.Items.Add(toestel);
            }
            foreach (var toestel in _toestelManager.ZoekToestellen(Status.onderhoud, 0, null, null))
            {
                ListViewToestelTracker.Items.Add(toestel);
            }
            foreach (var toestel in _toestelManager.ZoekToestellen(Status.verwijderd, 0, null, null))
            {
                ListViewToestelTracker.Items.Add(toestel);
            }
        }
        private void RadioButtonDeviceAvailable_Checked(object sender, RoutedEventArgs e)
        {
            ListViewToestelTracker.Items.Clear();
            foreach (var toestel in _toestelManager.ZoekToestellen(Status.operatief, 0, null, null))
            {
                ListViewToestelTracker.Items.Add(toestel);
            }
        }
        private void RadioButtonDeviceService_Checked(object sender, RoutedEventArgs e)
        {
            ListViewToestelTracker.Items.Clear();
            foreach (var toestel in _toestelManager.ZoekToestellen(Status.onderhoud, 0, null, null))
            {
                ListViewToestelTracker.Items.Add(toestel);
            }
        }
        private void RadioButtonDeviceDeleted_Checked(object sender, RoutedEventArgs e)
        {
            ListViewToestelTracker.Items.Clear();
            foreach (var toestel in _toestelManager.ZoekToestellen(Status.verwijderd, 0, null, null))
            {
                ListViewToestelTracker.Items.Add(toestel);
            }
        }
        private void ButtonWijziging_Opened(object sender, RoutedEventArgs e)
        {
            if (ListViewToestelTracker.SelectedValue == null)
            {
                ComboBoxStatusUpdate.IsEnabled = false;
            }
            else if (!ListViewToestelTracker.SelectedValue.ToString().Contains("verwijderd"))
            {
                ComboBoxStatusUpdate.IsEnabled = true;
            }
            else ComboBoxStatusUpdate.IsEnabled = false;
        }

        private void ButtonStatusUpdate_Click(object sender, RoutedEventArgs e)
        {
            ComboBoxItem nieuweStatus = (ComboBoxItem)ComboBoxStatusUpdate.SelectedItem;
            MessageBoxResult result = MessageBox.Show($"U staat op het punt de status te wijzigen van \r\r >>> {ListViewToestelTracker.SelectedItem.ToString()} naar {nieuweStatus.Content.ToString()} <<< \r\r" +
                $"Bent u zeker dat u verder wil gaan?", "Opgelet!", MessageBoxButton.OKCancel, MessageBoxImage.Warning);

            if (result == MessageBoxResult.OK)
            {
                string updateToestel = _toestelManager.UpdateToestelStatus((DTOToestelInfo)ListViewToestelTracker.SelectedValue, nieuweStatus.Content.ToString());
                MessageBox.Show(updateToestel, "");
            }

            ListViewToestelTracker.Items.Clear();
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
            string maakToestel = _toestelManager.SchrijfToestelInDB(TextBoxNieuwToestelToestelnaam.Text.Trim(), toesteltype.Content.ToString());
            MessageBox.Show(maakToestel, "");

            ListViewToestelTracker.Items.Clear();
            RadioButtonDeviceAll_Checked(sender, e);
        }
        #endregion
        
        #region Reservatie Tab
        private void TextBoxReservatieNummer_TextChanged(object sender, TextChangedEventArgs e)
        {
            ButtonReservatieZoek.IsEnabled = true;
            if(!string.IsNullOrWhiteSpace(TextBoxReservatieNummer.Text))
            {
                TextBoxReservatieKlantNummer.IsEnabled = false;
                TextBoxReservatieToestelNummer.IsEnabled = false;
                DatePickerReservatieSelector.IsEnabled = false;
            }
            else
            {
                TextBoxReservatieKlantNummer.IsEnabled = true;
                TextBoxReservatieToestelNummer.IsEnabled = true;
                DatePickerReservatieSelector.IsEnabled = true;
                ButtonReservatieZoek.IsEnabled = false;
            }
        }
        private void TextBoxReservatieKlantNummer_TextChanged(object sender, TextChangedEventArgs e)
        {
            ButtonReservatieZoek.IsEnabled = true;
            if (!string.IsNullOrWhiteSpace(TextBoxReservatieKlantNummer.Text))
            {
                TextBoxReservatieNummer.IsEnabled = false;
                TextBoxReservatieToestelNummer.IsEnabled = false;
                DatePickerReservatieSelector.IsEnabled = false;
            }
            else
            {
                TextBoxReservatieNummer.IsEnabled = true;
                TextBoxReservatieToestelNummer.IsEnabled = true;
                DatePickerReservatieSelector.IsEnabled = true;
                ButtonReservatieZoek.IsEnabled = false;
            }
        }
        private void TextBoxReservatieToestelNummer_TextChanged(object sender, TextChangedEventArgs e)
        {
            ButtonReservatieZoek.IsEnabled = true;
            if (!string.IsNullOrWhiteSpace(TextBoxReservatieToestelNummer.Text))
            {
                TextBoxReservatieNummer.IsEnabled = false;
                TextBoxReservatieKlantNummer.IsEnabled = false;
                DatePickerReservatieSelector.IsEnabled = false;
            }
            else
            {
                TextBoxReservatieNummer.IsEnabled = true;
                TextBoxReservatieKlantNummer.IsEnabled = true;
                DatePickerReservatieSelector.IsEnabled = true;
                ButtonReservatieZoek.IsEnabled = false;
            }
        }
        private void DatePickerReservatieSelector_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            ButtonReservatieZoek.IsEnabled = true;
            if (DatePickerReservatieSelector.SelectedDate != null)
            {
                TextBoxReservatieNummer.IsEnabled = false;
                TextBoxReservatieKlantNummer.IsEnabled = false;
                TextBoxReservatieToestelNummer.IsEnabled = false;
            }
            else
            {
                TextBoxReservatieNummer.IsEnabled = true;
                TextBoxReservatieKlantNummer.IsEnabled = true;
                TextBoxReservatieToestelNummer.IsEnabled = true;
                ButtonReservatieZoek.IsEnabled = false;
            }
        }
        private void ButtonReservatieZoek_Click(object sender, RoutedEventArgs e)
        {
            ListViewReservatieTracker.Items.Clear();
            bool x = int.TryParse(TextBoxReservatieNummer.Text.Trim(), out int reservatienummer);
            bool y = int.TryParse(TextBoxReservatieKlantNummer.Text.Trim(), out int klantnummer);
            bool z = int.TryParse(TextBoxReservatieToestelNummer.Text.Trim(), out int toestelnummer);
            DateTime? datum;
            if(DatePickerReservatieSelector.SelectedDate != null)
            {
                datum = DatePickerReservatieSelector.SelectedDate.Value;
            }
            else
            {
                datum = null;
            }
            foreach (var reservatie in _reservatieManager.ZoekReservatie(reservatienummer, klantnummer, toestelnummer, datum))
            {
                ListViewReservatieTracker.Items.Add(reservatie);
            }

            TextBoxReservatieNummer.Clear();
            TextBoxReservatieKlantNummer.Clear();
            TextBoxReservatieToestelNummer.Clear();
            DatePickerReservatieSelector.SelectedDate=null;
            RadioButtonReservatieAll.IsChecked = false;
        }
        private void RadioButtonReservatieAll_Checked(object sender, RoutedEventArgs e)
        {
            ListViewReservatieTracker.Items.Clear();
            foreach (var reservatie in _reservatieManager.ZoekReservatie(null, null, null, null))
            {
                ListViewReservatieTracker.Items.Add(reservatie);
            }
        }
        #endregion

        #region Logout
        private void ButtonLogOut_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainwindow = new MainWindow();
            this.Close();
            mainwindow.Show();
        }
        #endregion

    }
}
