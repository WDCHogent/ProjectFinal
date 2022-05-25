using FitnessReservatieBL.Domeinen;
using FitnessReservatieBL.Domeinen.Enums;
using FitnessReservatieBL.DTO;
using System;
using System.Collections.Generic;

namespace FitnessReservatieBL.Interfaces
{
    public interface IToestelRepository
    {
        IReadOnlyList<Toestel> GeefVrijToestelVoorGeselecteerdTijdslot(DateTime datum, string toesteltype, int beginuur, int einduur);
        IReadOnlyList<DTOToestelInfo> ZoekToestellen(Status? status, int toestelnummer, string toestelnaam, string toesteltype);
        bool BestaatToestel(Toestel toestel);
        void SchrijfToestelInDB(Toestel toestel);
        string UpdateToestelStatus(DTOToestelInfo toestelInfo, string toestelStatus);
    }
}
