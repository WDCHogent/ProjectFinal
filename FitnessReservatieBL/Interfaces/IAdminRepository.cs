using FitnessReservatieBL.Domeinen;

namespace FitnessReservatieBL.Interfaces
{
    public interface IAdminRepository
    {
        Admin SelecteerAdmin(string adminnummer);
    }
}
