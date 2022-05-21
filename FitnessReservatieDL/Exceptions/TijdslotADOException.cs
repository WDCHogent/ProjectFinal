using System;

namespace FitnessReservatieDL.Exceptions
{
    public class TijdslotADOException : Exception
    {
        public TijdslotADOException(string message) : base(message)
        {
        }
        public TijdslotADOException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
