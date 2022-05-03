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
    public class ReservatieManager
    {
        private IReservatieRepository _reservatieRepo;

        public ReservatieManager(IReservatieRepository repo)
        {
            this._reservatieRepo = repo;
        }

        public IReadOnlyList<ReservatieInfo> SelecteerReservatie(int? klantnummer, int? toestelnummer)
        {
            if ((!klantnummer.HasValue) && (!toestelnummer.HasValue)) throw new ReservatieManagerException("ReservatieManagerException");
            try
            {
                return _reservatieRepo.SelecteerReservatie(klantnummer, toestelnummer);
            }
            catch (Exception ex)
            {
                throw new ReservatieManagerException("ReservatieManagerException", ex);
            }
        }
    }
}
