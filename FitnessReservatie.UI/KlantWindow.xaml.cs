using FitnessReservatieBL.Domeinen;
using FitnessReservatieBL.Domeinen.Eigenschappen;
using FitnessReservatieBL.DTO;
using FitnessReservatieBL.Interfaces;
using FitnessReservatieBL.Managers;
using FitnessReservatieBL.Managers.Eigenschappen;
using FitnessReservatieDL.ADO.NET;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;

namespace FitnessReservatie.UI
{
    /// <summary>
    /// Interaction logic for KlantWindow.xaml
    /// </summary>
    public partial class KlantWindow : Window
    {
        private Klant _ingelogdeKlant;
        private IReadOnlyList<DTOKlantReservatieInfo> _reservatiesKlant;
        private IReadOnlyList<Tijdslot> _einduurItemsSource;

        private int _aantalGereserveerdeUrenPerDatum;

        private int _maxAantalTijdsloten = 4;

        private ToestelTypeManager _toestelTypeManager;
        private TijdslotManager _tijdslotManager;

        private ReservatieManager _reservatieManager;
        private KlantManager _klantManager;
        private ToestelManager _toestelManager;

        public KlantWindow(Klant klant)
        {
            InitializeComponent();
            this._ingelogdeKlant = klant;
            LabelWelkomKlant.Content += $"{_ingelogdeKlant.Voornaam} {_ingelogdeKlant.Naam}";

            IKlantRepository klantRepo = new KlantRepoADO(ConfigurationManager.ConnectionStrings["FinalDBConnection"].ToString());
            _klantManager = new KlantManager(klantRepo);
            _reservatiesKlant = _klantManager.GeefKlantReservaties(_ingelogdeKlant.Klantnummer);
            ListViewReservations.ItemsSource = _reservatiesKlant;

            LabelKlantnummerReturned.Content += $"{_ingelogdeKlant.Klantnummer}";
            LabelNaamReturned.Content += $"{_ingelogdeKlant.Naam}";
            LabelVoornaamReturned.Content += $"{_ingelogdeKlant.Voornaam}";
            LabelMailadresReturned.Content += $"{_ingelogdeKlant.Mailadres}";

            IToestelTypeRepository toesteltypeRepo = new ToestelTypeRepoADO(ConfigurationManager.ConnectionStrings["FinalDBConnection"].ToString());
            _toestelTypeManager = new ToestelTypeManager(toesteltypeRepo);
            ComboBoxToesteltypeSelector1.ItemsSource = _toestelTypeManager.SelecteerToestelType();
            ComboBoxToesteltypeSelector2.ItemsSource = ComboBoxToesteltypeSelector1.ItemsSource;

            ITijdslotRepository tijdslotRepo = new TijdslotRepoADO(ConfigurationManager.ConnectionStrings["FinalDBConnection"].ToString());
            _tijdslotManager = new TijdslotManager(tijdslotRepo);

            ComboBoxBeginuurSelector1.ItemsSource = _tijdslotManager.SelecteerBeginuur();
            _einduurItemsSource = _tijdslotManager.SelecteerEinduur();

            DatePickerDatumSelector.BlackoutDates.AddDatesInPast();
            DatePickerDatumSelector.BlackoutDates.Add(new CalendarDateRange(DateTime.Today.AddDays(8), DateTime.MaxValue));

            IToestelRepository toestelRepo = new ToestelRepoADO(ConfigurationManager.ConnectionStrings["FinalDBConnection"].ToString());
            _toestelManager = new ToestelManager(toestelRepo);

            IReservatieRepository reservatieRepo = new ReservatieRepoADO(ConfigurationManager.ConnectionStrings["FinalDBConnection"].ToString());
            _reservatieManager = new ReservatieManager(reservatieRepo);
        }

