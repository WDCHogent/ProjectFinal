using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitnessReservatieBL.Exceptions
{
    public class AdminException : Exception
    {
        public AdminException(string message) : base(message)
        {
        }
        public AdminException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
