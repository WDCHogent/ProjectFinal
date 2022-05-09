using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitnessReservatieBL.DTO
{
    public class ReservatieInfo
    {
        public ReservatieInfo(int klantnummer, string klantnaam, string klantvoornaam, string klantemail,  DateTime datum, int tijdslot, int toestelnummer, string toestelnaam)
        {
            Klantnummer = klantnummer;
            Klantnaam = klantnaam;
            Klantvoornaam = klantvoornaam;
            Klantemail = klantemail;
            Datum = datum;
            Tijdslot = tijdslot;
            Toestelnummer = toestelnummer;
            Toestelnaam = toestelnaam;
        }

        public int Klantnummer { get; set; }
        public string Klantnaam { get; set; }
        public string Klantvoornaam { get; set; }
        public string Klantemail { get; set; }
        public DateTime Datum { get; set; }
        public int Tijdslot { get; set; }
        public int Toestelnummer { get; set; }
        public string Toestelnaam { get; set; }
    }
}
