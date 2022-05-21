using System;

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
