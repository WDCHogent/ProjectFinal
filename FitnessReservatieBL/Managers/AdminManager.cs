using FitnessReservatieBL.Domeinen;
using FitnessReservatieBL.Exceptions;
using FitnessReservatieBL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitnessReservatieBL.Managers
{
    public class AdminManager
    {
        private IAdminRepository _adminRepo;

        public AdminManager(IAdminRepository repo)
        {
            this._adminRepo = repo;
        }

        public Admin SelecteerAdmin(string adminnummer)
        {
            try
            {
                return _adminRepo.SelecteerAdmin(adminnummer);
            }
            catch (Exception ex)
            {
                throw new AdminManagerException("KlantManager - SelecteerKlant", ex);
            }
        }
    }
}
