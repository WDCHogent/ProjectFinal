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
        public Tijdslot(int tijdslotId, int tijdslot)
        {
            ZetTijdslotId(tijdslotId);
            ZetTijdslot(tijdslot);
        }

        public int TijdslotId { get; private set; }
        public int Tslot { get; set; }

        public void ZetTijdslotId(int tijdslotId)
        {
            if (tijdslotId <= 0) throw new TijdslotException("Tijdslot - ZetTijdSlotId - 'Einduur kan niet vroeger zijn dan beginuur'");
            TijdslotId = tijdslotId;
        }

        public void ZetTijdslot(int tijdslot)
        {
            if (tijdslot <= 0) throw new TijdslotException("Tijdslot - ZetTijdSlotId - 'Einduur kan niet vroeger zijn dan beginuur'");
            Tslot = tijdslot;
        }
        public override string ToString()
        {
            return $"{Tslot}h";
        }
    }
}
