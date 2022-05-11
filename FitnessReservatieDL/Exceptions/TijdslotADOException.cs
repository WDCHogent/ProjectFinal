using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitnessReservatieDL.Exceptions
{
    public class TijdslotADOException : Exception
    {
        public TijdslotADOException(string message) : base(message)
        {
        }
        public TijdslotADOException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
