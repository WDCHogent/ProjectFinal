using System;

namespace FitnessReservatieBL.Exceptions
{
    public class ToestelTypeException : Exception
    {
        public ToestelTypeException(string message) : base(message)
        {
        }
        public ToestelTypeException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
