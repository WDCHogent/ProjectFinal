using FitnessReservatieBL.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitnessReservatieBL.Domeinen.Eigenschappen
{
    public class Tijdslot
    {
        public Tijdslot(int beginuur, int einduur)
        {
            ZetBeginuur(beginuur);
            ZetEinduur(einduur);
        }

        public int Beginuur { get; private set; }
        public int Einduur { get; private set; }

        //Later new SortedSet<int>() en tijdsloten in db
        private SortedSet<int> Tijdsloten = new SortedSet<int> { 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22 };

        public void ZetBeginuur(int beginuur)
        {
            if (!Tijdsloten.Contains(beginuur)) throw new TijdslotException($"Tijdslot - ZetBeginuur - 'Tijdslot moet tussen {Tijdsloten.Min()} & {Tijdsloten.Max()} zijn'");
            Beginuur = beginuur;
        }

        public void ZetEinduur(int einduur)
        {
            if (!Tijdsloten.Contains(einduur)) throw new TijdslotException("Tijdslot - ZetBeginuur - 'Tijdslot mag niet langer zijn dan 2u'");
            if (einduur < Beginuur) throw new TijdslotException("Tijdslot - ZetEinduur - 'Einduur kan niet vroeger zijn dan beginuur'");
            Einduur = einduur;
        }

        private void VoegTijdslotToe(int tijdslot)
        {
            if (tijdslot < 0 || tijdslot > 24) throw new TijdslotException("Tijdslot - VoegTijdslotToe - 'Geen geldige tijdwaarde'");
            Tijdsloten.Add(tijdslot);
        }

        public override string ToString()
        {
            return $"{Beginuur}u - {Einduur}u";
        }
    }
}
