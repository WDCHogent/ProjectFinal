using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitnessReservatieBL.DTO
{
    public class DTOToestelReservatieInfo
    {
        public DTOToestelReservatieInfo(int reservatienummer, DateTime datum, int beginuur, int einduur, int klantnummer, string klantvoornaam, string klantnaam)
        {
            Reservatienummer = reservatienummer;
            Datum = datum;
            Beginuur = beginuur;
            Einduur = einduur;
            Klantvoornaam = klantvoornaam;
            Klantnaam = klantnaam;
        }

        public int Reservatienummer { get; set; }
        public DateTime Datum { get; set; }
        public int Beginuur { get; set; }
        public int Einduur { get; set; }
        public string Klantvoornaam { get; set; }
        public string Klantnaam { get; set; }
    }
}
