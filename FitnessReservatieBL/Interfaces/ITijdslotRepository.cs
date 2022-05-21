using FitnessReservatieBL.Domeinen.Eigenschappen;
using System.Collections.Generic;

namespace FitnessReservatieBL.Interfaces
{
    public interface ITijdslotRepository
    {
        IReadOnlyList<Tijdslot> SelecteerTijdslot();
    }
}
