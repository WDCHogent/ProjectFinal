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
            if (tijdslot <= 0) throw new TijdslotException("Tijdslot - ZetTijdSlot - 'Mag niet leeg zijn'");
            if (tijdslot > 24) throw new TijdslotException("Tijdslot - ZetTijdSlot - 'ongeldig tijdslot'");
            Tslot = tijdslot;
        }
        public override string ToString()
        {
            return $"{Tslot}";
        }
    }
}
