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
    public class KlantManager
    {
        private IKlantRepository _klantRepo;

        public KlantManager(IKlantRepository repo)
        {
            this._klantRepo = repo;
        }

        public Klant SelecteerKlant(int? klantnummer, string mailadres)
        {
            if ((!klantnummer.HasValue) && string.IsNullOrWhiteSpace(mailadres)) throw new KlantManagerException("KlantManagerException - SelecteerKlant");
            try
            {
                return _klantRepo.SelecteerKlant(klantnummer, mailadres);
            }
            catch (Exception ex)
            {
                throw new KlantManagerException("KlantManagerException - SelecteerKlant", ex);
            }
        }
    }
}
