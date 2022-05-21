using System;

namespace FitnessReservatieBL.Exceptions
{
    public class ReservatieException : Exception
    {
        public ReservatieException(string message) : base(message)
        {
        }
        public ReservatieException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
