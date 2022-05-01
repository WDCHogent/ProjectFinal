using FitnessReservatieBL.Domeinen.Eigenschappen;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitnessReservatieBL.Interfaces
{
    public interface IToestelTypeRepository
    {
        ToestelType SelecteerToestelType(string toestelNaam);
        bool BestaatToestelType(string toestelNaam);
    }
}
