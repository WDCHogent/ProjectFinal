using FitnessReservatieBL.Domeinen;
using FitnessReservatieBL.Domeinen.Eigenschappen;
using FitnessReservatieBL.DTO;
using FitnessReservatieBL.Exceptions;
using FitnessReservatieBL.Interfaces;
using System;
using System.Collections.Generic;

namespace FitnessReservatieBL.Managers.Eigenschappen
{
    public class ReservatieInfoManager
    {
        private IReservatieInfoRepository _reservatieInfoRepo;

        public ReservatieInfoManager(IReservatieInfoRepository reservatieInfoRepo)
        {
            this._reservatieInfoRepo = reservatieInfoRepo;
        }

        public ReservatieInfo ValideerReservatieInfo(DateTime datum, int beginuur, int einduur, Toestel toestel)
        {
            try
            {
                ReservatieInfo reservatieinfo = _reservatieInfoRepo.ValideerReservatieInfo(datum, beginuur, einduur, toestel);
                return reservatieinfo;
            }
            catch (Exception ex)
            {
                throw new ReservatieInfoManagerException("ReservatieInfoManager - ValideerReservatieInfo", ex);
            }
        }

        public void MaakReservatieInfo(Reservatie reservatie, int beginuur, int einduur, Toestel toestel)
        {
            if (reservatie == null) throw new ReservatieInfoManagerException("ReservatieInfoManager - MaakReservatieInfo - 'Reservatie is null'");
            else if (toestel == null) throw new ReservatieInfoManagerException("ReservatieInfoManager - MaakReservatieInfo - 'Geen vrije toestellen meer'");

            try
            {
                _reservatieInfoRepo.MaakReservatieInfo(reservatie, beginuur, einduur, toestel);
            }
            catch (Exception ex)
            {
                throw new ReservatieInfoManagerException("ReservatieInfoManager - MaakReservatieInfo", ex);
            }
        }
    }
}
