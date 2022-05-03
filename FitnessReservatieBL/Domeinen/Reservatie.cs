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
        public Reservatie(Klant klant,DateTime datum, Tijdslot tijdslot, Toestel toestel)
        {
            ZetKlant(klant);
            ZetDatum(datum);
            ZetTijdslot(tijdslot);
            ZetToestel(toestel);
        }

        public Klant Klant { get; private set; }
        public DateTime Datum { get; private set; }
        public Tijdslot Tijdslot { get; private set; }
        public Toestel Toestel { get; private set; }

        public void ZetKlant(Klant klant)
        {
            if (klant == null) throw new ReservatieException("Reservatie - ZetKlant - 'Klant bestaat niet'");
            Klant = klant;
        }

        public void ZetDatum(DateTime datum)
        {
            if (datum < DateTime.Now) throw new ReservatieException("Reservatie - ZetDatum - 'ongeldige datum'");
            if (datum >= DateTime.Now.AddDays(7)) throw new ReservatieException("Reservatie - ZetDatum - 'datum te ver in de toekomst'");
            if (Klant.GeefReservaties().Where(r => r.Datum == this.Datum).Count() >= 4) throw new ReservatieException("Reservatie - ZetKlant - 'Aantal reservaties mag niet meer dan 4 zijn per dag'");
            Datum = datum;
        }

        public void ZetTijdslot(Tijdslot tijdslot)
        {
            if (tijdslot == null) throw new ReservatieException("Reservatie - ZetTijdslot - 'Gelieve een tijdslot op te geven'");
            if ((Klant.GeefReservaties().Where(r => r.Tijdslot.TSlot.AddHours(-1) == this.Tijdslot.TSlot && r.Tijdslot.TSlot.AddHours(-2) == this.Tijdslot.TSlot)).Count() > 0) throw new ReservatieException("Reservatie - ZetKlant - 'Aantal reservaties mag niet meer dan 4 zijn per dag'");
            Tijdslot = tijdslot;
        }

        public void ZetToestel(Toestel toestel)
        {
            if (toestel == null) throw new ReservatieException("Reservatie - ZetToestel - 'Gelieve een toestel op te geven'");
            Toestel = toestel;
        }

        internal void 

        public override string ToString()
        {
            return $"{Klant},{Datum},{Tijdslot},{Toestel}";
        }
    }
}
