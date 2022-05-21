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
        private IReservatieRepository _reservatieRepo;
        private IKlantRepository _klantRepo;
        private IToestelRepository _toestelRepo;

        public ReservatieInfoManager(IReservatieInfoRepository reservatieInfoRepo, IReservatieRepository reservatieRepo, IKlantRepository klantRepo, IToestelRepository toestelRepo)
        {
            this._reservatieInfoRepo = reservatieInfoRepo;
            this._reservatieRepo = reservatieRepo;
            this._toestelRepo = toestelRepo;
            this._klantRepo = klantRepo;
        }

        public ReservatieInfo ValideerReservatieInfo(DateTime datum, int beginuur, int einduur, Toestel toestel)
        {
            if (toestel == null) throw new ReservatieInfoManagerException("ReservatieInfoManager - ValideerReservatieInfo - 'Geen vrije toestellen meer'");
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
                ReservatieInfo reservatieinfo = new ReservatieInfo(reservatie.Reservatienummer, beginuur, einduur, toestel);
                _reservatieInfoRepo.MaakReservatieInfo(reservatieinfo);
            }
            catch (Exception ex)
            {
                throw new ReservatieInfoManagerException("ReservatieInfoManager - MaakReservatieInfo", ex);
            }
        }
    }
}
