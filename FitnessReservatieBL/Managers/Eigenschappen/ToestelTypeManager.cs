using FitnessReservatieBL.Domeinen.Eigenschappen;
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

        public int GeefToestelTypeNummer(string toestelTypeNaam)
        {
            try
            {
                return _toestelTypeRepo.GeefToestelTypeNummer(toestelTypeNaam);
            }
            catch (Exception ex)
            {
                throw new ToestelTypeManagerException("ToestelTypeManager - GeefToestelTypeNummer", ex);
            }
        }
    }
}
