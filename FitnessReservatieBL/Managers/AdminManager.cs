using FitnessReservatieBL.Domeinen;
using FitnessReservatieBL.Exceptions;
using FitnessReservatieBL.Interfaces;
using System;

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
            if (string.IsNullOrWhiteSpace(adminnummer)) throw new AdminManagerException("AdminManager - SelecteerAdmin - input is null");
            try
            {
                return _adminRepo.SelecteerAdmin(adminnummer);
            }
            catch (Exception ex)
            {
                throw new AdminManagerException("AdminManager - SelecteerAdmin", ex);
            }
        }
    }
}
