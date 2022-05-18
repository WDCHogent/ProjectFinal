using FitnessReservatieBL.Domeinen;
using FitnessReservatieBL.Domeinen.Eigenschappen;
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

        public ReservatieManager(IReservatieRepository reservatieRepo)
        {
            this._reservatieRepo = reservatieRepo;
        }

        public Reservatie MaakReservatie(Klant klant, DateTime datum)
        {
            if (klant == null) throw new ReservatieManagerException("ReservatieManager - MaakReservatie - klant is null");
            try
            {
                Reservatie reservatie = new Reservatie(klant, datum);
                if(!_reservatieRepo.BestaatReservatie(reservatie))
                {
                    reservatie = _reservatieRepo.MaakReservatie(reservatie);
                    return reservatie;
                }
                else if (_reservatieRepo.BestaatReservatie(reservatie))
                {
                    reservatie = _reservatieRepo.GeefReservatie(reservatie);
                    return reservatie;
                }
                else
                {
                    throw new ReservatieManagerException("ReservatieManager - MaakReservatie");
                }
            }
            catch (ReservatieManagerException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new ReservatieManagerException("MaakReservatie", ex);
            }
        }
    }
}
