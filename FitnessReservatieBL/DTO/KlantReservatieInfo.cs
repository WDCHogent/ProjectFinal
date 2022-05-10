using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitnessReservatieBL.DTO
{
    public class KlantReservatieInfo
    {
        public KlantReservatieInfo(int reservatienummer, DateTime datum, int beginuur, int einduur, string toestelnaam, int toestelId)
        {
            Reservatienummer = reservatienummer;
            Datum = datum;
            Beginuur = beginuur;
            Einduur = einduur;
            Toestelnaam = toestelnaam;
            ToestelId = toestelId;
        }

        public int Reservatienummer { get; set; }
        public DateTime Datum { get; set; }
        public int Beginuur { get; set; }
        public int Einduur { get; set; }
        public string Toestelnaam { get; set; }
        public int ToestelId { get; set; }

        public override string ToString()
        {
            return $"{Reservatienummer} | {Datum.ToShortDateString()} ~ {Beginuur}h-{Einduur}h ~ {Toestelnaam}{ToestelId}";
        }
    }
}
