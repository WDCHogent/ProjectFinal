using FitnessReservatieBL.Domeinen.Eigenschappen;
using System.Collections.Generic;

namespace FitnessReservatieBL.Interfaces
{
    public interface IToestelTypeRepository
    {
        IReadOnlyList<ToestelType> SelecteerToestelType();
        int GeefToestelTypeNummer(string toestelTypeNaam);
    }
}
