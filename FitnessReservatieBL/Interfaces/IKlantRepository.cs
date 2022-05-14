using FitnessReservatieBL.Domeinen;
using FitnessReservatieBL.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitnessReservatieBL.Interfaces
{
    public interface IKlantRepository
    {
        Klant SelecteerKlant(int? klantnummer, string mailadres);
        IReadOnlyList<DTOKlantReservatieInfo> GeefKlantReservaties(int klantnummer);
    }
}
