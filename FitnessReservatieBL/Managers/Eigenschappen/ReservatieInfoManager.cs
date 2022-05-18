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
        private IReservatieRepository _reservatieRepo;
        private IKlantRepository _klantRepo;
        private IToestelRepository _toestelRepo;

        public ReservatieInfoManager(IReservatieInfoRepository reservatieInfoRepo, IReservatieRepository reservatieRepo, IKlantRepository klantRepo ,IToestelRepository toestelRepo)
        {
            this._reservatieInfoRepo = reservatieInfoRepo;
            this._reservatieRepo = reservatieRepo;
            this._toestelRepo = toestelRepo;
            this._klantRepo = klantRepo;
        }

        public void MaakReservatieInfo(Reservatie reservatie, int beginuur, int einduur, Toestel toestel)
        {
            if (reservatie == null) throw new ReservatieInfoManagerException("ReservatieInfoManager - MaakReservatieInfo - 'Reservatie is null'");
            else if (toestel == null) throw new ReservatieInfoManagerException("ReservatieInfoManager - MaakReservatieInfo - 'Geen vrije toestellen meer'");

            //Controles klant
            int GereserveerdeUren = 0;
            foreach (var klantreservatie in _klantRepo.GeefKlantReservaties(reservatie.Klant.Klantnummer)) { if (klantreservatie.Datum == reservatie.Datum) GereserveerdeUren += klantreservatie.Einduur-klantreservatie.Beginuur; }

            bool toestelReedsGereserveerd = false;
            foreach (var klantreservatie in _klantRepo.GeefKlantReservaties(reservatie.Klant.Klantnummer)) { if (klantreservatie.Datum == reservatie.Datum && klantreservatie.Toestelnaam == toestel.ToestelNaam) toestelReedsGereserveerd = true; }

            if (GereserveerdeUren >= 4) throw new ReservatieInfoManagerException("ReservatieInfoManager - MaakReservatieInfo - 'Daglimiet overschreden'");
            if ((GereserveerdeUren + (einduur - beginuur)) >= 4) throw new ReservatieInfoManagerException("ReservatieInfoManager - MaakReservatieInfo - 'Daglimiet overschreden'");

            if ((toestelReedsGereserveerd == true)) throw new ReservatieInfoManagerException("ReservatieInfoManager - MaakReservatieInfo - 'Toestel werd reeds gereserveerd'");
            //

            try
            {
                ReservatieInfo reservatieinfo = new ReservatieInfo(reservatie.Reservatienummer, beginuur, einduur, toestel);
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
