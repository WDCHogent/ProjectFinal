using FitnessReservatieBL.Domeinen;
using FitnessReservatieBL.DTO;
using FitnessReservatieBL.Exceptions;
using FitnessReservatieBL.Interfaces;
using System;
using System.Collections.Generic;

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
                throw new KlantManagerException("KlantManager - SelecteerKlant", ex);
            }
        }

        public IReadOnlyList<DTOKlantReservatieInfo> GeefKlantReservaties(int klantnummer)
        {
            if (klantnummer <= 0) throw new KlantManagerException("KlantManager - GeefKlantReservatie - Klant is null");
            try
            {
                return _klantRepo.GeefKlantReservaties(klantnummer);
            }
            catch (Exception ex)
            {
                throw new KlantManagerException("KlantManager - GeefKlantReservaties", ex);
            }
        }

        public IReadOnlyList<Klant> ZoekKlanten(int klantnummer, string zoekterm)
        {
            try
            {
                return _klantRepo.ZoekKlanten(klantnummer, zoekterm);
            }
            catch (Exception ex)
            {
                throw new KlantManagerException("KlantManager - ZoekKlanten", ex);
            }
        }
    }
}
