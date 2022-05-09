using FitnessReservatieBL.Domeinen;
using FitnessReservatieBL.Domeinen.Eigenschappen;
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

        public KlantWindow(Klant klant)
        {
            InitializeComponent();
            this._ingelogdeKlant = klant;
            LabelWelkomKlant.Content += $"{_ingelogdeKlant.Voornaam} {_ingelogdeKlant.Naam},";
            IToestelTypeRepository toesteltypeRepo = new ToestelTypeRepoADO(ConfigurationManager.ConnectionStrings["FinalDBConnection"].ToString());
            _toestelTypeManager = new ToestelTypeManager(toesteltypeRepo);
            ComboBox_ToesteltypeSelector.ItemsSource = _toestelTypeManager.SelecteerToestelOpToestelType();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
