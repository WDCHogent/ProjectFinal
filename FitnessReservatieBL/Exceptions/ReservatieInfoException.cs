using System;

namespace FitnessReservatieBL.Exceptions
{
    public class ReservatieInfoException : Exception
    {
        public ReservatieInfoException(string message) : base(message)
        {
        }
        public ReservatieInfoException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
