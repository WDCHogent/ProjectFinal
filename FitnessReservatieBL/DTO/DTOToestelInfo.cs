using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitnessReservatieBL.DTO
{
    public class DTOToestelInfo
    {
        public DTOToestelInfo(int toestelnummer, string toestelnaam, string status, string toesteltypenaam) : this(toestelnummer, toestelnaam, toesteltypenaam)
        {
            Toestelnummer = toestelnummer;
            Toestelnaam = toestelnaam;
            Status = status;
            Toesteltypenaam = toesteltypenaam;
        }

        public DTOToestelInfo (int toestelnummer, string toestelnaam, string toesteltypenaam)
        {
            Toestelnummer = toestelnummer;
            Toestelnaam = toestelnaam;
            Toesteltypenaam = toesteltypenaam;
        }

        public int Toestelnummer { get; set; }
        public string Toestelnaam { get; set; }
        public string Status { get; set; }
        public string Toesteltypenaam { get; set; }

    }
}
