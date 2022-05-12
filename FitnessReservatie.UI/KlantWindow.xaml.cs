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
        private IReadOnlyList<KlantReservatieInfo> _reservatiesKlant;
        private IReadOnlyList<Tijdslot> _einduurItemsSource;

        private ToestelTypeManager _toestelTypeManager;
        private TijdslotManager _tijdslotManager;

        private ReservatieManager _reservatieManager;
        private KlantManager _klantManager;

        public KlantWindow(Klant klant)
        {
            InitializeComponent();
            this._ingelogdeKlant = klant;
            LabelWelkomKlant.Content += $"{_ingelogdeKlant.Voornaam} {_ingelogdeKlant.Naam}";

            IKlantRepository klantRepo = new KlantRepoADO(ConfigurationManager.ConnectionStrings["FinalDBConnection"].ToString());
            _klantManager = new KlantManager(klantRepo);
            _reservatiesKlant = _klantManager.GeefKlantReservaties(_ingelogdeKlant.Klantnummer);
            ListViewReservations.ItemsSource = _reservatiesKlant;

            IToestelTypeRepository toesteltypeRepo = new ToestelTypeRepoADO(ConfigurationManager.ConnectionStrings["FinalDBConnection"].ToString());
            _toestelTypeManager = new ToestelTypeManager(toesteltypeRepo);
            ComboBoxToesteltypeSelector1.ItemsSource = _toestelTypeManager.SelecteerToestelType();

            ITijdslotRepository tijdslotRepo = new TijdslotRepoADO(ConfigurationManager.ConnectionStrings["FinalDBConnection"].ToString());
            _tijdslotManager = new TijdslotManager(tijdslotRepo);

            ComboBoxBeginuurSelector1.ItemsSource = _tijdslotManager.SelecteerBeginuur();
            _einduurItemsSource = _tijdslotManager.SelecteerEinduur();

            DatePickerDatumSelector.BlackoutDates.AddDatesInPast();
            DatePickerDatumSelector.BlackoutDates.Add(new CalendarDateRange(DateTime.Today.AddDays(8), DateTime.Today.AddMonths(1).AddDays(-1)));

            IReservatieRepository reservatieRepo = new ReservatieRepoADO(ConfigurationManager.ConnectionStrings["FinalDBConnection"].ToString());
            _reservatieManager = new ReservatieManager(reservatieRepo);

        }

        private void ButtonLogOut_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainwindow = new MainWindow();
            this.Close();
            mainwindow.Show();
        }

        private void ComboBoxBeginuurSelector1_SelectionChanged1(object sender, SelectionChangedEventArgs e)
        {
            CheckboxAddAnother.IsChecked = false;
            CheckboxAddAnother.IsEnabled = false;

            ComboBoxEinduurSelector1.Items.Clear();
            if (ComboBoxBeginuurSelector1.SelectedIndex == _einduurItemsSource.Count - 1)
            {
                ComboBoxEinduurSelector1.Items.Add(_einduurItemsSource[this.ComboBoxBeginuurSelector1.SelectedIndex]);
            }
            else if (ComboBoxBeginuurSelector1.SelectedIndex == _einduurItemsSource.Count - 2)
            {
                ComboBoxEinduurSelector1.Items.Add(_einduurItemsSource[this.ComboBoxBeginuurSelector1.SelectedIndex + 1]);
            }
            else if (ComboBoxBeginuurSelector1.SelectedIndex == _einduurItemsSource.Count - 3)
            {
                ComboBoxEinduurSelector1.Items.Add(_einduurItemsSource[this.ComboBoxBeginuurSelector1.SelectedIndex + 1]);
                ComboBoxEinduurSelector1.Items.Add(_einduurItemsSource[this.ComboBoxBeginuurSelector1.SelectedIndex + 2]);
            }
            else if (ComboBoxBeginuurSelector1.SelectedIndex == _einduurItemsSource.Count - 4)
            {
                ComboBoxEinduurSelector1.Items.Add(_einduurItemsSource[this.ComboBoxBeginuurSelector1.SelectedIndex + 1]);
                ComboBoxEinduurSelector1.Items.Add(_einduurItemsSource[this.ComboBoxBeginuurSelector1.SelectedIndex + 2]);
                ComboBoxEinduurSelector1.Items.Add(_einduurItemsSource[this.ComboBoxBeginuurSelector1.SelectedIndex + 3]);
            }
            else
            {
                ComboBoxEinduurSelector1.Items.Add(_einduurItemsSource[this.ComboBoxBeginuurSelector1.SelectedIndex + 1]);
                ComboBoxEinduurSelector1.Items.Add(_einduurItemsSource[this.ComboBoxBeginuurSelector1.SelectedIndex + 2]);
                ComboBoxEinduurSelector1.Items.Add(_einduurItemsSource[this.ComboBoxBeginuurSelector1.SelectedIndex + 3]);
                ComboBoxEinduurSelector1.Items.Add(_einduurItemsSource[this.ComboBoxBeginuurSelector1.SelectedIndex + 4]);
            }
            ButtonBevestigReservatie.IsEnabled = false;
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
                }
                else if (ComboBoxEinduurSelector1.SelectedIndex == 0)
                {
                    CheckboxAddAnother.IsEnabled = true;
                    ComboBoxBeginuurSelector2.Items.Add(_einduurItemsSource[this.ComboBoxBeginuurSelector1.SelectedIndex + 1]);
                }
            }
            //

            ButtonBevestigReservatie.IsEnabled = true;
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
                    ComboBoxEinduurSelector2.Items.Add(_einduurItemsSource[this.ComboBoxBeginuurSelector1.SelectedIndex + 4]);
                }
            }
            //
        }


        private void ComboBoxEinduurSelector2_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ButtonBevestigReservatie.IsEnabled = true;
        }
    }
}