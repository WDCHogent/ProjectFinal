using FitnessReservatieBL.Domeinen.Eigenschappen;
using FitnessReservatieBL.DTO;
using FitnessReservatieBL.Exceptions;
using FitnessReservatieBL.Interfaces;
using System;
using System.Collections.Generic;

namespace FitnessReservatieBL.Managers.Eigenschappen
{
    public class ToestelTypeManager
    {
        private IToestelTypeRepository _toestelTypeRepo;

        public ToestelTypeManager(IToestelTypeRepository repo)
        {
            this._toestelTypeRepo = repo;
        }

        public IReadOnlyList<ToestelType> SelecteerToestelType()
        {
            try
            {
                return _toestelTypeRepo.SelecteerToestelType();
            }
            catch (Exception ex)
            {
                throw new ToestelTypeManagerException("ToestelTypeManager - SelecteerToestelType", ex);
            }
        }
    }
}
