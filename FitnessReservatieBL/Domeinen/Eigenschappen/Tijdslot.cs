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
        public Tijdslot(int tijdslot)
        {
            ZetTijdslot(tijdslot);
        }

        public int Tslot { get; set; }

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
