using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitnessReservatieDL.Exceptions
{
    public class ToestelTypeRepoADOException : Exception
    {
        public ToestelTypeRepoADOException(string message) : base(message)
        {
        }
        public ToestelTypeRepoADOException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
