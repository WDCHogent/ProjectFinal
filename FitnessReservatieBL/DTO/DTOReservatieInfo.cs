using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitnessReservatieBL.DTO
{
    public class DTOReservatieInfo
    {
        public DTOReservatieInfo(int reservatienummer, int klantnummer, string klantnaam, string klantvoornaam, string klantemail, DateTime datum, int beginuur, int einduur, string toestelnaam)
        {
            Klantnummer = klantnummer;
            Klantnaam = klantnaam;
            Klantvoornaam = klantvoornaam;
            Klantemail = klantemail;
            Datum = datum;
            Beginuur = beginuur;
            Einduur = einduur;
            Toestelnaam = toestelnaam;
        }

        public int Klantnummer { get; set; }
        public string Klantnaam { get; set; }
        public string Klantvoornaam { get; set; }
        public string Klantemail { get; set; }
        public DateTime Datum { get; set; }
        public int Beginuur { get; set; }
        public int Einduur { get; set; }
        public string Toestelnaam { get; set; }

        public override string ToString()
        {
            return $"{Klantnummer},{Klantnaam},{Klantvoornaam},{Klantemail},{Datum},{Beginuur},{Einduur},{Toestelnaam}";
        }
    }
}
