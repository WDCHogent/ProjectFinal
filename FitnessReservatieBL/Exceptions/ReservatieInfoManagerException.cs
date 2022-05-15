using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitnessReservatieBL.Exceptions
{
    public class ReservatieInfoManagerException : Exception
    {
        public ReservatieInfoManagerException(string message) : base(message)
        {
        }
        public ReservatieInfoManagerException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
