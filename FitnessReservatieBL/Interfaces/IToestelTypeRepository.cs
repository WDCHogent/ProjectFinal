using FitnessReservatieBL.Domeinen.Eigenschappen;
using FitnessReservatieBL.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitnessReservatieBL.Interfaces
{
    public interface IToestelTypeRepository
    {
        IReadOnlyList<ToestelTypeInfo> SelecteerToestelOpToestelType();
    }
}
