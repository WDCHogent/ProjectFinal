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
        //Aangemelde klantinfo
        private Klant _ingelogdeKlant;
        //

        //Klantreservatie voor geselecteerde dag 
        private List<DTOKlantReservatieInfo> _klantReservatiesVoorDagX = new List<DTOKlantReservatieInfo>();

        //Klantreservaties        
        private ObservableCollection<DTOKlantReservatieInfo> _reservatiesKlant;
        //

        //Combobox shenanigans
        private IReadOnlyList<ToestelType> _toesteltypeItemsSource;
        private IReadOnlyList<Tijdslot> _tijdslotItemsSource;
        //

        //Tijdslot controles & Maxima aantal tijdsloten.
        private int _aantalGereserveerdeUrenPerDatum;
        private int _maxAantalTijdsloten = 4;
        //

        //IRepositories
        private ToestelTypeManager _toestelTypeManager;
        private TijdslotManager _tijdslotManager;
        private ReservatieManager _reservatieManager;
        private ReservatieInfoManager _reservatieInfoManager;
        private KlantManager _klantManager;
        private ToestelManager _toestelManager;
        //

        public KlantWindow(Klant klant)
        {
            InitializeComponent();
            this._ingelogdeKlant = klant;
            var connectiestring = ConfigurationManager.ConnectionStrings["FinalDBConnection"].ToString();

            LabelWelkomKlant.Content += $"{_ingelogdeKlant.Voornaam} {_ingelogdeKlant.Naam}";

            //My Personal Information Tab
            LabelKlantnummerReturned.Content += $"{_ingelogdeKlant.Klantnummer}";
            LabelNaamReturned.Content += $"{_ingelogdeKlant.Naam}";
            LabelVoornaamReturned.Content += $"{_ingelogdeKlant.Voornaam}";
            LabelMailadresReturned.Content += $"{_ingelogdeKlant.Mailadres}";
            //

            //IRepositories instancieren
            IKlantRepository klantRepo = new KlantRepoADO(connectiestring);
            _klantManager = new KlantManager(klantRepo);

            IToestelTypeRepository toesteltypeRepo = new ToestelTypeRepoADO(connectiestring);
            _toestelTypeManager = new ToestelTypeManager(toesteltypeRepo);

            ITijdslotRepository tijdslotRepo = new TijdslotRepoADO(connectiestring);
            _tijdslotManager = new TijdslotManager(tijdslotRepo);

            IToestelRepository toestelRepo = new ToestelRepoADO(connectiestring);
            _toestelManager = new ToestelManager(toestelRepo);

            IReservatieRepository reservatieRepo = new ReservatieRepoADO(connectiestring);
            _reservatieManager = new ReservatieManager(reservatieRepo);

            IReservatieInfoRepository reservatieInfoRepo = new ReservatieInfoRepoADO(connectiestring);
            _reservatieInfoManager = new ReservatieInfoManager(reservatieInfoRepo, reservatieRepo, klantRepo, toestelRepo);
            //

            //Opvulling velden 'Maak Reservatie' Tab
            _reservatiesKlant = new ObservableCollection<DTOKlantReservatieInfo>(_klantManager.GeefKlantReservaties(_ingelogdeKlant.Klantnummer));
            foreach (var reservatieKlant in _reservatiesKlant)
            {
                ListViewReservations.Items.Add(reservatieKlant);
            }

            DatePickerDatumSelector.BlackoutDates.AddDatesInPast();
            DatePickerDatumSelector.BlackoutDates.Add(new CalendarDateRange(DateTime.Today.AddDays(8), DateTime.MaxValue));

            _toesteltypeItemsSource = _toestelTypeManager.SelecteerToestelType();

            _tijdslotItemsSource = _tijdslotManager.SelecteerTijdslot();
            //
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

        private void CheckboxAddAnother_Checked(object sender, RoutedEventArgs e)
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
        private void CheckboxAddAnother_Unchecked(object sender, RoutedEventArgs e)
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
            CheckboxAddAnother.IsEnabled = true;
            CheckboxAddAnother.IsChecked = false;

            ComboBoxToesteltypeSelector2.Items.Clear();
            ComboBoxBeginuurSelector2.Items.Clear();

            //Beginuur1.Value = 21u
            if (Convert.ToInt32(ComboBoxBeginuurSelector1.SelectedValue) == _tijdslotItemsSource[_tijdslotItemsSource.Count - 2].Tslot || _aantalGereserveerdeUrenPerDatum + (Convert.ToInt32(ComboBoxEinduurSelector1.SelectedValue) - Convert.ToInt32(ComboBoxBeginuurSelector1.SelectedValue)) > _maxAantalTijdsloten)
            {
                CheckboxAddAnother.IsEnabled = false;
                CheckboxAddAnother.IsChecked = false;

                foreach (var reservatieKlantVoorDagX in _klantReservatiesVoorDagX)
                {
                    if (Convert.ToInt32(ComboBoxEinduurSelector1.SelectedValue) == reservatieKlantVoorDagX.Beginuur)
                    {
                        CheckboxAddAnother.IsEnabled = false;
                        CheckboxAddAnother.IsChecked = false;
                    }
                }
            }


            //Beginuur1.Value = 20u
            else if (Convert.ToInt32(ComboBoxBeginuurSelector1.SelectedValue) == _tijdslotItemsSource[_tijdslotItemsSource.Count - 3].Tslot)
            {
                if (ComboBoxEinduurSelector1.SelectedIndex == 1)
                {
                    CheckboxAddAnother.IsEnabled = false;
                    CheckboxAddAnother.IsChecked = false;
                }
                else if (ComboBoxEinduurSelector1.SelectedIndex == 0)
                {
                    CheckboxAddAnother.IsEnabled = true;
                    ComboBoxBeginuurSelector2.Items.Add(Convert.ToInt32(ComboBoxEinduurSelector1.SelectedValue));
                    if (_aantalGereserveerdeUrenPerDatum >= _maxAantalTijdsloten - 1)
                    {
                        CheckboxAddAnother.IsEnabled = false;
                        CheckboxAddAnother.IsChecked = false;
                    }
                    else if (_aantalGereserveerdeUrenPerDatum + (Convert.ToInt32(ComboBoxEinduurSelector1.SelectedValue) - Convert.ToInt32(ComboBoxBeginuurSelector1.SelectedValue)) > _maxAantalTijdsloten - 1)
                    {
                        CheckboxAddAnother.IsEnabled = false;
                        CheckboxAddAnother.IsChecked = false;
                    }
                }
                foreach (var reservatieKlantVoorDagX in _klantReservatiesVoorDagX)
                {
                    if (Convert.ToInt32(ComboBoxEinduurSelector1.SelectedValue) == reservatieKlantVoorDagX.Beginuur)
                    {
                        CheckboxAddAnother.IsEnabled = false;
                        CheckboxAddAnother.IsChecked = false;
                    }
                }
            }
            //


            //Beginuur1 tot = 18u
            else
            {
                CheckboxAddAnother.IsEnabled = true;
                ComboBoxBeginuurSelector2.Items.Add(Convert.ToInt32(ComboBoxEinduurSelector1.SelectedValue));
                if (_aantalGereserveerdeUrenPerDatum >= _maxAantalTijdsloten - 1)
                {
                    CheckboxAddAnother.IsEnabled = false;
                    CheckboxAddAnother.IsChecked = false;
                }
                else if (_aantalGereserveerdeUrenPerDatum + (Convert.ToInt32(ComboBoxEinduurSelector1.SelectedValue) - Convert.ToInt32(ComboBoxBeginuurSelector1.SelectedValue)) > _maxAantalTijdsloten - 1)
                {
                    CheckboxAddAnother.IsEnabled = false;
                    CheckboxAddAnother.IsChecked = false;
                }
                foreach (var reservatieKlantVoorDagX in _klantReservatiesVoorDagX)
                {
                    if (Convert.ToInt32(ComboBoxEinduurSelector1.SelectedValue) == reservatieKlantVoorDagX.Beginuur)
                    {
                        CheckboxAddAnother.IsEnabled = false;
                        CheckboxAddAnother.IsChecked = false;
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
            try
            {
                //Checkt vrije toestellen
                Toestel? geselecteerdToestel = null;
                IReadOnlyList<Toestel> beschikbareToestellen = _toestelManager.GeefVrijeToestellenVoorGeselecteerdTijdslot(DatePickerDatumSelector.SelectedDate.Value, ComboBoxToesteltypeSelector1.SelectedValue.ToString(), Convert.ToInt32(ComboBoxBeginuurSelector1.SelectedValue), Convert.ToInt32(ComboBoxEinduurSelector1.SelectedValue));
                foreach (var beschikbaarToestel in beschikbareToestellen)
                {
                    if (beschikbaarToestel != null)
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
                    else
                    {
                        geselecteerdToestel = null;
                    }
                }
                //

                //Reservatie maken
                if (geselecteerdToestel == null) throw new Exception();
                else
                {
                    //Voorkomt dat Reservatie wordt aangemaakt indien er geen geldige ReservatieInfo is.
                    ReservatieInfo reservatieInfo = _reservatieInfoManager.ValideerReservatieInfo(DatePickerDatumSelector.SelectedDate.Value, Convert.ToInt32(ComboBoxBeginuurSelector1.SelectedValue), Convert.ToInt32(ComboBoxEinduurSelector1.SelectedValue), geselecteerdToestel);
                    //
                    if (reservatieInfo == null)
                    {
                        throw new Exception();
                    }
                    else
                    {
                        Reservatie reservatie = _reservatieManager.MaakReservatie(_ingelogdeKlant, DatePickerDatumSelector.SelectedDate.Value);
                        _reservatieInfoManager.MaakReservatieInfo(reservatie, Convert.ToInt32(ComboBoxBeginuurSelector1.SelectedValue), Convert.ToInt32(ComboBoxEinduurSelector1.SelectedValue), geselecteerdToestel);

                        MessageBox.Show($"New reservation has been made at {ComboBoxBeginuurSelector1.SelectedValue}h-{ComboBoxEinduurSelector1.SelectedValue}h on {geselecteerdToestel}\r Check out the 'My Reservation Tab' to see all reservations.", "Reservation confirmed");
                    }
                }
                //

                //ListView Refresh
                _reservatiesKlant.Clear();
                _reservatiesKlant = new ObservableCollection<DTOKlantReservatieInfo>(_klantManager.GeefKlantReservaties(_ingelogdeKlant.Klantnummer));
                ListViewReservations.Items.Clear();
                foreach (var reservatieKlant in _reservatiesKlant)
                {
                    ListViewReservations.Items.Add(reservatieKlant);
                }
                //

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
                MessageBox.Show($"There's no more available {ComboBoxToesteltypeSelector1.SelectedValue.ToString()} at {ComboBoxBeginuurSelector1.SelectedValue}h-{ComboBoxEinduurSelector1.SelectedValue}h", "Something went wrong :(");
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