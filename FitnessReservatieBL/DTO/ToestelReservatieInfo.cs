using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitnessReservatieBL.DTO
{
    public class ToestelReservatieInfo
    {
        public ToestelReservatieInfo(int reservatienummer, DateTime datum, int tijdslot, string klantvoornaam, string klantnaam)
        {
            Reservatienummer = reservatienummer;
            Datum = datum;
            Tijdslot = tijdslot;
            Klantvoornaam = klantvoornaam;
            Klantnaam = klantnaam;
        }

        public int Reservatienummer { get; set; }
        public DateTime Datum { get; set; }
        public int Tijdslot { get; set; }
        public string Klantvoornaam { get; set; }
        public string Klantnaam { get; set; }
    }
}
