using FitnessReservatieBL.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitnessReservatieBL.Domeinen.Eigenschappen
{
    public class ReservatieInfo
    {
        public ReservatieInfo(int reservatienummer, int beginuur, int einduur, Toestel toestel)
        {
            ZetReservatienummer(reservatienummer);
            ZetBeginuur(beginuur);
            ZetEinduur(einduur);
            ZetToestel(toestel);
        }

        public int Reservatienummer { get; private set; }
        public int Beginuur { get; private set; }
        public int Einduur { get; private set; }
        public Toestel Toestel { get; private set; }

        public void ZetReservatienummer(int reservatienummer)
        {
            if (reservatienummer <= 0) throw new ReservatieInfoException("ReservatieInfo - ZetReservatienummer - 'Mag niet leeg zijn'");
            Reservatienummer = reservatienummer;
        }

        public void ZetBeginuur(int beginuur)
        {
            if (beginuur <= 0) throw new ReservatieInfoException("ReservatieInfo - ZetBeginuur - 'Mag niet leeg zijn'");
            Beginuur = beginuur;
        }

        public void ZetEinduur(int einduur)
        {
            if (einduur <= 0) throw new ReservatieInfoException("ReservatieInfo - ZetEinduur - 'Mag niet leeg zijn'");
            if (einduur < Beginuur) throw new ReservatieInfoException("ReservatieInfo - ZetEinduur - 'Einduur kan niet vroeger zijn dan beginuur'");
            Einduur = einduur;
        }

        public void ZetToestel(Toestel toestel)
        {
            if (toestel == null) throw new ReservatieInfoException("ReservatieInfo - ZetToestelType - 'Mag niet leeg zijn'");
            Toestel = toestel;
        }

        public override string ToString()
        {
            return $"{Reservatienummer},{Beginuur},{Einduur},{Toestel}";
        }
    }
}
