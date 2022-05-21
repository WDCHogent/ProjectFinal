using FitnessReservatieBL.Domeinen;

namespace FitnessReservatieBL.Interfaces
{
    public interface IReservatieRepository
    {
        bool BestaatReservatie(Reservatie reservatie);
        Reservatie MaakReservatie(Reservatie reservatie);
        Reservatie GeefReservatie(Reservatie reservatie);
    }
}
