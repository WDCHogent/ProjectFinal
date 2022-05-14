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

        //public Reservatie(int reservatienummer, Klant klant, DateTime datum, Toestel toestel, Tijdslot beginuur, Tijdslot einduur)
        //{
        //    ZetReservatienummer(reservatienummer);
        //    ZetKlant(klant);
        //    ZetDatum(datum);
        //    ZetToestel(toestel);
        //    ZetBeginuur(beginuur);
        //    ZetEinduur(einduur);
        //}

        public Reservatie(int reservatienummer, Klant klant, DateTime datum)
        {
            ZetReservatienummer(reservatienummer);
            ZetKlant(klant);
            ZetDatum(datum);
        }

        public int Reservatienummer { get; set; }
        public Klant Klant { get; private set; }
        public DateTime Datum { get; private set; }
        public Tijdslot Beginuur { get; private set; }
        public Tijdslot Einduur { get; private set; }
        public Toestel Toestel { get; private set; }

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
            Datum = datum;
        }

        public void ZetToestel(Toestel toestel)
        {
            if (toestel == null) throw new ReservatieException("Reservatie - ZetToestel'");
            Toestel = toestel;
        }

        public void ZetBeginuur(Tijdslot beginuur)
        {
            if (beginuur == null) throw new ReservatieException("Reservatie - ZetBeginuur - 'Gelieve een beginuur op te geven'");
            Beginuur = beginuur;
        }

        public void ZetEinduur(Tijdslot einduur)
        {
            if (einduur == null) throw new ReservatieException("Reservatie - ZetEinduur - 'Gelieve een einduur op te geven'");
            if (einduur.Tslot < Beginuur.Tslot) throw new ReservatieException("Reservatie - ZetEinduur - 'Einduur kan niet kleiner zijn dan beginuur'");
            if (einduur.Tslot - Beginuur.Tslot > 4) throw new ReservatieException("Reservatie - ZetEinduur - 'Een tijdslot kan niet groeter zijn dan 4u'");
            Einduur = einduur;
        }

        public override string ToString()
        {
            return $"{Klant} | {Datum} ~ {Beginuur}h-{Einduur}h ~ {Toestel}";
        }
    }
}
