using FitnessReservatieBL.Domeinen;
using FitnessReservatieBL.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitnessReservatieBL.Interfaces
{
    public interface IReservatieRepository
    {
        bool BestaatReservatie(Reservatie reservatie);
        Reservatie MaakReservatie(Reservatie reservatie);
        Reservatie GeefReservatie(Reservatie reservatie);
    }
}
