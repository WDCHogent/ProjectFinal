using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitnessReservatieBL.Exceptions
{
    public class ToestelTypeException : Exception
    {
        public ToestelTypeException(string message) : base(message)
        {
        }
        public ToestelTypeException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
