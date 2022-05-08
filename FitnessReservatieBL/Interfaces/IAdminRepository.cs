using FitnessReservatieBL.Domeinen;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitnessReservatieBL.Interfaces
{
    public interface IAdminRepository
    {
        Admin SelecteerAdmin(string adminnummer);
    }
}