        private void ButtonLogOut_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainwindow = new MainWindow();
            this.Close();
            mainwindow.Show();
        }
        private void DatePickerDatumSelector_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBoxEinduurSelector1.Items.Clear();
            ComboBoxBeginuurSelector2.Items.Clear();
            ComboBoxEinduurSelector2.Items.Clear();
            _aantalGereserveerdeUrenPerDatum = 0;
            foreach (DTOKlantReservatieInfo klantreservatie in _reservatiesKlant)
            {
                ComboBoxToesteltypeSelector1.IsEnabled = true;
                if (klantreservatie.Datum == DatePickerDatumSelector.SelectedDate)
                {
                    _aantalGereserveerdeUrenPerDatum += klantreservatie.Einduur - klantreservatie.Beginuur;
                    if (_aantalGereserveerdeUrenPerDatum >= _maxAantalTijdsloten)
                    {
                        ComboBoxToesteltypeSelector1.IsEnabled = false;
                    }
                }
            }

        }
        private void ComboBoxToesteltypeSelector1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBoxBeginuurSelector1.IsEnabled = true;
        }

        private void ComboBoxBeginuurSelector1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            CheckboxAddAnother.IsChecked = false;
            CheckboxAddAnother.IsEnabled = false;

            ComboBoxEinduurSelector1.Items.Clear();
            if (ComboBoxBeginuurSelector1.SelectedIndex == _einduurItemsSource.Count - 1)
            {
                ComboBoxEinduurSelector1.Items.Add(_einduurItemsSource[this.ComboBoxBeginuurSelector1.SelectedIndex]);
            }
            else if (ComboBoxBeginuurSelector1.SelectedIndex == _einduurItemsSource.Count - 2 || _aantalGereserveerdeUrenPerDatum >= _maxAantalTijdsloten-1)
            {
                ComboBoxEinduurSelector1.Items.Add(_einduurItemsSource[this.ComboBoxBeginuurSelector1.SelectedIndex + 1]);
            }
            else
            {
                ComboBoxEinduurSelector1.Items.Add(_einduurItemsSource[this.ComboBoxBeginuurSelector1.SelectedIndex + 1]);
                ComboBoxEinduurSelector1.Items.Add(_einduurItemsSource[this.ComboBoxBeginuurSelector1.SelectedIndex + 2]);
            }
            ButtonBevestigReservatie.IsEnabled = false;
            ComboBoxEinduurSelector1.IsEnabled = true;
        }

        private void CheckboxAddAnother_Checked(object sender, RoutedEventArgs e)
        {
            LabelToestelSelector2.Visibility = Visibility.Visible;
            LabelTijdslotSelector2.Visibility = Visibility.Visible;
            ComboBoxToesteltypeSelector2.Visibility = Visibility.Visible;
            ComboBoxBeginuurSelector2.Visibility = Visibility.Visible;
            ComboBoxEinduurSelector2.Visibility = Visibility.Visible;
        }
        private void CheckboxAddAnother_Unchecked(object sender, RoutedEventArgs e)
        {
            LabelToestelSelector2.Visibility = Visibility.Hidden;
            LabelTijdslotSelector2.Visibility = Visibility.Hidden;
            ComboBoxToesteltypeSelector2.Visibility = Visibility.Hidden;
            ComboBoxBeginuurSelector2.Visibility = Visibility.Hidden;
            ComboBoxEinduurSelector2.Visibility = Visibility.Hidden;
        }

        private void ComboBoxEinduurSelector1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBoxBeginuurSelector2.Items.Clear();
            //Beginuur1 = 21u
            if (ComboBoxBeginuurSelector1.SelectedIndex == _einduurItemsSource.Count - 2)
            {
                CheckboxAddAnother.IsEnabled = false;
            }
            //

            //Beginuur1 = 20u
            if (ComboBoxBeginuurSelector1.SelectedIndex == _einduurItemsSource.Count - 3)
            {
                if (ComboBoxEinduurSelector1.SelectedIndex == 1)
                {
                    CheckboxAddAnother.IsEnabled = false;
                }
                else if (ComboBoxEinduurSelector1.SelectedIndex == 0)
                {
                    CheckboxAddAnother.IsEnabled = true;
                    ComboBoxBeginuurSelector2.Items.Add(_einduurItemsSource[this.ComboBoxBeginuurSelector1.SelectedIndex + 1]);
                    if (_aantalGereserveerdeUrenPerDatum >= _maxAantalTijdsloten-1) CheckboxAddAnother.IsEnabled = false;
                }
            }
            //

            //Beginuur1 = 19u
            if (ComboBoxBeginuurSelector1.SelectedIndex == _einduurItemsSource.Count - 4)
            {
                if (ComboBoxEinduurSelector1.SelectedIndex == 2)
                {
                    CheckboxAddAnother.IsEnabled = false;
                }
                else if (ComboBoxEinduurSelector1.SelectedIndex == 1)
                {
                    CheckboxAddAnother.IsEnabled = true;
                    ComboBoxBeginuurSelector2.Items.Add(_einduurItemsSource[this.ComboBoxBeginuurSelector1.SelectedIndex + 2]);
                    if (_aantalGereserveerdeUrenPerDatum >= _maxAantalTijdsloten-2) CheckboxAddAnother.IsEnabled = false;
                }
                else if (ComboBoxEinduurSelector1.SelectedIndex == 0)
                {
                    CheckboxAddAnother.IsEnabled = true;
                    ComboBoxBeginuurSelector2.Items.Add(_einduurItemsSource[this.ComboBoxBeginuurSelector1.SelectedIndex + 1]);
                }
            }
            //

            //Beginuur1 tot= 18u
            if (ComboBoxBeginuurSelector1.SelectedIndex <= _einduurItemsSource.Count - 5)
            {
                if (ComboBoxEinduurSelector1.SelectedIndex == 3)
                {
                    CheckboxAddAnother.IsEnabled = false;
                }
                else if (ComboBoxEinduurSelector1.SelectedIndex == 2)
                {
                    CheckboxAddAnother.IsEnabled = true;
                    ComboBoxBeginuurSelector2.Items.Add(_einduurItemsSource[this.ComboBoxBeginuurSelector1.SelectedIndex + 3]);
                }
                else if (ComboBoxEinduurSelector1.SelectedIndex == 1)
                {
                    CheckboxAddAnother.IsEnabled = true;
                    ComboBoxBeginuurSelector2.Items.Add(_einduurItemsSource[this.ComboBoxBeginuurSelector1.SelectedIndex + 2]);
                    if (_aantalGereserveerdeUrenPerDatum >= _maxAantalTijdsloten-2) CheckboxAddAnother.IsEnabled = false;
                }
                else if (ComboBoxEinduurSelector1.SelectedIndex == 0)
                {
                    CheckboxAddAnother.IsEnabled = true;
                    ComboBoxBeginuurSelector2.Items.Add(_einduurItemsSource[this.ComboBoxBeginuurSelector1.SelectedIndex + 1]);
                    if (_aantalGereserveerdeUrenPerDatum >= _maxAantalTijdsloten-3) CheckboxAddAnother.IsEnabled = false;
                }
            }
            //

            ButtonBevestigReservatie.IsEnabled = true;
            ComboBoxToesteltypeSelector2.IsEnabled = true;
        }
        private void ComboBoxToesteltypeSelector2_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBoxBeginuurSelector2.IsEnabled = true;
        }

        private void ComboBoxBeginuurSelector2_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBoxEinduurSelector2.Items.Clear();

            //Beginuur2 = 21u
            if (ComboBoxBeginuurSelector1.SelectedIndex == _einduurItemsSource.Count - 3 && ComboBoxEinduurSelector1.SelectedIndex == 0)
            {
                ComboBoxEinduurSelector2.Items.Add(_einduurItemsSource[this.ComboBoxBeginuurSelector1.SelectedIndex + 2]);
            }
            //

            //Beginuur2 = 20u
            if (ComboBoxBeginuurSelector1.SelectedIndex == _einduurItemsSource.Count - 4)
            {
                if (ComboBoxEinduurSelector1.SelectedIndex == 1)
                {
                    ComboBoxEinduurSelector2.Items.Add(_einduurItemsSource[this.ComboBoxBeginuurSelector1.SelectedIndex + 3]);
                }
                else if (ComboBoxEinduurSelector1.SelectedIndex == 0)
                {
                    ComboBoxEinduurSelector2.Items.Add(_einduurItemsSource[this.ComboBoxBeginuurSelector1.SelectedIndex + 2]);
                    ComboBoxEinduurSelector2.Items.Add(_einduurItemsSource[this.ComboBoxBeginuurSelector1.SelectedIndex + 3]);
                }
            }
            //

            //Beginuur2 tot = 19u
            if (ComboBoxBeginuurSelector1.SelectedIndex <= _einduurItemsSource.Count - 5)
            {
                if (ComboBoxEinduurSelector1.SelectedIndex == 2)
                {
                    ComboBoxEinduurSelector2.Items.Add(_einduurItemsSource[this.ComboBoxBeginuurSelector1.SelectedIndex + 4]);
                }
                else if (ComboBoxEinduurSelector1.SelectedIndex == 1)
                {
                    ComboBoxEinduurSelector2.Items.Add(_einduurItemsSource[this.ComboBoxBeginuurSelector1.SelectedIndex + 3]);
                    ComboBoxEinduurSelector2.Items.Add(_einduurItemsSource[this.ComboBoxBeginuurSelector1.SelectedIndex + 4]);
                }
                else if (ComboBoxEinduurSelector1.SelectedIndex == 0)
                {
                    ComboBoxEinduurSelector2.Items.Add(_einduurItemsSource[this.ComboBoxBeginuurSelector1.SelectedIndex + 2]);
                    ComboBoxEinduurSelector2.Items.Add(_einduurItemsSource[this.ComboBoxBeginuurSelector1.SelectedIndex + 3]);
                }
            }
            //

            ComboBoxEinduurSelector2.IsEnabled = true;
        }

        private void ComboBoxEinduurSelector2_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ButtonBevestigReservatie.IsEnabled = true;
        }

        private void ButtonBevestigReservatie_Click(object sender, RoutedEventArgs e)
        {
            var x = _toestelManager.GeefVrijeToestellenVoorGeselecteerdTijdslot(DatePickerDatumSelector.SelectedDate.Value.ToString("yyyy-MM-dd"), ComboBoxToesteltypeSelector1.Text, Convert.ToInt32(ComboBoxBeginuurSelector1.Text.Remove(ComboBoxBeginuurSelector1.Text.Length - 1)), Convert.ToInt32(ComboBoxEinduurSelector1.Text.Remove(ComboBoxEinduurSelector1.Text.Length - 1)));
            foreach (var item in x)
            {
                MessageBox.Show(item.ToestelNaam);
            }
        }
    }
}