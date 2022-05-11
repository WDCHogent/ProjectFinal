using FitnessReservatieBL.Domeinen.Eigenschappen;
using FitnessReservatieBL.Exceptions;
using FitnessReservatieBL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitnessReservatieBL.Managers.Eigenschappen
{
    public class TijdslotManager
    {
        private ITijdslotRepository _tijdslotRepo;

        public TijdslotManager(ITijdslotRepository repo)
        {
            this._tijdslotRepo = repo;
        }

        public IReadOnlyList<Tijdslot> SelecteerBeginuur()
        {
            try
            {
                return _tijdslotRepo.SelecteerBeginuur();
            }
            catch (Exception ex)
            {
                throw new TijdslotManagerException("TijdslotManager - SelecteerBeginuur", ex);
            }
        }

        public IReadOnlyList<Tijdslot> SelecteerEinduur()
        {
            try
            {
                return _tijdslotRepo.SelecteerEinduur();
            }
            catch (Exception ex)
            {
                throw new TijdslotManagerException("TijdslotManager - SelecteerEinduur", ex);
            }
        }
    }
}
