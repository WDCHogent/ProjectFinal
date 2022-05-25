using FitnessReservatieBL.Domeinen;
using FitnessReservatieBL.Domeinen.Eigenschappen;
using FitnessReservatieBL.DTO;
using FitnessReservatieBL.Exceptions;
using FitnessReservatieBL.Interfaces;
using System;
using System.Collections.Generic;

namespace FitnessReservatieBL.Managers
{
    public class ReservatieManager
    {
        private IReservatieRepository _reservatieRepo;
        private IReservatieInfoRepository _reservatieInfoRepo;
        private IKlantRepository _klantRepo;
        private IToestelRepository _toestelRepo;

        public ReservatieManager(IReservatieRepository reservatieRepo, IReservatieInfoRepository reservatieInfoRepo, IKlantRepository klantRepo, IToestelRepository toestelRepo)
        {
            this._reservatieRepo = reservatieRepo;
            this._reservatieInfoRepo = reservatieInfoRepo;
            this._toestelRepo = toestelRepo;
            this._klantRepo = klantRepo;
        }

        public void MaakReservatie(Klant klant, DateTime datum, int beginuur, int einduur, string toestelTypeNaam)
        {
            try
            {
                Toestel geselecteerdToestel = null;
                int aantalGereserveerdeUrenPerDatum = 0;
                IReadOnlyList<Toestel> beschikbareToestellen = _toestelRepo.GeefVrijToestelVoorGeselecteerdTijdslot(datum, toestelTypeNaam, beginuur, einduur);
                IReadOnlyList<DTOKlantReservatieInfo> reservatiesKlant = _klantRepo.GeefKlantReservaties(klant.Klantnummer);
                List<DTOKlantReservatieInfo> klantReservatiesVoorDagX = new();
                foreach (DTOKlantReservatieInfo klantReservatie in reservatiesKlant)
                {
                    if (klantReservatie.Datum == datum)
                    {
                        klantReservatiesVoorDagX.Add(klantReservatie);
                        aantalGereserveerdeUrenPerDatum += klantReservatie.Einduur - klantReservatie.Beginuur;
                    }
                }
                foreach (var beschikbaarToestel in beschikbareToestellen)
                {
                    if (beschikbaarToestel != null)
                    {
                        if (aantalGereserveerdeUrenPerDatum != 0)
                        {
                            foreach (var klantReservatieVoorDagX in klantReservatiesVoorDagX)
                            {
                                if (!klantReservatieVoorDagX.Toestelnaam.Contains(beschikbaarToestel.ToestelNaam))
                                {
                                    geselecteerdToestel = beschikbaarToestel;
                                    break;
                                }
                            }
                        }
                        else
                        {
                            geselecteerdToestel = beschikbaarToestel;
                            break;
                        }
                    }
                    else
                    {
                        geselecteerdToestel = null;
                    }
                }
                if (geselecteerdToestel == null) throw new ReservatieInfoManagerException($"Er zijn helaas geen {toestelTypeNaam}en beschikbaar meer voor tijdslot {beginuur}u-{einduur}u");
                else
                {
                    Reservatie reservatie = new Reservatie(klant, datum);
                    ReservatieInfo reservatieInfo = _reservatieInfoRepo.ValideerReservatieInfo(datum, beginuur, einduur, geselecteerdToestel);
                    if (reservatieInfo == null)
                    {
                        throw new ReservatieManagerException("ReservatieManager - MaakReservatie");
                    }
                    else
                    {
                        if (!_reservatieRepo.BestaatReservatie(reservatie))
                        {
                            _reservatieInfoRepo.MaakReservatieInfo(_reservatieRepo.MaakReservatie(reservatie), beginuur, einduur, reservatieInfo.Toestel);
                        }
                        else if (_reservatieRepo.BestaatReservatie(reservatie))
                        {
                            _reservatieInfoRepo.MaakReservatieInfo(_reservatieRepo.GeefReservatie(reservatie), beginuur, einduur, reservatieInfo.Toestel);
                        }
                        else
                        {
                            throw new ReservatieManagerException("ReservatieManager - MaakReservatie");
                        }
                    }
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
