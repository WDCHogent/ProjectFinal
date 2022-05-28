using FitnessReservatieBL.Domeinen;
using FitnessReservatieBL.Domeinen.Eigenschappen;
using System;

namespace FitnessReservatieBL.Interfaces
{
    public interface IReservatieInfoRepository
    {
        void MaakReservatieInfo(Reservatie reservatie, Toestel toestel, int beginuur, int einduur);
        ReservatieInfo ValideerReservatieInfo(DateTime datum, int beginuur, int einduur, Toestel toestel);
    }
}
