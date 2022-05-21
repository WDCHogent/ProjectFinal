using System;

namespace FitnessReservatieBL.Exceptions
{
    public class TijdslotManagerException : Exception
    {
        public TijdslotManagerException(string message) : base(message)
        {
        }
        public TijdslotManagerException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
