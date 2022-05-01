using FitnessReservatieBL.Domeinen.Eigenschappen;
using FitnessReservatieBL.Exceptions;
using FitnessReservatieBL.Interfaces;
using System;

namespace FitnessReservatieBL.Managers.Eigenschappen
{
    public class ToestelTypeManager
    {
        private IToestelTypeRepository _repo;

        public ToestelTypeManager(IToestelTypeRepository repo)
        {
            this._repo = repo;
        }

        public ToestelType SelecteerToestelType(string toestelNaam)
        {
            try
            {
                if (this._repo.BestaatToestelType(toestelNaam))
                {
                    return _repo.SelecteerToestelType(toestelNaam);
                }
                else
                {
                    throw new ToestelTypeManagerException("ToestelTypeManager - SelecteerToestelType");
                }
            }
            catch (Exception ex)
            {
                throw new ToestelTypeManagerException("ToestelTypeManager - SelecteerToestelType", ex);
            }
        }
    }
}
