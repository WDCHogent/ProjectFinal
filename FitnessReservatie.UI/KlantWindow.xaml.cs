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
    /// Interaction logic for KlantWindow.xaml
    /// </summary>
    public partial class KlantWindow : Window
    {
        private Klant _ingelogdeKlant;

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
            ListViewReservations.ItemsSource = _klantManager.GeefKlantReservaties(_ingelogdeKlant.Klantnummer);

            IToestelTypeRepository toesteltypeRepo = new ToestelTypeRepoADO(ConfigurationManager.ConnectionStrings["FinalDBConnection"].ToString());
            _toestelTypeManager = new ToestelTypeManager(toesteltypeRepo);
            ComboBoxToesteltypeSelector.ItemsSource = _toestelTypeManager.SelecteerToestelType();

            ITijdslotRepository tijdslotRepo = new TijdslotRepoADO(ConfigurationManager.ConnectionStrings["FinalDBConnection"].ToString());
            _tijdslotManager = new TijdslotManager(tijdslotRepo);

            ComboBoxBeginuurSelector.ItemsSource = _tijdslotManager.SelecteerBeginuur();

            DatePickerDatumSelector.BlackoutDates.AddDatesInPast();
            DatePickerDatumSelector.BlackoutDates.Add(new CalendarDateRange(DateTime.Today.AddDays(8), DateTime.Today.AddMonths(1).AddDays(-1)));

            IReservatieRepository reservatieRepo = new ReservatieRepoADO(ConfigurationManager.ConnectionStrings["FinalDBConnection"].ToString());
            _reservatieManager = new ReservatieManager(reservatieRepo);
        }

        private void ComboBoxBeginuurSelector_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBoxEinduurSelector.Items.Clear();
            if (ComboBoxBeginuurSelector.SelectedIndex == _tijdslotManager.SelecteerEinduur().Count - 1)
            {
                ComboBoxEinduurSelector.Items.Add(_tijdslotManager.SelecteerEinduur()[this.ComboBoxBeginuurSelector.SelectedIndex]);
            }
            else if (ComboBoxBeginuurSelector.SelectedIndex == _tijdslotManager.SelecteerEinduur().Count - 2)
            {
                ComboBoxEinduurSelector.Items.Add(_tijdslotManager.SelecteerEinduur()[this.ComboBoxBeginuurSelector.SelectedIndex + 1]);
            }
            else if (ComboBoxBeginuurSelector.SelectedIndex == _tijdslotManager.SelecteerEinduur().Count - 3)
            {
                ComboBoxEinduurSelector.Items.Add(_tijdslotManager.SelecteerEinduur()[this.ComboBoxBeginuurSelector.SelectedIndex + 1]);
                ComboBoxEinduurSelector.Items.Add(_tijdslotManager.SelecteerEinduur()[this.ComboBoxBeginuurSelector.SelectedIndex + 2]);
            }
            else if (ComboBoxBeginuurSelector.SelectedIndex == _tijdslotManager.SelecteerEinduur().Count - 4)
            {
                ComboBoxEinduurSelector.Items.Add(_tijdslotManager.SelecteerEinduur()[this.ComboBoxBeginuurSelector.SelectedIndex + 1]);
                ComboBoxEinduurSelector.Items.Add(_tijdslotManager.SelecteerEinduur()[this.ComboBoxBeginuurSelector.SelectedIndex + 2]);
                ComboBoxEinduurSelector.Items.Add(_tijdslotManager.SelecteerEinduur()[this.ComboBoxBeginuurSelector.SelectedIndex + 3]);
            }
            else
            {
                ComboBoxEinduurSelector.Items.Add(_tijdslotManager.SelecteerEinduur()[this.ComboBoxBeginuurSelector.SelectedIndex + 1]);
                ComboBoxEinduurSelector.Items.Add(_tijdslotManager.SelecteerEinduur()[this.ComboBoxBeginuurSelector.SelectedIndex + 2]);
                ComboBoxEinduurSelector.Items.Add(_tijdslotManager.SelecteerEinduur()[this.ComboBoxBeginuurSelector.SelectedIndex + 3]);
                ComboBoxEinduurSelector.Items.Add(_tijdslotManager.SelecteerEinduur()[this.ComboBoxBeginuurSelector.SelectedIndex + 4]);
            }
        }

    }
}
