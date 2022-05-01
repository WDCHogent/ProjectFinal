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
            TSlot = tijdslot;
        }

        public int TSlot { get; private set; }
    }
}
