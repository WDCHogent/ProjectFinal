using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitnessReservatieDL.Exceptions
{
    public class ReservatieInfoRepoADOException : Exception
    {
        public ReservatieInfoRepoADOException(string message) : base(message)
        {
        }

        public ReservatieInfoRepoADOException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
