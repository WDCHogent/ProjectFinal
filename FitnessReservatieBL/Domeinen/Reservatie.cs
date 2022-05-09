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
        //    //TODO : Replacment for program-class > to reservatiebeheerder
        //    //Klant.VoegReservatieToe(this);
        //    //Toestel.VoegReservatieToe(this);

        public Reservatie(int reservatienummer, Klant klant, DateTime datum, Toestel toestel, Tijdslot tijdslot)
        {
            ZetKlant(klant);
            ZetDatum(datum);
            ZetToestel(toestel);
            ZetTijdslot(tijdslot);
        }

        public int Reservatienummer { get; set; }
        public Klant Klant { get; private set; }
        public DateTime Datum { get; private set; }
        public Tijdslot Tijdslot { get; private set; }
        public Toestel Toestel { get; private set; }

        public ToestelType ToestelType { get; private set; }

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
            if (datum < DateTime.Now) throw new ReservatieException("Reservatie - ZetDatum - 'ongeldige datum'");
            if (datum >= DateTime.Now.AddDays(7)) throw new ReservatieException("Reservatie - ZetDatum - 'datum te ver in de toekomst'");
            //if (Klant.GeefReservaties().Where(r => r.Datum.ToShortDateString().Contains(datum.ToShortDateString())).Count() >= 4) throw new ReservatieException("Reservatie - ZetKlant - 'Aantal reservaties mag niet meer dan 4 zijn per dag'");
            Datum = datum;
        }

        public void ZetToestel(Toestel toestel)
        {
            if (toestel == null) throw new ReservatieException("Reservatie - ZetToestel'");
            //controle voor query
            //if (Toestel.GeefReservaties().Where(r => r.Datum == this.Datum).Count() > 14) throw new ReservatieException($"Reservatie - ZetToestel - 'Geen {toestel.ToestelType.ToString()} beschikbaar voor {Datum.ToShortDateString()}'");
            Toestel = toestel;
        }

        public void ZetTijdslot(Tijdslot tijdslot)
        {
            if (tijdslot == null) throw new ReservatieException("Reservatie - ZetTijdslot - 'Gelieve een tijdslot op te geven'");
            // controles voor query
            //if ((Klant.GeefReservaties().Where(r => (r.Datum.ToShortDateString().Contains(Datum.ToShortDateString()) && r.Toestel == Toestel && r.Tijdslot.Einduur == tijdslot.Beginuur - 1) || (r.Datum.ToShortDateString().Contains(Datum.ToShortDateString()) && r.Toestel == Toestel && r.Tijdslot.Einduur == tijdslot.Beginuur - 2)).Count() > 1)) throw new ReservatieException("Reservatie - ZetTijdslot - 'Geen Toestel vrij op dit tijdslot'");
            //if ((Klant.GeefReservaties().Where(r => (r.Datum.ToShortDateString().Contains(Datum.ToShortDateString()) && r.Toestel == Toestel && r.Tijdslot.Beginuur == tijdslot.Einduur + 1) || (r.Datum.ToShortDateString().Contains(Datum.ToShortDateString()) && r.Toestel == Toestel && r.Tijdslot.Beginuur == tijdslot.Einduur + 2)).Count() > 1)) throw new ReservatieException("Reservatie - ZetKlant - 'Geen Toestel vrij op dit tijdslot'");
            //if (Toestel.GeefReservaties().Where(r => (r.Datum.ToShortDateString().Contains(Datum.ToShortDateString()) && r.Tijdslot == r.Tijdslot)).Count() > 1) throw new ReservatieException("Reservatie - ZetKlant - 'Geen Toestel vrij op dit tijdslot'");
            Tijdslot = tijdslot;
        }

        public override string ToString()
        {
            return $"{Klant},{Datum},{Tijdslot},{Toestel}";
        }
    }
}
