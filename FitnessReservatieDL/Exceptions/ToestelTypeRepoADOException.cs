using System;

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
