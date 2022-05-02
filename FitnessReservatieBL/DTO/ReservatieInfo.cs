using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitnessReservatieBL.DTO
{
    public class ReservatieInfo
    {
        public ReservatieInfo(int klantnummer, DateTime datum, int tijdslot, int toestelnummer, string toestelnaam)
        {
            Klantnummer = klantnummer;
            Datum = datum;
            Tijdslot = tijdslot;
            Toestelnummer = toestelnummer;
            Toestelnaam = toestelnaam;
        }

        public int Klantnummer { get; set; }
        public DateTime Datum { get; set; }
        public int Tijdslot { get; set; }
        public int Toestelnummer { get; set; }
        public string Toestelnaam { get; set; }
    }
}
