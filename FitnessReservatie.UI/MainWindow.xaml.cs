using FitnessReservatieBL.Managers;
using FitnessReservatieDL.ADO.NET;
using System;
using System.Configuration;
using System.Windows;
using System.Windows.Controls;

namespace FitnessReservatie.UI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private KlantManager _klantManager;
        public MainWindow()
        {
            InitializeComponent();
            _klantManager = new KlantManager(new KlantRepoADO(ConfigurationManager.ConnectionStrings["FinalDBConnection"].ToString()));
        }

        private void TextBoxKlantnummer_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(TextBoxKlantnummer.Text)) ButtonLogin.IsEnabled = true;
            else ButtonLogin.IsEnabled = false;
        }

        private void TextBoxEmailadres_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(TextBoxEmailadres.Text)) ButtonLogin.IsEnabled = true;
            else ButtonLogin.IsEnabled = false;
        }

        private void ButtonLogin_Click(object sender, RoutedEventArgs e)
        {

            try
            {
                int? klantnummer;
                string mailadres;

                if (string.IsNullOrWhiteSpace(TextBoxKlantnummer.Text) && string.IsNullOrWhiteSpace(TextBoxEmailadres.Text)) MessageBox.Show("Ongeldig klantnummer of mailadres", "Something went wrong");
                if (!string.IsNullOrWhiteSpace(TextBoxKlantnummer.Text)) klantnummer = int.Parse(TextBoxKlantnummer.Text);
                else klantnummer = null;
                mailadres = TextBoxEmailadres.Text;
                var x = _klantManager.SelecteerKlant(klantnummer, mailadres);
                if (x != null) MessageBox.Show($"Hallo {x.Voornaam} {x.Naam}", "Login Successful");
                else MessageBox.Show($"Deze klant bestaat niet :(", "Something went wrong");
                Close();
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "Login Failed");
            }
        }
    }
}
