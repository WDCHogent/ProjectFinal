using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitnessReservatieBL.Exceptions
{
    public class TijdslotManagerException : Exception
    {
        public TijdslotManagerException(string message) : base(message)
        {
        }
        public TijdslotManagerException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
