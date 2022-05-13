using FitnessReservatieBL.Domeinen;
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

        //public Toestel GeefVrijToestel(int klantnummer, DateTime datum, string beginuur, string einduur, string toesteltype)
        //{
        //    if (klantnummer <= 0) throw new ToestelManagerException("ToestelManager - GeefVrijToestel - Ongeldige input");
        //    try
        //    {
        //        return _toestelRepo.GeefVrijToestel(klantnummer, datum, beginuur, einduur, toesteltype);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new ToestelManagerException("ToestelManager - GeefVrijToestel", ex);
        //    }
        //}
    }
}
