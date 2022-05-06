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
        public Tijdslot(int tijdslotId, int beginuur, int einduur)
        {
            ZetBeginuur(beginuur);
            ZetEinduur(einduur);
        }

        public int TijdslotId { get; private set; }
        public int Beginuur { get; private set; }
        public int Einduur { get; private set; }

        public void ZetTijdslotId(int tijdslotId)
        {
            if (tijdslotId <= 0) throw new TijdslotException("Tijdslot - ZetTijdSlotId - 'Einduur kan niet vroeger zijn dan beginuur'");
            TijdslotId = tijdslotId;
        }

        public void ZetBeginuur(int beginuur)
        {
            if (beginuur <= 0) throw new TijdslotException("Tijdslot - ZetTijdSlotId - 'Einduur kan niet vroeger zijn dan beginuur'");
            Beginuur = beginuur;
        }

        public void ZetEinduur(int einduur)
        {
            if (einduur < Beginuur) throw new TijdslotException("Tijdslot - ZetEinduur - 'Einduur kan niet vroeger zijn dan beginuur'");
            if (einduur - Beginuur > 4) throw new TijdslotException("Tijdslot - ZetEinduur - 'Er kunnen maximaal 4 tijdslots gereserveerd worden'");
            Einduur = einduur;
        }

        public override string ToString()
        {
            return $"{Beginuur}u - {Einduur}u";
        }
    }
}
