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
        public Reservatie(Klant klant, DateTime datum, Tijdslot tijdslot, Toestel toestel)
        {
            ZetKlant(klant);
            ZetDatum(datum);
            ZetTijdslot(tijdslot);
            ZetToestel(toestel);
        }

        //internal Reservatie(Klant klant, DateTime datum, Tijdslot tijdslot, ToestelType toestelType)
        //{
        //    ZetKlant(klant);
        //    ZetDatum(datum);
        //    ZetTijdslot(tijdslot);
        //    ZetToestelType(toestelType);
        //}

        public Klant Klant { get; private set; }
        public DateTime Datum { get; private set; }
        public Tijdslot Tijdslot { get; private set; }
        public Toestel Toestel { get; private set; }

        //public ToestelType ToestelType { get; private set; }

        public void ZetKlant(Klant klant)
        {
            if (klant == null) throw new ReservatieException("Reservatie - ZetKlant - 'Klant bestaat niet'");
            Klant = klant;
        }

        public void ZetDatum(DateTime datum)
        {
            if (datum < DateTime.Now) throw new ReservatieException("Reservatie - ZetDatum - 'ongeldige datum'");
            if (datum >= DateTime.Now.AddDays(7)) throw new ReservatieException("Reservatie - ZetDatum - 'datum te ver in de toekomst'");
            Datum = datum;
        }

        public void ZetTijdslot(Tijdslot tijdslot)
        {
            if (tijdslot == null) throw new ReservatieException("Reservatie - ZetTijdslot - 'Gelieve een tijdslot op te geven'");
            Tijdslot = tijdslot;
        }

        public void ZetToestel(Toestel toestel)
        {
            if (toestel == null) throw new ReservatieException("Reservatie - ZetToestel - 'Gelieve een toestel op te geven'");
            Toestel = toestel;
        }

        //public void ZetToestelType(ToestelType toestelType)
        //{
        //    if (ToestelType == null) throw new ReservatieException("Reservatie - ZetToestelType - 'Gelieve een toesteltype op te geven'");
        //    ToestelType = toestelType;
        //}
    }
}
