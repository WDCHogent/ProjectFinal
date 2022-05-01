using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitnessReservatieBL.Exceptions
{
    public class ToestelTypeManagerException : Exception
    {
        public ToestelTypeManagerException(string message) : base(message)
        {
        }
        public ToestelTypeManagerException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
