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

        public ToestelManager(IToestelRepository repo)
        {
            this._toestelRepo = repo;
        }

        public IReadOnlyList<Toestel> GeefVrijeToestellenVoorGeselecteerdTijdslot(DateTime datum, string toesteltype, int beginuur, int einduur)
        {
            //if (klantnummer <= 0) throw new ToestelManagerException("ToestelManager - GeefVrijToestel - Ongeldige input");
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
                Toestel toestel = new Toestel(toestelnaam, (Status)Enum.Parse(typeof(Status), "operatief"), new ToestelType(toesteltypenaam));
                if (!_toestelRepo.BestaatToestel(toestel))
                {
                    return _toestelRepo.SchrijfToestelInDB(toestel);     
                }
            }
            catch (Exception ex)
            {
                throw new ToestelManagerException("ToestelManager - SchrijfToestelInDB", ex);
            }
        }
    }
}
