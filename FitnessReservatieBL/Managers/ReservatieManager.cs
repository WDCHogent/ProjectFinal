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

        public void MaakReservatie(Klant klant, DateTime datum, string toestelTypeNaam1, int beginuur1, int einduur1, string toestelTypeNaam2, int beginuur2, int einduur2)

        {
            try
            {
                Toestel geselecteerdToestel1 = null; Toestel geselecteerdToestel2 = null;
                List<Toestel> beschikbareToestellen1 = new(); List<Toestel> beschikbareToestellen2 = new();
                int aantalGereserveerdeUrenPerDatum = 0;

                //Geeft vrije toestellen terug voor datum en geselecteerd tijdslot.
                beschikbareToestellen1 = _toestelRepo.GeefVrijToestelVoorGeselecteerdTijdslot(datum, toestelTypeNaam1, beginuur1, einduur1);

                //Geeft reservaties van klant voor dag X.
                IReadOnlyList<DTOKlantReservatieInfo> reservatiesKlantVoorDagX = _klantRepo.GeefKlantReservatiesVoorDagX(klant, datum);

                //Aantal gereserveerde uren voor dag X.
                foreach (DTOKlantReservatieInfo klantReservatie in reservatiesKlantVoorDagX)
                {
                    aantalGereserveerdeUrenPerDatum += klantReservatie.Einduur - klantReservatie.Beginuur;
                }
                //

                //Geeft vrij toestel terug uit vrije toestellen.
                if (aantalGereserveerdeUrenPerDatum >= 4) throw new ReservatieManagerException("ReservatieManager - MaakReservatie");

                if (beschikbareToestellen1 != null)
                {
                    if (aantalGereserveerdeUrenPerDatum != 0)
                    {
                        foreach (var beschikbaarToestel1 in beschikbareToestellen1)
                        {
                            foreach (var klantReservatieVoorDagX in reservatiesKlantVoorDagX)
                            {
                                //Controle tijdslot.
                                if (beginuur1 == klantReservatieVoorDagX.Beginuur || einduur1 == klantReservatieVoorDagX.Einduur || beginuur2 == klantReservatieVoorDagX.Beginuur || einduur2 == klantReservatieVoorDagX.Einduur || beginuur2 == beginuur1 || einduur2 == einduur1 ) throw new ReservatieManagerException("ReservatieManager - MaakReservatie");
                                if ((einduur1 - beginuur1 == 2 || einduur2 - beginuur2 == 2) && (beginuur1 == klantReservatieVoorDagX.Beginuur || beginuur1 + 1 == klantReservatieVoorDagX.Beginuur || einduur1 == klantReservatieVoorDagX.Einduur || einduur1 - 1 == klantReservatieVoorDagX.Einduur ||
                                    beginuur2 == klantReservatieVoorDagX.Beginuur || beginuur2 + 1 == klantReservatieVoorDagX.Beginuur || einduur2 == klantReservatieVoorDagX.Einduur || einduur2 - 1 == klantReservatieVoorDagX.Einduur ||
                                    beginuur2 == beginuur1 || beginuur2 == beginuur1 + 1 || einduur2 == einduur1 || einduur2 == einduur1 - 1)) throw new ReservatieManagerException("ReservatieManager - MaakReservatie");
                                //

                                else if (aantalGereserveerdeUrenPerDatum >= 1 && (einduur1 - beginuur1) == 2 && klantReservatieVoorDagX.Toestelnaam.Contains(beschikbaarToestel1.ToestelNaam) && (klantReservatieVoorDagX.Einduur == beginuur1 || klantReservatieVoorDagX.Beginuur == einduur1))
                                {
                                    beschikbareToestellen1.Remove(beschikbaarToestel1);
                                }
                                else if (aantalGereserveerdeUrenPerDatum == 2 && (einduur1 - beginuur1) >= 1 && klantReservatieVoorDagX.Toestelnaam.Contains(beschikbaarToestel1.ToestelNaam) && (klantReservatieVoorDagX.Einduur == beginuur1 || klantReservatieVoorDagX.Beginuur == einduur1))
                                {
                                    beschikbareToestellen1.Remove(beschikbaarToestel1);
                                }
                                else if (aantalGereserveerdeUrenPerDatum == 3 && (einduur1 - beginuur1) == 1 && klantReservatieVoorDagX.Toestelnaam.Contains(beschikbaarToestel1.ToestelNaam) && ((klantReservatieVoorDagX.Einduur - klantReservatieVoorDagX.Beginuur == 2 && klantReservatieVoorDagX.Einduur == beginuur1)))
                                {
                                    beschikbareToestellen1.Remove(beschikbaarToestel1);
                                }
                                else if (aantalGereserveerdeUrenPerDatum == 3 && (einduur1 - beginuur1) == 1 && klantReservatieVoorDagX.Toestelnaam.Contains(beschikbaarToestel1.ToestelNaam) && ((klantReservatieVoorDagX.Einduur - klantReservatieVoorDagX.Beginuur == 1 && klantReservatieVoorDagX.Einduur == beginuur1)))
                                {
                                    geselecteerdToestel1 = beschikbaarToestel1;
                                    break;
                                }

                            }
                            if (!beschikbareToestellen1.Contains(beschikbaarToestel1)) break;
                        }
                        geselecteerdToestel1 = beschikbareToestellen1[0];
                    }
                    else geselecteerdToestel1 = beschikbareToestellen1[0];
                }
                else
                {
                    geselecteerdToestel1 = null;
                }
                if (toestelTypeNaam2 != null && toestelTypeNaam2 == toestelTypeNaam1) beschikbareToestellen2 = beschikbareToestellen1;
                else if (toestelTypeNaam2 != null && toestelTypeNaam2 != toestelTypeNaam1) beschikbareToestellen2 = _toestelRepo.GeefVrijToestelVoorGeselecteerdTijdslot(datum, toestelTypeNaam2, beginuur2, einduur2);
                else beschikbareToestellen2 = null;
                if (beschikbareToestellen2 != null)
                {
                    if (aantalGereserveerdeUrenPerDatum == 2 && einduur1 - beginuur1 == 1 && einduur2 - beginuur2 == 1 && toestelTypeNaam2 == toestelTypeNaam1 && (einduur1 == beginuur2 || beginuur1 == einduur2)) geselecteerdToestel2 = geselecteerdToestel1;
                    else if (einduur1 - beginuur1 == 1 && einduur2 - beginuur2 == 2 && toestelTypeNaam2 == toestelTypeNaam1 && (einduur1 == beginuur2 || beginuur1 == einduur2)) geselecteerdToestel2 = beschikbareToestellen2[1];
                    else if (einduur1 - beginuur1 == 2 && toestelTypeNaam2 == toestelTypeNaam1 && (einduur1 == beginuur2 || beginuur1 == einduur2)) geselecteerdToestel2 = beschikbareToestellen2[1];
                    else geselecteerdToestel2 = beschikbareToestellen2[0];
                }
                else
                {
                    geselecteerdToestel2 = null;
                }
                //

                if (geselecteerdToestel1 == null) throw new ReservatieManagerException("ReservatieManager - MaakReservatie");
                else
                {
                    Reservatie reservatie = new Reservatie(klant, datum);
                    ReservatieInfo reservatieInfo2 = null;
                    ReservatieInfo reservatieInfo1 = _reservatieInfoRepo.ValideerReservatieInfo(datum, beginuur1, einduur1, geselecteerdToestel1);
                    if (toestelTypeNaam2 != null) reservatieInfo2 = _reservatieInfoRepo.ValideerReservatieInfo(datum, beginuur2, einduur2, geselecteerdToestel2);
                    if (reservatieInfo1 == null)
                    {
                        throw new ReservatieManagerException("ReservatieManager - MaakReservatie");
                    }
                    else
                    {
                        if (!_reservatieRepo.BestaatReservatie(reservatie))
                        {
                            Reservatie nieuweReservatie = _reservatieRepo.MaakReservatie(reservatie);
                            _reservatieInfoRepo.MaakReservatieInfo(nieuweReservatie, reservatieInfo1.Toestel, beginuur1, einduur1);
                            if (reservatieInfo2 != null) _reservatieInfoRepo.MaakReservatieInfo(nieuweReservatie, reservatieInfo2.Toestel, beginuur2, einduur2);
                        }
                        else if (_reservatieRepo.BestaatReservatie(reservatie))
                        {
                            Reservatie gemaakteReservatie = _reservatieRepo.GeefReservatie(reservatie);
                            _reservatieInfoRepo.MaakReservatieInfo(gemaakteReservatie, reservatieInfo1.Toestel, beginuur1, einduur1);
                            if (reservatieInfo2 != null) _reservatieInfoRepo.MaakReservatieInfo(gemaakteReservatie, reservatieInfo2.Toestel, beginuur2, einduur2);
                        }
                        else
                        {
                            throw new ReservatieManagerException("ReservatieManager - MaakReservatie");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new ReservatieManagerException("MaakReservatie", ex);
            }
        }

        public IReadOnlyList<DTOReservatieInfo> ZoekReservatie(int? reservatienummer, int? klantnummer, int? toestelnummer, DateTime? datum)
        {
            try
            {
                return _reservatieRepo.ZoekReservatie(reservatienummer, klantnummer, toestelnummer, datum);
            }
            catch (Exception ex)
            {
                throw new ReservatieManagerException("ReservatieManager - ZoekReservatie", ex);
            }
        }
    }
}
