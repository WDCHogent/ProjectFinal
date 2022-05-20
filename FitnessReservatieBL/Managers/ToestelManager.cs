using FitnessReservatieBL.Domeinen;
using FitnessReservatieBL.Domeinen.Enums;
using FitnessReservatieBL.DTO;
using FitnessReservatieBL.Exceptions;
using FitnessReservatieBL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        public IReadOnlyList<DTOToestelInfo> GeefToestellenADHVStatus(Status status)
        {
            try
            {
                return _toestelRepo.GeefToestellenADHVStatus(status);
            }
            catch (Exception ex)
            {
                throw new ToestelManagerException("ToestelManager - GeefToestellenMetStatus", ex);
            }
        }

        public IReadOnlyList<DTOToestelInfo> GeefToestellenADHVParameters(int? toestelnummer, string toestelnaam, string toesteltype)
        {
            try
            {
                return _toestelRepo.GeefToestellenADHVParameters(toestelnummer, toestelnaam, toesteltype);
            }
            catch (Exception ex)
            {
                throw new ToestelManagerException("ToestelManager - GeefToestellenMetStatus", ex);
            }
        }


    }
}
