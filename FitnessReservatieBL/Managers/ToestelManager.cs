using FitnessReservatieBL.Domeinen;
using FitnessReservatieBL.Domeinen.Eigenschappen;
using FitnessReservatieBL.Domeinen.Enums;
using FitnessReservatieBL.DTO;
using FitnessReservatieBL.Exceptions;
using FitnessReservatieBL.Interfaces;
using System;
using System.Collections.Generic;

namespace FitnessReservatieBL.Managers
{
    public class ToestelManager
    {
        private IToestelRepository _toestelRepo;
        private IToestelTypeRepository _toestelTypeRepo;

        public ToestelManager(IToestelRepository toestelRepo, IToestelTypeRepository toestelTypeRepo)
        {
            this._toestelRepo = toestelRepo;
            this._toestelTypeRepo = toestelTypeRepo;
        }

        public IReadOnlyList<Toestel> GeefVrijeToestellenVoorGeselecteerdTijdslot(DateTime datum, string toesteltype, int beginuur, int einduur)
        {
            if (string.IsNullOrWhiteSpace(toesteltype) && beginuur == 0 && einduur == 0) throw new ToestelManagerException("ToestelManager - GeefVrijeToestellenVoorGeselecteerdTijdslot - Input is null");
            try
            {
                return _toestelRepo.GeefVrijToestelVoorGeselecteerdTijdslot(datum, toesteltype, beginuur, einduur);
            }
            catch (Exception ex)
            {
                throw new ToestelManagerException("ToestelManager - GeefVrijeToestellenVoorGeselecteerdTijdslot", ex);
            }
        }
        public IReadOnlyList<DTOToestelInfo> ZoekToestellen(Status? status, int toestelnummer, string toestelnaam, string toesteltype)
        {
            try
            {
                return _toestelRepo.ZoekToestellen(status, toestelnummer, toestelnaam, toesteltype);
            }
            catch (Exception ex)
            {
                throw new ToestelManagerException("ToestelManager - GeefToestellenMetStatus", ex);
            }
        }
        public string UpdateToestelStatus(DTOToestelInfo toestelInfo, string toestelStatus)
        {
            if (toestelInfo == null) throw new ToestelManagerException("ToestelManager - UpdateToestelStatus - toestel is null");
            if (toestelStatus == null) throw new ToestelManagerException("ToestelManager - UpdateToestelStatus - status is null");
            try
            {
                return _toestelRepo.UpdateToestelStatus(toestelInfo, toestelStatus);
            }
            catch (Exception ex)
            {
                throw new ToestelManagerException("ToestelManager - UpdateToestelStatus", ex);
            }
        }
        public string SchrijfToestelInDB(string toestelnaam, string toesteltypenaam)
        {
            try
            {
                int toestelTypeNummer = _toestelTypeRepo.GeefToestelTypeNummer(toesteltypenaam);
                Toestel toestel = new Toestel(toestelnaam, (Status)Enum.Parse(typeof(Status), "operatief"), new ToestelType(toesteltypenaam));
                if (!_toestelRepo.BestaatToestel(toestel, toestelTypeNummer))
                {
                    return _toestelRepo.SchrijfToestelInDB(toestel, toestelTypeNummer);
                }
                else return $"{toestelnaam} bestaat reeds.";      
            }
            catch (Exception ex)
            {
                throw new ToestelManagerException("ToestelManager - SchrijfToestelInDB", ex);
            }
        }
    }
}
