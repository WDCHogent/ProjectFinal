using System;

namespace FitnessReservatieBL.Exceptions
{
    internal class KlantException : Exception
    {
        public KlantException(string message) : base(message)
        {
        }
        public KlantException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
