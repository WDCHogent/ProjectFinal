using FitnessReservatieBL.Domeinen;
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
using System.Windows.Shapes;

namespace FitnessReservatie.UI
{
    /// <summary>
    /// Interaction logic for AdminWindow.xaml
    /// </summary>
    public partial class AdminWindow : Window
    {
        private Admin _ingelogdeAdmin;
        public AdminWindow(Admin admin)
        {
            InitializeComponent();
            this._ingelogdeAdmin = admin;
            LabelWelkomAdmin.Content += $"{_ingelogdeAdmin.Voornaam} {_ingelogdeAdmin.Naam}";
        }
    }
}
