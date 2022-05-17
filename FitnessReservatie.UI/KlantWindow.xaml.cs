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
        private Klant _ingelogdeKlant;
        private ObservableCollection<DTOKlantReservatieInfo> _reservatiesKlant;
        private IReadOnlyList<ToestelType> _toesteltypeItemsSource;
        private IReadOnlyList<Tijdslot> _tijdslotItemsSource;

        private int _aantalGereserveerdeUrenPerDatum;
        private List<DTOKlantReservatieInfo> _klantReservatiesVoorDagX = new List<DTOKlantReservatieInfo>();
        private int _maxAantalTijdsloten = 4;

        private ToestelTypeManager _toestelTypeManager;
        private TijdslotManager _tijdslotManager;

        private ReservatieManager _reservatieManager;
        private ReservatieInfoManager _reservatieInfoManager;

        private KlantManager _klantManager;
        private ToestelManager _toestelManager;

        public KlantWindow(Klant klant)
        {
            InitializeComponent();
            this._ingelogdeKlant = klant;
            LabelWelkomKlant.Content += $"{_ingelogdeKlant.Voornaam} {_ingelogdeKlant.Naam}";

            IKlantRepository klantRepo = new KlantRepoADO(ConfigurationManager.ConnectionStrings["FinalDBConnection"].ToString());
            _klantManager = new KlantManager(klantRepo);
            _reservatiesKlant = new ObservableCollection<DTOKlantReservatieInfo>(_klantManager.GeefKlantReservaties(_ingelogdeKlant.Klantnummer));
            foreach (var reservatieKlant in _reservatiesKlant)
            {
                ListViewReservations.Items.Add(reservatieKlant);
            }

            LabelKlantnummerReturned.Content += $"{_ingelogdeKlant.Klantnummer}";
            LabelNaamReturned.Content += $"{_ingelogdeKlant.Naam}";
            LabelVoornaamReturned.Content += $"{_ingelogdeKlant.Voornaam}";
            LabelMailadresReturned.Content += $"{_ingelogdeKlant.Mailadres}";

            IToestelTypeRepository toesteltypeRepo = new ToestelTypeRepoADO(ConfigurationManager.ConnectionStrings["FinalDBConnection"].ToString());
            _toestelTypeManager = new ToestelTypeManager(toesteltypeRepo);
            _toesteltypeItemsSource = _toestelTypeManager.SelecteerToestelType();

            ITijdslotRepository tijdslotRepo = new TijdslotRepoADO(ConfigurationManager.ConnectionStrings["FinalDBConnection"].ToString());
            _tijdslotManager = new TijdslotManager(tijdslotRepo);

            _tijdslotItemsSource = _tijdslotManager.SelecteerTijdslot();

            DatePickerDatumSelector.BlackoutDates.AddDatesInPast();
            DatePickerDatumSelector.BlackoutDates.Add(new CalendarDateRange(DateTime.Today.AddDays(8), DateTime.MaxValue));

            IToestelRepository toestelRepo = new ToestelRepoADO(ConfigurationManager.ConnectionStrings["FinalDBConnection"].ToString());
            _toestelManager = new ToestelManager(toestelRepo);

            IReservatieRepository reservatieRepo = new ReservatieRepoADO(ConfigurationManager.ConnectionStrings["FinalDBConnection"].ToString());
            _reservatieManager = new ReservatieManager(reservatieRepo);

            IReservatieInfoRepository reservatieInfoRepo = new ReservatieInfoRepoADO(ConfigurationManager.ConnectionStrings["FinalDBConnection"].ToString());
            _reservatieInfoManager = new ReservatieInfoManager(reservatieInfoRepo);
        }

        private void ButtonLogOut_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainwindow = new MainWindow();
            this.Close();
            mainwindow.Show();
        }
        private void DatePickerDatumSelector_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            //Checkbox controle en reset
            CheckboxAddAnother.IsChecked = false;
            CheckboxAddAnother.IsEnabled = false;
            //

            //Cleart inputvelden bij verandering datum
            ComboBoxToesteltypeSelector1.Items.Clear();
            ComboBoxBeginuurSelector1.Items.Clear();
            ComboBoxEinduurSelector1.Items.Clear();

            ComboBoxToesteltypeSelector2.Items.Clear();
            ComboBoxBeginuurSelector2.Items.Clear();
            ComboBoxEinduurSelector2.Items.Clear();
            //

            //Checkt reservatielimiet
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

            //Voegt Toesteltypes toe aan ComboBoxToesteltypeSelector1
            foreach (ToestelType toesteltype in _toesteltypeItemsSource)
            {
                ComboBoxToesteltypeSelector1.Items.Add(toesteltype.ToestelNaam);
            }
            //

            //Reset reservatiebutton bij verandering ComboBoxEinduurSelector1
            ButtonBevestigReservatie.IsEnabled = false;
            //
        }
        private void ComboBoxToesteltypeSelector1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //Checkbox controle en reset
            CheckboxAddAnother.IsChecked = false;
            CheckboxAddAnother.IsEnabled = false;
            //

            //Cleart inputvelden bij verandering Toesteltype 1
            ComboBoxBeginuurSelector1.Items.Clear();
            ComboBoxEinduurSelector1.Items.Clear();

            ComboBoxToesteltypeSelector2.Items.Clear();
            ComboBoxBeginuurSelector2.Items.Clear();
            ComboBoxEinduurSelector2.Items.Clear();
            //

            //Voegt Beginuuren toe en ontgrendeld ComboBoxBeginuurSelector1
            for (int i = 0; i < _tijdslotItemsSource.Count - 1; i++)
            {
                //Controle reservatie tijdsloten & toekomst
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
                //

                //Controle reservatie tijdsloten
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
                //
            }
            ComboBoxBeginuurSelector1.IsEnabled = true;

            //Reset reservatiebutton bij verandering ComboBoxEinduurSelector1
            ButtonBevestigReservatie.IsEnabled = false;
            //
        }

        private void ComboBoxBeginuurSelector1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //Checkbox controle en reset
            CheckboxAddAnother.IsChecked = false;
            ButtonBevestigReservatie.IsEnabled = true;

            //Cleart inputvelden bij verandering Toesteltype 1
            ComboBoxEinduurSelector1.Items.Clear();

            ComboBoxToesteltypeSelector2.Items.Clear();
            ComboBoxBeginuurSelector2.Items.Clear();
            ComboBoxEinduurSelector2.Items.Clear();
            //

            //Voegt uren toe aan ComboBoxEinduurSelector1
            if (Convert.ToInt32(ComboBoxBeginuurSelector1.SelectedValue) == _tijdslotItemsSource[_tijdslotItemsSource.Count-2].Tslot)
            {
                ComboBoxEinduurSelector1.Items.Add(Convert.ToInt32(ComboBoxBeginuurSelector1.SelectedValue) + 1);
            }
            else if(_aantalGereserveerdeUrenPerDatum >= 2)
            {
                ComboBoxEinduurSelector1.Items.Add(Convert.ToInt32(ComboBoxBeginuurSelector1.SelectedValue) + 1);
            }
            else
            {
                ComboBoxEinduurSelector1.Items.Add(Convert.ToInt32(ComboBoxBeginuurSelector1.SelectedValue) + 1);
                ComboBoxEinduurSelector1.Items.Add(Convert.ToInt32(ComboBoxBeginuurSelector1.SelectedValue) + 2);
            }
            
            ComboBoxEinduurSelector1.IsEnabled = true;
            ComboBoxEinduurSelector1.SelectedIndex = 0;
            //
        }

        private void CheckboxAddAnother_Checked(object sender, RoutedEventArgs e)
        {
            LabelToestelSelector2.Visibility = Visibility.Visible;
            LabelTijdslotSelector2.Visibility = Visibility.Visible;
            ComboBoxToesteltypeSelector2.Visibility = Visibility.Visible;
            ComboBoxBeginuurSelector2.Visibility = Visibility.Visible;
            ComboBoxEinduurSelector2.Visibility = Visibility.Visible;
            LabelHour1.Visibility = Visibility.Visible;
            LabelHour2.Visibility = Visibility.Visible;
        }
        private void CheckboxAddAnother_Unchecked(object sender, RoutedEventArgs e)
        {
            LabelToestelSelector2.Visibility = Visibility.Hidden;
            LabelTijdslotSelector2.Visibility = Visibility.Hidden;
            ComboBoxToesteltypeSelector2.Visibility = Visibility.Hidden;
            ComboBoxBeginuurSelector2.Visibility = Visibility.Hidden;
            ComboBoxEinduurSelector2.Visibility = Visibility.Hidden;
            LabelHour1.Visibility = Visibility.Hidden;
            LabelHour2.Visibility = Visibility.Hidden;
        }

        private void ComboBoxEinduurSelector1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBoxToesteltypeSelector2.Items.Clear();
            ComboBoxBeginuurSelector2.Items.Clear();

            //Beginuur1.Value = 21u
            if (Convert.ToInt32(ComboBoxBeginuurSelector1.SelectedValue) == _tijdslotItemsSource[_tijdslotItemsSource.Count-2].Tslot)
            {
                CheckboxAddAnother.IsEnabled = false;
            }


            //Bewginuur1.Value = 20u
            if (Convert.ToInt32(ComboBoxBeginuurSelector1.SelectedValue) == _tijdslotItemsSource[_tijdslotItemsSource.Count - 3].Tslot)
            {
                if (Convert.ToInt32(ComboBoxEinduurSelector1.SelectedValue) == _tijdslotItemsSource[_tijdslotItemsSource.Count - 2].Tslot)
                {
                    CheckboxAddAnother.IsEnabled = false;
                }
                else if (Convert.ToInt32(ComboBoxEinduurSelector1.SelectedValue) == _tijdslotItemsSource[_tijdslotItemsSource.Count - 3].Tslot)
                {
                    CheckboxAddAnother.IsEnabled = true;
                    ComboBoxBeginuurSelector2.Items.Add(_tijdslotItemsSource[_tijdslotItemsSource.Count - 2].Tslot);
                    if (_aantalGereserveerdeUrenPerDatum >= _maxAantalTijdsloten - 1) CheckboxAddAnother.IsEnabled = false;
                }
            }


            //Beginuur1 = 19u
            //if (Convert.ToInt32(ComboBoxBeginuurSelector1.SelectedValue) >= _tijdslotItemsSource[_tijdslotItemsSource.Count - 3].Tslot)
            //{
            //    if (ComboBoxEinduurSelector1.SelectedIndex == 2)
            //    {
            //        CheckboxAddAnother.IsEnabled = false;
            //    }
            //    else if (ComboBoxEinduurSelector1.SelectedIndex == 1)
            //    {
            //        CheckboxAddAnother.IsEnabled = true;
            //        ComboBoxBeginuurSelector2.Items.Add(_tijdslotItemsSource[this.ComboBoxBeginuurSelector1.SelectedIndex + 2]);
            //        if (_aantalGereserveerdeUrenPerDatum >= _maxAantalTijdsloten - 2) CheckboxAddAnother.IsEnabled = false;
            //    }
            //    else if (ComboBoxEinduurSelector1.SelectedIndex == 0)
            //    {
            //        CheckboxAddAnother.IsEnabled = true;
            //        ComboBoxBeginuurSelector2.Items.Add(_tijdslotItemsSource[this.ComboBoxBeginuurSelector1.SelectedIndex + 1]);
            //    }
            //}


            //Beginuur1 tot = 18u
            //if (Convert.ToInt32(ComboBoxBeginuurSelector1.SelectedValue) >= _tijdslotItemsSource[_tijdslotItemsSource.Count - 4].Tslot)
            //{
            //    if (ComboBoxEinduurSelector1.SelectedIndex == 3)
            //    {
            //        CheckboxAddAnother.IsEnabled = false;
            //    }
            //    else if (ComboBoxEinduurSelector1.SelectedIndex == 2)
            //    {
            //        CheckboxAddAnother.IsEnabled = true;
            //        ComboBoxBeginuurSelector2.Items.Add(_tijdslotItemsSource[this.ComboBoxBeginuurSelector1.SelectedIndex + 3]);
            //    }
            //    else if (ComboBoxEinduurSelector1.SelectedIndex == 1)
            //    {
            //        CheckboxAddAnother.IsEnabled = true;
            //        ComboBoxBeginuurSelector2.Items.Add(_tijdslotItemsSource[this.ComboBoxBeginuurSelector1.SelectedIndex + 2]);
            //        if (_aantalGereserveerdeUrenPerDatum >= _maxAantalTijdsloten - 2) CheckboxAddAnother.IsEnabled = false;
            //    }
            //    else if (ComboBoxEinduurSelector1.SelectedIndex == 0)
            //    {
            //        CheckboxAddAnother.IsEnabled = true;
            //        ComboBoxBeginuurSelector2.Items.Add(_tijdslotItemsSource[this.ComboBoxBeginuurSelector1.SelectedIndex + 1]);
            //        if (_aantalGereserveerdeUrenPerDatum >= _maxAantalTijdsloten - 3) CheckboxAddAnother.IsEnabled = false;
            //    }
            //}


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
            if (ComboBoxBeginuurSelector1.SelectedIndex == _tijdslotItemsSource.Count - 3 && ComboBoxEinduurSelector1.SelectedIndex == 0)
            {
                ComboBoxEinduurSelector2.Items.Add(_tijdslotItemsSource[this.ComboBoxBeginuurSelector1.SelectedIndex + 2]);
            }
            //

            //Beginuur2 = 20u
            if (ComboBoxBeginuurSelector1.SelectedIndex == _tijdslotItemsSource.Count - 4)
            {
                if (ComboBoxEinduurSelector1.SelectedIndex == 1)
                {
                    ComboBoxEinduurSelector2.Items.Add(_tijdslotItemsSource[this.ComboBoxBeginuurSelector1.SelectedIndex + 3]);
                }
                else if (ComboBoxEinduurSelector1.SelectedIndex == 0)
                {
                    ComboBoxEinduurSelector2.Items.Add(_tijdslotItemsSource[this.ComboBoxBeginuurSelector1.SelectedIndex + 2]);
                    ComboBoxEinduurSelector2.Items.Add(_tijdslotItemsSource[this.ComboBoxBeginuurSelector1.SelectedIndex + 3]);
                }
            }
            //

            //Beginuur2 tot = 19u
            if (ComboBoxBeginuurSelector1.SelectedIndex <= _tijdslotItemsSource.Count - 5)
            {
                if (ComboBoxEinduurSelector1.SelectedIndex == 2)
                {
                    ComboBoxEinduurSelector2.Items.Add(_tijdslotItemsSource[this.ComboBoxBeginuurSelector1.SelectedIndex + 4]);
                }
                else if (ComboBoxEinduurSelector1.SelectedIndex == 1)
                {
                    ComboBoxEinduurSelector2.Items.Add(_tijdslotItemsSource[this.ComboBoxBeginuurSelector1.SelectedIndex + 3]);
                    ComboBoxEinduurSelector2.Items.Add(_tijdslotItemsSource[this.ComboBoxBeginuurSelector1.SelectedIndex + 4]);
                }
                else if (ComboBoxEinduurSelector1.SelectedIndex == 0)
                {
                    ComboBoxEinduurSelector2.Items.Add(_tijdslotItemsSource[this.ComboBoxBeginuurSelector1.SelectedIndex + 2]);
                    ComboBoxEinduurSelector2.Items.Add(_tijdslotItemsSource[this.ComboBoxBeginuurSelector1.SelectedIndex + 3]);
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
            try
            {
                Toestel? geselecteerdToestel = null;
                IReadOnlyList<Toestel> beschikbareToestellen = _toestelManager.GeefVrijeToestellenVoorGeselecteerdTijdslot(DatePickerDatumSelector.SelectedDate.Value, ComboBoxToesteltypeSelector1.Text, Convert.ToInt32(ComboBoxBeginuurSelector1.Text), Convert.ToInt32(ComboBoxEinduurSelector1.Text));
                foreach (var beschikbaarToestel in beschikbareToestellen)
                {
                    if (_aantalGereserveerdeUrenPerDatum != 0)
                    {
                        foreach (var klantReservatieVoorDagX in _klantReservatiesVoorDagX)
                        {
                            if (!klantReservatieVoorDagX.Toestelnaam.Contains(beschikbaarToestel.ToestelNaam))
                            {
                                geselecteerdToestel = beschikbaarToestel;
                                break;
                            }
                        }
                    }
                    else
                    {
                        geselecteerdToestel = beschikbaarToestel;
                        break;
                    }
                }
                if (geselecteerdToestel == null) throw new Exception();
                else
                {
                    Reservatie reservatie = _reservatieManager.MaakReservatie(_ingelogdeKlant, DatePickerDatumSelector.SelectedDate.Value);
                    _reservatieInfoManager.MaakReservatieInfo(reservatie.Reservatienummer, Convert.ToInt32(ComboBoxBeginuurSelector1.Text), Convert.ToInt32(ComboBoxEinduurSelector1.Text), geselecteerdToestel);
                }

                _reservatiesKlant.Clear();
                _reservatiesKlant = new ObservableCollection<DTOKlantReservatieInfo>(_klantManager.GeefKlantReservaties(_ingelogdeKlant.Klantnummer));
                ListViewReservations.Items.Clear();
                foreach (var reservatieKlant in _reservatiesKlant)
                {
                    ListViewReservations.Items.Add(reservatieKlant);
                }

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
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Reservation Failed");
            }
        }
    }
}