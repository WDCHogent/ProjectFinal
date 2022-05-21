using FitnessReservatieBL.Domeinen;
using FitnessReservatieBL.Domeinen.Eigenschappen;
using System;

namespace FitnessReservatieBL.Interfaces
{
    public interface IReservatieInfoRepository
    {
        void MaakReservatieInfo(ReservatieInfo reservatieinfo);
        ReservatieInfo ValideerReservatieInfo(DateTime datum, int beginuur, int einduur, Toestel toestel);
    }
}
