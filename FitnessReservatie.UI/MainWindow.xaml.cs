using System;
using System.Collections.Generic;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace FitnessReservatie.UI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void TextBoxKlantnummer_TextChanged(object sender, TextChangedEventArgs e)
        {
            if(!string.IsNullOrWhiteSpace(TextBoxKlantnummer.Text)) ButtonLogin.IsEnabled = true;
            else ButtonLogin.IsEnabled = false;
        }

        private void TextBoxEmailadres_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(TextBoxKlantnummer.Text)) ButtonLogin.IsEnabled = true;
            else ButtonLogin.IsEnabled = false;
        }

        private void ButtonLogin_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
