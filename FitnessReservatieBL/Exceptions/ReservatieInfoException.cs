using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitnessReservatieBL.Exceptions
{
    public class ReservatieInfoException : Exception
    {
        public ReservatieInfoException(string message) : base(message)
        {
        }
        public ReservatieInfoException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
