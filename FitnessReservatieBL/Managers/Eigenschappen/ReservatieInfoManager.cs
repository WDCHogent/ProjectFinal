using FitnessReservatieBL.Domeinen;
using FitnessReservatieBL.Domeinen.Eigenschappen;
using FitnessReservatieBL.Exceptions;
using FitnessReservatieBL.Interfaces;
using System;

namespace FitnessReservatieBL.Managers.Eigenschappen
{
    public class ReservatieInfoManager
    {
        private IReservatieInfoRepository _reservatieInfoRepo;

        public ReservatieInfoManager(IReservatieInfoRepository repo)
        {
            this._reservatieInfoRepo = repo;
        }

        public void MaakReservatieInfo(int reservatienummer, int beginuur, int einduur, Toestel toestel)
        {
            try
            {
                ReservatieInfo reservatieinfo = new ReservatieInfo(reservatienummer, beginuur, einduur, toestel);
                _reservatieInfoRepo.MaakReservatieInfo(reservatieinfo);
            }
            catch (ReservatieInfoManagerException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new ReservatieInfoManagerException("MaakReservatieInfo", ex);
            }
        }
    }
}
