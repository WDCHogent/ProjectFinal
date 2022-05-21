using System;

namespace FitnessReservatieBL.Exceptions
{
    public class KlantManagerException : Exception
    {
        public KlantManagerException(string message) : base(message)
        {
        }
        public KlantManagerException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
