using System;

namespace FitnessReservatieDL.Exceptions
{
    public class KlantRepoADOException : Exception
    {
        public KlantRepoADOException(string message) : base(message)
        {
        }
        public KlantRepoADOException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
