using FitnessReservatieBL.Domeinen;
using FitnessReservatieBL.DTO;
using System;
using System.Collections.Generic;

namespace FitnessReservatieBL.Interfaces
{
    public interface IKlantRepository
    {
        Klant SelecteerKlant(int? klantnummer, string mailadres);
        IReadOnlyList<DTOKlantReservatieInfo> GeefKlantReservaties(Klant klant);
        IReadOnlyList<DTOKlantReservatieInfo> GeefKlantReservatiesVoorDagX(Klant klant, DateTime datum);
        IReadOnlyList<Klant> ZoekKlanten(int klantnummer, string zoekterm);
    }
}
