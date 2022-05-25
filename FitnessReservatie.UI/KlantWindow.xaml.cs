using FitnessReservatieBL.Domeinen;
using FitnessReservatieBL.Domeinen.Eigenschappen;
using FitnessReservatieBL.DTO;
using FitnessReservatieBL.Interfaces;
using FitnessReservatieBL.Managers;
using FitnessReservatieBL.Managers.Eigenschappen;
using FitnessReservatieDL.ADO.NET;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
        #region Aangemelde klantinfo
        private Klant _ingelogdeKlant;
        #endregion

        # region Klantreservatie voor geselecteerde dag 
        private List<DTOKlantReservatieInfo> _klantReservatiesVoorDagX = new List<DTOKlantReservatieInfo>();
        #endregion

        # region Klantreservaties
        private ObservableCollection<DTOKlantReservatieInfo> _reservatiesKlant;
        #endregion

        # region Combobox shenanigans
        private IReadOnlyList<ToestelType> _toesteltypeItemsSource;
        private IReadOnlyList<Tijdslot> _tijdslotItemsSource;
        #endregion

        # region Tijdslot controles & Maxima aantal tijdsloten.
        private int _aantalGereserveerdeUrenPerDatum;
        private int _maxAantalTijdsloten = 4;
        #endregion

        #region IRepositories
        private ToestelTypeManager _toestelTypeManager;
        private TijdslotManager _tijdslotManager;
        private ReservatieManager _reservatieManager;
        private ReservatieInfoManager _reservatieInfoManager;
        private KlantManager _klantManager;
        private ToestelManager _toestelManager;
        #endregion

        public KlantWindow(Klant klant)
        {
            InitializeComponent();
            this._ingelogdeKlant = klant;
            var connectiestring = ConfigurationManager.ConnectionStrings["FinalDBConnection"].ToString();

            LabelWelkomKlant.Content += $"{_ingelogdeKlant.Voornaam} {_ingelogdeKlant.Naam}";

            #region My Personal Information Tab
            LabelKlantnummerReturned.Content += $"{_ingelogdeKlant.Klantnummer}";
            LabelNaamReturned.Content += $"{_ingelogdeKlant.Naam}";
            LabelVoornaamReturned.Content += $"{_ingelogdeKlant.Voornaam}";
            LabelMailadresReturned.Content += $"{_ingelogdeKlant.Mailadres}";
            #endregion

            #region IRepositories instancieren
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

            #region Opvulling velden 'Maak Reservatie' Tab
            _reservatiesKlant = new ObservableCollection<DTOKlantReservatieInfo>(_klantManager.GeefKlantReservaties(_ingelogdeKlant));
            foreach (var reservatieKlant in _reservatiesKlant)
            {
                ListViewReservaties.Items.Add(reservatieKlant);
            }

            DatePickerDatumSelector.BlackoutDates.AddDatesInPast();
            DatePickerDatumSelector.BlackoutDates.Add(new CalendarDateRange(DateTime.Today.AddDays(8), DateTime.MaxValue));

            _toesteltypeItemsSource = _toestelTypeManager.SelecteerToestelType();

            _tijdslotItemsSource = _tijdslotManager.SelecteerTijdslot();
            #endregion
        }

        private void DatePickerDatumSelector_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            #region Checkbox controle en reset
            CheckboxVoegToestelToe.IsChecked = false;
            CheckboxVoegToestelToe.IsEnabled = false;
            #endregion

            #region Cleart inputvelden bij verandering datum
            ComboBoxToesteltypeSelector1.Items.Clear();
            ComboBoxBeginuurSelector1.Items.Clear();
            ComboBoxEinduurSelector1.Items.Clear();

            ComboBoxToesteltypeSelector2.Items.Clear();
            ComboBoxBeginuurSelector2.Items.Clear();
            ComboBoxEinduurSelector2.Items.Clear();
            #endregion

            #region Checkt reservatielimiet
            _aantalGereserveerdeUrenPerDatum = 0;
            _klantReservatiesVoorDagX.Clear();

            ComboBoxToesteltypeSelector1.IsEnabled = true;
            foreach (DTOKlantReservatieInfo klantreservatie in _reservatiesKlant)
            {
                if (klantreservatie.Datum == DatePickerDatumSelector.SelectedDate)
                {
                    _klantReservatiesVoorDagX.Add(klantreservatie);
                    _aantalGereserveerdeUrenPerDatum += klantreservatie.Einduur - klantreservatie.Beginuur;
                    if (_aantalGereserveerdeUrenPerDatum >= _maxAantalTijdsloten)
                    {
                        ComboBoxToesteltypeSelector1.IsEnabled = false;
                        ComboBoxBeginuurSelector1.IsEnabled = false;
                        ComboBoxEinduurSelector1.IsEnabled = false;
                        break;
                    }
                }
            }
            #endregion

            #region Voegt Toesteltypes toe aan ComboBoxToesteltypeSelector1
            foreach (ToestelType toesteltype in _toesteltypeItemsSource)
            {
                ComboBoxToesteltypeSelector1.Items.Add(toesteltype.ToestelNaam);
            }
            #endregion

            #region Reset reservatiebutton bij verandering ComboBoxEinduurSelector1
            ButtonBevestigReservatie.IsEnabled = false;
            #endregion
        }

        private void ComboBoxToesteltypeSelector1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            #region Checkbox controle en reset
            CheckboxVoegToestelToe.IsChecked = false;
            CheckboxVoegToestelToe.IsEnabled = false;
            #endregion

            #region Cleart inputvelden bij verandering Toesteltype 1
            ComboBoxBeginuurSelector1.Items.Clear();
            ComboBoxEinduurSelector1.Items.Clear();

            ComboBoxToesteltypeSelector2.Items.Clear();
            ComboBoxBeginuurSelector2.Items.Clear();
            ComboBoxEinduurSelector2.Items.Clear();
            #endregion

            #region Voegt Beginuuren toe en ontgrendeld ComboBoxBeginuurSelector1
            for (int i = 0; i < _tijdslotItemsSource.Count - 1; i++)
            {
                #region Controle reservatie tijdsloten & toekomst
                if (DatePickerDatumSelector.SelectedDate == DateTime.Today)
                {
                    if (_tijdslotItemsSource[i].Tslot > DateTime.Now.Hour)
                    {
                        ComboBoxBeginuurSelector1.Items.Add(_tijdslotItemsSource[i].Tslot);

                        if (_aantalGereserveerdeUrenPerDatum != 0)
                        {
                            foreach (var klantReservatieVoorDagX in _klantReservatiesVoorDagX)
                            {
                                if (_tijdslotItemsSource[i].Tslot >= klantReservatieVoorDagX.Beginuur && _tijdslotItemsSource[i].Tslot < klantReservatieVoorDagX.Einduur)
                                {
                                    ComboBoxBeginuurSelector1.Items.Remove(_tijdslotItemsSource[i].Tslot);
                                }
                            }
                        }
                    }
                }
                #endregion

                #region Controle reservatie tijdsloten
                else
                {
                    ComboBoxBeginuurSelector1.Items.Add(_tijdslotItemsSource[i].Tslot);

                    if (_aantalGereserveerdeUrenPerDatum != 0)
                    {
                        foreach (var klantReservatieVoorDagX in _klantReservatiesVoorDagX)
                        {
                            //Controle reservatie in toekomst
                            if (_tijdslotItemsSource[i].Tslot >= klantReservatieVoorDagX.Beginuur && _tijdslotItemsSource[i].Tslot < klantReservatieVoorDagX.Einduur)
                            {
                                ComboBoxBeginuurSelector1.Items.Remove(_tijdslotItemsSource[i].Tslot);
                            }
                        }
                    }
                }
                #endregion
            }
            #endregion

            ComboBoxBeginuurSelector1.IsEnabled = true;

            #region Reset reservatiebutton bij verandering ComboBoxEinduurSelector1
            ButtonBevestigReservatie.IsEnabled = false;
            #endregion
        }

        private void ComboBoxBeginuurSelector1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //Checkbox controle en reset
            CheckboxVoegToestelToe.IsChecked = false;
            ButtonBevestigReservatie.IsEnabled = true;

            //Cleart inputvelden bij verandering Toesteltype 1
            ComboBoxEinduurSelector1.Items.Clear();

            ComboBoxToesteltypeSelector2.Items.Clear();
            ComboBoxBeginuurSelector2.Items.Clear();
            ComboBoxEinduurSelector2.Items.Clear();
            //

            //Voegt uren toe aan ComboBoxEinduurSelector1
            if (Convert.ToInt32(ComboBoxBeginuurSelector1.SelectedValue) == _tijdslotItemsSource[_tijdslotItemsSource.Count - 2].Tslot)
            {
                ComboBoxEinduurSelector1.Items.Add(Convert.ToInt32(ComboBoxBeginuurSelector1.SelectedValue) + 1);
            }
            else if (_aantalGereserveerdeUrenPerDatum == _maxAantalTijdsloten - 1)
            {
                ComboBoxEinduurSelector1.Items.Add(Convert.ToInt32(ComboBoxBeginuurSelector1.SelectedValue) + 1);
            }
            else if (_aantalGereserveerdeUrenPerDatum == _maxAantalTijdsloten - 2)
            {
                ComboBoxEinduurSelector1.Items.Add(Convert.ToInt32(ComboBoxBeginuurSelector1.SelectedValue) + 1);
                ComboBoxEinduurSelector1.Items.Add(Convert.ToInt32(ComboBoxBeginuurSelector1.SelectedValue) + 2);

                foreach (var klantreservatie in _klantReservatiesVoorDagX)
                {
                    if ((klantreservatie.Einduur - klantreservatie.Beginuur == 1 && klantreservatie.Einduur - 1 == Convert.ToInt32(ComboBoxEinduurSelector1.Items[0])) || (klantreservatie.Einduur - klantreservatie.Beginuur == 2 && klantreservatie.Einduur - 1 == Convert.ToInt32(ComboBoxEinduurSelector1.Items[1])))
                    {
                        ComboBoxEinduurSelector1.Items.Remove(ComboBoxEinduurSelector1.Items[1]);
                    }
                }
            }
            else
            {
                ComboBoxEinduurSelector1.Items.Add(Convert.ToInt32(ComboBoxBeginuurSelector1.SelectedValue) + 1);
                ComboBoxEinduurSelector1.Items.Add(Convert.ToInt32(ComboBoxBeginuurSelector1.SelectedValue) + 2);
                foreach (var klantreservatie in _klantReservatiesVoorDagX)
                {
                    if ((klantreservatie.Einduur - klantreservatie.Beginuur == 1 && klantreservatie.Einduur - 1 == Convert.ToInt32(ComboBoxEinduurSelector1.Items[0])))
                    {
                        ComboBoxEinduurSelector1.Items.Remove(ComboBoxEinduurSelector1.Items[1]);
                    }
                    else if ((klantreservatie.Einduur - klantreservatie.Beginuur == 2 && klantreservatie.Einduur - 1 == Convert.ToInt32(ComboBoxEinduurSelector1.Items[1])))
                    {
                        ComboBoxEinduurSelector1.Items.Remove(ComboBoxEinduurSelector1.Items[1]);
                    }
                    else if ((klantreservatie.Einduur - klantreservatie.Beginuur == 2 && klantreservatie.Einduur - 1 == Convert.ToInt32(ComboBoxEinduurSelector1.Items[2])))
                    {
                        ComboBoxEinduurSelector1.Items.Remove(ComboBoxEinduurSelector1.Items[1]);
                    }
                }
            }

            ComboBoxEinduurSelector1.IsEnabled = true;
            ComboBoxEinduurSelector1.SelectedIndex = 0;
            //
        }

        private void CheckboxVoegToestelToe_Checked(object sender, RoutedEventArgs e)
        {
            LabelToestelSelector2.Visibility = Visibility.Visible;
            LabelTijdslotSelector2.Visibility = Visibility.Visible;
            ComboBoxToesteltypeSelector2.Visibility = Visibility.Visible;
            ComboBoxBeginuurSelector2.Visibility = Visibility.Visible;
            ComboBoxEinduurSelector2.Visibility = Visibility.Visible;
            LabelHour1.Visibility = Visibility.Visible;
            LabelHour2.Visibility = Visibility.Visible;

            ComboBoxToesteltypeSelector2.IsEnabled = true;
            ComboBoxBeginuurSelector2.IsEnabled = false;
            ComboBoxEinduurSelector2.IsEnabled = false;
        }
        private void CheckboxVoegToestelToe_Unchecked(object sender, RoutedEventArgs e)
        {
            LabelToestelSelector2.Visibility = Visibility.Hidden;
            LabelTijdslotSelector2.Visibility = Visibility.Hidden;
            ComboBoxToesteltypeSelector2.Visibility = Visibility.Hidden;
            ComboBoxBeginuurSelector2.Visibility = Visibility.Hidden;
            ComboBoxEinduurSelector2.Visibility = Visibility.Hidden;
            LabelHour1.Visibility = Visibility.Hidden;
            LabelHour2.Visibility = Visibility.Hidden;

            ComboBoxToesteltypeSelector2.Items.Clear();
            ComboBoxBeginuurSelector2.Items.Clear();
            ComboBoxEinduurSelector2.Items.Clear();
        }

        private void ComboBoxEinduurSelector1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            CheckboxVoegToestelToe.IsEnabled = true;
            CheckboxVoegToestelToe.IsChecked = false;

            ComboBoxToesteltypeSelector2.Items.Clear();
            ComboBoxBeginuurSelector2.Items.Clear();

            //Beginuur1.Value = 21u
            if (Convert.ToInt32(ComboBoxBeginuurSelector1.SelectedValue) == _tijdslotItemsSource[_tijdslotItemsSource.Count - 2].Tslot || _aantalGereserveerdeUrenPerDatum + (Convert.ToInt32(ComboBoxEinduurSelector1.SelectedValue) - Convert.ToInt32(ComboBoxBeginuurSelector1.SelectedValue)) > _maxAantalTijdsloten)
            {
                CheckboxVoegToestelToe.IsEnabled = false;
                CheckboxVoegToestelToe.IsChecked = false;

                foreach (var reservatieKlantVoorDagX in _klantReservatiesVoorDagX)
                {
                    if (Convert.ToInt32(ComboBoxEinduurSelector1.SelectedValue) == reservatieKlantVoorDagX.Beginuur)
                    {
                        CheckboxVoegToestelToe.IsEnabled = false;
                        CheckboxVoegToestelToe.IsChecked = false;
                    }
                }
            }


            //Beginuur1.Value = 20u
            else if (Convert.ToInt32(ComboBoxBeginuurSelector1.SelectedValue) == _tijdslotItemsSource[_tijdslotItemsSource.Count - 3].Tslot)
            {
                if (ComboBoxEinduurSelector1.SelectedIndex == 1)
                {
                    CheckboxVoegToestelToe.IsEnabled = false;
                    CheckboxVoegToestelToe.IsChecked = false;
                }
                else if (ComboBoxEinduurSelector1.SelectedIndex == 0)
                {
                    CheckboxVoegToestelToe.IsEnabled = true;
                    ComboBoxBeginuurSelector2.Items.Add(Convert.ToInt32(ComboBoxEinduurSelector1.SelectedValue));
                    if (_aantalGereserveerdeUrenPerDatum >= _maxAantalTijdsloten - 1)
                    {
                        CheckboxVoegToestelToe.IsEnabled = false;
                        CheckboxVoegToestelToe.IsChecked = false;
                    }
                    else if (_aantalGereserveerdeUrenPerDatum + (Convert.ToInt32(ComboBoxEinduurSelector1.SelectedValue) - Convert.ToInt32(ComboBoxBeginuurSelector1.SelectedValue)) > _maxAantalTijdsloten - 1)
                    {
                        CheckboxVoegToestelToe.IsEnabled = false;
                        CheckboxVoegToestelToe.IsChecked = false;
                    }
                }
                foreach (var reservatieKlantVoorDagX in _klantReservatiesVoorDagX)
                {
                    if (Convert.ToInt32(ComboBoxEinduurSelector1.SelectedValue) == reservatieKlantVoorDagX.Beginuur)
                    {
                        CheckboxVoegToestelToe.IsEnabled = false;
                        CheckboxVoegToestelToe.IsChecked = false;
                    }
                }
            }
            //


            //Beginuur1 tot = 18u
            else
            {
                CheckboxVoegToestelToe.IsEnabled = true;
                ComboBoxBeginuurSelector2.Items.Add(Convert.ToInt32(ComboBoxEinduurSelector1.SelectedValue));
                if (_aantalGereserveerdeUrenPerDatum >= _maxAantalTijdsloten - 1)
                {
                    CheckboxVoegToestelToe.IsEnabled = false;
                    CheckboxVoegToestelToe.IsChecked = false;
                }
                else if (_aantalGereserveerdeUrenPerDatum + (Convert.ToInt32(ComboBoxEinduurSelector1.SelectedValue) - Convert.ToInt32(ComboBoxBeginuurSelector1.SelectedValue)) > _maxAantalTijdsloten - 1)
                {
                    CheckboxVoegToestelToe.IsEnabled = false;
                    CheckboxVoegToestelToe.IsChecked = false;
                }
                foreach (var reservatieKlantVoorDagX in _klantReservatiesVoorDagX)
                {
                    if (Convert.ToInt32(ComboBoxEinduurSelector1.SelectedValue) == reservatieKlantVoorDagX.Beginuur)
                    {
                        CheckboxVoegToestelToe.IsEnabled = false;
                        CheckboxVoegToestelToe.IsChecked = false;
                    }
                }
            }

            ButtonBevestigReservatie.IsEnabled = true;

            //Voegt Toesteltypes toe aan ComboBoxToesteltypeSelector1
            foreach (ToestelType toesteltype in _toesteltypeItemsSource)
            {
                ComboBoxToesteltypeSelector2.Items.Add(toesteltype.ToestelNaam);
            }
            //

        }
        private void ComboBoxToesteltypeSelector2_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBoxBeginuurSelector2.IsEnabled = true;
        }

        private void ComboBoxBeginuurSelector2_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBoxEinduurSelector2.Items.Clear();

            //Voegt uren toe aan ComboBoxEinduurSelector2
            if (Convert.ToInt32(ComboBoxBeginuurSelector2.SelectedValue) == _tijdslotItemsSource[_tijdslotItemsSource.Count - 2].Tslot)
            {
                ComboBoxEinduurSelector2.Items.Add(Convert.ToInt32(ComboBoxBeginuurSelector2.SelectedValue) + 1);
            }
            else if (_aantalGereserveerdeUrenPerDatum == _maxAantalTijdsloten - 1)
            {
                ComboBoxEinduurSelector2.Items.Add(Convert.ToInt32(ComboBoxBeginuurSelector2.SelectedValue) + 1);
            }
            else if (_aantalGereserveerdeUrenPerDatum == _maxAantalTijdsloten - 2)
            {
                ComboBoxEinduurSelector2.Items.Add(Convert.ToInt32(ComboBoxBeginuurSelector2.SelectedValue) + 1);
                ComboBoxEinduurSelector2.Items.Add(Convert.ToInt32(ComboBoxBeginuurSelector2.SelectedValue) + 2);

                foreach (var klantreservatie in _klantReservatiesVoorDagX)
                {
                    if ((klantreservatie.Einduur - klantreservatie.Beginuur == 1 && klantreservatie.Einduur - 1 == Convert.ToInt32(ComboBoxEinduurSelector2.Items[0])))
                    {
                        ComboBoxEinduurSelector2.Items.Remove(ComboBoxEinduurSelector2.Items[1]);
                    }
                    else if ((klantreservatie.Einduur - klantreservatie.Beginuur == 2 && klantreservatie.Einduur - 1 == Convert.ToInt32(ComboBoxEinduurSelector2.Items[1])))
                    {
                        ComboBoxEinduurSelector2.Items.Remove(ComboBoxEinduurSelector2.Items[1]);
                    }
                    else if (_aantalGereserveerdeUrenPerDatum + (Convert.ToInt32(ComboBoxEinduurSelector1.SelectedValue) - Convert.ToInt32(ComboBoxBeginuurSelector1.SelectedValue)) >= _maxAantalTijdsloten - 1)
                    {
                        ComboBoxEinduurSelector2.Items.Remove(ComboBoxEinduurSelector2.Items[1]);
                    }
                }
            }
            else if (_aantalGereserveerdeUrenPerDatum + (Convert.ToInt32(ComboBoxEinduurSelector1.SelectedValue) - Convert.ToInt32(ComboBoxBeginuurSelector1.SelectedValue)) >= _maxAantalTijdsloten - 1)
            {
                ComboBoxEinduurSelector2.Items.Add(Convert.ToInt32(ComboBoxBeginuurSelector2.SelectedValue) + 1);
                foreach (var reservatieKlantVoorDagX in _klantReservatiesVoorDagX)
                {
                    if (Convert.ToInt32(ComboBoxBeginuurSelector2.SelectedValue) == reservatieKlantVoorDagX.Beginuur - 1)
                    {
                        ComboBoxEinduurSelector2.Items.Remove(ComboBoxEinduurSelector2.Items[1]);
                    }
                }
            }
            else
            {
                ComboBoxEinduurSelector2.Items.Add(Convert.ToInt32(ComboBoxBeginuurSelector2.SelectedValue) + 1);
                ComboBoxEinduurSelector2.Items.Add(Convert.ToInt32(ComboBoxBeginuurSelector2.SelectedValue) + 2);
                foreach (var reservatieKlantVoorDagX in _klantReservatiesVoorDagX)
                {
                    if (Convert.ToInt32(ComboBoxBeginuurSelector2.SelectedValue) == reservatieKlantVoorDagX.Beginuur - 1)
                    {
                        ComboBoxEinduurSelector2.Items.Remove(ComboBoxEinduurSelector2.Items[1]);
                    }
                }
            }

            ComboBoxEinduurSelector2.IsEnabled = true;
        }

        private void ComboBoxEinduurSelector2_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ButtonBevestigReservatie.IsEnabled = true;
        }

        private void ButtonBevestigReservatie_Click(object sender, RoutedEventArgs e)
        {
            DateTime geselecteerdeDatum = DatePickerDatumSelector.SelectedDate.Value;
            int beginuur = Convert.ToInt32(ComboBoxBeginuurSelector1.SelectedValue);
            int einduur = Convert.ToInt32(ComboBoxEinduurSelector1.SelectedValue);
            try
            {
                //
                _reservatieManager.MaakReservatie(_ingelogdeKlant, geselecteerdeDatum, beginuur, einduur, ComboBoxToesteltypeSelector1.SelectedValue.ToString());
                //

                //ListView Refresh
                _reservatiesKlant.Clear();
                _reservatiesKlant = new ObservableCollection<DTOKlantReservatieInfo>(_klantManager.GeefKlantReservaties(_ingelogdeKlant));
                ListViewReservaties.Items.Clear();

                DTOKlantReservatieInfo reservatieinfo = null;
                foreach (var reservatieKlant in _reservatiesKlant)
                {
                    ListViewReservaties.Items.Add(reservatieKlant);
                    if (reservatieKlant.Datum == geselecteerdeDatum && reservatieKlant.Beginuur == beginuur && reservatieKlant.Einduur == einduur)
                    {
                        reservatieinfo = reservatieKlant;
                    }
                }
                //

                MessageBox.Show($"Er werd een nieuwe reservatie aangemaakt \r op {geselecteerdeDatum.ToShortDateString()} om {beginuur}u-{einduur}u voor {reservatieinfo.Toestelnaam}. \r\r Kijk op het tabblad 'Mijn reservaties' om alle reservaties te zien.", "Reservering bevestigd!");

                //Cleart inputvelden bij maken reservatie
                DatePickerDatumSelector.SelectedDate = DateTime.Today;

                ComboBoxToesteltypeSelector1.Items.Clear();
                ComboBoxBeginuurSelector1.Items.Clear();
                ComboBoxEinduurSelector1.Items.Clear();

                ComboBoxToesteltypeSelector2.Items.Clear();
                ComboBoxBeginuurSelector2.Items.Clear();
                ComboBoxEinduurSelector2.Items.Clear();
                //
            }
            catch (Exception)
            {
                MessageBox.Show($"Er kunnen helaas geen {ComboBoxToesteltypeSelector1.SelectedValue.ToString()}en meer gereserveerd worden voor tijdslot {beginuur}u-{einduur}u.", "Er ging iets mis :(");
            }
        }

        private void ButtonLogOut_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainwindow = new MainWindow();
            this.Close();
            mainwindow.Show();
        }
    }
}