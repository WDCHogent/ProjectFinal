using FitnessReservatieBL.Domeinen;
using FitnessReservatieBL.DTO;
using System.Collections.Generic;

namespace FitnessReservatieBL.Interfaces
{
    public interface IKlantRepository
    {
        Klant SelecteerKlant(int? klantnummer, string mailadres);
        IReadOnlyList<DTOKlantReservatieInfo> GeefKlantReservaties(int klantnummer);
        IReadOnlyList<Klant> ZoekKlanten(int klantnummer, string zoekterm);
    }
}
