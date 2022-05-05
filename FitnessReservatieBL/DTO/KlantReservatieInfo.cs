using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitnessReservatieBL.DTO
{
    public class KlantReservatieInfo
    {
        public KlantReservatieInfo(int reservatienummer, DateTime datum, int tijdslot, string toestelnaam)
        {
            Reservatienummer = reservatienummer;
            Datum = datum;
            Tijdslot = tijdslot;
            Toestelnaam = toestelnaam;
        }

        public int Reservatienummer { get; set; }
        public DateTime Datum { get; set; }
        public int Tijdslot { get; set; }
        public string Toestelnaam { get; set; }
    }
}
