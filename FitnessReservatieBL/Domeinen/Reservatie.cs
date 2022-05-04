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
        //UI opvraag constructor.
        public Reservatie(int reservatienummer, Klant klant, DateTime datum, Tijdslot tijdslot, ToestelType toesteltype)
        {
            ZetKlant(klant);
            ZetDatum(datum);
            ZetToestelType(toesteltype);
            ZetTijdslot(tijdslot);
        }

        //ReservatieConstructor
        public Reservatie(int reservatienummer, Klant klant, DateTime datum, Tijdslot tijdslot, Toestel toestel)
        {
            ZetKlant(klant);
            ZetDatum(datum);
            ZetTijdslot(tijdslot);
            ZetToestel(toestel);

            //TODO : Replacment for program-class
            //Klant.VoegReservatieToe(this);
        }

        public int Reservatienummer { get; set; }
        public Klant Klant { get; private set; }
        public DateTime Datum { get; private set; }
        public ToestelType ToestelType { get; private set; }
        public Tijdslot Tijdslot { get; private set; }

        public Toestel Toestel { get; private set; }

        public void ZetReservatienummer(int reservatienummer)
        {
            if (reservatienummer <= 0) throw new ReservatieException("Reservatie - ZetReservatrie");
            Reservatienummer = reservatienummer;
        }

        public void ZetKlant(Klant klant)
        {
            if (klant == null) throw new ReservatieException("Reservatie - ZetKlant - 'Klant bestaat niet'");
            Klant = klant;
        }

        public void ZetDatum(DateTime datum)
        {
            if (datum < DateTime.Now) throw new ReservatieException("Reservatie - ZetDatum - 'ongeldige datum'");
            if (datum >= DateTime.Now.AddDays(7)) throw new ReservatieException("Reservatie - ZetDatum - 'datum te ver in de toekomst'");
            if (Klant.GeefReservaties().Where(r => r.Datum.ToShortDateString().Contains(datum.ToShortDateString())).Count() >= 4) throw new ReservatieException("Reservatie - ZetKlant - 'Aantal reservaties mag niet meer dan 4 zijn per dag'");
            Datum = datum;
        }
        public void ZetToestelType(ToestelType toesteltype)
        {
            if (toesteltype == null) throw new ReservatieException("Reservatie - ZetToestel - 'Gelieve een toestel op te geven'");
            ToestelType = toesteltype;
        }

        public void ZetTijdslot(Tijdslot tijdslot)
        {
            if (tijdslot == null) throw new ReservatieException("Reservatie - ZetTijdslot - 'Gelieve een tijdslot op te geven'");
            if ((Klant.GeefReservaties().Count() > 0)) throw new ReservatieException("Reservatie - ZetKlant - 'Aantal reservaties mag niet meer dan 4 zijn per dag'");
            Tijdslot = tijdslot;
        }



        public void ZetToestel(Toestel toestel)
        {
            if (toestel == null) throw new ReservatieException("Reservatie - ZetToestel - 'Gelieve een toestel op te geven'");
            Toestel = toestel;
        }

        public override string ToString()
        {
            return $"{Klant},{Datum},{Tijdslot},{Toestel}";
        }
    }
}
