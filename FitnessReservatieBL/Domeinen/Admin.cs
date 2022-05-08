using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitnessReservatieBL.Domeinen
{
    public class Admin
    {
        public Admin(string adminnummer, string naam, string voornaam)
        {
            Adminnummer = adminnummer;
            Naam = naam;
            Voornaam = voornaam;
        }

        public string Adminnummer { get; private set; }
        public string Naam { get; private set; }
        public string Voornaam { get; private set; }
    }
}
