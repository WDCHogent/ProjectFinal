using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitnessReservatieBL.DTO
{
    public class KlantInfo
    {
        public KlantInfo(int klantnummer, string naam, string voornaam, string mailadres, string abonnementType)
        {
            Klantnummer = klantnummer;
            Naam = naam;
            Voornaam = voornaam;
            Mailadres = mailadres;
            AbonnementType = abonnementType;
        }

        public int Klantnummer { get; private set; }
        public string Naam { get; private set; }
        public string Voornaam { get; private set; }
        public string Mailadres { get; private set; }
        public string AbonnementType { get; private set; }
    }
}
