using FitnessReservatieBL.Domeinen.Eigenschappen;
using FitnessReservatieBL.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitnessReservatieBL.Domeinen
{
    public class Reservatie
    {

        public Reservatie(int reservatienummer, Klant klant, DateTime datum) : this(klant, datum)
        {
            ZetReservatienummer(reservatienummer);
        }

        public Reservatie(Klant klant, DateTime datum)
        {
            ZetKlant(klant);
            ZetDatum(datum);
        }

        public int Reservatienummer { get; set; }
        public Klant Klant { get; private set; }
        public DateTime Datum { get; private set; }

        public void ZetReservatienummer(int reservatienummer)
        {
            if (reservatienummer <= 0) throw new ReservatieException("Reservatie - ZetReservatrie");
            Reservatienummer = reservatienummer;
        }

        public void ZetKlant(Klant klant)
        {
            if (klant == null) throw new ReservatieException("Reservatie - ZetKlant - 'Mag niet leeg zijn'");
            Klant = klant;
        }

        public void ZetDatum(DateTime datum)
        {
            if (datum < DateTime.Now.AddDays(-1)) throw new ReservatieException("Reservatie - ZetDatum - 'ongeldige datum'");
            if (datum >= DateTime.Now.AddDays(7)) throw new ReservatieException("Reservatie - ZetDatum - 'datum te ver in de toekomst'");
            Datum = datum;
        }

        public override string ToString()
        {
            return $"{Reservatienummer},{Klant},{Datum}";
        }
    }
}
