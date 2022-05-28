using FitnessReservatieBL.Domeinen;
using FitnessReservatieBL.DTO;
using System;
using System.Collections.Generic;

namespace FitnessReservatieBL.Interfaces
{
    public interface IReservatieRepository
    {
        bool BestaatReservatie(Reservatie reservatie);
        Reservatie MaakReservatie(Reservatie reservatie);
        Reservatie GeefReservatie(Reservatie reservatie);
        IReadOnlyList<DTOReservatieInfo> ZoekReservatie(int? reservatienummer, int? klantnummer, int? toestelnummer, DateTime? datum);
    }
}
