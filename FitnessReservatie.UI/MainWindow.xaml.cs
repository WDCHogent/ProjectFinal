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
            if (!string.IsNullOrWhiteSpace(TextBoxKlantnummer.Text)) ButtonKlantLogin.IsEnabled = true;
            else ButtonKlantLogin.IsEnabled = false;
        }

        private void TextBoxEmailadres_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(TextBoxEmailadres.Text)) ButtonKlantLogin.IsEnabled = true;
            else ButtonKlantLogin.IsEnabled = false;
        }
        private void TextBoxAdmin_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(TextBoxAdmin.Text)) ButtonKlantLogin.IsEnabled = true;
            else ButtonKlantLogin.IsEnabled = false;
        }

        private void ButtonKlantLogin_Click(object sender, RoutedEventArgs e)
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
                if (x != null) MessageBox.Show($"Welkom terug\r{x.Voornaam} {x.Naam}", "Login Successful");
                else MessageBox.Show($"Deze klant bestaat niet :(", "Something went wrong");
                Close();
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "Login Failed");
            }
        }

        private void ButtonAdminLogin_Click(object sender, RoutedEventArgs e)
        {
            //try
            //{
            //    string adminnummer;

            //    if (string.IsNullOrWhiteSpace(TextBoxKlantnummer.Text) && string.IsNullOrWhiteSpace(TextBoxEmailadres.Text)) MessageBox.Show("Ongeldig klantnummer of mailadres", "Something went wrong");
            //    if (!string.IsNullOrWhiteSpace(TextBoxKlantnummer.Text)) klantnummer = int.Parse(TextBoxKlantnummer.Text);
            //    else klantnummer = null;
            //    mailadres = TextBoxEmailadres.Text;
            //    var x = _klantManager.SelecteerKlant(klantnummer, mailadres);
            //    if (x != null) MessageBox.Show($"Hallo {x.Voornaam} {x.Naam}", "Login Successful");
            //    else MessageBox.Show($"Deze klant bestaat niet :(", "Something went wrong");
            //    Close();
            //}
            //catch (Exception ex)
            //{

            //    MessageBox.Show(ex.Message, "Login Failed");
            //}
        }

        private void ButtonKlant_Click(object sender, RoutedEventArgs e)
        {
            ImageBackgroundAdmin.Visibility = Visibility.Visible;
            ImageBackgroundKlant.Visibility = Visibility.Hidden;

            LabelAdminTitel.Visibility = Visibility.Visible;
            LabelKlantTitel.Visibility = Visibility.Hidden;

            LabelAdmin.Visibility = Visibility.Visible;
            TextBoxAdmin.Visibility = Visibility.Visible;

            LabelKlantnummer.Visibility = Visibility.Hidden;
            TextBoxKlantnummer.Visibility = Visibility.Hidden;
            LabelEmailadres.Visibility = Visibility.Hidden;
            TextBoxEmailadres.Visibility = Visibility.Hidden;

            ButtonAdminLogin.Visibility = Visibility.Visible;
            ButtonKlantLogin.Visibility = Visibility.Hidden;

            ButtonAdmin.Visibility= Visibility.Visible;
            ButtonKlant.Visibility= Visibility.Hidden;
        }

        private void ButtonAdmin_Click(object sender, RoutedEventArgs e)
        {
            ImageBackgroundKlant.Visibility = Visibility.Visible;
            ImageBackgroundAdmin.Visibility = Visibility.Hidden;

            LabelKlantTitel.Visibility = Visibility.Visible;
            LabelAdminTitel.Visibility = Visibility.Hidden;

            LabelKlantnummer.Visibility = Visibility.Visible;
            TextBoxKlantnummer.Visibility = Visibility.Visible;
            LabelEmailadres.Visibility = Visibility.Visible;
            TextBoxEmailadres.Visibility = Visibility.Visible;

            LabelAdmin.Visibility = Visibility.Hidden;
            TextBoxAdmin.Visibility = Visibility.Hidden;

            ButtonKlantLogin.Visibility = Visibility.Visible;
            ButtonAdminLogin.Visibility = Visibility.Hidden;

            ButtonKlant.Visibility = Visibility.Visible;
            ButtonAdmin.Visibility = Visibility.Hidden;
        }

    }
}
