using FitnessReservatieBL.Domeinen.Eigenschappen;
using FitnessReservatieBL.Exceptions;
using FitnessReservatieBL.Interfaces;
using System;
using System.Collections.Generic;

namespace FitnessReservatieBL.Managers.Eigenschappen
{
    public class TijdslotManager
    {
        private ITijdslotRepository _tijdslotRepo;

        public TijdslotManager(ITijdslotRepository repo)
        {
            this._tijdslotRepo = repo;
        }

        public IReadOnlyList<Tijdslot> SelecteerTijdslot()
        {
            try
            {
                return _tijdslotRepo.SelecteerTijdslot();
            }
            catch (Exception ex)
            {
                throw new TijdslotManagerException("TijdslotManager - SelecteerEinduur", ex);
            }
        }
    }
}
