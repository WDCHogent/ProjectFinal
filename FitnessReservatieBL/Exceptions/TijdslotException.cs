using System;

namespace FitnessReservatieBL.Exceptions
{
    public class TijdslotException : Exception
    {
        public TijdslotException(string message) : base(message)
        {
        }
        public TijdslotException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
