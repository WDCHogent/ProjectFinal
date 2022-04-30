using FitnessReservatieBL.Domeinen.Tijdsloten;
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
        public Reservatie(Klant klant, DateTime datum, ToestellenPerTijdslot toestellenPerTijdslot)
        {
            Klant = klant;
            Datum = datum;
            ToestellenPerTijdslot = toestellenPerTijdslot;
        }

        public Klant Klant { get; private set; }
        public DateTime Datum { get; private set; }
        public ToestellenPerTijdslot ToestellenPerTijdslot { get; private set; }

        public void ZetKlant(Klant klant)
        {
        }

        public void ZetDatum(DateTime datum)
        {
            if (datum < DateTime.Now) throw new ReservatieException("Reservatie - ZetDatum - 'ongeldige datum'");
            Datum = datum;
        }

        public void ZetToestellenPerTijdslot(ToestellenPerTijdslot toestellenPerTijdslot)
        {
        }
    }
}
